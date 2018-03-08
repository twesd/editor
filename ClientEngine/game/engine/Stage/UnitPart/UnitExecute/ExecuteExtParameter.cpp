#include "ExecuteExtParameter.h"
#include "../../UnitInstance/UnitInstanceStandard.h"
#include "../../UnitManager.h"
//#include "../UnitSelectSceneNodes/UnitSelectSceneNodeBase.h"

ExecuteExtParameter::ExecuteExtParameter(SharedParams_t params) : ExecuteBase(params),
	_selectors(),_parameters(), BreakOnFirstSuccess(true)
{
}

ExecuteExtParameter::~ExecuteExtParameter()
{
	for (u32 i = 0; i < _selectors.size() ; i++)
	{
		_selectors[i]->drop();
	}
}


// Добавление выборки
// Объект захватывается (grab) в данном методе
void ExecuteExtParameter::AddSelector( UnitSelectSceneNodeBase* selector )
{
	_selectors.push_back(selector);
	selector->grab();
}

// Установить экземпляр юнита, которому принадлежит действие
void ExecuteExtParameter::SetUnitInstance( UnitInstanceStandard* unitInstance )
{
	ExecuteBase::SetUnitInstance(unitInstance);

	for (u32 i = 0; i < _selectors.size() ; i++)
	{
		_selectors[i]->SetUnitInstance(unitInstance);
	}
}

// Добавить параметр
void ExecuteExtParameter::AddParameter( stringc name, stringc val )
{
	Parameter parameter;
	parameter.Name = name;
	parameter.Value = val;
	_parameters.push_back(parameter);
}

// Выполнить действие
void ExecuteExtParameter::Run( scene::ISceneNode* node, core::array<Event_t*>& events )
{
	ExecuteBase::Run(node, events);
	

	// Получаем менеджер юнитов
	UnitManager* unitManager = UnitInstance->GetUnitManager();

	for (u32 i = 0; i < _selectors.size() ; i++)
	{
		core::array<scene::ISceneNode*> nodes = _selectors[i]->Select(events);
		for (u32 indexNode = 0; indexNode < nodes.size() ; indexNode++)
		{
			UnitInstanceBase* instanceBase = unitManager->GetInstanceBySceneNode(nodes[indexNode]);
			UnitInstanceStandard* instanceStandard = dynamic_cast<UnitInstanceStandard*>(instanceBase);
			if (instanceStandard == NULL) continue;
			UnitBehavior* behavior = instanceStandard->GetBehavior();
			for (u32 indexParam = 0; indexParam < _parameters.size() ; indexParam++)
			{
				bool success = behavior->SetParameter(_parameters[indexParam].Name, _parameters[indexParam].Value);
				if (success && BreakOnFirstSuccess)
					return;
			}
		}
	}
}
