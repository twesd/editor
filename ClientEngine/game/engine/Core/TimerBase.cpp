#include "TimerBase.h"

TimerBase::TimerBase(const stringc& name, ITimer* realTimer)
{
	_DEBUG_BREAK_IF(name.size() == 0)

	Name = name;
	_realTimer = realTimer;
	VirtualTimerSpeed = 1.0f;
	VirtualTimerStopCounter = 0;
	LastVirtualTime = 0;
	StartRealTime = 0;
	StaticTime = 0;

	InitVirtualTimer();
}

//! returns current virtual time
u32 TimerBase::GetTime() const
{
	if (IsStopped())
		return LastVirtualTime;

	return LastVirtualTime + (u32)((StaticTime - StartRealTime) * VirtualTimerSpeed);
}

//! ticks, advances the virtual timer
void TimerBase::Tick()
{
	StaticTime = _realTimer->getRealTime();
}

//! sets the current virtual time
void TimerBase::SetTime(u32 time)
{
	StaticTime = _realTimer->getRealTime();
	LastVirtualTime = time;
	StartRealTime = StaticTime;
}

//! stops the virtual timer
void TimerBase::Stop()
{
	if (!IsStopped())
	{
		// stop the virtual timer
		LastVirtualTime = GetTime();
	}
}

//! starts the virtual timer
void TimerBase::Start()
{
	if (!IsStopped())
	{
		// restart virtual timer
		SetTime(LastVirtualTime);
	}
}

//! sets the speed of the virtual timer
void TimerBase::SetSpeed(f32 speed)
{
	SetTime(GetTime());

	VirtualTimerSpeed = speed;
	if (VirtualTimerSpeed < 0.0f)
		VirtualTimerSpeed = 0.0f;
}

//! gets the speed of the virtual timer
f32 TimerBase::SetSpeed() const
{
	return VirtualTimerSpeed;
}

//! returns if the timer currently is stopped
bool TimerBase::IsStopped() const
{
	return VirtualTimerStopCounter != 0;
}

void TimerBase::InitVirtualTimer()
{
	StaticTime = _realTimer->getRealTime();
	StartRealTime = StaticTime;
}