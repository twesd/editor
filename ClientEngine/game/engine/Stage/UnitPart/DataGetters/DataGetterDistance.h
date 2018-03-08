#pragma once
#include "DataGetterBase.h"
#include "Core/CompareType.h"
#include "Core/NodeWorker.h"

// Получить модель по индентификатору
class DataGetterDistance : public DataGetterBase
{
public:
	DataGetterDistance(SharedParams_t params);
	virtual ~DataGetterDistance(void);	
	
	// Получить данные
	virtual void* GetDataItem(core::array<Event_t*>& events);
	
	// Получить модель по индентификатору
	int FilterNodeId;

	// Расстояние
	f32 Distance;

	// Тип сравнения
	CompareTypeEnum CompareType;

	// Учитывать размеры юнитов
	bool UseUnitSize;
};
