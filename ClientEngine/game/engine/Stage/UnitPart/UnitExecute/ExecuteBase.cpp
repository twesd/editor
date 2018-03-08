#include "ExecuteBase.h"
#include "../../UnitInstance/UnitInstanceStandard.h"
#include "../UnitAction.h"

ExecuteBase::ExecuteBase(SharedParams_t params) : Base(params),
	UnitInstance(NULL), Action(NULL), Clause(NULL),
	IsCanRun(true), IsRunned(false)
{
}

ExecuteBase::~ExecuteBase()
{
	if (Clause != NULL)
	{
		Clause->drop();
	}
}

// Установить экземпляр юнита которому принадлежит поведения
void ExecuteBase::SetUnitInstance( UnitInstanceStandard* unitInstance )
{
	_DEBUG_BREAK_IF(UnitInstance != NULL)
	UnitInstance = unitInstance;
}

// Установить действие
void ExecuteBase::SetUnitAction( UnitAction* action )
{
	_DEBUG_BREAK_IF(Action != NULL)
	Action = action;
}

// Установить условия для действия
void ExecuteBase::SetClause( UnitClause* clause )
{
	_DEBUG_BREAK_IF(Clause != NULL)
	Clause = clause;
}

// Обновление данных
void ExecuteBase::Update( scene::ISceneNode* node, core::array<Event_t*>& events )
{
	if(Clause != NULL) 
	{
		Clause->Update(node, events);

		IsCanRun = Clause->IsApprove(events);
	}
	else
	{
		IsCanRun = true;
	}
}

// Начало действия
void ExecuteBase::Begin()
{
	if(Clause != NULL) 
		Clause->Begin();
	IsCanRun = true;
	IsRunned = false;
}

// Выполнить действие
void ExecuteBase::Run( scene::ISceneNode* node, core::array<Event_t*>& events )
{
	IsRunned = true;
}
