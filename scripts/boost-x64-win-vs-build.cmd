rem Modified from 'https://github.com/Studiofreya/boost-build-scripts'

rem Updated Bill Berry for Boost 1.67 2018
rem This may be copied to your boost source directory and run.
rem ...or see below 'boost_dir'.

rem For BOOST build documentation see: https://boostorg.github.io/build/manual/develop/index.html
rem Assumes Visual Studio 2017 at the time of this update.
rem Added 'cxxflags' option to assign c++ version to build against.
rem Added comments concerning b2 options.
rem Added Options:
rem   'link'         - Determines if Boost build (b2 application) creates 'shared' or 'static' libraries
rem   'runtime-link' - Determines if shared or static version of C and C++ runtimes should be used.

set restore_point_directory=%CD%

@echo off
set "build_shared_or_static=shared"
set "runtime_link=shared"
set boost_dir="C:\opt\boost\v1.67"
set cores=%NUMBER_OF_PROCESSORS%

cd %boost_dir%

rem Available Visual Studio Toolsets:

rem    Visual Studio 2012 -> set msvcver=msvc-11.0
rem    Visual Studio 2013 -> set msvcver=msvc-12.0
rem    Visual Studio 2015 -> set msvcver=msvc-14.0
rem    Visual Studio 2017 -> set msvcver=msvc-14.1
  
set msvcver=msvc-14.1
  
rem Start building boost
echo Building %boost_dir% with %cores% cores using toolset '%msvcver%' and 'std:c++latest'.
  
rem Remove the following remarks if you need to build the 'b2' build application for boost.
rem cd %boost_dir%
rem call bootstrap.bat

b2.exe -j%cores% toolset=%msvcver% cxxflags=/std:c++latest address-model=64 architecture=x86 link=%build_shared_or_static% threading=multi runtime-link=%runtime_link% variant=debug stage --stagedir=stage/x64 
b2.exe -j%cores% toolset=%msvcver% cxxflags=/std:c++latest address-model=64 architecture=x86 link=%build_shared_or_static% threading=multi runtime-link=%runtime_link% variant=release stage --stagedir=stage/x64 
b2.exe -j%cores% toolset=%msvcver% cxxflags=/std:c++latest address-model=32 architecture=x86 link=%build_shared_or_static% threading=multi runtime-link=%runtime_link% variant=debug stage --stagedir=stage/win32
b2.exe -j%cores% toolset=%msvcver% cxxflags=/std:c++latest address-model=32 architecture=x86 link=%build_shared_or_static% threading=multi runtime-link=%runtime_link% variant=release stage --stagedir=stage/win32

cd %restore_point_directory%