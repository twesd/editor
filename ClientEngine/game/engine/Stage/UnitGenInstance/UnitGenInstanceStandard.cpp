#include "UnitGenInstanceStandard.h"



UnitGenInstanceStandard::UnitGenInstanceStandard(SharedParams_t params) : UnitGenInstanceBase(params),
	PathBehavior(), StartScriptFileName()
{
}

UnitGenInstanceStandard::~UnitGenInstanceStandard(void)
{	
}

// Необходимо ли удалить описание. Случай когда юниты больше создаватья не будут
bool UnitGenInstanceStandard::CanDispose()
{
	for (u32 i = 0; i < Creations.size(); i++)
	{
		UnitCreationBase* creation = Creations[i];
		if(!creation->CanDispose())
			return false;
	}
	return true;
}

