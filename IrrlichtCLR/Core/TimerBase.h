#pragma once

#include "Base.h"

class TimerBase
{
public:
	TimerBase(const stringc& name, ITimer* realTimer);

	//! Returns current virtual time in milliseconds.
	/** This value starts with 0 and can be manipulated using setTime(),
	stopTimer(), startTimer(), etc. This value depends on the set speed of
	the timer if the timer is stopped, etc. If you need the system time,
	use getRealTime() */
	u32 GetTime() const;

	//! sets current virtual time
	void SetTime(u32 time);

	//! Stops the virtual timer.
	/** The timer is reference counted, which means everything which calls
	stop() will also have to call start(), otherwise the timer may not
	start/stop correctly again. */
	void Stop();

	//! Starts the virtual timer.
	/** The timer is reference counted, which means everything which calls
	stop() will also have to call start(), otherwise the timer may not
	start/stop correctly again. */
	void Start();

	//! Sets the speed of the timer
	/** The speed is the factor with which the time is running faster or
	slower then the real system time. */
	void SetSpeed(f32 speed = 1.0f);

	//! Returns current speed of the timer
	/** The speed is the factor with which the time is running faster or
	slower then the real system time. */
	f32 SetSpeed() const;

	//! Returns if the virtual timer is currently stopped
	bool IsStopped() const;

	//! Advances the virtual time
	/** Makes the virtual timer update the time value based on the real
	time. This is called automatically when calling IrrlichtDevice::run(),
	but you can call it manually if you don't use this method. */
	void Tick();

	stringc Name;

private:
	void InitVirtualTimer();

	ITimer* _realTimer;

	f32 VirtualTimerSpeed;

	s32 VirtualTimerStopCounter;

	u32 LastVirtualTime;

	u32 StartRealTime;

	u32 StaticTime;
};