#pragma once
#include "UnitEventBase.h"
#include "Core/CompareType.h"
#include "Core/NodeWorker.h"

class UnitEventDistance : public UnitEventBase
{
public:
	UnitEventDistance(SharedParams_t params);
	virtual ~UnitEventDistance(void);

	// Начало выполнения
	virtual void Begin();

	// Выполняется ли условие
	virtual bool IsApprove(core::array<Event_t*>& events);

	// Интервал времени
	f32 Distance;

	// Фильтр моделей
	int FilterNodeId;

	// Тип сравнения
	CompareTypeEnum CompareType;

	// Получение модели из данных
	stringc DataName;

	// Учитывать размеры юнитов
	bool UseUnitSize;
};

