#include "DataGetterTapControl.h"
#include "../../UnitInstance/UnitInstanceStandard.h"
#include "../../../Controls/ControlEvent.h"

DataGetterTapControl::DataGetterTapControl(SharedParams_t params) : DataGetterBase(params)
{
}

DataGetterTapControl::~DataGetterTapControl()
{
	
}

// Установить тип данных
void DataGetterTapControl::SetGetterType( stringc getterType )
{
	if(getterType == "SceneNode")
	{
		_getterType = SceneNode;
	}
	else if(getterType == "Position")
	{
		_getterType = Position;
	}
	else
	{
		_DEBUG_BREAK_IF(true)	
	}
}

// Получить данные
void* DataGetterTapControl::GetDataItem(  core::array<Event_t*>& events )
{
	for (u32 i = 0; i < events.size(); i++)
	{
		Event_t* eventInst = events[i];
		if(eventInst->EventId != EVENT_CONTROL_TAPSCENE) 
			continue;
		ControlTapSceneDesc_t* desc = (ControlTapSceneDesc_t*)eventInst->Params.CommonParam;
		if(desc->Name != TapSceneName) continue;
		return desc->Node;
	}
	return NULL;	
}

