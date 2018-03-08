#include "UnitEventControlTapScene.h"
#include "../../../Controls/ControlEvent.h"
#include "../UnitBehavior.h"
#include "../../UnitInstance/UnitInstanceStandard.h"

UnitEventControlTapScene::UnitEventControlTapScene(SharedParams_t params) : UnitEventBase(params),
	TapSceneName(), DataName()
{
	IgnoreNode = false;
	IdentNode = true;
	FilterId = -1;
	TapSceneState = BUTTON_STATE_NONE;
}

UnitEventControlTapScene::~UnitEventControlTapScene(void)
{
}

// Выполняется ли условие
bool UnitEventControlTapScene::IsApprove( core::array<Event_t*>& events )
{
	for (u32 i = 0; i < events.size(); i++)
	{
		Event_t* eventInst = events[i];
		if(eventInst->EventId != EVENT_CONTROL_TAPSCENE) 
			continue;
		ControlTapSceneDesc_t* desc = (ControlTapSceneDesc_t*)eventInst->Params.CommonParam;

		if(desc->Name == TapSceneName && desc->State == TapSceneState)
		{
			if (IgnoreNode) return true;			
			if(FilterId != -1)
			{
				if(desc->Node == NULL) return false;
				bool validNode = ((desc->Node->getID() & FilterId) != 0);
				if(!validNode) return false;
			}
			ISceneNode* node = Behavior->GetSceneNode();
			if ((IdentNode && node == desc->Node) || (!IdentNode && node != desc->Node))
			{
				if(DataName != "")
				{
					UnitInstanceStandard* uInst = Behavior->GetUnitInstance();
					_DEBUG_BREAK_IF(uInst == NULL)
					uInst->SetData(DataName, desc->Node);					
				}
				return true;
			}
			return false;
		}
	}
	return false;
}

