#pragma once
#include "Core/Base.h"
#include "IModule.h"

class ModuleManager : public Base
{
public:
	ModuleManager(SharedParams_t params);
	~ModuleManager(void);

	void AddModule(IModule* module);

	// Выполнить модуль
	void Execute(stringc& name);

	// Очистить данные модуля
	void ClearData();

	void SetUnitManager(UnitManager* unitManager);

	void SetControlManager(ControlManager* controlManager);

	void SetSoundManager( SoundManager*  soundManager );

	void SetInAppManager(InAppPurchases* inAppManager);
private:
	core::array<IModule*> _modules;
};
