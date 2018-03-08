#pragma once

#include "../Base.h"

typedef struct 
{
	Base* BaseData;

	Base* UnitInstanceData;// Объект типа UnitInstance*

	Base* ControlManagerData;
	Base* ControlOwnerData;

}ParserUserData;