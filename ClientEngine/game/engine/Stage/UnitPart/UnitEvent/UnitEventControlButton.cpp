#include "UnitEventControlButton.h"
#include "../../../Controls/ControlEvent.h"

UnitEventControlButton::UnitEventControlButton(SharedParams_t params) : UnitEventBase(params),
	ButtonName(), ButtonState(0)
{
}

UnitEventControlButton::~UnitEventControlButton(void)
{
}

// Выполняется ли условие
bool UnitEventControlButton::IsApprove( core::array<Event_t*>& events )
{
	for (u32 i = 0; i < events.size(); i++)
	{
		Event_t* eventInst = events[i];
		if(eventInst->EventId != EVENT_CONTROL_BUTTON) 
			continue;
		ControlButtonDesc_t* buttonDesc = (ControlButtonDesc_t*)eventInst->Params.CommonParam;
		if(buttonDesc->Name == ButtonName && buttonDesc->BtnState == ButtonState)
			return true;
	}
	return false;
}

