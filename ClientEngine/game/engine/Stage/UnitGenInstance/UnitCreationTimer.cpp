#include "UnitCreationTimer.h"

UnitCreationTimer::UnitCreationTimer(SharedParams_t params) : UnitCreationBase(params)
{
	_creationTime = 0;
	_isFirstItemFire = false;
	_isEnabled = true;
	_disableOnCreate = false;	
	_isUpdateTimesSet = false;
}

UnitCreationTimer::~UnitCreationTimer(void)
{
}

// Необходимо ли создать юнит
bool UnitCreationTimer::NeedCreate()
{
	if(!_isEnabled) 
	{
		return false;
	}

	if(StartTime == 0 && Interval == 0 && EndTime == 0)
	{
		_disableOnCreate = true;
		return true;
	}

	if(UpdateTimesOnFirstCheck && !_isUpdateTimesSet)
	{
		// Обновить StartTime и EndTime в соответствии с текущем времени при первой проверки
		//
		StartTime += GetNowTime();
		if(EndTime != 0)// EndTime = 0 означает, что конечное время бесконечно
		{
			EndTime += GetNowTime();
		}
		_isUpdateTimesSet = true;
	}

	if(_creationTime == 0) 
	{
		_creationTime = StartTime;
	}

	u32 nowTime = GetNowTime();
	if(nowTime > _creationTime && (EndTime == 0 || nowTime < EndTime || !_isFirstItemFire))
	{
		if(Interval != 0)
		{
			_creationTime = nowTime + Interval;
		}
		else
		{
			_isEnabled = false;
		}

		_isFirstItemFire = true;
		return true;
	}
	if(EndTime != 0 && EndTime >= StartTime && nowTime > EndTime)
	{
		_isEnabled = false;
	}
	return false;
}

// Можно ли удалить данное условие создания
bool UnitCreationTimer::CanDispose()
{
	return !_isEnabled;
}

void UnitCreationTimer::UnitCreated()
{
	if(_disableOnCreate)
	{
		_isEnabled = false;
	}
}
