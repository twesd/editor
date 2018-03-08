#pragma once
#include "CameraGenBase.h"

class CameraGenStatic : public CameraGenBase
{
public:
	CameraGenStatic();
	virtual ~CameraGenStatic(void);

	CameraGenBase::GenCameraType GetType()
	{
		return CameraGenBase::Static;
	}
};
