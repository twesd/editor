#include "UnitEventOperator.h"
#include "../UnitBehavior.h"

UnitEventOperator::UnitEventOperator(SharedParams_t params) : UnitEventBase(params)
{
}

UnitEventOperator::~UnitEventOperator(void)
{
}

// Выполняется ли условие
bool UnitEventOperator::IsApprove( core::array<Event_t*>& events )
{
	return true;
}

