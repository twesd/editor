#pragma once
#include "UnitEventBase.h"
#include "../../../Controls/ControlEvent.h"

class UnitEventControlCircle : public UnitEventBase
{
public:
	UnitEventControlCircle(SharedParams_t params);
	virtual ~UnitEventControlCircle(void);

	// Выполняется ли условие
	virtual bool IsApprove(core::array<Event_t*>& events);
	
	// Состояния (CONTROL_CIRCLE_STATE)
	core::array<CONTROL_CIRCLE_STATE> States;

	// Наименование контрола
	stringc ControlName;

};
