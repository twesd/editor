#pragma once
#include "../engine/Core/Base.h"
#include "../engine/IModule.h"
#include "../engine/Stage/UnitManager.h"
#include "../engine/Controls/ControlManager.h"
#include "../engine/Controls/ControlText.h"
#include "../engine/Controls/ControlButton.h"
#include "../engine/Controls/ControlImage.h"

class ModuleTutorial1 : public IModule, Base
{
public:
	ModuleTutorial1(SharedParams_t params);
	virtual ~ModuleTutorial1(void);

	stringc GetName();

	void Execute();


	void ClearData();
private:
	void Init();
	
	void ArrowLeftTick(float minV, float maxV);
	
	void ArrowDownTick(float minV, float maxV);

	bool _isInit;

	int _state;
	
	int _completeTime;

	float _xPos;

	float _yPos;

	bool _dirFlag;

	bool _is2x;

	bool _is568h;

	ControlImage* _arrowLeft;

	ControlImage* _arrowDown;

	ControlImage* _bgText;

	ControlImage* _cntrlText;

	bool _stateInit;

	stringc _menuPath;

};
