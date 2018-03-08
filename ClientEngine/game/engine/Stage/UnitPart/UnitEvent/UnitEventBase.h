#pragma once
#include "Core/Base.h"
#include "Core/OperatorType.h"
#include "UnitEventType.h"

class UnitBehavior;

class UnitEventBase : public Base
{
public:
	UnitEventBase(SharedParams_t params);
	virtual ~UnitEventBase(void);

	virtual UnitEventTypeEnum GetEventType()
	{
		return UNIT_EVENT_TYPE_UNKNOWN;
	}

	// Начало выполнения
	virtual void Begin();

	// Выполняется ли условие
	virtual bool IsApprove(core::array<Event_t*>& events) = 0;
	
	// Установить поведение, которому принадлежит условия
	// Установка ссылки на объект
	virtual void SetBehavior(UnitBehavior* behavior);
protected:
	// Экземпляр поведений, которому принадлежит условия
	UnitBehavior* Behavior;
};
