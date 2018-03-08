#include "ExecuteDeleteUnit.h"
#include "../../UnitInstance/UnitInstanceStandard.h"

ExecuteDeleteUnit::ExecuteDeleteUnit(SharedParams_t params) : 
	ExecuteBase(params), BehaviorsPath(),
	_root()
{
}

ExecuteDeleteUnit::~ExecuteDeleteUnit()
{

}

// Выполнить действие
void ExecuteDeleteUnit::Run(scene::ISceneNode* node, core::array<Event_t*>& events)
{
	ExecuteBase::Run(node, events);

	_DEBUG_BREAK_IF(UnitInstance == NULL)
	UnitInstance->DeleteChild(_root + BehaviorsPath);
}

// Установить основную директорию
void ExecuteDeleteUnit::SetRoot( stringc root )
{
	_root = root;
}
