#include "ExecuteTimer.h"
#include "Stage/UnitManager.h"
#include "Stage/UnitInstance/UnitInstanceStandard.h"

ExecuteTimer::ExecuteTimer(SharedParams_t params) : ExecuteBase(params)
{
}

ExecuteTimer::~ExecuteTimer()
{
}

// Выполнить действие
void ExecuteTimer::Run( scene::ISceneNode* node, core::array<Event_t*>& events )
{
	ExecuteBase::Run(node, events);

	TimerBase* timer;
	if (UseStageTimer)
	{
		UnitManager* manager = UnitInstance->GetUnitManager();
		timer = manager->GetTimerByName(TimerName, true);
	}
	else
	{
		timer = UnitInstance->GetTimerByName(TimerName, true);
	}
	if (timer == NULL)
	{
		return;
	}

	if (SetTime)
	{
		timer->SetTime(Time);
	}
	if (StopTimer)
	{
		timer->Stop();
	}
	if (StartTimer)
	{
		timer->Start();
	}
}
