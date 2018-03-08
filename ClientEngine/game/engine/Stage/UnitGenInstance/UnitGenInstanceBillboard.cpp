#include "UnitGenInstanceBillboard.h"


UnitGenInstanceBillboard::UnitGenInstanceBillboard(SharedParams_t params) : UnitGenInstanceBase(params)
{
}

UnitGenInstanceBillboard::~UnitGenInstanceBillboard(void)
{	
}

// Необходимо ли удалить описание. Случай когда юниты больше создаватья не будут
bool UnitGenInstanceBillboard::CanDispose()
{	
	for (u32 i = 0; i < Creations.size(); i++)
	{
		UnitCreationBase* creation = Creations[i];
		if(!creation->CanDispose())
		{
			return false;
		}
	}
	return true;
}

