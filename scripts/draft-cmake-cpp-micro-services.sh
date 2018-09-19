#!/bin/bash

# Prologue
#
# SLX - Simple Library Extensions
#
# Copyright 2000-2018 Bill Berry
#
# Work: wberry.cpp@gmail.com
#
# Licensed under the Apache License, Version 2.0 (the "License");
# you may not use this file except in compliance with the License.
# You may obtain a copy of the License at
#
# http://www.apache.org/licenses/LICENSE-2.0
#
# Unless required by applicable law or agreed to in writing, software
# distributed under the License is distributed on an "AS IS" BASIS,
# WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
# See the License for the specific language governing permissions and
# limitations under the License.
#
# endPrologue

clear

user_options=$1

SLX_RESTORE_POINT_DIR=$(pwd)

SCRIPTDIR=$(cd "$(dirname "$0")" && pwd)
. "$SCRIPTDIR/slx-core.sh"

SLX_DEV_DIR=$(pwd)
if [[ $SLX_DEV_DIR = *"scripts"* ]]; then
  cd ..
  SLX_DEV_DIR=$(pwd)
fi

# For some projects it is desirable to know what branch you are
# building against. The slx-core script defines the branch for
# for you (i.e. '$git_branch_name').
#
# Example:
#    SLX_TARGET_PROJECT_DIR="$SLX_DEV_DIR/_build/$git_branch_name/microservice"

# Define Directories and Paths here:
#

SLX_PROJECT_NAME="CppMicroServices"
SLX_PROJECTS_ROOT="$SLX_DEV_DIR/src/projects"
SLX_PROJECT_SOURCES_DIR="$SLX_PROJECTS_ROOT/$SLX_PROJECT_NAME"

SLX_TARGET_DEBUG_NAME="CppMicroServices4d.dll"
SLX_TARGET_RELEASE_NAME="CppMicroServices4.dll"
SLX_TARGETS_ROOT_DIR="$SLX_DEV_DIR/_build"
SLX_TARGETS_COMMON_DIR="$SLX_TARGETS_ROOT_DIR/slx/bin"
SLX_TARGET_PROJECT_DIR="$SLX_DEV_DIR/_build/TT/$SLX_PROJECT_NAME"

SLX_TARGET_DEBUG_BIN_DIR="$SLX_TARGET_PROJECT_DIR/bin/Debug"
SLX_TARGET_DEBUG_LIB_DIR="$SLX_TARGET_PROJECT_DIR/lib/Debug"

SLX_TARGET_RELEASE_BIN_DIR="$SLX_TARGET_PROJECT_DIR/bin/Release"
SLX_TARGET_RELEASE_LIB_DIR="$SLX_TARGET_PROJECT_DIR/lib/Release"

# Deprecated...see for loop below.
# if [[ "$user_options" == *"clean"* ]]; then
    # if [ -d $SLX_TARGET_PROJECT_DIR ]; then
      # echo_ib "Removing Target: $SLX_TARGET_PROJECT_DIR..."
      # ${SUDO} rm -Rf "$SLX_TARGET_PROJECT_DIR"
  # fi
# fi

mkdir -p "$SLX_TARGET_PROJECT_DIR"

# private:
function acquire_or_update_cpp_microservices()
{
    cd "$SLX_PROJECTS_ROOT"

    declare -A git_project_routings=(
        ["CppMicroServices"]="https://github.com/CppMicroServices/CppMicroServices.git"
    )

    for k in "${!git_project_routings[@]}"
      do
        if [[ "$user_options" == *"clean"* ]]; then
          echo_iy "${TAB}Removing $k..."
          ${SUDO} rm -f -R "$k"
        fi

        # Debug Hints:
        #
        #echo "key  : $k"
        #echo "value: ${git_project_routings[$k]}"

        if [[ ! -d "$k" ]]; then
          prompt_iy "${TAB}Cloning '$k'..."
          git clone "${git_project_routings[$k]}"
          #
          # 755 - You may change and run it; others can run it but not change it...
          #
          ${SUDO} chmod -R 755 "$k"
        else
          echo_iy "${TAB}Pulling '$k'..."
          cd "$k"
          git pull
          cd ..
        fi
    done

    cd "$SLX_RESTORE_POINT_DIR"
}

# private:
function cmake_and_build_cpp_microservices()
{
    acquire_or_update_cpp_microservices
    
    if [ true = "$is_windows" ]; then

      # Debug and Release versions are built by this script...therefore
      # only the presence of the dbug output is inspected.
      #
      echo "Checking for '$SLX_TARGET_DEBUG_BIN_DIR'..."
      if [ ! -e "$SLX_TARGET_DEBUG_BIN_DIR/$SLX_TARGET_DEBUG_NAME" ]; then
        echo_iy "$SLX_PROJECT_NAME targets not found...generating projects..."
        sleep 2

        # To run $SLX_PROJECT_NAME 'buildwin.cmd' requires for this script to cd 
        # into the directory of its (the .cmd file) location.
        #
        cd "$SLX_PROJECT_SOURCES_DIR"

        echo
        echo_iy "Building $SLX_PROJECT_NAME targets..."
        sleep 1

        cmake  -G"Visual Studio 15 2017 Win64" -B"$SLX_TARGET_PROJECT_DIR" -H"$SLX_PROJECT_SOURCES_DIR" \
        -DUS_BUILD_DOC_HTML:BOOL=OFF \
        -DUS_BUILD_DOC_MAN:BOOL=OFF \
        -DUS_BUILD_EXAMPLES=ON \
        -DUS_BUILD_SHARED_LIBS=ON \
        -DUS_BUILD_TESTING=OFF \
        -DUS_ENABLE_THREADING_SUPPORT=ON \
        "-DCMAKE_CONFIGURATION_TYPES:STRING=Debug;Release" >/dev/null

        cmake --build "$SLX_TARGET_PROJECT_DIR" --config Debug 
        cmake --build "$SLX_TARGET_PROJECT_DIR" --config Release

        if [ ! -e "$SLX_TARGET_DEBUG_BIN_DIR/$SLX_TARGET_DEBUG_NAME" ]; then
          cd "$RestoreDirectoryPoint"
          echo "$SLX_TARGET_DEBUG_BIN_DIR/$SLX_TARGET_DEBUG_NAME"
          exit_on_error_prompt "Unknown error...build did not complete without errors"
        else
          prompt_iy "$SLX_PROJECT_NAME Build Complete..."
          echo_ok
        fi
		
    else
        prompt_iy "$SLX_PROJECT_NAME targets Found..."
        echo_ok
    fi    
    
  else
    if [ true = "$is_linux" ]; then
      if [ ! -d "$CLONE_TO_DIR/lib/Linux" ]; then
	     mikdir "$CLONE_TO_DIR/lib/Linux"
         sudo chmod -R 777 "$CLONE_TO_DIR"
         cd "$CLONE_TO_DIR"
         ./configure --no-tests --no-samples
         make -s
         echo
         echo_iy "Done!"
         
       else
         echo_iy "$SLX_PROJECT_NAME Found....OK"
       fi
    else
      echo
	  cd "$RestoreDirectoryPoint"
      exit_on_error_prompt "Error: script is not yet configured to support '$system_os_type'!"
    fi
  fi
  
  cd $SLX_RESTORE_POINT_DIR
}

# public:
function create_cpp_microservices()
{
  # Start building things here...
  #
  initialize_build_time

  cmake_and_build_cpp_microservices

  lib_set=(
    "$SLX_TARGET_DEBUG_BIN_DIR/*"
    "$SLX_TARGET_DEBUG_BIN_DIR/*"
    "$SLX_TARGET_DEBUG_LIB_DIR/*"
    "$SLX_TARGET_DEBUG_LIB_DIR/*"
    
    "$SLX_TARGET_RELEASE_BIN_DIR/*"
    "$SLX_TARGET_RELEASE_BIN_DIR/*"
    "$SLX_TARGET_RELEASE_LIB_DIR/*"
    "$SLX_TARGET_RELEASE_LIB_DIR/*"
  )

  echo
  echo_iy "Almost done...copying dependencies..."
  echo
  mkdir -p "$SLX_TARGETS_COMMON_DIR" 
  for item in ${lib_set[*]}
  do
      cp -u "$item" "$SLX_TARGETS_COMMON_DIR" 
  done

  report_total_build_time

  cd "$RestoreDirectoryPoint"
}

# For now just call from within this script.
# TODO: Move to build options scripting
create_cpp_microservices
