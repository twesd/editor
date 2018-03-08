#pragma once
#include "Core/Base.h"
#include "IModule.h"
#include "Stage/UnitManager.h"
#include "Controls/ControlManager.h"
#include "Controls/ControlText.h"
#include "Controls/ControlTapScene.h"

class ModuleTapAndShot : public IModule, Base
{
public:
	ModuleTapAndShot(SharedParams_t params);
	virtual ~ModuleTapAndShot(void);

	stringc GetName();

	void Execute();

	void ClearData();

private:
	void Init();
	
	void Shot(UnitInstanceStandard* player);

	bool _isInit;
	
	u32 _endTime;

	ControlText* _timeText;

	IControl* _controlEnergy;

	ControlTapScene* _tapScene;

	f32 _targetAngle;

	u32 _prevTime;

	bool _needShot;

	u32 _timeForHideMessage;
};
