#include "UnitEventControlCircle.h"
#include "../UnitBehavior.h"
#include "../../UnitInstance/UnitInstanceStandard.h"

UnitEventControlCircle::UnitEventControlCircle(SharedParams_t params) : UnitEventBase(params),
	ControlName()
{
	
}

UnitEventControlCircle::~UnitEventControlCircle(void)
{
}

// Выполняется ли условие
bool UnitEventControlCircle::IsApprove( core::array<Event_t*>& events )
{
	for (u32 i = 0; i < events.size(); i++)
	{
		Event_t* eventInst = events[i];
		if(eventInst->EventId != EVENT_CONTROL_CIRCLE) 
			continue;
		ControlCircleDesc_t* desc = (ControlCircleDesc_t*)eventInst->Params.CommonParam;
		
		if(desc->Name == ControlName)
		{
			bool stateSuccess =false;
			for (u32 iState = 0; iState < States.size() ; iState++)
			{
				if (States[iState] == desc->State)
				{
					stateSuccess = true;
					break;
				}
			}			
			return stateSuccess;
		}
	}
	return false;
}

