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

SCRIPTDIR=$(cd "$(dirname "$0")" && pwd)
. "$SCRIPTDIR/colors.sh"

Tab=" "
TAB=" "

function line()
{
 echo -e "$Tab ${NC}$1"
}

function prompt()
{
  echo -e -n "$Tab $1"
}

DefaultlnColor="${NC}"

function default_color()
{
  echo -e "$DefaultlnColor"
}

function echo_ig
{
  echo -e "$Tab ${IGreen}$1${NC}"
}

function echo_ir()
{
   echo -e "$Tab ${IRed}$1${NC}"
}

function echo_iy()
{
  echo -e "$Tab ${IYellow}$1${NC}"
}

function echo_ib()
{
  echo -e "$Tab ${ICyan}$1${NC}"
}

function echo_ip()
{
  echo -e "$Tab ${IPurple}$1${NC}"
}

function prompt_ig()
{
  echo -e -n "$Tab ${IGreen}$1${NC}"
}

function prompt_iy()
{
  echo -e -n "$Tab ${IYellow}$1${NC}"
}

function prompt_uiy()
{
  echo -e -n "$Tab ${UYellow}$1${NC}"
}

function prompt_ip()
{
  echo -e -n "$Tab ${IPurple}$1${NC}"
}

function prompt_default()
{
  echo -e -n "$Tab ${NC}$1"
}

function echo_link_iy()
{
  # $1 is a description
  # $2 is the text to underline
  #
  echo -e "$Tab ${IYellow}$1${UCyan}$2${NC}"
}

function echo_link_default()
{
  # $1 is a description
  # $2 is the text to underline
  #
  echo -e "$Tab $1${UCyan}$2${NC}"
}

function echo_error()
{
   echo
   echo -e "$Tab ${IYellow}$1${NC}...${IRed}Failed!${NC}"
   echo
}

function echo_ok()
{
	echo -e "${IGreen}OK${NC}"
}

function echo_failed()
{
	echo -e "${IRed}FAILED!${NC}"
}

function exit_on_error_prompt()
{
   echo -e "$Tab ${IYellow}$1${NC}...${IRed}Failed!${NC}"
   echo
   read -rp "$Tab Press any key to Exit..."
   exit 1
}

function exit_on_success_prompt()
{
   echo -e "$Tab ${IYellow}$1${NC}...${IGreen}Succeeded!${NC}"
   echo
   read -rp "$Tab Press any key to Exit..."
   f_nc
   exit 0
}

function backup_file()
{
  # $1 is source
  # $2 is destination

  if [ ! -e $2 ]; then
    echo -e "$Tab ${IYellow}Backing UP: ${IPurple} '$1'${IYellow} to ${IPurple}'$2'${IYellow}...${NC}"
    sudo cp $1 $2
  fi
}

# $1 is the expected version (i.e. the desired version of the OS to be on).
# 
function require_linux_os_version()
{
    clear
    ExpectedOsVersion=$1
    show_step "Checking for OS version ${version}..."

    ActualOsVersion=$(lsb_release -rs)
    if [ "$ActualOsVersion" != "${ExpectedOsVersion}" ]; then
        exit_on_error_prompt "$Tab A precondition of the script failed: Unexpected OS version: '${ActualOsVersion}'; Expected version: '${ExpectedOsVersion}'."
    fi

    # make sure the virtual box group exists...
    # this will have no effect (other than adding a group)
    # when not running on virtual hosts.
    #
    #sudo usermod -aG vboxsf $(whoami)

    prompt_iy "$Tab OS version is Ubuntu ${ActualOsVersion}..."
	
    echo_ig "Passed!"
}

bool_value=0
function directory_exists()
{
  full_path=$1
  if [ -d "$full_path" ]; then
    bool_value=1
  else
    bool_value=0
  fi
}

function file_exists()
{
  full_path=$1
  if [ -f "$full_path" ]; then
    bool_value=1
  else
    bool_value=0
  fi
}

function assert_directory_exists()
{
  abort_on_error=$2
  directory_exists $1

  if [ 1 == "$bool_value" ]; then
    echo -e "$Tab ${IYellow}Directory '$1' exists - " "${IGreen}Success!${NC}"
  else
    echo -e "$Tab ${IYellow}Directory '$1' does not exist - " "${IRed}Not Found!${NC}"
    if [ "true" == "$abort_on_error" ]; then
    echo "Exiting!"
      exit 0
    fi
  fi
}

function assert_file_exists()
{
  file_exists $1
  if [ 1 == "$bool_value" ]; then
    echo -e "$Tab ${IYellow}File '$1' exists - " "${IGreen}Success!${NC}"
  else
    echo -e "$Tab ${IYellow}File '$1' does not exist - " "${IRed}Error!${NC}"
    if [ "true" == $2]; then
      exit 0
    fi
  fi
}

function exit_when_file_not_exist()
{
  file_exists $1
  if [ 1 != "$bool_value" ]; then
    exit 0
  fi
}

total_build_time=0;

initialize_build_time()
{
  # Note: 'SECONDS' is a bash shell feature.
  #
  SECONDS=0
  total_build_time=0
}

report_build_time()
{
  a=SECONDS

  b=total_build_time

  # Accumulate the build time here:
  #
  total_build_time=$((a+b))
  echo
  echo_iy "$Tab Build Time: $((a / 60)) minutes and $((a % 60)) seconds."
  SECONDS=0
}

report_total_build_time()
{
  a=SECONDS
  b=total_build_time
  total=$((a+b))
  echo
  TimeFormat="Total Build Time: $((total / 60)) minutes and $((total % 60)) seconds."
  echo_iy "$Tab$TimeFormat"
  echo
}

# A function that compares two strings that is not
# case sensitive.
#
# $1 = rhv
# $2 = lhv
#
# Example:
#   icompare "$some_value" "foo"
#   if [ true = "$are_equal" ]; then
#     rm -rf "do something"
#   fi
#
are_equal=true
icompare()
{
  are_equal=false
  shopt -s nocasematch
  if [[ "$1" == "$2" ]]; then
    are_equal=true
  fi
  shopt -u nocasematch
}

compare()
{
  are_equal=false
  if [[ "$1" == "$2" ]]; then
    are_equal=true
  fi
}

function insert_after_line # file line newText
{
   local file="$1" line="$2" newText="$3"
   sed -i -e "/^$line$/a"$'\\\n'"$newText"$'\n' "$file"
}

is_installed="false"
function is_app_installed
{
  is_installed=false
  if [ -x "$(command -v $1)" ]; then
     is_installed="true"
  fi
}

function require_admin()
{
    net session > /dev/null 2>&1
    if [ $? -eq 0 ]; then 
      echo
    else 
      echo "$Tab This script must be run as an administrator."
      exit 0 
    fi
}

function system_os
{
	os_="$(uname -s)"
	echo "$os_"
}

system_os_type="$OSTYPE"

case "$OSTYPE" in
  solaris*) 
   system_os_type="solaris"
   ;; 
  darwin*)  
   system_os_type="darwin"
   ;;   
  linux*)   
   system_os_type="linux"
   ;; 
  bsd*)     
    system_os_type="bsd"
    ;;
  msys*)    
   system_os_type="windows"
   ;;
  *)        
   ;;
esac

is_windows=false
is_linux=false
is_darwin=false

function configure_for_os
{
	echo -e "$Tab ${IYellow}Configuring script for for $system_os_type..."
	echo
	icompare "$system_os_type" "windows" 
	if [ true = "$are_equal" ] 
	  then
		is_windows=true
	fi

	icompare "$system_os_type" "linux" 
	if [ true = "$are_equal" ] 
	  then
		is_linux=true
	fi
  
  icompare "$system_os_type" "darwin" 
	if [ true = "$are_equal" ] 
	  then
		is_darwin=true
	fi
}

function update_os
{
  configure_for_os
  
  os_="$(uname -s)"
  
  echo_iy "Updating $os_..."
  
  if [ true = "$is_linux" ]; then
       sudo apt-get update
  else
     echo_iy "'$system_os_type' does not support updating the system from the shell."
	   echo_link "See: " "https://chocolatey.org/"
  fi
	   
  #sudo apt-get autoremove
}

 #_________________________________________________________________
 # Note:
 #    The following is only supported only in Visual Studio 2017.
 #    It assumes the standard 'dotnet' install location and may
 #    not be available on some Windows' systems.
 #
 #    $1 == solution path
 #    $2 == project path
 #
 #    Result is 'project' will be inserted into 'solution'.
 #_________________________________________________________________
function windows_inject_project_into_solution()
{
  if [ true = "$is_windows" ]; then
	 if [ ! -d "/c/Program Files/dotnet" ]; then
		alias dotnet="\"/c/Program Files/dotnet/dotnet.exe\""
		if [ -f $dotnet ]; then
			$dotnet sln $1 add $2
		fi
	fi
  fi
}

function require_bash_version_4_or_above()
{
	if  [[ ${BASH_VERSINFO:-0} -ge 4 ]]; then 
		  echo_iy " Checking Bash version...your version is '${BASH_VERSION}'."
		  sleep 1
	else
	      echo -e "${IRed}Bash 4.x or above is required to use the SLX Bash Script Libraries!${NC}"
		  echo
		  exit 1 
	fi
}

# result=$(posix_drive_to_windows)
function posix_drive_to_windows()
{
   local p=$(echo "$1" | sed -e 's/^\///' -e 's/\//\\/g' -e 's/^./\0:/')
   echo "$p"
}

function windows_drive_to_posix()
{
  local p=$(echo "$1" | sed -e 's#\\#/#g' -e 's#\$#/\\$#g')
  echo "$p"
}
