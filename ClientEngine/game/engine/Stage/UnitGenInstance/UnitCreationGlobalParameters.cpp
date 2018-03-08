#include "UnitCreationGlobalParameters.h"
#include "UnitGenInstanceBase.h"

UnitCreationGlobalParameters::UnitCreationGlobalParameters(SharedParams_t params) : UnitCreationBase(params)
{
	
}

UnitCreationGlobalParameters::~UnitCreationGlobalParameters(void)
{
}

// Необходимо ли создать юнит
bool UnitCreationGlobalParameters::NeedCreate()
{
	_DEBUG_BREAK_IF(GenInstance == NULL)
	for (u32 i = 0; i < Parameters.size(); i++)
	{
		Parameter* p = &Parameters[i];
		if(GetGlobalParameter(p->Name) != p->Value)
		{
			return false;
		}
	}
		
	return true;
}

// Можно ли удалить данное условие создания
bool UnitCreationGlobalParameters::CanDispose()
{
	return false;
}

void UnitCreationGlobalParameters::UnitCreated()
{

}
