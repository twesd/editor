#pragma once
#include "UnitSelectSceneNodeBase.h"
#include "Core/CompareType.h"
#include "Core/NodeWorker.h"

class UnitSelectSceneNodeDistance : public UnitSelectSceneNodeBase
{
public:
	UnitSelectSceneNodeDistance(SharedParams_t params);
	virtual ~UnitSelectSceneNodeDistance(void);	
	
	// Выполнить выборку
	virtual core::array<scene::ISceneNode*> Select(core::array<Event_t*>& events);

	// Расстояние
	f32 Distance;

	// Тип сравнения
	CompareTypeEnum CompareType;

	// Учитывать размеры юнитов
	bool UseUnitSize;

};
