// Prologue
//
// SLX - Simple Library Extensions
//
// Copyright 2000-2018 Bill Berry
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//
// Initial prototype.
//
// endPrologue
#pragma once

#if defined(_WINDOWS)
#	include "targetver.h"
#	define WIN32_LEAN_AND_MEAN
#	include <windows.h>
#endif

#ifdef CPPFILECOPY_EXPORTS  
#define DLL_DECLSPEC __declspec(dllexport)   
#else  
#define DLL_DECLSPEC __declspec(dllimport)   
#endif

extern "C"
{
	bool DLL_DECLSPEC cpp_filesystem_file_copy(const wchar_t* source, const wchar_t* destination);
	bool DLL_DECLSPEC cpp_buffered_file_copy(const wchar_t* source, const wchar_t* destination, unsigned bufferSizeKB);
}

// C-Sharp Code Example:
//
//#if DEBUG
//	[DllImport(@"cpp-file - copyd.dll", CharSet = CharSet.Auto)]
//#else
//	[DllImport(@"cpp-file - copy.dll", CharSet = CharSet.Auto)]
//#endif
//	public static extern bool cpp_filesystem_file_copy(string source, string destination);
//	public static extern bool cpp_buffered_file_copy(string source, string destination, uint bufferSizeKB);



