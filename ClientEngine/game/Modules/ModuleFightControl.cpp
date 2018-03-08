#include "ModuleFightControl.h"
#include "ManagerEvents.h"
#include "Stage/StageManagerEvents.h"
#include "Core/Randomizer.h"

ModuleFightControl::ModuleFightControl(SharedParams_t params) : Base(params)
{
	ClearData();
}

ModuleFightControl::~ModuleFightControl(void)
{
}

irr::core::stringc ModuleFightControl::GetName()
{
	return "ModuleFightControl";
}


void ModuleFightControl::Init()
{
	if (_isInit) return;

	_controlEnergyPlayer = CntrlManager->GetControlByName(stringc("EnergyPlayer"));
	_controlEnergyCPU = CntrlManager->GetControlByName(stringc("EnergyCPU"));

	_endAttackTime = GetNowTime() + 3000;
	_endBlockTime = 0;

	//SndManager->PlayMusic("Music/Music4.mp3", true, true);

	_isInit = true;
}

void ModuleFightControl::Execute()
{
	_DEBUG_BREAK_IF(UManager == NULL)	
	_DEBUG_BREAK_IF(CntrlManager == NULL)	

	if(!_isInit)
	{
		Init();
		return;
	}

	UnitInstanceBase* instanceBase = UManager->GetInstanceByName(stringc("PlayerUnit"));
	UnitInstanceStandard* instancePlayer = dynamic_cast<UnitInstanceStandard*>(instanceBase);
	
	instanceBase = UManager->GetInstanceByName(stringc("CPUUnit"));
	UnitInstanceStandard* instanceCPU = dynamic_cast<UnitInstanceStandard*>(instanceBase);

	if(_completeTime != 0)
	{
		if(GetNowTime() > _completeTime)
		{
			if (instancePlayer == NULL)
			{
				GetEvent()->PostEvent(Event::ID_STAGE_MANAGER, STAGE_MANAGER_EVENT_STOP_UNIT_MANAGER);
				CntrlManager->SetControlPackage(stringc("LooseMenu.controls"));
				Timer->stop();
				return;
			}

			if (instanceCPU == NULL)
			{
				GetEvent()->PostEvent(Event::ID_STAGE_MANAGER, STAGE_MANAGER_EVENT_STOP_UNIT_MANAGER);
				CntrlManager->SetControlPackage(stringc("WinMenu.controls"));
				Timer->stop();
				return;
			}
		}
		return;
	}
	
	
	if (instancePlayer == NULL)
	{
		_completeTime = GetNowTime() + 2000;
		return;
	}
	
	if (instanceCPU == NULL)
	{
		_completeTime = GetNowTime() + 2000;
		return;
	}

	const Parameter* uparam = instancePlayer->GetBehavior()->GetParameter(stringc("Energy"));
	float energy = uparam->GetAsFloat();
	recti bounds = _controlEnergyPlayer->GetBounds();
	int width;
	int height;
	recti clipArea;
	width = (int)((f32)bounds.getWidth() * (energy / 100.0f));	
	clipArea = recti(bounds.UpperLeftCorner, 
		dimension2di(width, bounds.getHeight()));

	_controlEnergyPlayer->SetClip(clipArea);

	uparam = instanceCPU->GetBehavior()->GetParameter(stringc("Energy"));
	energy = uparam->GetAsFloat();
	bounds = _controlEnergyCPU->GetBounds();
	width = (int)((f32)bounds.getWidth() * (energy / 100.0f));	
	clipArea = recti(bounds.UpperLeftCorner, 
		dimension2di(width, bounds.getHeight()));
	_controlEnergyCPU->SetClip(clipArea);

	UnitAction* curActCPU = instanceCPU->GetBehavior()->GetCurrentAction();
	UnitAction* curActPlayer = instancePlayer->GetBehavior()->GetCurrentAction();

	f32 dist = instanceCPU->SceneNode->getPosition().getDistanceFrom(
		instancePlayer->SceneNode->getPosition());

	if(_endAttackTime > 0)
	{
		if(_endAttackTime < GetNowTime())
		{
			_endAttackTime = 0;
			_endBlockTime = GetNowTime() + Randomizer::Rand(3) * 1000;
			return;
		}

		if(dist > 10.0f)
		{	
			if(_gotoTime != 0 && _gotoTime > GetNowTime())
			{
				instanceCPU->GetBehavior()->ApplyAction(stringc("Idle"));
				return;
			}
			instanceCPU->GetBehavior()->ApplyAction(stringc("Forward"));
			_gotoTime = 0;
			return;
		}
		else
		{
			_gotoTime = GetNowTime() + Randomizer::Rand(3) * 1000;
			if(Randomizer::Rand(1) > 0)
			{
				if(curActCPU->Name != "Leg1" && 
					curActCPU->Name != "Leg2" &&
					curActCPU->Name != "Hand1" &&
					curActCPU->Name != "Hand2")
				{
					if(_handCounter == 0)
					{
						instanceCPU->GetBehavior()->ApplyAction(stringc("Hand1"));
					}
					else 
					{
						instanceCPU->GetBehavior()->ApplyAction(stringc("Hand2"));
					}
					_handCounter = !_handCounter;
				}
			}
			else 
			{
				if(curActCPU->Name != "Leg1" && 
					curActCPU->Name != "Leg2" &&
					curActCPU->Name != "Hand1" &&
					curActCPU->Name != "Hand2")
				{
					if(_legCounter == 0)
					{
						instanceCPU->GetBehavior()->ApplyAction(stringc("Leg1"));
					}
					else 
					{
						instanceCPU->GetBehavior()->ApplyAction(stringc("Leg2"));
					}
					_legCounter = !_legCounter;
				}
			}
		}
	} 
	else
	{	
		bool isAttackAction = (
			curActPlayer->Name == "Leg1" || 
			curActPlayer->Name == "Leg2" ||
			curActPlayer->Name == "Leg3" ||
			curActPlayer->Name == "Hand1" ||
			curActPlayer->Name == "Hand2" ||
			curActPlayer->Name == "Hand3");
		if(_endBlockTime < GetNowTime())
		{
			if(!isAttackAction)
			{
				_endBlockTime = 0;
				_endAttackTime = GetNowTime() + 3000;
				return;
			}
		}
		if(!isAttackAction)
		{
			instanceCPU->GetBehavior()->ApplyAction(stringc("Idle"));
		}
		else 
		{
			instanceCPU->GetBehavior()->ApplyAction(stringc("Block"));
		}
	}
}

void ModuleFightControl::ClearData()
{
	IModule::ClearData();
	_controlEnergyCPU = NULL;
	_controlEnergyPlayer = NULL;
	_handCounter = 0;
	_gotoTime = 0;
	_legCounter = 0;
	_isInit = false;
	_completeTime = 0;
}

