#include "UnitSelectSceneNodeTapControl.h"
#include "../../UnitInstance/UnitInstanceStandard.h"
#include "../../../Controls/ControlEvent.h"

UnitSelectSceneNodeTapControl::UnitSelectSceneNodeTapControl(SharedParams_t params) : UnitSelectSceneNodeBase(params),
	TapSceneName()
{
}

UnitSelectSceneNodeTapControl::~UnitSelectSceneNodeTapControl()
{
}

// Выполнить выборку
core::array<scene::ISceneNode*> UnitSelectSceneNodeTapControl::Select( core::array<Event_t*>& events )
{
	core::array<scene::ISceneNode*> selectNodes;
	for (u32 i = 0; i < events.size(); i++)
	{
		Event_t* eventInst = events[i];
		if(eventInst->EventId != EVENT_CONTROL_TAPSCENE) 
			continue;
		ControlTapSceneDesc_t* desc = (ControlTapSceneDesc_t*)eventInst->Params.CommonParam;
		if(desc->Name != TapSceneName) continue;
		if(desc->Node == NULL) continue;

		if((desc->Node->getID() & FilterNodeId) == 0) continue;
		if(!desc->Node->isVisible()) continue;
		selectNodes.push_back(desc->Node);
	}
	return selectNodes;
}
