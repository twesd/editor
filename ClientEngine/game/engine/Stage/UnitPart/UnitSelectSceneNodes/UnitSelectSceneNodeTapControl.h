#pragma once
#include "UnitSelectSceneNodeBase.h"
#include "Core/CompareType.h"

class UnitSelectSceneNodeTapControl : public UnitSelectSceneNodeBase
{
public:
	UnitSelectSceneNodeTapControl(SharedParams_t params);
	virtual ~UnitSelectSceneNodeTapControl(void);	
	
	// Выполнить выборку
	virtual core::array<scene::ISceneNode*> Select(core::array<Event_t*>& events);

	// Наименование контрола TapScene
	stringc TapSceneName;
};
