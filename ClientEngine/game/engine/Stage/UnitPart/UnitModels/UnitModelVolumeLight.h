#pragma once
#include "UnitModelBase.h"

class UnitModelVolumeLight : public UnitModelBase
{
public:
	UnitModelVolumeLight(SharedParams_t params);

	UNIT_MODEL_TYPE GetModelType()
	{
		return UNIT_MODEL_TYPE_EMPTY;
	}

	ISceneNode* LoadSceneNode();

	u32 SubdivU;

	u32 SubdivV;

	video::SColor Foot;

	video::SColor Tail;
};
