#include "ModuleTutorial1.h"
#include "../engine/ManagerEvents.h"
#include "../engine/Stage/StageManagerEvents.h"
#include "ModuleHelper.h"


ModuleTutorial1::ModuleTutorial1(SharedParams_t params) : Base(params)
{
	ClearData();

	_is2x = ModuleHelper::Is2x(&params);
	if(ModuleHelper::Is568h(&params))
	{
		_menuPath = "Menu568h/";
	}
	else if (ModuleHelper::Is2x(&params))
	{
		_menuPath = "Menu2x/";
	}
	else 
	{
		_menuPath = "Menu/";
	}
	
	_is568h = ModuleHelper::Is568h(&params);
}

ModuleTutorial1::~ModuleTutorial1(void)
{
}

irr::core::stringc ModuleTutorial1::GetName()
{
	return "ModuleTutorial1";
}


void ModuleTutorial1::Init()
{
	if (_isInit) return;

	_stateInit = false;

	stringc showingBuild = GetGlobalParameter("ShowHintBuilding");
	stringc bombVal = GetGlobalParameter("ShowHumanBomb");
	if(showingBuild == "true" || bombVal == "true")
	{
		_state = 3;
		_stateInit = false;
	}

	_isInit = true;
}

void ModuleTutorial1::Execute()
{
	_DEBUG_BREAK_IF(UManager == NULL)	
	_DEBUG_BREAK_IF(CntrlManager == NULL)	

	if(!_isInit)
	{
		Init();
		return;
	}
	
	ISceneCollisionManager* collisionManager = SceneManager->getSceneCollisionManager();

	// Ожидаем standard.controls
	if(CntrlManager->GetCurrentPackageName() == "Standard.controls")
	{
		IControl* cntrl;
		
		
		stringc textPath = ModuleHelper::GetLangTextPath(&SharedParams);

		if(_state == 0)
		{
			position2di pos;

			_state = 1;
			
			_bgText = new ControlImage(SharedParams);
			_bgText->Name = "BgText";
			pos = position2di(0,245);
			if (_is2x)
			{
				pos.X *= 2;
				pos.Y *= 2;
			}
			_bgText->Init(_menuPath + stringc("Tutorials/BgText.png"), pos);
			CntrlManager->AddControl(_bgText);

			_cntrlText = new ControlImage(SharedParams);
			CntrlManager->AddControl(_cntrlText);

			pos = position2di(30,260);
			if (_is2x)
			{
				pos.X *= 2;
				pos.Y *= 2;
			}			
			_cntrlText->Init(textPath + stringc("TutorialCreateBuild.png"), pos);

			cntrl = CntrlManager->GetControlByName(stringc("Mineral"));
			pos = cntrl->GetPosition();
			pos.X += 50;
			pos.Y -= 10;
			if (_is2x)
			{
				pos.X += 50;
				pos.Y -= 10;
			}			
			_xPos = (f32)pos.X;

			_arrowLeft = new ControlImage(SharedParams);
			_arrowLeft->Name = "ArrowLeft";
			_arrowLeft->Init(_menuPath + stringc("Tutorials/ArrowLeft.png"), pos);
			CntrlManager->AddControl(_arrowLeft);
	

			cntrl = CntrlManager->GetControlByName(stringc("Soldier"));
			cntrl->IsVisible = false;

			cntrl = CntrlManager->GetControlByName(stringc("SoldierDisable"));
			cntrl->IsVisible = true;

		}
		else if (_state == 1)
		{
			if (_is2x)
			{
				ArrowLeftTick(50 * 2, 70 * 2);
			}
			else
			{
				ArrowLeftTick(50, 70);
			}

			cntrl = CntrlManager->GetControlByName(stringc("Soldier"));
			cntrl->IsVisible = false;
			cntrl = CntrlManager->GetControlByName(stringc("SoldierDisable"));
			cntrl->IsVisible = true;


			if(GetGlobalParameter("CreateUnitMineral") == "true")
			{
				_state = 2;
				_stateInit = false;
			}
		}
		else if (_state == 2)
		{
			if(!_stateInit)
			{
				_arrowLeft->IsVisible = false;

				position2di posM(60, 130);
				if (_is2x)
				{
					posM.X *= 2;
					posM.Y *= 2;
				}

				_arrowDown = new ControlImage(SharedParams);
				_arrowDown->Name = "ArrowDown";
				_arrowDown->Init(_menuPath + stringc("Tutorials/ArrowDown.png"), posM);
				CntrlManager->AddControl(_arrowDown);

				_yPos = (f32)posM.Y;

				_stateInit = true;
			}

			cntrl = CntrlManager->GetControlByName(stringc("Soldier"));
			cntrl->IsVisible = false;
			cntrl = CntrlManager->GetControlByName(stringc("SoldierDisable"));
			cntrl->IsVisible = true;

			if (_is2x)
			{
				ArrowDownTick(130 * 2, 150 * 2);
			}
			else
			{
				ArrowDownTick(130, 150);
			}

			core::array<UnitInstanceBase*> instances = UManager->GetInstances();
			int instCount = 0;
			for (u32 i = 0; i < instances.size() ; i++)
			{
				if((instances[i]->SceneNode != NULL) && (instances[i]->SceneNode->getID() & 258))
					instCount++;
			}
			if (instCount > 0)
			{
				_state = 3;
				_stateInit = false;

				SetGlobalParameter("ShowHintBuilding", "true");
				CntrlManager->SetControlPackage("TutorialMineralHint.controls");
				Timer->stop();
			}
		}
		else if (_state == 3)
		{
			// Create soldier Button
			cntrl = CntrlManager->GetControlByName(stringc("Mineral"));
			cntrl->IsVisible = false;
			cntrl = CntrlManager->GetControlByName(stringc("MineralDisable"));
			cntrl->IsVisible = true;
			position2di pos;

			cntrl = CntrlManager->GetControlByName(stringc("Soldier"));
			cntrl->IsVisible = true;

			if(!_stateInit)
			{
				_arrowDown->IsVisible = false;
				IControl* cntrl = CntrlManager->GetControlByName(stringc("Soldier"));
				position2di posS = cntrl->GetPosition();
				posS.X += 50;
				posS.Y -= 10;
				_xPos = (f32)posS.X;
				_arrowLeft->IsVisible = true;
				_arrowLeft->SetPosition(posS);

				pos = position2di(30,260);
				if (_is2x)
				{
					pos.X *= 2;
					pos.Y *= 2;
				}
				_cntrlText->Init(textPath + stringc("TutorialCreateSoldier.png"), pos);
				_stateInit = true;
			}

			if (_is2x)
			{
				ArrowLeftTick(60 * 2, 80 * 2);
			}
			else
			{
				ArrowLeftTick(60, 80);
			}
			if(GetGlobalParameter("CreateUnitSoldier") == "true")
			{
				_state = 4;
				_stateInit = false;
			}
		}
		else if (_state == 4)
		{
			// Create soldier Place

			cntrl = CntrlManager->GetControlByName(stringc("Mineral"));
			cntrl->IsVisible = false;
			cntrl = CntrlManager->GetControlByName(stringc("MineralDisable"));
			cntrl->IsVisible = true;

			if(!_stateInit)
			{
				_arrowLeft->IsVisible = false;

				position2di posM(140, 130);
				if (_is2x)
				{
					posM.X *= 2;
					posM.Y *= 2;
				}
				_arrowDown->IsVisible = true;
				_arrowDown->SetPosition(posM);

				_yPos = (f32)posM.Y;
				_stateInit = true;
			}

			if (_is2x)
			{
				ArrowDownTick(130 * 2, 150 * 2);
			}
			else
			{
				ArrowDownTick(130, 150);
			}

			core::array<UnitInstanceBase*> instances = UManager->GetInstances();
			int instCount = 0;
			for (u32 i = 0; i < instances.size() ; i++)
			{
				if((instances[i]->SceneNode != NULL) && (instances[i]->SceneNode->getID() & 4))
					instCount++;
			}
			if (instCount > 0)
			{
				_state = 5;
				_stateInit = false;
			}
		}
		else if (_state == 5)
		{
			// Выбор солдата
			//

			cntrl = CntrlManager->GetControlByName(stringc("Mineral"));
			cntrl->IsVisible = false;
			cntrl = CntrlManager->GetControlByName(stringc("MineralDisable"));
			cntrl->IsVisible = true;

			cntrl = CntrlManager->GetControlByName(stringc("Soldier"));
			cntrl->IsVisible = false;
			cntrl = CntrlManager->GetControlByName(stringc("SoldierDisable"));
			cntrl->IsVisible = true;

			if(!_stateInit)
			{
				position2di pos(30,260);
				if (_is2x)
				{
					pos.X *= 2;
					pos.Y *= 2;
				}
				_cntrlText->Init(textPath + stringc("TutorialSelectSoldier.png"), pos);
				_arrowDown->IsVisible = false;
				_arrowLeft->IsVisible = false;

				_stateInit = true;
			}


			core::array<UnitInstanceBase*> instances = UManager->GetInstances();
			UnitInstanceStandard* sInst;
			int instCount = 0;
			for (u32 i = 0; i < instances.size() ; i++)
			{
				if((instances[i]->SceneNode != NULL) && (instances[i]->SceneNode->getID() & 4))
				{
					instCount++;
					sInst = dynamic_cast<UnitInstanceStandard*>(instances[i]);
				}
			}
			if (instCount > 0)
			{
				vector3df sPos = sInst->SceneNode->getPosition();
				position2di pos1 = collisionManager->getScreenCoordinatesFrom3DPosition(sPos);
				
				pos1.X -= 40;
				pos1.Y -= 80;

				if(_arrowDown->IsVisible == false)
				{
					_arrowDown->SetPosition(pos1);
					_arrowDown->IsVisible = true;
				}

				ArrowDownTick(pos1.Y - 20, pos1.Y);		
				const Parameter* pp = sInst->GetBehavior()->GetParameter("Selected");
				if(pp != NULL && pp->Value == "true")
				{
					_state = 7;
					_stateInit = false;
					_arrowDown->IsVisible = false;
				}
			}
			else 
			{
				_state = 3;
				_stateInit = false;
			}
		}
		else if (_state == 7)
		{
			// Атаковать центр
			//

			cntrl = CntrlManager->GetControlByName(stringc("Mineral"));
			cntrl->IsVisible = false;
			cntrl = CntrlManager->GetControlByName(stringc("MineralDisable"));
			cntrl->IsVisible = true;

			cntrl = CntrlManager->GetControlByName(stringc("Soldier"));
			cntrl->IsVisible = false;
			cntrl = CntrlManager->GetControlByName(stringc("SoldierDisable"));
			cntrl->IsVisible = true;

			if(!_stateInit)
			{
				position2di pos;
				pos = position2di(30,260);
				if (_is2x)
				{
					pos.X *= 2;
					pos.Y *= 2;
				}				
				_cntrlText->Init(textPath + stringc("TutorialAttackCenter.png"), pos);
				_arrowDown->IsVisible = true;
				pos = position2di(390, 0);
				if (_is2x)
				{
					pos.X *= 2;
					pos.Y *= 2;
				}
				if (_is568h)
				{
					pos.X += 88;
				}
				

				_arrowDown->SetPosition(pos);
				_arrowLeft->IsVisible = false;
				_stateInit = true;
			}

			if (_is2x)
			{
				ArrowDownTick(0 * 2, 15 * 2);
			}
			else
			{
				ArrowDownTick(0, 15);
			}
			

			core::array<UnitInstanceBase*> instances = UManager->GetInstances();
			int instCount = 0;
			for (u32 i = 0; i < instances.size() ; i++)
			{
				if((instances[i]->SceneNode != NULL) && (instances[i]->SceneNode->getID() & 4))
					instCount++;
			}
			if (instCount == 0)
			{
				stringc bombVal = GetGlobalParameter("ShowHumanBomb");
				if (bombVal != "true")
				{
					SetGlobalParameter("ShowHumanBomb", "true");
					CntrlManager->SetControlPackage("TutorialHumanBomb.controls");
					Timer->stop();
				}				
				_state = 3;
				_stateInit = false;
			}
		}
	}
}

void ModuleTutorial1::ClearData()
{
	IModule::ClearData();
	_isInit = false;
	_state = 0;
	_completeTime = 0;
	_arrowLeft = NULL;
	_arrowDown = NULL;
	_xPos = 0;
	_dirFlag = true;
	_bgText = NULL;
	_cntrlText = NULL;
}

void ModuleTutorial1::ArrowLeftTick(float minV, float maxV)
{
	position2di pos = _arrowLeft->GetPosition();
	if(_dirFlag)
	{
		_xPos -= GetChangeValue(50);
		if(_xPos < minV)
		{
			_xPos = minV;
			_dirFlag = false;
		}
	}
	else
	{
		_xPos += GetChangeValue(50);
		if(_xPos > maxV)
		{
			_xPos = maxV;
			_dirFlag = true;
		}
	}

	if(_xPos < minV) _xPos = minV;
	if(_xPos > maxV) _xPos = maxV;
	pos.X = (s32)_xPos;
	
	_arrowLeft->SetPosition(pos);
}

void ModuleTutorial1::ArrowDownTick(float minV, float maxV)
{
	position2di pos = _arrowDown->GetPosition();
	if(_dirFlag)
	{
		_yPos -= GetChangeValue(50);
		if(_yPos < minV)
		{
			_yPos = minV;
			_dirFlag = false;
		}
	}
	else
	{
		_yPos += GetChangeValue(50);
		if(_yPos > maxV)
		{
			_yPos = maxV;
			_dirFlag = true;
		}
	}
	if(_yPos < minV) _yPos = minV;
	if(_yPos > maxV) _yPos = maxV;
	pos.Y = (s32)_yPos;
	_arrowDown->SetPosition(pos);
}

