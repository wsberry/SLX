#include "slx-file-tools.hpp"

#if defined(_WINDOWS)
//
// Required for Windows shared libraries (i.e. dynamic linked libraries)
//
BOOL APIENTRY DllMain(HMODULE /*hModule*/,
	DWORD   /*ul_reason_for_call*/,
	LPVOID  /*lpReserved*/
)
{
	//switch (ul_reason_for_call)
	//{
	//case DLL_PROCESS_ATTACH:
	//case DLL_THREAD_ATTACH:
	//case DLL_THREAD_DETACH:
	//case DLL_PROCESS_DETACH:
	//	break;
	//}
	return TRUE;
}
#endif // _WINDOWS

#include <string>

bool slx::system::file::filesystem_copy(const std::string& src, const std::string& dst)
{
   assert(!src.empty() && "arg 'src' should not be empty!");
   assert(!dst.empty() && "arg 'dst' should not be empty!");

   if (src.empty() || dst.empty()) return false;

   auto no_error = true;

   try
   {
#if CPP_14_OR_EARLIER
      ghc::filesystem::copy(src, dst, ghc::filesystem::copy_options::overwrite_existing);
#else
      std::filesystem::copy(src, dst, std::filesystem::copy_options::overwrite_existing);
#endif
   }
   catch (const std::exception& ex)
   {
      ex;
      no_error = false;
      assert(false && ex.what());
   }

   return no_error;
}


bool SLX_FILE_TOOLS_DLL_DECLSPEC filesystem_copy(const wchar_t* source, const wchar_t* destination)
{
	if (nullptr == source || nullptr == destination) return false;

	std::wstring sw = source;
   std::wstring dw = destination;
   const std::string sn(sw.begin(), sw.end());
   const std::string dn(dw.begin(), dw.end());

   return slx::system::file::filesystem_copy(sn, dn);
}

bool SLX_FILE_TOOLS_DLL_DECLSPEC buffered_copy(const wchar_t* source, const wchar_t* destination)
{
   // TODO: Create 'C' FILE buffer implementation here.
   //       Use ghc::filesystem::copy for now.
   //
   if (nullptr == source || nullptr == destination) return false;

   std::wstring sw = source;
   std::wstring dw = destination;
   const std::string sn(sw.begin(), sw.end());
   const std::string dn(dw.begin(), dw.end());

   return slx::system::file::filesystem_copy(sn, dn);
}



