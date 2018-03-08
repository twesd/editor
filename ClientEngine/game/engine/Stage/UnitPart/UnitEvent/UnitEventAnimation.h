#pragma once
#include "UnitEventBase.h"

class UnitEventAnimation : public UnitEventBase
{
public:
	UnitEventAnimation(SharedParams_t params);
	virtual ~UnitEventAnimation(void);

	// Выполняется ли условие
	virtual bool IsApprove(core::array<Event_t*>& events);

	// Начало выполнения
	void Begin();

	// Номер кадра
	int FrameNr;

	// Окончание анимации
	bool OnEnd;

private:
	// Начало выполнения действия
	bool _isStarted;
};

