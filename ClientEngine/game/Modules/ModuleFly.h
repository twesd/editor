#pragma once
#include "Core/Base.h"
#include "IModule.h"
#include "Stage/UnitManager.h"
#include "Controls/ControlManager.h"
#include "Controls/ControlText.h"

class ModuleFly : public IModule, Base
{
public:
	ModuleFly(SharedParams_t params);
	virtual ~ModuleFly(void);

	stringc GetName();

	void Execute();

	void ClearData();

private:
	void Init();
	
	bool _isInit;
	
	u32 _endTime;

	ControlText* _timeText;

	IControl* _controlEnergy;

	f32 _targetAngle;

	u32 _prevTime;

	bool _needShot;

};
