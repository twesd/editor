#pragma once
#include "Core/Base.h"

class UnitManager;
class ControlManager;
class SoundManager;
class InAppPurchases;

class IModule
{
public:
	IModule()
	{
		ClearData();
	}

	virtual stringc GetName() = 0;
	
	virtual void SetUnitManager(UnitManager* unitManager)
	{
		UManager = unitManager;
	}

	virtual void SetControlManager(ControlManager* controlManager)
	{
		CntrlManager = controlManager;
	}

	virtual void SetSoundManager(SoundManager* soundManager)
	{
		SndManager = soundManager;
	}

	virtual void SetInAppManager(InAppPurchases* inAppManager)
	{
		InAppManager = inAppManager;
	}

	virtual void ClearData()
	{
		UManager = NULL;
		CntrlManager = NULL;
		SndManager = NULL;
		InAppManager = NULL;
	}

	virtual void Execute() = 0;
	
	virtual ~IModule(void) { };

protected:

	UnitManager* UManager;

	ControlManager* CntrlManager;

	SoundManager* SndManager;

	InAppPurchases* InAppManager;
};
