#pragma once
#include "UnitSelectSceneNodeBase.h"
#include "Core/CompareType.h"

class UnitSelectSceneNodeId : public UnitSelectSceneNodeBase
{
public:
	UnitSelectSceneNodeId(SharedParams_t params);
	virtual ~UnitSelectSceneNodeId(void);

	// Выполнить выборку
	virtual core::array<scene::ISceneNode*> Select(core::array<Event_t*>& events);

private:
	void SelectNodes( ISceneNode* start, ISceneNode* mainNode, core::array<scene::ISceneNode*> &selectNodes );
};
