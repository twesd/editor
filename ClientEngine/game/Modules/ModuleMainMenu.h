#pragma once
#include "../engine/Core/Base.h"
#include "../engine/IModule.h"
#include "../engine/Stage/UnitManager.h"
#include "../engine/Controls/ControlManager.h"
#include "../engine/Controls/ControlText.h"

class ModuleMainMenu : public IModule, Base
{
public:
	ModuleMainMenu(SharedParams_t params);
	virtual ~ModuleMainMenu(void);

	stringc GetName();

	void Execute();

	void ClearData();

private:

	void Init();

	void UpdateLanguage();

	void UpdateMainMenu();

	void UpdateMissionSelect();

	void UpdateSettings();
	
	void UpdateStageSelect(int stageOffset);

	bool _isInit;

	bool _isFirst;

	int _completeTime;

	bool _isDone;
};
