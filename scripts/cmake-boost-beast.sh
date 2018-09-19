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
#    SLX_TARGET_PROJECT_DIR="$SLX_DEV_DIR/$CMAKE_BUILD_DIR/$git_branch_name/microservice"

# Project Attributes Defined Here.
#
is_header_only=true
SLX_PROJECT_NAME="beast" # note this project is header only.
SLX_TARGET_NAME="header only"
SLX_IS_EXE=false
SLX_IS_STATIC_LIB=false
SLX_DEBUG_POSTFIX="d"
SLX_PROJECT_URI="https://github.com/boostorg/beast.git"
SLX_PROJECTS_ROOT="$SLX_DEV_DIR/src/cpp"
SLX_PROJECT_SOURCES_DIR="$SLX_PROJECTS_ROOT/$SLX_PROJECT_NAME"

# boost builder 'b2' is required to generate documentation.
#
SLX_BOOST_BUILDER_PATH="/opt/boost/v1.67/b2"

if [ true = "$is_windows" ]; then
  SLX_BOOST_BUILDER_PATH="C:/opt/boost/v1.67/b2.exe"
else
  #
  # Ensure the boost build path exists (optional on windows)
  #
  assert_file_exists $SLX_BOOST_BUILDER_PATH false # Don't exit on error
fi

# Determine Target Extension.
#
SLX_TARGET_EXTENSION=""
if [ true = "$is_windows" ]; then
   SLX_TARGET_EXTENSION=".exe"
   if [[ false = "$SLX_IS_EXE" &&  true = "$SLX_IS_STATIC_LIB" ]]; then
      SLX_TARGET_EXTENSION=".lib"
   elif [ false = "$SLX_IS_EXE" ]; then
      SLX_TARGET_EXTENSION=".dll"
   fi
else
  #
  # Assume Linux and OSX
  #
   if [[ false = "$SLX_IS_EXE" &&  true = "$SLX_IS_STATIC_LIB" ]]; then
      SLX_TARGET_EXTENSION=".a"
   elif [ false = "$SLX_IS_EXE" ]; then
      SLX_TARGET_EXTENSION=".so"
   fi
fi

# Darwin Shared Library...
#
if [[ true == "$is_darwin" && false = "$SLX_IS_STATIC_LIB" ]]; then
   SLX_TARGET_EXTENSION=".dylib"
fi

SLX_TARGET_DEBUG_NAME="${SLX_TARGET_NAME}${SLX_DEBUG_POSTFIX}${SLX_TARGET_EXTENSION}"
SLX_TARGET_RELEASE_NAME="${SLX_TARGET_NAME}${SLX_TARGET_EXTENSION}"
SLX_TARGET_PROJECT_DIR="$SLX_DEV_DIR/$CMAKE_BUILD_DIR/$SLX_PROJECT_NAME/bin"

SLX_TARGET_DEBUG_BIN_DIR="$SLX_TARGET_PROJECT_DIR"
SLX_TARGET_DEBUG_LIB_DIR="$SLX_TARGET_PROJECT_DIR"
SLX_TARGET_RELEASE_BIN_DIR="$SLX_TARGET_PROJECT_DIR"
SLX_TARGET_RELEASE_LIB_DIR="$SLX_TARGET_PROJECT_DIR"

# A shared binaries directory...not used if header only.
#
SLX_TARGETS_COMMON_DIR="$SLX_DEV_DIR/$CMAKE_BUILD_DIR/bin"

mkdir -p "$SLX_TARGET_DEBUG_BIN_DIR"
mkdir -p "$SLX_TARGET_DEBUG_LIB_DIR"

mkdir -p "$SLX_TARGET_RELEASE_BIN_DIR"
mkdir -p "$SLX_TARGET_RELEASE_LIB_DIR"



# private:don't call this directly, instead call 'build_project'
#
function acquire_or_update_project_source()
{
    cd "$SLX_PROJECTS_ROOT"

    declare -A git_project_routings=(
        [$SLX_PROJECT_NAME]=$SLX_PROJECT_URI
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
function cmake_and_build_project()
{
    acquire_or_update_project_source
 
    if [ true = "$is_windows" ]; then
        #
        # To run $SLX_PROJECT_NAME 'buildwin.cmd' requires for this script to cd 
        # into the directory of its (the .cmd file) location.
        #
        cd "$SLX_PROJECT_SOURCES_DIR"
        echo
        echo_iy "Generating $SLX_PROJECT_NAME targets from CMake..."
        sleep 1
        cmake  -G"$CMAKE_GENERATOR" -B"$SLX_TARGET_PROJECT_DIR" -H"$SLX_PROJECT_SOURCES_DIR" \
        $CMAKE_CONFIGURATION_TYPES >/dev/null
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
    exit_on_error_prompt "TODO: Not Implemented!" 
  fi
  
  cd $SLX_RESTORE_POINT_DIR
}

# public:
function build_project()
{
  # Start building things here...
  #
  initialize_build_time

  cmake_and_build_project

  if [ false = "$is_header_only" ]; then
    lib_set=(
      "$SLX_TARGET_DEBUG_BIN_DIR/*"
      "$SLX_TARGET_DEBUG_LIB_DIR/*"
      "$SLX_TARGET_RELEASE_BIN_DIR/*"
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
   fi
  
  report_total_build_time

  cd "$RestoreDirectoryPoint"
}

# For now just call from within this script.
# TODO: Move to build options scripting
build_project
