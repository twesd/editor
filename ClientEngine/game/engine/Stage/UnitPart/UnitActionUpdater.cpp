#include "UnitActionUpdater.h"
#include "../UnitInstance/UnitInstanceStandard.h"
#include "../../Core/Animators/IExtendAnimator.h"
#include "../Paths/PathAnimator.h"

//#define  DEBUG_ACTION_UPDATER

UnitActionUpdater::UnitActionUpdater(SharedParams_t params) : Base(params)
{
	_behavior = NULL;
}

UnitActionUpdater::~UnitActionUpdater(void)
{
	
}

bool UnitActionUpdater::ApplyAction(
	scene::ISceneNode* node, 
	UnitAction* newAction, 
	UnitAction** ppCurrentAction,
	core::array<Event_t*>& events )
{
	UnitAction* currentAction = *ppCurrentAction;
	if(newAction == NULL || newAction == currentAction) 
		return false;
	if(currentAction != NULL && (newAction->Priority < currentAction->Priority)) 
		return false;
	
	UnitBehavior* behavior = _behavior;


#ifdef DEBUG_ACTION_UPDATER
	printf("[UnitActionUpdater(%s)] Behavior[%s] set \n",node->getName(), newAction->Name.c_str());
#endif	

	if(!newAction->NoChangeCurrentAction)
	{
		if(node != NULL)
		{
			// Удаляем аниматоры
			node->removeAnimators();
		}
	}

	newAction->Begin();
	newAction->Execute(node, events);
	if(newAction->HasAnimation())
	{
		behavior->NotifyAnimationStart();
	}
	
	// Если задано не изменять текущее действие, то выходим
	if(newAction->NoChangeCurrentAction)
		return false;

	*ppCurrentAction = newAction;
	return true;
}

// Необходимо ли отменить текущие действие
bool UnitActionUpdater::CurrentActionNeedBreak(UnitAction* currentAction, core::array<Event_t*>& events )
{
	if(currentAction == NULL)
	{
		return false;
	}
	return currentAction->NeedBreak(events, _behavior->GetAnimationEnd(), _behavior->GetSceneNode());
}

void UnitActionUpdater::SetBehavior( UnitBehavior* behavior )
{
	_behavior = behavior;
}
