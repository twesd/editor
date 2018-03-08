#include "ModuleStandardControl.h"
#include "../engine/Controls/ControlButton.h"
#include "../engine/Controls/ControlImage.h"
#include "../engine/Controls/ControlText.h"
#include "../engine/ManagerEvents.h"
#include "../engine/Stage/UnitManager.h"
#include "../engine/Stage/StageManagerEvents.h"
#include "../engine/UserSettings/UserSettingsWorker.h"
#include "Core/GeometryWorker.h"
#include "Controls/ControlButton.h"
#include "Controls/ControlText.h"
#include "ModuleHelper.h"

ModuleStandardControl::ModuleStandardControl(SharedParams_t params) : Base(params)
{
	ClearData();
}

irr::core::stringc ModuleStandardControl::GetName()
{
	return "ModuleStandardControl";
}

void ModuleStandardControl::Execute()
{
	_DEBUG_BREAK_IF(CntrlManager == NULL)

	stringc cntrlName;
	
	if(!_isInit)
	{
		Init();
		return;
	}
	
	if(_completeTime != 0)
	{
		if(GetNowTime() > _completeTime)
		{
			if(GetGlobalParameter("PlayerWin")=="true")
			{
				GetEvent()->PostEvent(Event::ID_MANAGER, MANAGER_EVENT_STAGE_COMPLETE);
				Timer->stop();
			}
			else
			{
				cntrlName = stringc("LooseMenu.controls");
				CntrlManager->SetControlPackage(cntrlName);
				Timer->stop();
			}
		}
		return;
	}

	// Check end of stage
	if(GetGlobalParameter("StageComplete")=="true")
	{
		_completeTime = GetNowTime();		
		return;
	}


	UnitInstanceBase* instanceBaseDown = UManager->GetInstanceByName(stringc("PlayerDown"));
	UnitInstanceStandard* playerInstance = dynamic_cast<UnitInstanceStandard*>(instanceBaseDown);
	if (playerInstance == NULL)
	{
		_completeTime = GetNowTime() + 1000;
		return;
	}	
	UpdateLifeIndicator(playerInstance);


	UnitInstanceStandard* playerInstanceUp = NULL;
	core::array<UnitInstanceBase*> childs = instanceBaseDown->GetChilds();
	for (u32 iCh = 0; iCh < childs.size(); iCh++)
	{
		UnitInstanceStandard* chItem = dynamic_cast<UnitInstanceStandard*>(childs[iCh]);
		if (chItem->GetBehaviorPath().find("PlayerUp.behavior") >= 0)
		{
			playerInstanceUp = chItem;
			break;;
		}
	}

	if(playerInstanceUp != NULL)
	{
		UpdateWeapons(playerInstanceUp);

		UpdatePlayerGun(playerInstance, playerInstanceUp);
	}
}

void ModuleStandardControl::ClearData()
{
	IModule::ClearData();
	_isInit = false;
	_isFirstWeponSet = false;
	_completeTime = 0;
	_isPlayerInGun = false;
	_playerGun = NULL;
	_weaponCurrent = NULL;
	_weapons.clear();
}

void ModuleStandardControl::Init()
{
	// For Test
	//SetGameParameter("Weapon0", "Simple");
	//SetGameParameter("Weapon1", "Riffle");
	//SetGameParameter("Weapon0", "Laser");
	//SetGameParameter("Weapon2", "Rocket");
	//SetGameParameter("Weapon1", "Riffle");
	//SetGameParameter("Weapon2", "DoubleGun");
	//SetGameParameter("Weapon2", "DoubleRocket");
	//SetGameParameter("Weapon2", "FireGun");

	_controlEnergy = CntrlManager->GetControlByName(stringc("Energy"));

	stringc weapon0 = GetGameParameter("Weapon0");
	InitWeaponItem(weapon0, 0);

	stringc weapon1 = GetGameParameter("Weapon1");
	InitWeaponItem(weapon1, 1);

	stringc weapon2 = GetGameParameter("Weapon2");
	InitWeaponItem(weapon2, 2);

	_isInit = true;

	
}

// Иницилизация оружия
void ModuleStandardControl::InitWeaponItem(const stringc& weaponStr, const int slotNr )
{
	if (weaponStr.size() == 0)
	{
		return;
	}

	Weapon_t weapon;
	weapon.Button = new ControlButton(SharedParams, SndManager);
	weapon.Button->Name = weaponStr;
	stringc textureNormal = stringc("Menu/Weapons/") + weaponStr + stringc(".png");
	stringc textureActivate = stringc("Menu/Weapons/") + weaponStr + stringc("Activate.png");

	position2di pos = position2di(805, (slotNr*95) + 12);
	f32 controlsScale = CntrlManager->GetControlsScale();
	position2di controlsOffset = CntrlManager->GetControlsOffset();
	pos.X = (s32)(controlsScale * pos.X) + controlsOffset.X;
	pos.Y = (s32)(controlsScale * pos.Y) + controlsOffset.Y;
	weapon.Button->Init(textureNormal, textureActivate, pos, 300);
	CntrlManager->AddControl(weapon.Button);
	weapon.IsSelected = false;
	weapon.SlotNr = slotNr;

	_weapons.push_back(weapon);
}


void ModuleStandardControl::UpdateLifeIndicator( UnitInstanceStandard* playerInstance)
{
	const Parameter* uparam = playerInstance->GetBehavior()->GetParameter(stringc("Energy"));
	float energy = uparam->GetAsFloat();
	recti bounds = _controlEnergy->GetBounds();

	int width = (int)((f32)bounds.getWidth() * (energy / 100.0f));	
	recti clipArea = recti(bounds.UpperLeftCorner, dimension2di(width, bounds.getHeight()));
	_controlEnergy->SetClip(clipArea);
}

void ModuleStandardControl::UpdateWeapons(UnitInstanceStandard* playerInstanceUp)
{
	if(!_isFirstWeponSet && _weapons.size() > 0)
	{
		SetWeapon(playerInstanceUp, &_weapons[0]);
		_isFirstWeponSet = true;
		return;
	}

	for (u32 i = 0; i < _weapons.size(); i++)
	{
		Weapon_t* wp = &_weapons[i];
		if (wp->Button->GetButtonState() == BUTTON_STATE_DOWN)
		{
			SetWeapon(playerInstanceUp, wp);
		}
	}
}

void ModuleStandardControl::SetWeapon( UnitInstanceStandard* playerInstanceUp, Weapon_t* wpForSet )
{
	for (u32 i = 0; i < _weapons.size(); i++)
	{
		Weapon_t* wpItem = &_weapons[i];
		wpItem->IsSelected = false;
		_weaponCurrent = wpItem;
	}
	playerInstanceUp->GetBehavior()->SetParameter(wpForSet->Button->Name + "Create", "true");
	wpForSet->IsSelected = true;
}

void ModuleStandardControl::UpdatePlayerGun( 
	UnitInstanceStandard* playerInstance, UnitInstanceStandard* playerInstanceUp )
{

	if (_isPlayerInGun)
	{
		vector3df rot = _playerGun->SceneNode->getRotation();

		GeometryWorker::NormalizeRotation(rot);
		playerInstance->SceneNode->setRotation(rot);
		playerInstanceUp->SceneNode->setRotation(vector3df(0, 0, 0));

		vector3df gunPos = _playerGun->SceneNode->getAbsolutePosition();

		vector3df playerPos(0, 2, 3);
		playerPos.rotateXZBy(-rot.Y);

		playerInstance->SceneNode->setPosition(gunPos + playerPos);	

		ControlButton* btn = dynamic_cast<ControlButton*>(CntrlManager->GetControlByName("ExitFromGun"));
		if (btn->GetButtonState() == BUTTON_STATE_UP)
		{
			CntrlManager->GetControlByName("ExitFromGun")->IsVisible = false;
			CntrlManager->GetControlByName("TextExitFromGun")->IsVisible = false;
			playerInstance->GetBehavior()->SetParameter("Stop","false");
			playerInstanceUp->GetBehavior()->SetParameter("Stop","false");
			playerInstance->SceneNode->setPosition(gunPos + vector3df(5, 0, 0));

			_playerGun->GetBehavior()->SetParameter("NeedPlayerOut", "true");
			_isPlayerInGun = false;
			SetWeapon(playerInstanceUp, _weaponCurrent);
		}
		return;
	}
	else 
	{
		if(GetGlobalParameter("NeedSetPlayerToGun") != "true")
		{
			return;
		}

		SetGlobalParameter("NeedSetPlayerToGun", "false");

		_playerGun = NULL;
		f32 minDistToGun = 0;
		vector3df playerPos = playerInstance->SceneNode->getAbsolutePosition();
		core::array<UnitInstanceBase*> instances = UManager->GetInstances();
		for (u32 iInst = 0; iInst < instances.size(); iInst++)
		{
			UnitInstanceBase* instance = instances[iInst];
			if (instance->Name.find("PlayerGun") >= 0)
			{
				vector3df pos = instance->SceneNode->getAbsolutePosition();
				f32 distToGun = playerPos.getDistanceFrom(pos);
				if (_playerGun == NULL || distToGun < minDistToGun)
				{
					_playerGun = dynamic_cast<UnitInstanceStandard*>(instance);
					minDistToGun = distToGun;
				}
			}
		}
		if (_playerGun == NULL)
		{
			_DEBUG_BREAK_IF(true)
			return;
		}

		_isPlayerInGun = true;

		playerInstance->GetBehavior()->SetParameter("Stop","true");
		playerInstanceUp->GetBehavior()->SetParameter("Stop","true");

		CntrlManager->GetControlByName("ExitFromGun")->IsVisible = true;
		CntrlManager->GetControlByName("TextExitFromGun")->IsVisible = true;
	}

}


