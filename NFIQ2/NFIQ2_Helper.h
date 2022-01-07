#pragma once
#include <nfiq2_exception.hpp>

#define EXTERN_DLL_EXPORT extern "C" __declspec(dllexport)

EXTERN_DLL_EXPORT void initializeNFIQ2(const char* modelFile);
EXTERN_DLL_EXPORT unsigned int computeQualityScore(const uint8_t* pData, uint32_t width, uint32_t height, uint8_t fingerCode, uint16_t ppi);
EXTERN_DLL_EXPORT void uninitializeNFIQ2();

EXTERN_DLL_EXPORT bool getLastError(const char*& outErrorMessage);
