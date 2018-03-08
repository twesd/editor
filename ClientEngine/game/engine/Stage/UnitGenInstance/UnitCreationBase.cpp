#include "UnitCreationBase.h"

UnitCreationBase::UnitCreationBase(SharedParams_t params) : Base(params)
{
	GenInstance = NULL;
}

UnitCreationBase::~UnitCreationBase(void)
{
}

void UnitCreationBase::SetGenInstance( UnitGenInstanceBase* genInstance )
{
	_DEBUG_BREAK_IF(genInstance == NULL)
	GenInstance = genInstance;
}

