#pragma once
#include "../../../Core/Base.h"
//#include "../../UnitInstance/UnitInstanceStandard.h"


class UnitInstanceStandard;
class UnitGenInstanceStandard;
class UnitAction;
class UnitClause;

class ExecuteBase : public Base
{
public:
	ExecuteBase(SharedParams_t params);
	virtual ~ExecuteBase(void);	
	
	// Выполнить действие
	virtual void Run(scene::ISceneNode* node, core::array<Event_t*>& events);

	// Обновление данных
	virtual void Update(scene::ISceneNode* node, core::array<Event_t*>& events);

	// Начало действия
	virtual void Begin();

	// Установить экземпляр юнита, которому принадлежит действие
	virtual void SetUnitInstance(UnitInstanceStandard* unitInstance);

	// Установить действие 
	virtual void SetUnitAction(UnitAction* action);

	// Установить условия для действия
	virtual void SetClause(UnitClause* clause);

	// Выполнялось ли действие
	bool IsRunned;

	//  Может ли выполниться действие
	bool IsCanRun;
protected:
	// Экземпляр юнита
	UnitInstanceStandard* UnitInstance;

	// Действие поведения
	UnitAction* Action;

	// Условия исполнения
	UnitClause* Clause;
};
