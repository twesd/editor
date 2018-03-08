#include "UnitEventActionEnd.h"
#include "../UnitBehavior.h"
#include "../UnitBlockAction.h"
#include "../UnitActionUpdater.h"

UnitEventActionEnd::UnitEventActionEnd(SharedParams_t params) : UnitEventBase(params)
{
	_actionUpdater = new UnitActionUpdater(SharedParams);
}

UnitEventActionEnd::~UnitEventActionEnd(void)
{
	delete _actionUpdater;
}

// Выполняется ли условие
bool UnitEventActionEnd::IsApprove( core::array<Event_t*>& events )
{
	UnitAction* action = Behavior->GetCurrentAction();
	if(action == NULL)
	{		
		return false;
	}

	UnitBlockAction* blockAction = dynamic_cast<UnitBlockAction*>(action);
	if(blockAction != NULL)
	{
		UnitAction* chAction = blockAction->GetCurrentAction();
		if(chAction == NULL)
		{			
			return false;
		}
		return _actionUpdater->CurrentActionNeedBreak(chAction, events);
	}
	else 
	{
		return _actionUpdater->CurrentActionNeedBreak(action, events);
	}	
}

void UnitEventActionEnd::SetBehavior( UnitBehavior* behavior )
{
	UnitEventBase::SetBehavior(behavior);
	
	_actionUpdater->SetBehavior(behavior);
}

