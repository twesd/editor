#pragma once
#include "UnitModelBase.h"

class UnitModelSphere : public UnitModelBase
{
public:
	UnitModelSphere(SharedParams_t params);

	UNIT_MODEL_TYPE GetModelType()
	{
		return UNIT_MODEL_TYPE_SPHERE;
	}

	ISceneNode* LoadSceneNode();

	// Радиус сферы
	f32 Radius;

	// Детализация
	int PolyCount;
};
