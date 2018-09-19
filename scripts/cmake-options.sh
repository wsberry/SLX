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


# Customize your cmake conventions in this file.
#
CMAKE_BUILD_DIR="_build"
CMAKE_GENERATOR="Visual Studio 15 2017 Win64"
CMAKE_CONFIGURATION_TYPES="-DCMAKE_CONFIGURATION_TYPES:STRING=Debug;Release"