#pragma once
#include "CameraGenBase.h"

class CameraGenFollowToNode : public CameraGenBase
{
public:
	CameraGenFollowToNode();
	virtual ~CameraGenFollowToNode(void);
	
	CameraGenBase::GenCameraType GetType()
	{
		return CameraGenBase::FollowToNode;
	}

	stringc UnitInstanceName;

	bool RotateWithUnit;
	
	// Размер карты 2d
	u32 MapSize2d;

	// Размер карты 3d
	f32 MapSize3d;

	int ObstacleFilterId;
};
