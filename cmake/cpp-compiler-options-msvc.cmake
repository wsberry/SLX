# Draft

# This work is adapted from:
# https://github.com/rbax/SquareOne

# Description
#
# Implements macros and functions for configuring Microsoft Visual 
# Studio projects on Windows platforms.
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

MESSAGE(STATUS "${TAB}Importing: 'cpp-compiler-options-msvc'")

# Only process this module if you are on Windows.
# At this time the assumed compiler for Windows Visual Studio 15.x
#
if (NOT MSVC)
    message(WARNING "${TAB}MSVC compiler options called but not used.")
	return()
endif()

# -----------------------------------------------------------
# CPP CMake Options for MSVC
# ----------------------------------------------------------- [Build Flag Notes]
SET(CXX_STANDARD_VERSION_DEFAULT_IS_17 17)

if (NOT DEFINED CXX_RTTI)
	option(CXX_RTTI "Enable or disable C++ RTTI." ON)
endif()

if (NOT DEFINED MAKE_LIBS_SHARED)
	option(MAKE_LIBS_SHARED "Create shared or dynamically linked libraries." TRUE)
endif()

if (NOT DEFINED MAKE_WIN32_LEAN_AND_MEAN)
	option(MAKE_WIN32_LEAN_AND_MEAN "Minimize what <windows.h> imports and defines." TRUE)
endif()

if (NOT DEFINED ADD_BIG_OBJ)
	option(ADD_BIG_OBJ "Increases the number of sections that an object file can contain (Microsoft Visual Studio Only)." TRUE)
endif()

# -----------------------------------------------------------
# 'CONFIGURE_BUILD' configures Microsoft Visual Studio build
# options and project level macros.
# ----------------------------------------------------------- [Build Flag Notes]
macro(CONFIGURE_MSVC_CPP_BUILD_SETTINGS)

	MESSAGE(STATUS "\n${TAB}Configuring MSVC CPP Settings...")
	MESSAGE(STATUS "${TAB}- Using ${CMAKE_GENERATOR} and Compiler ID: ${CMAKE_CXX_COMPILER_ID}.")
    ADD_DEFINITIONS_FUNC()
    ADD_SETTINGS_FUNC()
    DEPLOY_FUNC()

endmacro(CONFIGURE_MSVC_CPP_BUILD_SETTINGS)

# -----------------------------------------------------------
#    Directory Scope Command options for Build Flags (MSVC)
# ----------------------------------------------------------- [Build Flag Notes]
macro(ADD_DEFINITIONS_FUNC)

    add_definitions (
	
      # Remove Debug CRT Deprecate Warnings
      #
      -D_CRT_SECURE_NO_DEPRECATE    

      # enables template overloads of standard CRT functions that auto call more secure variants
      #
      -D_CRT_SECURE_CPP_OVERLOAD_STANDARD_NAMES             
      -D_CRT_SECURE_NO_WARNING

      # thread safe (i.e. re-entrant) versions
      #
      -D_REENTRANT			                      

      # Note:
      #  Programs that use intrinsic functions are faster because they do not 
      #  have the overhead of function calls, but may be larger because of the
      #  additional code created.
      #
      #  see: https://docs.microsoft.com/en-us/cpp/build/reference/oi-generate-intrinsic-functions
      #       There are specific x86 (compared to x64) behaviors when using this feature.
      # 
      -Oi	

      # Note: 
      #
      # -Ob[2]
      #
      # The default value, 2, allows expansion of functions marked as inline,
      # __inline, or __forceinline, and any other function that the compiler
      # chooses.
      #
      # see: https://docs.microsoft.com/en-us/cpp/build/reference/ob-inline-function-expansion
      -Ob2

      # -MP[n] If you omit the processMax argument, the compiler retrieves the number 
      # of effective processors on your computer from the operating system, and 
      # creates a process for each processor. This generally reduces the build time, but
      # may increase the build time with some Visual Studio solution configurations.
      #
      -MP	

      # Note:
      #
      # Math Constants are not defined in Standard C/C++. 
      # To use them, you must first define _USE_MATH_DEFINES and 
      # then include cmath or math.h.
      #
      # see: https://docs.microsoft.com/en-us/cpp/c-runtime-library/math-constants
      #
      -D_USE_MATH_DEFINES		

      # Windows' Platform Misc Macros:
      #
      -DWIN32   			# required even for x64
      -D_WINDOWS			# required even for x64
      -DGUID_WINDOWS      # required if windows native GUID generator
    )
	
	if (MAKE_WIN32_LEAN_AND_MEAN)
    add_definitions(
      -DWIN32_LEAN_AND_MEAN
      -DVC_EXTRALEAN
    )
		MESSAGE("\n${TAB}WIN32_LEAN_AND_MEAN is defined.\n${TAB}Some features defined in <Windows.h> will be disabled.\n${TAB}If you have compilation errors then consider disabling this feature.\n")
	endif(MAKE_WIN32_LEAN_AND_MEAN)

endmacro(ADD_DEFINITIONS_FUNC)

# -----------------------------------------------------------
# Compiler Settings
# ----------------------------------------------------------- [Settings]
macro(ADD_SETTINGS_FUNC)

  message(STATUS "${TAB}- Setting C++ Standard to ${CXX_STANDARD_VERSION_DEFAULT_IS_17}...")

  set(CMAKE_CXX_STANDARD ${CXX_STANDARD_VERSION_DEFAULT_IS_17})
  set(CMAKE_CXX_STANDARD_COMPUTED_DEFAULT ${CXX_STANDARD_VERSION_DEFAULT_IS_17})
	
	set(CMAKE_CXX_STANDARD_REQUIRED ON)
	
  # Suppress Microsoft specific language extensions
  #
  set(CMAKE_CXX_EXTENSIONS OFF)     

  # Simplify and suppress build types to Debug and Release
  #
  set(CMAKE_CONFIGURATION_TYPES "Debug;Release" CACHE STRING "" FORCE)

  if (WIN32)
    add_definitions("/std:c++${CXX_STANDARD_VERSION_DEFAULT_IS_17}")
    MESSAGE("C++ Version: /std:c++${CXX_STANDARD_VERSION_DEFAULT_IS_17}")
  endif()

	# see: https://docs.microsoft.com/en-us/cpp/build/reference/md-mt-ld-use-run-time-library
	# 
	if (MAKE_LIBS_SHARED)
	  message(STATUS "${TAB}- Shared libraries (/MD)...")
		set(CMAKE_CXX_FLAGS "${CMAKE_CXX_FLAGS} /MD")
		set(CMAKE_CXX_FLAGS_DEBUG "${CMAKE_CXX_FLAGS_DEBUG} /MDd")
		set(CMAKE_CXX_FLAGS_RELEASE "${CMAKE_CXX_FLAGS_RELEASE} /MD")
	else()
	  message(STATUS "${TAB}- Statically linked libraries (/MT)...")
		set(CMAKE_CXX_FLAGS "${CMAKE_CXX_FLAGS} /MT")
		set(CMAKE_CXX_FLAGS_DEBUG "${CMAKE_CXX_FLAGS_DEBUG} /MTd")
		set(CMAKE_CXX_FLAGS_RELEASE "${CMAKE_CXX_FLAGS_RELEASE} /MT")
	endif(MAKE_LIBS_SHARED)
	
	if (ADD_BIG_OBJ)
	  message(STATUS "${TAB}- Defining (/bigobj). Increases the number of sections that an object file may contain...")
		add_compile_options(/bigobj)
	endif()
	
	#set(CMAKE_CXX_FLAGS_MINSIZEREL "${CMAKE_CXX_FLAGS_MINSIZEREL} /MD")
	#set(CMAKE_CXX_FLAGS_RELWITHDEBINFO "${CMAKE_CXX_FLAGS_RELWITHDEBINFO} /MDd")
	
	# Suppress warnings from legacy code builds. This is frequently NOT
	# desirable. It is included here because I need it for many of my
	# projects. 
	#
	# I would recommend commenting out the following.
	#
  #  4566: character represented by universal-character-name
  set(warnings "/W3 /wd4018 /wd4099 /wd4101 /wd4172 /wd4244 /wd4251 /wd4267 /wd4305 /wd4474 /wd4477 /wd4512 /wd4566 /wd4800 /wd4996 /WX /EHsc")
  message(STATUS "${TAB}- C++ Project warnings have been set to: ${warnings}.") 

	if (CXX_RTTI)
	  #
		# Enable RTTI
		#
		add_compile_options("$<$<CXX_COMPILER_ID:MSVC>:/GR>")
		message(STATUS "${TAB}- RTTI has been enabled.")
	else()
	    message("${TAB}- RTTI is disabled.")
	endif(CXX_RTTI)
	
	if (NOT CONFIGURED_ONCE)

		# CXX
		#
		set(CMAKE_CXX_FLAGS "${CMAKE_CXX_FLAGS} ${warnings}"
			CACHE STRING "Flags used by the compiler during all build types." FORCE)

		# C
		#
		set(CMAKE_C_FLAGS   "${CMAKE_C_FLAGS} ${warnings}"
			CACHE STRING "Flags used by the compiler during all build types." FORCE)
				
		set(CONFIGURED_ONCE TRUE)

	endif()
	
	SET(CMAKE_DEBUG_POSTFIX   "d")

endmacro(ADD_SETTINGS_FUNC)

# -----------------------------------------------------------
# Move Files
# ----------------------------------------------------------- [Deploy]
macro(DEPLOY_FUNC)
# Deploy is not required for this cmake module.
endmacro(DEPLOY_FUNC)
