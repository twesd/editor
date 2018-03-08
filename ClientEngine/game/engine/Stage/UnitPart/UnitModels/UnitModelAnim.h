#pragma once
#include "UnitModelBase.h"

class UnitModelAnim : public UnitModelBase
{
public:
	UnitModelAnim(SharedParams_t params);

	UNIT_MODEL_TYPE GetModelType()
	{
		return UNIT_MODEL_TYPE_ANIM;
	}

	ISceneNode* LoadSceneNode();

	stringc ModelPath;

	bool Use32Bit;

	bool Culling;
};
