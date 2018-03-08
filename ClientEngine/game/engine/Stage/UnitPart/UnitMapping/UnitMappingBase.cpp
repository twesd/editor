#include "UnitMappingBase.h"
#include "../../UnitInstance/UnitInstanceStandard.h"

UnitMappingBase::UnitMappingBase(SharedParams_t params) : Base(params),
	UnitInstance(NULL)
{
}

UnitMappingBase::~UnitMappingBase()
{
}

// Установить экземпляр юнита которому принадлежит поведения
void UnitMappingBase::SetUnitInstance( UnitInstanceBase* unitInstance )
{
	_DEBUG_BREAK_IF(UnitInstance != NULL)
	UnitInstance = unitInstance;
}
