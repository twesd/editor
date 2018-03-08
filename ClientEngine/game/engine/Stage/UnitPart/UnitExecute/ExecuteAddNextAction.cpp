#include "ExecuteAddNextAction.h"
#include "../../UnitInstance/UnitInstanceStandard.h"

ExecuteAddNextAction::ExecuteAddNextAction(SharedParams_t params) : ExecuteBase(params),
	ActionName()
{
}

ExecuteAddNextAction::~ExecuteAddNextAction()
{
}

// Выполнить действие
void ExecuteAddNextAction::Run( scene::ISceneNode* node, core::array<Event_t*>& events )
{
	ExecuteBase::Run(node, events);

	UnitBehavior* behavior = UnitInstance->GetBehavior();
	behavior->AddCandidateNextAction(ActionName);
}
