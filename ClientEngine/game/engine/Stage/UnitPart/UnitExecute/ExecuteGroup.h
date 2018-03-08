#pragma once
#include "ExecuteBase.h"

class ExecuteGroup : public ExecuteBase
{
public:
	ExecuteGroup(SharedParams_t params);
	virtual ~ExecuteGroup(void);

	// Выполнить действие
	virtual void Run(scene::ISceneNode* node, core::array<Event_t*>& events);

	// Установить экземпляр юнита, которому принадлежит действие
	virtual void SetUnitInstance(UnitInstanceStandard* unitInstance);

	// Наименование след. действия
	core::array<ExecuteBase*> Executes;
};
