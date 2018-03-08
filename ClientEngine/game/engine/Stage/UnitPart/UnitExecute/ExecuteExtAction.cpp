#include "ExecuteExtAction.h"
#include "../../UnitInstance/UnitInstanceStandard.h"
#include "../../UnitManager.h"
//#include "../UnitSelectSceneNodes/UnitSelectSceneNodeBase.h"

ExecuteExtAction::ExecuteExtAction(SharedParams_t params) : ExecuteBase(params),
	ExtActionDescriptions(),_selectors()
{
}

ExecuteExtAction::~ExecuteExtAction()
{
	for (u32 i = 0; i < _selectors.size() ; i++)
	{
		_selectors[i]->drop();
	}
}


// Добавление выборки
// Объект захватывается (grab) в данном методе
void ExecuteExtAction::AddSelector( UnitSelectSceneNodeBase* selector )
{
	_selectors.push_back(selector);
	selector->grab();
}

// Установить экземпляр юнита, которому принадлежит действие
void ExecuteExtAction::SetUnitInstance( UnitInstanceStandard* unitInstance )
{
	ExecuteBase::SetUnitInstance(unitInstance);

	for (u32 i = 0; i < _selectors.size() ; i++)
	{
		_selectors[i]->SetUnitInstance(unitInstance);
	}
}

// Выполнить действие
void ExecuteExtAction::Run( scene::ISceneNode* node, core::array<Event_t*>& events )
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
			for (u32 indexDesc = 0; indexDesc < ExtActionDescriptions.size() ; indexDesc++)
			{
				// Если действие получилось применить, то прерываем цикл
				if(behavior->ApplyAction(ExtActionDescriptions[indexDesc].ActionName))
					break;
			}			
		}
	}
}
