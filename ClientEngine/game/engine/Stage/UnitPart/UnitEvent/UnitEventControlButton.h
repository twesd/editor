#pragma once
#include "UnitEventBase.h"

class UnitEventControlButton : public UnitEventBase
{
public:
	UnitEventControlButton(SharedParams_t params);
	virtual ~UnitEventControlButton(void);

	// Выполняется ли условие
	virtual bool IsApprove(core::array<Event_t*>& events);
	
	// Состояние кнопки (CONTROL_BTN_STATE)
	int ButtonState;

	// Наименование кнопки
	stringc ButtonName;
};
