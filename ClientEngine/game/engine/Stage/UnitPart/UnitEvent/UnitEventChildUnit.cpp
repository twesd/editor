#include "UnitEventChildUnit.h"
#include "../UnitBehavior.h"
#include "../../UnitInstance/UnitInstanceStandard.h"

UnitEventChildUnit::UnitEventChildUnit(SharedParams_t params) : UnitEventBase(params),
	_root()
{
	Parameters = new UnitParameters(params, false);
}

UnitEventChildUnit::~UnitEventChildUnit(void)
{
	if(Parameters != NULL) Parameters->drop();
}

// Выполняется ли условие
bool UnitEventChildUnit::IsApprove( core::array<Event_t*>& events )
{
	UnitInstanceStandard* UnitInstance = Behavior->GetUnitInstance();
	_DEBUG_BREAK_IF(UnitInstance == NULL)
	UnitInstanceStandard* childInst = UnitInstance->GetChildByPath(_root + ChildPath);
	if(childInst == NULL)
		return false;
	if(Parameters->Count() > 0)
	{
		UnitParameters* childParams = childInst->GetBehavior()->GetParameters();
		for (u32 i = 0; i < Parameters->Count() ; i++)
		{
			Parameter* paramItem = Parameters->GetByIndex(i);
			if(childParams->Get(paramItem->Name) != paramItem->Value)
				return false;
		}
	}

	return true;
}

// Начало выполнения
void UnitEventChildUnit::Begin()
{
	
}

// Установить основную директорию
void UnitEventChildUnit::SetRoot( stringc root )
{
	_root = root;
}

