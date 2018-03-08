#pragma once
#include "UnitEventBase.h"

class UnitEventTimer : public UnitEventBase
{
public:
	UnitEventTimer(SharedParams_t params);
	virtual ~UnitEventTimer(void);

	// Начало выполнения
	virtual void Begin();

	// Выполняется ли условие
	virtual bool IsApprove(core::array<Event_t*>& events);
	
	// Интервал времени
	u32 Interval;

	// Повторять действие
	bool Loop;

	// Наименование таймера, может быть пустым
	stringc TimerName;

	// Использовать таймер доступный во всей стадии
	bool UseStageTimer;

private:

	u32 GetTimerTime();

	// Начальное время
	u32 _startTime;
};

