#pragma once
#include "../../../Core/Base.h"

class UnitBehavior;

class DataGetterBase : public Base
{
public:
	DataGetterBase(SharedParams_t params);
	virtual ~DataGetterBase(void);	
	
	// Получить данные
	virtual void* GetDataItem(core::array<Event_t*>& events) = 0;

	// Установить ссылку на поведение
	virtual void SetUnitBehavior(UnitBehavior* behavior);
protected:
	// Экземпляр юнита
	UnitBehavior* Behavior;
};
