#include "UnitGenInstanceEmpty.h"


UnitGenInstanceEmpty::UnitGenInstanceEmpty(SharedParams_t params) : UnitGenInstanceBase(params),
	Scale(), NodeId(0)
{
}

UnitGenInstanceEmpty::~UnitGenInstanceEmpty(void)
{	
	
}

// Необходимо ли удалить описание. Случай когда юниты больше создаватья не будут
bool UnitGenInstanceEmpty::CanDispose()
{	
	for (u32 i = 0; i < Creations.size(); i++)
	{
		UnitCreationBase* creation = Creations[i];
		if(!creation->CanDispose())
			return false;
	}
	return true;
}



