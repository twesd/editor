#pragma once
#include "ExecuteBase.h"
#include "ExtActionDescription.h"
#include "../UnitSelectSceneNodes/UnitSelectSceneNodeBase.h"

class ExecuteExtAction : public ExecuteBase
{
public:
	ExecuteExtAction(SharedParams_t params);
	virtual ~ExecuteExtAction(void);

	// Выполнить действие
	virtual void Run(scene::ISceneNode* node, core::array<Event_t*>& events);

	// Установить экземпляр юнита, которому принадлежит действие
	virtual void SetUnitInstance(UnitInstanceStandard* unitInstance);

	// Описание внешнего действия
	core::array<ExtActionDescription> ExtActionDescriptions;

	// Добавление выборки
	// Объект захватывается (grab) в данном методе
	void AddSelector(UnitSelectSceneNodeBase* selector);

private:
	// Выборка
	core::array<UnitSelectSceneNodeBase*> _selectors;
};
