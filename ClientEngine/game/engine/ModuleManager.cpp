#include "ModuleManager.h"

ModuleManager::ModuleManager(SharedParams_t params) : Base(params),
	_modules()
{
	
}

ModuleManager::~ModuleManager(void)
{
	for (u32 i = 0; i < _modules.size() ; i++)
	{
		delete _modules[i];
	}
	_modules.clear();
}

void ModuleManager::AddModule( IModule* module )
{
	_modules.push_back(module);
}

// Выполнить модуль
void ModuleManager::Execute( stringc& name )
{
	for (u32 i = 0; i < _modules.size() ; i++)
	{
		if(_modules[i]->GetName() == name)
		{
			_modules[i]->Execute();
			return;
		}
	}
	_DEBUG_BREAK_IF(true)
}


void ModuleManager::ClearData()
{
	for (u32 i = 0; i < _modules.size() ; i++)
	{
		_modules[i]->ClearData();
	}
}

void ModuleManager::SetUnitManager( UnitManager* unitManager )
{
	for (u32 i = 0; i < _modules.size() ; i++)
	{
		_modules[i]->SetUnitManager(unitManager);
	}
}

void ModuleManager::SetControlManager( ControlManager* controlManager )
{
	for (u32 i = 0; i < _modules.size() ; i++)
	{
		_modules[i]->SetControlManager(controlManager);
	}
}

void ModuleManager::SetSoundManager( SoundManager* soundManager )
{
	for (u32 i = 0; i < _modules.size() ; i++)
	{
		_modules[i]->SetSoundManager(soundManager);
	}
}


void ModuleManager::SetInAppManager(InAppPurchases* inAppManager)
{
	for (u32 i = 0; i < _modules.size() ; i++)
	{
		_modules[i]->SetInAppManager(inAppManager);
	}
}