#pragma once
#include "UnitGenInstanceBase.h"
#include "CameraGen/CameraGenFollowToNode.h"
#include "CameraGen/CameraGenStatic.h"

class UnitGenInstanceCamera : public UnitGenInstanceBase
{
public:
	UnitGenInstanceCamera(SharedParams_t params);
	virtual ~UnitGenInstanceCamera(void);

	// Получить тип
	virtual GenInstanceType GetType()
	{
		return UnitGenInstanceBase::Camera;
	}

	// Необходимо ли удалить описание. Случай когда юниты больше создаватья не будут
	virtual bool CanDispose()
	{
		return true;
	}
	
	// Необходимо ли создать юнит
	virtual bool NeedCreate()
	{
		return true;
	}

	CameraGenBase* CameraGenBaseData;

	// Начальная цель
	vector3df StartTarget;

	// Дальность обзора
	f32 FarValue;

};
