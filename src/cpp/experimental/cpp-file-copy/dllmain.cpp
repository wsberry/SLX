#include "cpp-copyfile.hpp"

#if defined(WIN32)
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
#endif // WIN32

#include <string>

bool cpp_filesystem_file_copy(const wchar_t* source, const wchar_t* destination)
{
	if (nullptr == source || nullptr == destination) return false;

	// TODO:


	return true;
}

bool cpp_buffered_file_copy(const wchar_t* source, const wchar_t* destination, unsigned bufferSizeKB)
{
	if (nullptr == source || nullptr == destination) return false;

	// TODO:


	return true;
}



