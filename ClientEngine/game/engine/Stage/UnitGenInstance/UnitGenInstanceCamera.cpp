#include "UnitGenInstanceCamera.h"

UnitGenInstanceCamera::UnitGenInstanceCamera(SharedParams_t params) : UnitGenInstanceBase(params)
{
	CameraGenBaseData = NULL;
	FarValue = 1000.0f;
}

UnitGenInstanceCamera::~UnitGenInstanceCamera(void)
{
	if (CameraGenBaseData) delete CameraGenBaseData;
	CameraGenBaseData = NULL;
}

