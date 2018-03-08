#include "ModuleTranslate.h"
#include "../engine/ManagerEvents.h"
#include "../engine/Stage/StageManagerEvents.h"
#include "../engine/UserSettings/UserSettingsWorker.h"
#include "../engine/Controls/ControlImage.h"
#include "ModuleHelper.h"

ModuleTranslate::ModuleTranslate(SharedParams_t params) : Base(params)
{
	ClearData();
}

ModuleTranslate::~ModuleTranslate(void)
{
}

irr::core::stringc ModuleTranslate::GetName()
{
	return "ModuleTranslate";
}

void ModuleTranslate::Init()
{
	if (_isInit) return;


	stringc path = ModuleHelper::GetLangTextPath(&SharedParams);
	ControlImage* cntrl = NULL;

	cntrl = dynamic_cast<ControlImage*>(CntrlManager->GetControlByName("TextNext"));
	if(cntrl != NULL)
	{
		cntrl->Init(path + stringc("Next.png"), cntrl->GetPosition());
	}

	cntrl = dynamic_cast<ControlImage*>(CntrlManager->GetControlByName("TextNewUnit"));
	if(cntrl != NULL)
	{
		cntrl->Init(path + stringc("BonusNewUnit.png"), cntrl->GetPosition());
	}
	cntrl = dynamic_cast<ControlImage*>(CntrlManager->GetControlByName("TextNewAbility"));
	if(cntrl != NULL)
	{
		cntrl->Init(path + stringc("BonusNewAbility.png"), cntrl->GetPosition());
	}
	cntrl = dynamic_cast<ControlImage*>(CntrlManager->GetControlByName("TextFinal"));
	if(cntrl != NULL)
	{
		cntrl->Init(path + stringc("BonusFinal.png"), cntrl->GetPosition());
	}

	cntrl = dynamic_cast<ControlImage*>(CntrlManager->GetControlByName("TextTime"));
	if(cntrl != NULL)
	{
		cntrl->Init(path + stringc("Time.png"), cntrl->GetPosition());
	}
	cntrl = dynamic_cast<ControlImage*>(CntrlManager->GetControlByName("TextResources"));
	if(cntrl != NULL)
	{
		cntrl->Init(path + stringc("Resources.png"), cntrl->GetPosition());
	}

	if(CntrlManager->GetCurrentPackageName() == "BonusRocketMan.controls")
	{		
		cntrl = dynamic_cast<ControlImage*>(CntrlManager->GetControlByName("TextDescRocketMan"));
		cntrl->Init(path + stringc("BonusDescRocketMan.png"), cntrl->GetPosition());
	}
	else if(CntrlManager->GetCurrentPackageName() == "BonusAirDefense.controls")
	{		
		cntrl = dynamic_cast<ControlImage*>(CntrlManager->GetControlByName("TextDescAirDefense"));
		cntrl->Init(path + stringc("BonusDescAirDefense.png"), cntrl->GetPosition());
	}
	else if(CntrlManager->GetCurrentPackageName() == "BonusAirSoldier.controls")
	{		
		cntrl = dynamic_cast<ControlImage*>(CntrlManager->GetControlByName("TextDescAirSoldier"));
		cntrl->Init(path + stringc("BonusDescAirSoldier.png"), cntrl->GetPosition());
	}
	else if(CntrlManager->GetCurrentPackageName() == "BonusBombDefense.controls")
	{		
		cntrl = dynamic_cast<ControlImage*>(CntrlManager->GetControlByName("TextDescBombDefense"));
		cntrl->Init(path + stringc("BonusDescBombDefense.png"), cntrl->GetPosition());
	}
	else if(CntrlManager->GetCurrentPackageName() == "BonusBoomS.controls")
	{		
		cntrl = dynamic_cast<ControlImage*>(CntrlManager->GetControlByName("TextDescBoomS"));
		cntrl->Init(path + stringc("BonusDescBoomS.png"), cntrl->GetPosition());
	}
	else if(CntrlManager->GetCurrentPackageName() == "BonusBunker.controls")
	{		
		cntrl = dynamic_cast<ControlImage*>(CntrlManager->GetControlByName("TextDescBunker"));
		cntrl->Init(path + stringc("BonusDescBunker.png"), cntrl->GetPosition());
	}
	else if(CntrlManager->GetCurrentPackageName() == "BonusCar.controls")
	{		
		cntrl = dynamic_cast<ControlImage*>(CntrlManager->GetControlByName("TextDescCar"));
		cntrl->Init(path + stringc("BonusDescCar.png"), cntrl->GetPosition());
	}
	else if(CntrlManager->GetCurrentPackageName() == "BonusCarRocket.controls")
	{		
		cntrl = dynamic_cast<ControlImage*>(CntrlManager->GetControlByName("TextDescCarRocket"));
		cntrl->Init(path + stringc("BonusDescCarRocket.png"), cntrl->GetPosition());
	}
	else if(CntrlManager->GetCurrentPackageName() == "BonusCenterDefense.controls")
	{		
		cntrl = dynamic_cast<ControlImage*>(CntrlManager->GetControlByName("TextDescCenterDefense"));
		cntrl->Init(path + stringc("BonusDescCenterDefense.png"), cntrl->GetPosition());
	}
	else if(CntrlManager->GetCurrentPackageName() == "BonusDefense.controls")
	{		
		cntrl = dynamic_cast<ControlImage*>(CntrlManager->GetControlByName("TextDescDefense"));
		cntrl->Init(path + stringc("BonusDescDefense.png"), cntrl->GetPosition());
	}
	else if(CntrlManager->GetCurrentPackageName() == "BonusDetect.controls")
	{		
		cntrl = dynamic_cast<ControlImage*>(CntrlManager->GetControlByName("TextDescDetect"));
		cntrl->Init(path + stringc("BonusDescDetect.png"), cntrl->GetPosition());
	}
	else if(CntrlManager->GetCurrentPackageName() == "BonusGun.controls")
	{		
		cntrl = dynamic_cast<ControlImage*>(CntrlManager->GetControlByName("TextDescGun"));
		cntrl->Init(path + stringc("BonusDescGun.png"), cntrl->GetPosition());
	}
	else if(CntrlManager->GetCurrentPackageName() == "BonusGun2.controls")
	{		
		cntrl = dynamic_cast<ControlImage*>(CntrlManager->GetControlByName("TextDescGun2"));
		cntrl->Init(path + stringc("BonusDescGun2.png"), cntrl->GetPosition());
	}
	else if(CntrlManager->GetCurrentPackageName() == "Bonus.controls")
	{		
		cntrl = dynamic_cast<ControlImage*>(CntrlManager->GetControlByName("TextDesc"));
		cntrl->Init(path + stringc("BonusDesc.png"), cntrl->GetPosition());
	}
	else if(CntrlManager->GetCurrentPackageName() == "BonusPlane.controls")
	{		
		cntrl = dynamic_cast<ControlImage*>(CntrlManager->GetControlByName("TextDescPlane"));
		cntrl->Init(path + stringc("BonusDescPlane.png"), cntrl->GetPosition());
	}
	else if(CntrlManager->GetCurrentPackageName() == "BonusPlaneShield.controls")
	{		
		cntrl = dynamic_cast<ControlImage*>(CntrlManager->GetControlByName("TextDescPlaneShield"));
		cntrl->Init(path + stringc("BonusDescPlaneShield.png"), cntrl->GetPosition());
	}
	else if(CntrlManager->GetCurrentPackageName() == "BonusTextDescRocketManAirAtack.controls")
	{		
		cntrl = dynamic_cast<ControlImage*>(CntrlManager->GetControlByName("TextDescTextDescRocketManAirAttack"));
		cntrl->Init(path + stringc("BonusDescTextDescRocketManAirAttack.png"), cntrl->GetPosition());
	}
	else if(CntrlManager->GetCurrentPackageName() == "TutorialArcade.controls")
	{		
		cntrl = dynamic_cast<ControlImage*>(CntrlManager->GetControlByName("TextMove"));
		cntrl->Init(path + stringc("Move.png"), cntrl->GetPosition());
		cntrl = dynamic_cast<ControlImage*>(CntrlManager->GetControlByName("TextAttack"));
		cntrl->Init(path + stringc("Attack.png"), cntrl->GetPosition());
	}
	else if(CntrlManager->GetCurrentPackageName() == "EndGame.controls")
	{		
		cntrl = dynamic_cast<ControlImage*>(CntrlManager->GetControlByName("TextYouBest"));
		cntrl->Init(path + stringc("YouBest.png"), cntrl->GetPosition());
	}

	_isInit = true;
}


void ModuleTranslate::ClearData()
{
	IModule::ClearData();
	_isInit = false;

}


void ModuleTranslate::Execute()
{
	_DEBUG_BREAK_IF(UManager == NULL)	
	_DEBUG_BREAK_IF(CntrlManager == NULL)	


	if(!_isInit)
	{
		Init();
		return;
	}

}

