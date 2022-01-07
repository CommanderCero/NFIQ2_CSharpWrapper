#include "NFIQ2_Helper.h"

#include <memory>
#include <nfiq2.hpp>
#include <opencv2/core.hpp>
#include <iostream>

std::unique_ptr<NFIQ2::Algorithm> algorithm;
std::string lastError;

template<typename Func>
auto storeErrors(Func f)
{
	try
	{
		return f();
	}
	catch(std::exception& e)
	{
		lastError = std::string(e.what());
	}
}

void initializeNFIQ2(const char* modelFile)
{
	storeErrors([&]()
	{
		// Load default parameters
		NFIQ2::ModelInfo modelInfo(modelFile);
		algorithm = std::make_unique<NFIQ2::Algorithm>(modelInfo);
	});
}

unsigned int computeQualityScore(const uint8_t* pData, uint32_t width, uint32_t height, uint8_t fingerCode, uint16_t ppi)
{
	return storeErrors([&]()
	{
		NFIQ2::FingerprintImageData rawImage(pData, width * height, width, height, fingerCode, ppi);
		auto modules = NFIQ2::QualityFeatures::computeQualityModules(rawImage);
		return algorithm->computeQualityScore(modules);
	});
}

void uninitializeNFIQ2()
{
	if (algorithm != nullptr)
	{
		algorithm.release();
	}
}

bool getLastError(const char*& outErrorMessage)
{
	if (!lastError.empty())
	{
		outErrorMessage = lastError.c_str();
	}
	
	return !lastError.empty();
}