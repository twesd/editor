#pragma once
#include "UnitModelBase.h"

class UnitModelParticleSystem : public UnitModelBase
{
public:
	UnitModelParticleSystem(SharedParams_t params);

	UNIT_MODEL_TYPE GetModelType()
	{
		return UNIT_MODEL_TYPE_PARTICLE_SYSTEM;
	}

	ISceneNode* LoadSceneNode();
};
