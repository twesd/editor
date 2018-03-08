#pragma once
#include "UnitCreationBase.h"

class UnitCreationTimer : public UnitCreationBase
{
public:
	UnitCreationTimer(SharedParams_t params);
	virtual ~UnitCreationTimer(void);
	
	// Необходимо ли создать юнит
	bool NeedCreate();

	// Можно ли удалить данное условие создания
	bool CanDispose();

	// Юнит создан, все условия выполнены
	void UnitCreated();

	// Начальное время создания
	u32 StartTime;
	
	// Конечное время создания
	u32 EndTime;

	// Интервал
	u32 Interval;

	// Обновить StartTime и EndTime в соответствии с текущем времени при первой проверки
	bool UpdateTimesOnFirstCheck;

private:
	// Флаг обновления время, если UpdateTimesOnFirstCheck выставлен в true
	bool _isUpdateTimesSet;

	// Время в которое надо создать юнит
	u32 _creationTime;

	// Был ли создан первый элемент
	bool _isFirstItemFire;

	// Включен ли таймер
	bool _isEnabled;

	// После создания юнита завершить
	bool _disableOnCreate;
};
