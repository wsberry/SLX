#!/bin/bash

# Prologue
#
# SLX - Simple Library Extensions
#
# Copyright 2000-2018 Bill Berry
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

# http://www.shellcheck.net/

# This script defines the default configuration taxonomy
# used by SLX.

# Imports
#
SCRIPTDIR=$(cd "$(dirname "$0")" && pwd)
. "$SCRIPTDIR/core.sh"
. "$SCRIPTDIR/cmake-options.sh"

# Find and configure SLX root directories here:
#
SLX_ROOT="$(pwd)"
WhereAmI=$(basename "$SLX_ROOT")

# After the script runs use the following to restore
# the user's current directory point when the script
# was called.
#
RestoreDirectoryPoint="$SLX_ROOT"

# Acquire the absolute directory for the SLX source
# 
compare "$WhereAmI" "scripts" 
if [ true = "$are_equal" ]; then
   cd ..
   SLX_ROOT="$(pwd)"
fi

# Defined where the source files are located:
#
SLX_SOURCE="$SLX_ROOT/src"

# Get SLX git branch. This is used to separate branch build results.
#
git_branch_name="$(git symbolic-ref HEAD 2>/dev/null)" || git_branch_name="unnamed" # detached HEAD
git_branch_name=${git_branch_name##refs/heads/}

# Define the output destination folders. These directories are shared by all
# the various product builds for SLX.
#
# Note:
# 	The following need to be moved to post build events 
# 	for Visual Studio based projects:
#
OUTPUT_BIN="$SLX_ROOT/$CMAKE_BUILD_DIR/$git_branch_name/bin"
# OUTPUT_BIN_DEBUG="$OUTPUT_BIN/Debug"
# OUTPUT_BIN_RELEASE="$OUTPUT_BIN/Release"
# mkdir -p $OUTPUT_BIN_DEBUG
# mkdir -p $OUTPUT_BIN_RELEASE

function move_build()
{
	sourceFilesFolder=$1
	destinationFolder=$OUTPUT_BIN/$2

	echo_iy " Copying build results to '$destinationFolder'..."
	
	if [ -d "$sourceFilesFolder" ]; then
	
		lib_set=("$sourceFilesFolder/*") 
		
		for item in ${lib_set[*]}
		do
		    #
			# Copy the file only if it requires updating.
			#
			cp -u "$item" "$destinationFolder" 
		done
	fi
}


# Configure for Windows or POSIX compliant OS
#
configure_for_os
