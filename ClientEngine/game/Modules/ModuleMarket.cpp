#include "ModuleMarket.h"
#include "Core/TextureWorker.h"
#include "ManagerEvents.h"
#include "Stage/StageManagerEvents.h"
#include "UserSettings/UserSettingsWorker.h"
#include "Controls/ControlImage.h"
#include "ModuleHelper.h"

ModuleMarket::ModuleMarket(SharedParams_t params) : Base(params)
{
	ClearData();

	
}

ModuleMarket::~ModuleMarket(void)
{
}


void ModuleMarket::ClearData()
{
	IModule::ClearData();
	_isInit = false;
	_isFirst = true;
	_completeTime = 0;
	_items.clear();
	_isDone = false;
	_selectedCount = 0;
	_money = 0;

	_startBtn = NULL;
	_startBtnDisable = NULL;
	_moneyText = NULL;
	_buyItem = NULL;
}

irr::core::stringc ModuleMarket::GetName()
{
	return "ModuleMarket";
}

void ModuleMarket::Init()
{
	if (_isInit) return;

	_yBtnSize = 95 * CntrlManager->GetControlsScale();
	_topOffset = 125 * CntrlManager->GetControlsScale();
	_maxCount = 3;

	ControlItem_t* item;

	// force simple enable
	SharedParams.Settings->SetTextSetting("SimpleAvail", "true");
	item = InitItem("Simple");
	item->Price = 0;

	item = InitItem("Riffle");
	item->Price = 100;

	item = InitItem("MachineGun");
	item->Price = 200;

	item = InitItem("Rocket");
	item->Price = 300;

	item = InitItem("Blade");
	item->Price = 400;

	item = InitItem("DoubleGun");
	item->Price = 500;

	item = InitItem("DoubleRocket");
	item->Price = 600;

	item = InitItem("Laser");
	item->Price = 700;

	item = InitItem("FireGun");
	item->Price = 800;

	_startBtn = dynamic_cast<ControlButton*>(CntrlManager->GetControlByName("Continue"));
	_startBtnDisable = CntrlManager->GetControlByName("ContinueDisable");
	_moneyText = dynamic_cast<ControlText*>(CntrlManager->GetControlByName("Money"));

	stringc moneyStr = SharedParams.Settings->GetValue("Money");	
	_money = (s32)core::fast_atof(moneyStr.c_str());
	if(_money <= 0) 
	{
		_money = 0;
	}

	_isInit = true;
}



void ModuleMarket::Execute()
{
	_DEBUG_BREAK_IF(UManager == NULL)	
	_DEBUG_BREAK_IF(CntrlManager == NULL)	

	if (CntrlManager->GetCurrentPackageName() == "Market.controls")
	{
		UpdatePackageMain();
	}
	else if (CntrlManager->GetCurrentPackageName() == "MarketBuyItem.controls")
	{
		UpdatePackageBuyItem();
	}
}

ModuleMarket::ControlItem_t* ModuleMarket::InitItem(stringc cntrlName )
{
	bool isEnabled = (SharedParams.Settings->GetValue(cntrlName + "Avail") == "true");

	IControl* cntrl = CntrlManager->GetControlByName(cntrlName);
	cntrl->IsVisible = true;
	ControlItem_t cntrlDesc; 
	cntrlDesc.Control = cntrl;
	cntrlDesc.StartPos = cntrl->GetPosition();
	cntrlDesc.IsSelected = false;
	cntrlDesc.IsAnimated = false;
	cntrlDesc.SelectedIndex = -1;
	cntrlDesc.IsEnabled = isEnabled;

	if (isEnabled)
	{
		UpdateEnabledItem(cntrlName);

	}
	_items.push_back(cntrlDesc);
	return &_items.getLast();
}

// Загрузка стадии
void ModuleMarket::LoadStage()
{
	// Сохраняем информацию о оружие
	//
	SetGameParameter("Weapon0", "");
	SetGameParameter("Weapon1", "");
	SetGameParameter("Weapon2", "");
	for (u32 iItem = 0; iItem < _items.size() ; iItem++)
	{
		ControlItem_t* item = &_items[iItem];
		if (item->IsSelected)
		{
			stringc paramName = stringc("Weapon") + stringc(item->SelectedIndex);
			SetGameParameter(paramName, item->Control->Name);
		}
	}

	SndManager->StopAll();

	stringc stage;

	stringc mainMenuParameter = GetGlobalParameter("MainMenuStageNr");
	if(mainMenuParameter != "")
	{
		stage = mainMenuParameter;
	}
	else 
	{
		stage = SharedParams.Settings->GetValue("Stage");
	}
	stringc stageNrStr;
	for (u32 iCh = 0; iCh < stage.size() ; iCh++)
	{	
		char c = stage[iCh];		
		if(strchr("0123456789", c) != 0)// Это число
		{
			stageNrStr += c;
		}
	}
	int stageNr = (s32)core::fast_atof(stageNrStr.c_str());
	if(stageNr <= 0) 
	{
		stageNr = 1;
	}

	_isDone = true;

	ModuleHelper::DrawLoading(&SharedParams);

	EventBase->PostEvent(Event::ID_MANAGER, MANAGER_EVENT_AD_SHOW);

	EventParameters_t params;
	params.StrVar = stringc("Stage") + stringc(stageNr) + stringc(".stage");
	EventBase->PostEvent(Event::ID_MANAGER, MANAGER_EVENT_STAGE_LOAD, params);
	return;
}

void ModuleMarket::UpdatePackageMain()
{
	if(_isDone) return;

	if(_isFirst)
	{
		_isFirst = false;
		return;
	}

	if(!_isInit)
	{
		Init();
		return;
	}

	_moneyText->Text = stringc(_money);

	bool updateSeledItems = false;
	s32 removeSelIndex = 0;

	for (u32 iItem = 0; iItem < _items.size() ; iItem++)
	{
		ControlItem_t* item = &_items[iItem];
		if(item->Control->GetControlType() == CONTROL_TYPE_BUTTON)
		{
			UpdateEnabledItem(item->Control->Name);

			ControlButton* cntrlBtn = (ControlButton*)item->Control;
			if (item->IsAnimated)
			{
				position2di curPos = cntrlBtn->GetPosition();
				position2di dir = item->DestPos - curPos;				
				vector2df vec((f32)dir.X, (f32)dir.Y);
				f32 len = vec.getLength();
				if(len < 50)
				{
					item->IsAnimated = false;
					cntrlBtn->SetPosition(item->DestPos);
				}
				else
				{
					vec = vec.normalize() * GetChangeValue(900);				
					position2di nextPos =  curPos + position2di((s32)vec.X, (s32)vec.Y);
					cntrlBtn->SetPosition(nextPos);
				}
			}
			else 
			{
				if(cntrlBtn->GetButtonState() == BUTTON_STATE_UP)
				{
					position2di destPos;
					if(!item->IsEnabled)
					{
						if (item->Price <= _money)
						{
							_buyItem = item;
							CntrlManager->SetControlPackage("MarketBuyItem.controls");
						}
						else
						{
							CntrlManager->SetControlPackage("MarketNotEnoughMoney.controls");
						}
						return;
					}

					if(item->IsSelected)
					{
						updateSeledItems = true;
						removeSelIndex = item->SelectedIndex;

						item->SelectedIndex = -1;
						item->DestPos = item->StartPos;
						item->IsSelected = false;
						item->IsAnimated = true;
						_selectedCount--;
					}
					else
					{
						if (_selectedCount < _maxCount)
						{
							item->DestPos.X = (793 * CntrlManager->GetControlsScale())
								+ (CntrlManager->GetControlsOffset().X);
							item->DestPos.Y = _selectedCount*_yBtnSize+_topOffset;	
							item->IsSelected = true;
							item->SelectedIndex = _selectedCount;
							item->IsAnimated = true;
							_selectedCount++;
						}
					}					
				}
			}
		}
	}

	if(updateSeledItems)
	{
		for (u32 iItem = 0; iItem < _items.size() ; iItem++)
		{
			ControlItem_t* item = &_items[iItem];
			if(item->IsSelected && item->SelectedIndex > removeSelIndex)
			{
				position2di curPos = item->Control->GetPosition();
				item->SelectedIndex--;
				curPos.Y = item->SelectedIndex*_yBtnSize+_topOffset;	
				item->Control->SetPosition(curPos);
			}
		}
	}

	// Получаем количество доступных элементов
	//
	int countEnabledItems = 0;
	for (u32 iItem = 0; iItem < _items.size() ; iItem++)
	{
		ControlItem_t* item = &_items[iItem];
		if(item->IsEnabled)
		{
			countEnabledItems++;
		}
	}

	if (_selectedCount == countEnabledItems || _selectedCount == _maxCount)
	{
		_startBtn->IsVisible = true;
		_startBtnDisable->IsVisible = false;

		if(_startBtn->GetButtonState() == BUTTON_STATE_UP)
		{
			// Загрузка стадии
			LoadStage();
			return;
		}
	}
	else 
	{
		_startBtn->IsVisible = false;
		_startBtnDisable->IsVisible = true;
	}
}

void ModuleMarket::UpdatePackageBuyItem()
{
	ControlButton* btn = dynamic_cast<ControlButton*>(CntrlManager->GetControlByName("Ok"));

	if (btn->GetButtonState() == BUTTON_STATE_UP)
	{
		_money -= _buyItem->Price;
		_buyItem->IsEnabled = true;
		UpdateEnabledItem(_buyItem->Control->Name);
		CntrlManager->SetControlPackage("Market.controls");

		stringc setName = _buyItem->Control->Name + "Avail";
		SharedParams.Settings->SetTextSetting(setName, "true");
		SharedParams.Settings->SetTextSetting("Money", stringc(_money));
		UserSettingsWorker::Save(SharedParams, SharedParams.Settings);
	}
}

void ModuleMarket::UpdateEnabledItem( stringc cntrlName )
{
	bool isEnabled = (SharedParams.Settings->GetValue(cntrlName + "Avail") == "true");
	if(isEnabled)
	{
		IControl* cntrlLock = CntrlManager->GetControlByName(cntrlName + "Lock");
		if (cntrlLock != NULL)
		{
			cntrlLock->IsVisible = false;
		}
	}
}

