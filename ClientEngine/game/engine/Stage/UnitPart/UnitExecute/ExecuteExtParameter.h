#pragma once
#include "ExecuteBase.h"
#include "ExtActionDescription.h"
#include "../UnitSelectSceneNodes/UnitSelectSceneNodeBase.h"

class ExecuteExtParameter : public ExecuteBase
{
public:
	ExecuteExtParameter(SharedParams_t params);
	virtual ~ExecuteExtParameter(void);

	// Выполнить действие
	virtual void Run(scene::ISceneNode* node, core::array<Event_t*>& events);

	// Установить экземпляр юнита, которому принадлежит действие
	virtual void SetUnitInstance(UnitInstanceStandard* unitInstance);

	// Добавление выборки
	// Объект захватывается (grab) в данном методе
	void AddSelector(UnitSelectSceneNodeBase* selector);

	// Добавить параметр
	void AddParameter( stringc name, stringc val );

	// Прекратить обработку на первом удачном параметре
	bool BreakOnFirstSuccess;
private:
	core::array<Parameter> _parameters;

	// Выборка
	core::array<UnitSelectSceneNodeBase*> _selectors;
};
