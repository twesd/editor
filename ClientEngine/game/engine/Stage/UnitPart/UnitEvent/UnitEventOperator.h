#pragma once
#include "UnitEventBase.h"
#include "Core/OperatorType.h"


class UnitEventOperator : public UnitEventBase
{
public:
	UnitEventOperator(SharedParams_t params);
	virtual ~UnitEventOperator(void);

	virtual UnitEventTypeEnum GetEventType()
	{
		return UNIT_EVENT_TYPE_OPERATOR;
	}

	// Выполняется ли условие
	virtual bool IsApprove(core::array<Event_t*>& events);

	OperatorTypeEnum Operator;
};

