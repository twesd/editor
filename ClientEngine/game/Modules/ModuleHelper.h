#pragma once
#include "../engine/Core/Base.h"
#include "../engine/IModule.h"
#include "../engine/Stage/UnitManager.h"
#include "../engine/Controls/ControlManager.h"
#include "../engine/Controls/ControlText.h"

class ModuleHelper
{
public:
	static stringc GetLangTextPath(SharedParams_t* sharedParams);

	static bool Is2x(SharedParams_t* sharedParams);

	static bool Is568h( SharedParams_t* sharedParams );

	static void DrawLoading( SharedParams_t* sharedParams );
};
