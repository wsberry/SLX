# Draft

# Description
#
# Implements macros and functions for CMake.
#
# Copyright 2018 Bill Berry
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
# Release Status: Alpha Untested. 02/2018

SET(TAB "  ")

MESSAGE(STATUS "${TAB}Importing: 'common SLX cmake library'")

# Turn on the ability to create folders to organize projects (.vcproj)
# It creates "CMakePredefinedTargets" folder by default and adds CMake
# defined projects like INSTALL.vcproj and ZERO_CHECK.vcproj
#
set_property(GLOBAL PROPERTY USE_FOLDERS ON)

# function debug_message
#   debug_message("Some message")
#
function(debug_message msg)
    if (CMAKE_VERBOSE)
        message(STATUS "${TAB}${msg}")
    endif()
endfunction()

# Creates a file grouping filter for IDE based projects.
# The '__result' must be added to the source list of the
# project, e.g. 'add_library(${PROJECT_NAME} SHARED ${__result})'
#
# Example: create_file_group("sources" "${SLX_PROJECT_SRC_DIR}/*.cpp")
#
function(create_file_group groupName fileListPathFilter)

    FILE(GLOB fileList "${fileListPathFilter}")

	# The following is required by some CMake versions after using
	# the GLOB feature.
	#
    SET(fileList ${fileList})
	
	#MESSAGE(STATUS "${groupName}: ${fileList}")
	
    source_group(${groupName} FILES ${fileList})
	
	set(__result ${fileList} PARENT_SCOPE)
	
endfunction(create_file_group)

