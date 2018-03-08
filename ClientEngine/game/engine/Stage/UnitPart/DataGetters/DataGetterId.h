#pragma once
#include "DataGetterBase.h"

class UnitInstanceStandard;

// Получить модель по индентификатору
class DataGetterId : public DataGetterBase
{
public:
	DataGetterId(SharedParams_t params);
	virtual ~DataGetterId(void);	
	
	// Получить данные
	virtual void* GetDataItem(core::array<Event_t*>& events);
	
	// Получить модель по индентификатору
	int FilterNodeId;

};
