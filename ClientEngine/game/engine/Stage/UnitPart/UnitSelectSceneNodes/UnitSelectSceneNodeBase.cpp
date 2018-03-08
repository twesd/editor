#include "UnitSelectSceneNodeBase.h"
#include "../../UnitInstance/UnitInstanceStandard.h"

UnitSelectSceneNodeBase::UnitSelectSceneNodeBase(SharedParams_t params) : Base(params)
{
	UnitInstance = NULL;
	FilterNodeId = -1;
	SelectChilds = false;
}

UnitSelectSceneNodeBase::~UnitSelectSceneNodeBase()
{
}

// Установить экземпляр юнита которому принадлежит поведения
void UnitSelectSceneNodeBase::SetUnitInstance( UnitInstanceBase* unitInstance )
{
	UnitInstance = unitInstance;
}
