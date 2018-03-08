#include "ExecuteDeleteSelf.h"
#include "../../UnitInstance/UnitInstanceStandard.h"

ExecuteDeleteSelf::ExecuteDeleteSelf(SharedParams_t params) : ExecuteBase(params)
{
}

ExecuteDeleteSelf::~ExecuteDeleteSelf()
{
}

// Выполнить действие
void ExecuteDeleteSelf::Run( scene::ISceneNode* node, core::array<Event_t*>& events )
{
	ExecuteBase::Run(node, events);

	UnitInstance->Erase();
}
