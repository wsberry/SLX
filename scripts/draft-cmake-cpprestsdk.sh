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

# Include any required header scripts:
#
SCRIPTDIR=$(cd "$(dirname "$0")" && pwd)
. "$SCRIPTDIR/core.sh"
. "$SCRIPTDIR/slx-core.sh"
  
# Finds the root of the slx repo.
#
SLX_DEV_DIR=$(pwd)

# This is where the user started make sure we
# return to this directory.
#
SLX_RESTORE_POINT_DIR=$(pwd)

# If the user started in the scripts directory 
# then change to the root of the slx repo.
#
if [[ $SLX_DEV_DIR = *"scripts"* ]]; then
  cd ..
  SLX_DEV_DIR=$(pwd)
fi

# For some projects it is desirable to know what branch you are
# building against. The slx-core script defines the git branch 
# for git based repos (i.e. '$git_branch_name').
#
# Example:
#    SLX_TARGET_PROJECT_DIR="$SLX_DEV_DIR/$CMAKE_BUILD_DIR/$git_branch_name/microservice"

# Define Directories and Paths here:
#
SLX_PROJECT_NAME="cpprestsdk"
SLX_PROJECTS_ROOT="$SLX_DEV_DIR/src/projects"
SLX_PROJECT_SOURCES_DIR="$SLX_PROJECTS_ROOT/$SLX_PROJECT_NAME"

# Defines the target directories for build outputs:
#
SLX_TARGET_DEBUG_NAME="cpprest141_2_10.lib"
SLX_TARGET_DEBUG_ALT_NAME="cpprest141_2_10d.lib"
SLX_TARGET_RELEASE_NAME="cpprestsdk.dll"
SLX_TARGETS_ROOT_DIR="$SLX_DEV_DIR/$CMAKE_BUILD_DIR"
SLX_TARGETS_COMMON_DIR="$SLX_TARGETS_ROOT_DIR/slx/bin"
SLX_TARGET_PROJECT_DIR="$SLX_DEV_DIR/$CMAKE_BUILD_DIR/$SLX_PROJECT_NAME"
SLX_TARGET_DEBUG_BIN_DIR="$SLX_TARGET_PROJECT_DIR/Debug"
SLX_TARGET_RELEASE_BIN_DIR="$SLX_TARGET_PROJECT_DIR/Release"
#
# This project does not produce a lib directory.
#
#SLX_TARGET_DEBUG_LIB_DIR="$SLX_TARGET_PROJECT_DIR/lib/Debug"
#SLX_TARGET_RELEASE_LIB_DIR="$SLX_TARGET_PROJECT_DIR/lib/Release"

# # Boost Lib Paths:
# #
# if [[ -v BOOST_LIBRARYDIR || ! -z "$BOOST_LIBRARYDIR" ]]; then
  # #
  # # TODO: Prompt the user or move this to a CMakeList generator.
  # #
  # Boost_INCLUDE_DIR="$BOOST_ROOT"
  # Boost_LIBRARY_DIR_DEBUG="$BOOST_LIBRARYDIR"
  # Boost_LIBRARY_DIR_RELEASE="$BOOST_LIBRARYDIR"
  # Boost_REGEX_LIBRARY_DEBUG="$BOOST_LIBRARYDIR/boost_regex-vc141-mt-gd-x64-1_67.lib"
  # Boost_REGEX_LIBRARY_RELEASE="$BOOST_LIBRARYDIR/boost_regex-vc141-mt-x64-1_67.lib"
  # Boost_SYSTEM_LIBRARY_DEBUG="$BOOST_LIBRARYDIR/boost_system-vc141-mt-gd-x64-1_67.lib"
  # Boost_SYSTEM_LIBRARY_RELEASE="$BOOST_LIBRARYDIR/boost_system-vc141-mt-x64-1_67.lib"
  # Boost_DATE_TIME_LIBRARY_DEBUG="$BOOST_LIBRARYDIR/boost_date_time-vc141-mt-gd-x64-1_67.lib"
  # Boost_DATE_TIME_LIBRARY_RELEASE="$BOOST_LIBRARYDIR/boost_date_time-vc141-mt-x64-1_67.lib"
# fi

# If Boost does not exist then try a fixup...
#
# directory_exists $BOOST_ROOT
# if [ 1 != "$bool_value" ]; then
  # prompt_iy "Warning: Using Boost Fixup Path...'"
  # echo -e -n "${IRed}$BOOST_LIBRARYDIR${NC}'"
  # echo -e -n "${IYellow} is not defined!${NC}"
  
  # export Boost_DIR="C:/opt/boost/v1.67"
  # export BOOST_ROOT="C:/opt/boost/v1.67"
  # export BOOST_LIBRARYDIR="C:/opt/boost/v1.67/stage/x64/lib"
  
  # Boost_INCLUDE_DIR="$BOOST_ROOT"
  # Boost_LIBRARY_DIR_DEBUG="$BOOST_LIBRARYDIR"
  # Boost_LIBRARY_DIR_RELEASE="$BOOST_LIBRARYDIR"
  # Boost_REGEX_LIBRARY_DEBUG="$BOOST_LIBRARYDIR/boost_regex-vc141-mt-gd-x64-1_67.lib"
  # Boost_REGEX_LIBRARY_RELEASE="$BOOST_LIBRARYDIR/boost_regex-vc141-mt-x64-1_67.lib"
  # Boost_SYSTEM_LIBRARY_DEBUG="$BOOST_LIBRARYDIR/boost_system-vc141-mt-gd-x64-1_67.lib"
  # Boost_SYSTEM_LIBRARY_RELEASE="$BOOST_LIBRARYDIR/boost_system-vc141-mt-x64-1_67.lib"
  # Boost_DATE_TIME_LIBRARY_DEBUG="$BOOST_LIBRARYDIR/boost_date_time-vc141-mt-gd-x64-1_67.lib"
  # Boost_DATE_TIME_LIBRARY_RELEASE="$BOOST_LIBRARYDIR/boost_date_time-vc141-mt-x64-1_67.lib"
# fi

# # Ensure the paths and directories exist.
# #
# echo_iy "Verifying paths..."
# assert_directory_exists $BOOST_ROOT false # Don't exit on error
# assert_directory_exists $Boost_LIBRARY_DIR_DEBUG false
# assert_directory_exists $Boost_LIBRARY_DIR_RELEASE false
# assert_file_exists $Boost_REGEX_LIBRARY_DEBUG false
# assert_file_exists $Boost_REGEX_LIBRARY_RELEASE false
# assert_file_exists $Boost_SYSTEM_LIBRARY_DEBUG false
# assert_file_exists $Boost_SYSTEM_LIBRARY_RELEASE false
# assert_file_exists $Boost_DATE_TIME_LIBRARY_DEBUG false
# assert_file_exists $Boost_DATE_TIME_LIBRARY_RELEASE false

# OpenSLL from https://mirror.firedaemon.com/
#
# LIB_EAY_RELEASE=$SLX_PROJECTS_ROOT/openssl-1.1/x64/lib/libcrypto.lib
# SSL_EAY_RELEASE=$SLX_PROJECTS_ROOT/openssl-1.1/x64/lib/libssl.lib
# OPENSSL_INCLUDE_DIR="$SLX_PROJECTS_ROOT/openssl-1.1/include"
export OPENSSL_ROOT_DIR="$SLX_PROJECTS_ROOT/openssl-1.1"
export OPENSSL_LIBRARIES="$SLX_PROJECTS_ROOT/openssl-1.1/x64/lib"

# The following conversion is required for cmake
# on windows.
#
# CMake cannot interpret the '/c/' drive specifier from the
# bash shell correctly.
SLX_OPENSSL_ROOT_DIR=$(posix_drive_to_windows $SLX_OPENSSL_ROOT_DIR)
SLX_OPENSSL_LIBRARIES=$(posix_drive_to_windows $SLX_OPENSSL_LIBRARIES)

# WEBSOCKETPP_DIR

mkdir -p "$SLX_TARGET_PROJECT_DIR"

# private:
function acquire_or_update_cpprestsdk()
{
    cd "$SLX_PROJECTS_ROOT"

    declare -A git_project_routings=(
        ["cpprestsdk"]="https://github.com/Microsoft/cpprestsdk.git"
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
function cmake_and_build_cpprestsdk()
{
    acquire_or_update_cpprestsdk
 
    if [ true = "$is_windows" ]; then
      # Debug and Release versions are built by this script...therefore
      # only the presence of the dbug output is inspected.
      #
      echo "Checking for '$SLX_TARGET_DEBUG_BIN_DIR'..."
      if [[ ! -e "$SLX_TARGET_DEBUG_BIN_DIR/$SLX_TARGET_DEBUG_NAME" || ! -e "$SLX_TARGET_DEBUG_BIN_DIR/$SLX_TARGET_DEBUG_ALT_NAME" ]]; then
        echo_iy "$SLX_PROJECT_NAME targets not found...generating projects..."
        sleep 2

        # To run $SLX_PROJECT_NAME 'buildwin.cmd' requires for this script to cd 
        # into the directory of its (the .cmd file) location.
        #
        cd "$SLX_PROJECT_SOURCES_DIR"

        echo
        echo_iy "Building $SLX_PROJECT_NAME targets..."
        sleep 1

        cmake  -G"$CMAKE_GENERATOR" -B"$SLX_TARGET_PROJECT_DIR" -H"$SLX_PROJECT_SOURCES_DIR" \
        -DBUILD_SAMPLES:BOOL=Off \
        -DBUILD_SHARED_LIBS:BOOL=Off \
        -DBUILD_TESTS:BOOL=Off \
        #
        # OpenSSL
        # #
        # -DOPENSSL_ROOT_DIR="\"$SLX_OPENSSL_ROOT_DIR\"" \
        # -DOPENSSL_LIBRARIES="\"$SLX_OPENSSL_LIBRARIES\""  
        # #
        # # ZLib
        # #
        # -DZLIB_INCLUDE_DIR:STRING="$SLX_PROJECTS_ROOT/zlib" 
        # -DZLIB_LIBRARY_DEBUG:STRING="$SLX_TARGETS_COMMON_DIR/zlibstaticd.lib" 
        # -DZLIB_LIBRARY_RELEASE:STRING="$SLX_TARGETS_COMMON_DIR/zlibstatic.lib" 
        #
        # Boost
        #
        #-DBoost_INCLUDE_DIR:STRING="$Boost_INCLUDE_DIR" 
        # -DBoost_LIBRARY_DIR_DEBUG:STRING="$Boost_LIBRARY_DIR_DEBUG" \
        # -DBoost_LIBRARY_DIR_RELEASE:STRING="$Boost_LIBRARY_DIR_RELEASE" \
        # -DBoost_REGEX_LIBRARY_DEBUG:STRING="$Boost_REGEX_LIBRARY_DEBUG" \
        # -DBoost_REGEX_LIBRARY_RELEASE:STRING="$Boost_REGEX_LIBRARY_RELEASE" \
        # -DBoost_SYSTEM_LIBRARY_DEBUG:STRING="$Boost_SYSTEM_LIBRARY_DEBUG" \
        # -DBoost_SYSTEM_LIBRARY_RELEASE:STRING="$Boost_SYSTEM_LIBRARY_RELEASE" \
        # -DBoost_DATE_TIME_LIBRARY_DEBUG:STRING="$Boost_DATE_TIME_LIBRARY_DEBUG" \
        # -DBoost_DATE_TIME_LIBRARY_RELEASE:STRING="$Boost_DATE_TIME_LIBRARY_RELEASE" \
         # $CMAKE_CONFIGURATION_TYPES >/dev/null
     
        cmake --build "$SLX_TARGET_PROJECT_DIR" --config Debug 
        

        cmake --build "$SLX_TARGET_PROJECT_DIR" --config Release
        

        if [[ ! -e "$SLX_TARGET_DEBUG_BIN_DIR/$SLX_TARGET_DEBUG_NAME" || ! -e "$SLX_TARGET_DEBUG_BIN_DIR/$SLX_TARGET_DEBUG_ALT_NAME" ]]; then
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
      exit_on_error_prompt "TODO: Not Implemented on Linux" 
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
function create_cpprestsdk()
{
  # Start building things here...
  #
  initialize_build_time

  cmake_and_build_cpprestsdk

  lib_set=(
    "$SLX_TARGET_DEBUG_BIN_DIR/*"
    #"$SLX_TARGET_DEBUG_LIB_DIR/*"

    "$SLX_TARGET_RELEASE_BIN_DIR/*"
    #"$SLX_TARGET_RELEASE_LIB_DIR/*"
  )

  echo
  echo_iy "Almost done...copying dependencies..."
  echo
  mkdir -p "$SLX_TARGETS_COMMON_DIR" 
  for item in ${lib_set[*]}
  do
      cp -u "$item" "$SLX_TARGETS_COMMON_DIR" 
  done
  
  # ZLIB generates a header, zconf.h, copy this to the source
  # directory for cpprestsdk:
  # 
  cp -u "$SLX_TARGET_PROJECT_DIR/zconf.h" "$SLX_PROJECT_SOURCES_DIR" 
  
  report_total_build_time

  cd "$RestoreDirectoryPoint"
}

# For now just call from within this script.
# TODO: Move to build options scripting
create_cpprestsdk
