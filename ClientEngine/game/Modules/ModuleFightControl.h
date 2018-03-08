#pragma once
#include "../engine/Core/Base.h"
#include "../engine/IModule.h"
#include "../engine/Stage/UnitManager.h"
#include "../engine/Controls/ControlManager.h"
#include "../engine/Controls/ControlText.h"

class ModuleFightControl : public IModule, Base
{
public:
	ModuleFightControl(SharedParams_t params);
	virtual ~ModuleFightControl(void);

	stringc GetName();

	void Execute();

	void ClearData();

private:
	void Init();

	bool _isInit;

	IControl* _controlEnergyPlayer;

	IControl* _controlEnergyCPU;

	u32 _endBlockTime;

	u32 _gotoTime;

	u32 _endAttackTime;

	int _handCounter;

	int _legCounter;

	u32 _completeTime;
};
