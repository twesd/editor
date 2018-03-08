#pragma once
#include "ExecuteBase.h"

class UnitBehavior;

class ExecuteParameter : public ExecuteBase
{
public:
	ExecuteParameter(SharedParams_t params);
	virtual ~ExecuteParameter(void);

	// Добавить параметр
	void AddParameter( stringc name, stringc val );

	// Выполнить действие
	virtual void Run(scene::ISceneNode* node, core::array<Event_t*>& events);

	// Являются ли параметры глобальными
	bool IsGlobal;

	// Устаноить поведение, которому принадлежит данное выполнение
	void SetBehavior(UnitBehavior* behavior);
private:

	core::array<Parameter> _parameters;

	// Поведение, которому принадлежит данное выполнение
	UnitBehavior* _behavior;
};
