// Prologue
//
// SLX - Simple Library Extensions
//
// Copyright 2000-2018 Bill Berry
// email: wsberry@gmail.com
// github: https://github.com/wsberry
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License src distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//
// Initial prototype.
//
// TODO: This source file is NOT COMPLETE!
//
// endPrologue
#pragma once
#ifndef SLX_SYSTEM_FILE_TOOLS_HPP__
#define SLX_SYSTEM_FILE_TOOLS_HPP


#  if !defined(_CRT_SECURE_NO_WARNINGS)
#    define _CRT_SECURE_NO_WARNINGS
#  endif

#if CPP_14_OR_EARLIER
#include <ghc/filesystem.hpp>
#else
#include <filesystem>
#endif

#include <fstream>
#include <iomanip>
#include <cstdio>
#include <cassert>
#include <string>


#if defined(_WINDOWS)

#	include "targetver.h"


#	define WIN32_LEAN_AND_MEAN
#	include <windows.h>

#	ifdef CPPFILECOPY_EXPORTS  
#		define SLX_FILE_TOOLS_DLL_DECLSPEC __declspec(dllexport)   
#	else  
#		define SLX_FILE_TOOLS_DLL_DECLSPEC __declspec(dllimport)   
#endif

#endif // defined(_WINDOWS)

extern "C"
{
   bool SLX_FILE_TOOLS_DLL_DECLSPEC filesystem_copy(const wchar_t* source_folder, const wchar_t* destination_folder);
   bool SLX_FILE_TOOLS_DLL_DECLSPEC buffered_copy(const wchar_t* source_folder, const wchar_t* destination_folder);

   // C-Sharp Code Example:
   //
   //#if DEBUG
   //	[DllImport(@"cpp-file-copyd.dll", CharSet = CharSet.Auto)]
   //#else
   //	[DllImport(@"cpp-file-copy.dll", CharSet = CharSet.Auto)]
   //#endif
   //
   //	public static extern bool filesystem_copy(string source, string destination);
   //	public static extern bool buffered_copy(string source, string destination);
}

namespace slx
{
   namespace system
   {
      namespace file
      {

         /**
          * \brief Converts ::std::ios_base mode to 'C' literal mode.
          * \param mode The Mode to be converted.
          * \return The mode as a const char*.
          */
#if CPP_14_OR_EARLIER
         inline const auto* to_string(const ::std::ios_base::openmode mode)
#else
         constexpr auto to_string(const std::ios_base::openmode mode)
#endif
         {
            switch(mode)
            {
            case ::std::ios_base::in: 
               return "r";

            case ::std::ios_base::in | ::std::ios_base::binary:
               return "rb";

            case ::std::ios_base::out: 
               return "w";

            case ::std::ios_base::out | ::std::ios_base::app:
               return "wa";

            case ::std::ios_base::out | ::std::ios_base::binary:
               return "wb";

            case ::std::ios_base::out | ::std::ios_base::binary | ::std::ios_base::app:
               return "wba";

            // Not Implemented:
            //
            // trunc - discard the contents of the stream when opening
            // ate   - seek to the end of stream immediately after open

            default:

               break;
         
            }

            assert(false && "'::std::ios_base::openmode' arg should be file open mode.");

            return "";
         }
         /**
          * \brief The default block size that .NET 4.5.7 uses.
          *        Based on experimentation between 4096 and 81920
          *        work best for read/write buffer sizes.
          */
#if CPP_14_OR_EARLIER
          const std::size_t default_file_buffer_size = 81920;
#else
           constexpr const std::size_t default_file_buffer_size = 81920;
#endif

         /**
          * \brief 
          * \tparam ReadBlockSize 
          * \tparam ByteT 
          * \param src 
          * \param os 
          * \param closeOutputFile 
          * \return 
          */
         template<std::size_t ReadBlockSize = default_file_buffer_size, class ByteT = char>
         std::size_t copy(::std::ifstream& src, ::std::ofstream& os, const bool closeOutputFile = true)
         {
            if (!src.is_open()) return;

            ByteT buf[ReadBlockSize] = {};

            std::size_t total_read{};

            try
            {
               do
               {
                  src.read(buf, ReadBlockSize);
                  os.write(buf, src.gcount());
                  total_read += src.gcount();
               } while (!src.eof());
            }
            catch (...)
            {
               assert(false && "Unexpected file excpetion occurred.");
            }

            src.close();

            if (closeOutputFile) os.close();

            return total_read;
         }

         /**
          * \brief 
          * \tparam ReadBlockSize 
          * \tparam ByteT 
          * \param src 
          * \param dst 
          * \param closeOutputFile 
          * \return 
          */
         template<std::size_t ReadBlockSize = default_file_buffer_size, class ByteT = char>
         std::size_t copy(const std::string& src, const std::string& dst, const bool closeOutputFile = true)
         {
            assert(!src.empty() && "arg 'src' should not be empty!");
            assert(!dst.empty() && "arg 'dst' should not be empty!");
            if (src.empty() || dst.empty()) return 0;

            ::std::ifstream ifs(src, std::ios::binary);
            ::std::ofstream ofs(dst, std::ios::binary);
            return copy<ReadBlockSize, ByteT>(ifs, ofs, closeOutputFile);
         }

         /**
          * \brief 
          * \param src 
          * \param dst 
          * \param closeOutputFile 
          */
         inline bool filesystem_copy(const std::string& src, const std::string& dst);

         /**
          * \brief 
          */
         struct c_iostream final
         {
            /**
             * \brief 
             */
            std::string path{};

            /**
             * \brief 
             */
            std::string mode{};

            /**
             * \brief 
             */
            ::std::FILE* file_ptr{ nullptr };

            
            /**
             * \brief 
             */
            c_iostream()
            {}

            /**
             * \brief 
             * \param filePath 
             * \param mode 
             */
            c_iostream(const std::string filePath, const ::std::ios_base::openmode mode)
            : path(filePath) , mode(to_string(mode))
            {
            }

            /**
             * \brief 
             */
            ~c_iostream()
            {
               close();
            }

            /**
             * \brief 
             * \return 
             */
            FILE* release()
            {
               auto* f = file_ptr;
               file_ptr = nullptr;
               return f;
            }

            /**
             * \brief 
             * \return 
             */
            bool open()
            {
               close();
               return nullptr != (file_ptr = ::std::fopen(path.c_str(), mode.c_str()));
            }

            /**
             * \brief 
             */
            void close()
            {
               if (nullptr != file_ptr) ::std::fclose(file_ptr);
               file_ptr = nullptr;
            }

            std::string file_name()
            {
               assert(false && "Not Implemented!"); return{};
            }

            std::string file_directory()
            {
               assert(false && "Not Implemented!"); return{};
            }


            std::string change_file_name(std::string fileName)
            {
               assert(false && "Not Implemented!"); return{};
            }

            std::string change_directory(std::string directoryPath)
            {
               assert(false && "Not Implemented!"); return{};
            }

            bool save()
            {
               assert(false && "Not Implemented!"); return{};
            }

            bool save_as(std::string path)
            {
               assert(false && "Not Implemented!"); return false;
            }


            /**
             * \brief 
             * \return 
             */
            FILE* begin()
            {
               if (nullptr == file_ptr) open();
               //
               // TODO: Check reliability across operating systems.
               //
               fseek(file_ptr, 0, SEEK_SET);

               return file_ptr;
            }

            /**
             * \brief 
             * \tparam ReadBlockSize 
             * \tparam ByteT 
             * \return 
             */
            template<std::size_t ReadBlockSize = default_file_buffer_size, class ByteT = char>
            std::size_t size()
            {
               if (path.empty()) return 0;

               open();

               if (nullptr == file_ptr) return 0;
               ByteT buf[ReadBlockSize] = {};
               size_t size;
               size_t total{};
               try
               {
                  while ((size = fread(buf, sizeof(ByteT), ReadBlockSize, file_ptr)) > 0)
                  {
                     total += size;
                  }
               }
               catch (...)
               {
                  assert(false && "Unexpected file excpetion occurred.");
               }

               begin();

               return total;
            }

            ::std::FILE* operator()() const { return file_ptr; }
         };

         /**
          * \brief 
          * \tparam ReadBlockSize 
          * \tparam ByteT 
          * \param src 
          * \param dst 
          * \param closeOutputFile 
          * \return 
          */
         template<std::size_t ReadBlockSize = default_file_buffer_size, class ByteT = char>
         ::std::FILE* c_copy(const char* const src, const char* const dst, const bool closeOutputFile = true)
         {
            assert(nullptr != src && "arg 'src' should not be nullptr!");
            assert(nullptr != dst && "arg 'dst' should not be nullptr!");
            if (nullptr == src || nullptr == dst) return nullptr;
            
            auto* srcF = ::std::fopen(src, "rb");
            auto* dstF = ::std::fopen(dst, "wb");

            assert(nullptr != srcF && "arg 'src' cannot be opened!");
            assert(nullptr != dstF && "arg 'dst' cannot be opened!");
            if (nullptr == srcF || nullptr == dstF) return nullptr;

            ByteT buf[ReadBlockSize] = {};

            size_t size{};

            try
            {
               while ((size = fread(buf, sizeof(ByteT), ReadBlockSize, srcF)) > 0)
               {
                  fwrite(buf, sizeof(char), size, dstF);
               }
            }
            catch(...)
            {
               assert(false && "Unexpected file excpetion occurred.");
            }

            ::std::fclose(srcF);

            if (closeOutputFile)
            {
               ::std::fclose(dstF);
               dstF = nullptr;
            }

            return dstF;
         }

         struct named_ifstream final : ::std::ifstream
         {
            std::string name{};
            bool open(const ios_base::openmode mode = ios_base::in)
            {
               if (is_open()) return true;
               ::std::ifstream::open(name, mode);
               return is_open();
            }
         };

         struct named_ofstream final : ::std::ofstream
         {
            std::string name{};
            bool open(const ios_base::openmode mode = ::std::ios_base::out)
            {
               if (is_open()) return true;
               ::std::ofstream::open(name, mode);
               return is_open();
            }
         };
      }
   }
}


#endif // SLX_SYSTEM_FILE_TOOLS_HPP
