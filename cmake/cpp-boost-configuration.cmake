# Draft 

SET(TAB "  ")

MESSAGE(STATUS "${TAB}Configuring BOOST...")

SET(BOOST_VERSION "1.67.0")

MACRO(configure_boost)

      SET(SLX_BOOST_FIXUP_ROOT_DIR "C:/opt/boost/v1.67")
      SET(SLX_BOOST_FIXUP_LIB_DIR "C:/opt/boost/v1.67/stage/x64/lib")

      # Configure BOOST libraries.
      # Note that BOOST should be installed on your system.
      # Boost typically installs in /c/local...
      #
      # In the case of a full install build, the user has the option to specify
      # where boost is by providing command line arguments to CMake
      # such as -DBOOST_ROOT=[C:\Some\Directory\boost] and 
      # _DBOOST_LIBRARY_DIR=[C:\Some\Directory\boost\boost_libs]
      #
      # Note: 
      #    This option is provided to allow a single process installer to unzip required dependencies
      #    and run cmake from a single process, without the need to access new environment variables
      #
      if ("" STREQUAL "${BOOST_ROOT}")
            SET(BOOST_ROOT "$ENV{BOOST_ROOT}")
      else()
            SET(BOOST_ROOT "${SLX_BOOST_FIXUP_ROOT_DIR}")
            MESSAGE(WARNING "A required Environment Variable, 'BOOST_ROOT', is not defined!")
            MESSAGE(WARNING "Using fixup where 'BOOST_ROOT'is '${BOOST_ROOT}'.")
      endif()

      if("" STREQUAL "${BOOST_LIBRARYDIR}")
            SET(BOOST_LIBRARYDIR "$ENV{BOOST_LIBRARYDIR}")
      els()
            SET(BOOST_LIBRARYDIR "${SLX_BOOST_FIXUP_LIB_DIR}")
            MESSAGE(WARNING "A required Environment Variable, 'BOOST_LIBRARYDIR', is not defined!")
            MESSAGE(WARNING "Using fixup where 'BOOST_LIBRARYDIR'is '${BOOST_LIBRARYDIR}'.")
      endif()

      SET(BOOST_ENVIRONMENT_INC_DIR "${BOOST_ROOT}")

      # The following settings are configured for the ROS Emulator
      #
      set (Boost_NO_SYSTEM_PATHS ON)
      set (Boost_USE_MULTITHREADED ON)
      set (Boost_USE_STATIC_LIBS ON)
      set (Boost_USE_STATIC_RUNTIME OFF)
      set (BOOST_ALL_DYN_LINK OFF)

      # This is the minimum version required.
      #
      find_package(Boost ${BOOST_VERSION} REQUIRED)

      if(Boost_FOUND)
         message(STATUS "\nBoost Lib Path is: ${BOOST_LIBRARYDIR}")
         message(STATUS "Boost include directory is: ${Boost_INCLUDE_DIR}")
         
         # From CMake  Documentation:
         #
         #     If the SYSTEM option is given,the compiler will be told the 
         #     directories are meant as system include directories on 
         #     some platforms (signaling this setting might achieve effects
         #     such as the compiler skipping warnings [...])."
         #
         include_directories (SYSTEM ${Boost_INCLUDE_DIR})

      else()
         message(WARNING "\nBOOST configuration warning. BOOST CPP Package was not found! Is it installed?")
         message(WARNING "Assuming the following location: '${BOOST_ENVIRONMENT_INC_DIR}'.\n")
         include_directories(SYSTEM ${BOOST_ENVIRONMENT_INC_DIR})
      endif()

endMACRO()
