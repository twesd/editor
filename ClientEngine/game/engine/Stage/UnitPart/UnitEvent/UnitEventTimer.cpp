#include "UnitEventTimer.h"
#include "Stage/UnitManager.h"
#include "Stage/UnitInstance/UnitInstanceStandard.h"

UnitEventTimer::UnitEventTimer(SharedParams_t params) : UnitEventBase(params),
	Interval(0), Loop(false),
	_startTime(0)
{
}

UnitEventTimer::~UnitEventTimer(void)
{
}

// Выполняется ли условие
bool UnitEventTimer::IsApprove( core::array<Event_t*>& events )
{
	if (TimerName.size() != 0)
	{
		TimerBase* timer;
		if (UseStageTimer)
		{
			UnitManager* manager = Behavior->GetUnitInstance()->GetUnitManager();
			timer = manager->GetTimerByName(TimerName, false);
		}
		else
		{
			timer = Behavior->GetUnitInstance()->GetTimerByName(TimerName, false);
		}
		
		if (timer == NULL)
		{
			// Если таймера не существует, то считаем, что условие не выполняется
			return false;
		}
	}

	u32 diff = GetTimerTime() - _startTime;
	if (diff > Interval)
	{
		if(Loop) 
		{
			_startTime = GetTimerTime();
		}

		return true;
	}
	return false;
}

// Начало выполнения
void UnitEventTimer::Begin()
{
	_startTime = GetTimerTime();
}

u32 UnitEventTimer::GetTimerTime()
{
	if (TimerName.size() == 0)
	{
		return GetNowTime();
	}

	TimerBase* timer;
	if (UseStageTimer)
	{
		UnitManager* manager = Behavior->GetUnitInstance()->GetUnitManager();
		timer = manager->GetTimerByName(TimerName, false);
	}
	else
	{
		timer = Behavior->GetUnitInstance()->GetTimerByName(TimerName, false);
	}
	if (timer == NULL)
	{
		return GetNowTime();
	}
	return timer->GetTime();
}

