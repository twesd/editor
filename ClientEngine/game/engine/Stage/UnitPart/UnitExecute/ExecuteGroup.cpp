#include "ExecuteGroup.h"
#include "../../UnitInstance/UnitInstanceStandard.h"

ExecuteGroup::ExecuteGroup(SharedParams_t params) : ExecuteBase(params),
	Executes()
{
}

ExecuteGroup::~ExecuteGroup()
{
	for (u32 i = 0; i < Executes.size() ; i++)
	{
		Executes[i]->drop();
	}
	Executes.clear();
}

// Установить экземпляр юнита, которому принадлежит действие
void ExecuteGroup::SetUnitInstance(UnitInstanceStandard* unitInstance)
{
	ExecuteBase::SetUnitInstance(unitInstance);

	for (u32 i = 0; i < Executes.size() ; i++)
	{
		Executes[i]->SetUnitInstance(unitInstance);
	}
}

// Выполнить действие
void ExecuteGroup::Run( scene::ISceneNode* node, core::array<Event_t*>& events )
{
	ExecuteBase::Run(node, events);

	for (u32 i = 0; i < Executes.size() ; i++)
	{
		Executes[i]->Run(node, events);
	}
}
