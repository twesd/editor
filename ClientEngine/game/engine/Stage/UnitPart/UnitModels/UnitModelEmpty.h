#pragma once
#include "UnitModelBase.h"

class UnitModelEmpty : public UnitModelBase
{
public:
	UnitModelEmpty(SharedParams_t params);

	UNIT_MODEL_TYPE GetModelType()
	{
		return UNIT_MODEL_TYPE_EMPTY;
	}

	ISceneNode* LoadSceneNode();
};
