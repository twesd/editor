#pragma once
#include "../../../Core/Base.h"

enum UNIT_MODEL_TYPE
{
	UNIT_MODEL_TYPE_EMPTY,
	UNIT_MODEL_TYPE_ANIM,
	UNIT_MODEL_TYPE_BILLBOARD,
	UNIT_MODEL_TYPE_SPHERE,
	UNIT_MODEL_TYPE_PARTICLE_SYSTEM
};

class UnitModelBase : public Base
{
public:
	UnitModelBase(SharedParams_t params) : Base(params) {}

	virtual UNIT_MODEL_TYPE GetModelType() = 0;  

	virtual ISceneNode* LoadSceneNode() = 0;
};
