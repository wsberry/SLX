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



bool filesystem_copy(const wchar_t* source, const wchar_t* destination)
{
	if (nullptr == source || nullptr == destination) return false;

	// TODO:


	return true;
}

void slx::system::file::filesystem_copy(const std::string& src, const std::string& dst, bool closeOutputFile)
{
	assert(!src.empty() && "arg 'src' should not be empty!");
	assert(!dst.empty() && "arg 'dst' should not be empty!");
	if (src.empty() || dst.empty()) return;

	try
	{
		std::filesystem::copy(src, dst, std::filesystem::copy_options::overwrite_existing);
	}
	catch (...)
	{
		assert(false && "Unexpected file excpetion occurred.");
	}
}

bool buffered_copy(const wchar_t* source, const wchar_t* destination, const std::size_t bufferSizeKB)
{
	if (nullptr == source || nullptr == destination) return false;

	// TODO:


	return true;
}



