#pragma once
#include "IControl.h"
#include "ControlButton.h"

typedef struct 
{
	stringc Path;
	core::array<IControl*> Controls;
	bool IsDefault;
}ControlPackage;
