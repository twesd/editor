#pragma once
#include "UnitSelectSceneNodeBase.h"
#include "Core/CompareType.h"

class UnitSelectSceneNodeData : public UnitSelectSceneNodeBase
{
public:
	UnitSelectSceneNodeData(SharedParams_t params);
	virtual ~UnitSelectSceneNodeData(void);	
	
	// Выполнить выборку
	virtual core::array<scene::ISceneNode*> Select(core::array<Event_t*>& events);

	// Наименование параметра Data
	stringc DataName;
};
