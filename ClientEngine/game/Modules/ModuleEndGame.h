#pragma once
#include "../engine/Core/Base.h"
#include "../engine/IModule.h"
#include "../engine/Stage/UnitManager.h"
#include "../engine/Controls/ControlManager.h"
#include "../engine/Controls/ControlText.h"

class ModuleEndGame : public IModule, Base
{
public:
	ModuleEndGame(SharedParams_t params);
	virtual ~ModuleEndGame(void);

	stringc GetName();

	void Execute();

	void ClearData();

private:
	void Init();

	bool _isInit;
};
