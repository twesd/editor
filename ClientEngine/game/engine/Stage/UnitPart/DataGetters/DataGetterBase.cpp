#include "DataGetterBase.h"
#include "../../UnitInstance/UnitInstanceStandard.h"


DataGetterBase::DataGetterBase(SharedParams_t params) : Base(params),
	Behavior(NULL)
{
}

DataGetterBase::~DataGetterBase()
{
	
}

// Установить ссылку на поведение
void DataGetterBase::SetUnitBehavior( UnitBehavior* behavior )
{
	_DEBUG_BREAK_IF(behavior == NULL)
	Behavior = behavior;
}

