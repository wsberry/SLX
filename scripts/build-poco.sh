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

SCRIPTDIR=$(cd "$(dirname "$0")" && pwd)
. "$SCRIPTDIR/slx-core.sh"

is_header_only=true
SLX_PROJECT_NAME="poco" 
SLX_TARGET_NAME="poco"
SLX_IS_EXE=false
SLX_IS_STATIC_LIB=false
SLX_DEBUG_POSTFIX="d"
SLX_PROJECT_URI="https://github.com/pocoproject/poco.git"
SLX_PROJECTS_ROOT="$SLX_DEV_DIR/src/cpp"
SLX_PROJECT_SOURCES_DIR="$SLX_PROJECTS_ROOT/$SLX_PROJECT_NAME"
SLX_CLONE_TO_DIR="$SLX_SOURCE/cpp"
SLX_SOURCE_DIR="$SLX_SOURCE/cpp/$SLX_TARGET_NAME"


# Defines the directory to direct POCO build outputs to.
#
# Where 'git_branch_name' is the name of the git branch that is
# currently active for the developer.
#
SLX_TARGET_DIRECTORY="$SLX_ROOT/_build/$git_branch_name/poco"

# TODO: Add a clean option for the following.
#
#if [ -d $SLX_TARGET_DIRECTORY ]; then
#	echo_ib "Removing Target: $SLX_TARGET_DIRECTORY..."
#	rm -Rf "$SLX_TARGET_DIRECTORY"
#	sleep 2
#fi

mkdir -p "$SLX_TARGET_DIRECTORY"

function download_or_update_dependencies()
{
    error=false

    directory_restore_point=$(pwd)

    cd "$SLX_CLONE_TO_DIR"

    declare -A git_project_routings=(
        ["$SLX_TARGET_NAME"]="$SLX_PROJECT_URI"
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

    cd "$directory_restore_point"
}


function build_poco()
{
    TMP_DIR="$(pwd)"


    # If POCO does not exist the build the libraries.
    #
    download_or_update_dependencies

    if [ true = "$is_windows" ]; then

      if [ ! -e "$SLX_TARGET_DIRECTORY/bin/Debug/PocoFoundationd.dll" ]; then
        echo_iy "POCO not found...generating 'Poco.sln' and projects..."
        sleep 2

        # To run POCO 'buildwin.cmd' requires for this script to cd 
        # into the directory of its (the .cmd file) location.
        #
        cd "$SLX_SOURCE_DIR"

        echo
        echo_iy "Building POCO targets..."
        sleep 1

        cmake  -G"Visual Studio 15 2017 Win64" -B"$SLX_TARGET_DIRECTORY" -H"$SLX_SOURCE_DIR" \
        -DENABLE_ENCODINGS:BOOL=ON \
        -DENABLE_ENCODINGS_COMPILER:BOOL=OFF \
        -DENABLE_XML:BOOL=ON \
        -DENABLE_JSON:BOOL=ON \
        -DENABLE_MONGODB:BOOL=OFF \
        -DENABLE_REDIS:BOOL=ON \
        -DENABLE_PDF:BOOL=ON \
        -DENABLE_UTIL:BOOL=ON \
        -DENABLE_NET:BOOL=ON \
        -DENABLE_NETSSL:BOOL=ON \
        -DENABLE_NETSSL_WIN:BOOL=ON \
        -DENABLE_CRYPTO:BOOL=ON \
        -DENABLE_DATA:BOOL=ON \
        -DENABLE_DATA_SQLITE:BOOL=OFF \
        -DENABLE_DATA_MYSQL:BOOL=OFF \
        -DENABLE_DATA_ODBC:BOOL=OFF \
        -DENABLE_SEVENZIP:BOOL=OFF \
        -DENABLE_ZIP:BOOL=OFF \
        -DENABLE_APACHECONNECTOR:BOOL=OFF \
        -DENABLE_CPPPARSER:BOOL=OFF \
        -DENABLE_POCODOC:BOOL=OFF \
        -DENABLE_PAGECOMPILER:BOOL=ON \
        -DENABLE_PAGECOMPILER_FILE2PAGE:BOOL=OFF \
        -DFORCE_OPENSSL:BOOL=OFF \
        -DENABLE_TESTS:BOOL=OFF \
        -DPOCO_STATIC:BOOL=OFF \
        -DPOCO_UNBUNDLED:BOOL=OFF \
        -DENABLE_MSVC_MP:BOOL=OFF \
        -DENABLE_LONG_RUNNING_TESTS:BOOL=OFF \
        -DPOCO_MT:BOOL=OFF \
        "-DCMAKE_CONFIGURATION_TYPES:STRING=Debug;Release" >/dev/null

        cmake --build "$SLX_TARGET_DIRECTORY" --config Debug 
        cmake --build "$SLX_TARGET_DIRECTORY" --config Release

        if [ ! -e "$SLX_TARGET_DIRECTORY/bin/Debug/PocoFoundationd.dll" ]; then
          cd "$RestoreDirectoryPoint"
          exit_on_error_prompt "Unknown error...build did not complete without errors!"
        else
          prompt_iy "POCO Build Complete..."
          echo_ok
        fi
		
    else
        prompt_iy "POCO Found..."
        echo_ok
    fi    
    
  else
    if [ true = "$is_linux" ]; then
      if [ ! -d "$SLX_SOURCE_DIR/lib/Linux" ]; then
	     mikdir "$SLX_SOURCE_DIR/lib/Linux"
         #
         # The following is not safe on some systems (modify as required).
         #
         echo_ip "Warning requesting '777' on some systems my be a security violation or risk!\n"

         sudo chmod -R 777 "$SLX_SOURCE_DIR"
         cd "$SLX_SOURCE_DIR"
         ./configure --no-tests --no-samples
         make -s
         echo
         echo_iy "Done!"
         
       else
         echo_iy "POCO Found....OK"
       fi
    else
      echo
	  cd "$RestoreDirectoryPoint"
      exit_on_error_prompt "Error: script is not yet configured to support '$system_os_type'!"
    fi
  fi
  
  cd $TMP_DIR
}


# Start building things here...
#
initialize_build_time

build_poco

# windows_inject_project_into_solution "Some .sln" "Some .vcxproj"


# TODO: Move the following to a CMake post build...
#
debug_destination="$SLX_TARGET_DIRECTORY/bin"

lib_set=(
	"$SLX_TARGET_DIRECTORY/bin/Debug/*"
	"$SLX_TARGET_DIRECTORY/bin/Release/*"
	
    "$SLX_TARGET_DIRECTORY/lib/Debug/*"
	"$SLX_TARGET_DIRECTORY/lib/Release/*"
)

echo
echo_iy "Almost done...copying dependencies..."
echo
mkdir -p "$SLX_ROOT/_build/$git_branch_name/bin" 
for item in ${lib_set[*]}
do
    cp -u "$item" "$SLX_ROOT/_build/$git_branch_name/bin" 
done

report_total_build_time

cd "$RestoreDirectoryPoint"

