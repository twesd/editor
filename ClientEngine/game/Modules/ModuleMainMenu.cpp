#include "ModuleMainMenu.h"
#include "Core/TextureWorker.h"
#include "ManagerEvents.h"
#include "Stage/StageManagerEvents.h"
#include "UserSettings/UserSettingsWorker.h"
#include "Controls/ControlImage.h"
#include "ModuleHelper.h"

ModuleMainMenu::ModuleMainMenu(SharedParams_t params) : Base(params)
{
	ClearData();
}

ModuleMainMenu::~ModuleMainMenu(void)
{
}

irr::core::stringc ModuleMainMenu::GetName()
{
	return "ModuleMainMenu";
}

void ModuleMainMenu::Init()
{
	if (_isInit) return;

	UserSettingsWorker::Load(SharedParams, SharedParams.Settings);
	stringc sndEnabled = SharedParams.Settings->GetValue(stringc("SoundEnable"));
	stringc musikEnabled = SharedParams.Settings->GetValue(stringc("MusicEnable"));
	SndManager->SetEnableSound(sndEnabled == "true");
	SndManager->SetEnableMusic(musikEnabled == "true");

	ISound* snd = SndManager->PlayMusic("Units/Music/MusicMenu.mp3", true, true);
	if(snd != NULL)
		snd->SetVolume(0.3f);

	stringc test;
	test = "D";
	test += "E";
	test += "M";
	test += "O ";

	UpdateLanguage();
	test += "NE";
	test += "NAS";

	ControlText* cntrl = new ControlText(SharedParams);
	CntrlManager->AddControl(cntrl);
	test += "HEV";
	cntrl->Text = test;
	cntrl->Init("Font");
	cntrl->SetPosition(position2di(270, 520));		 

	_isInit = true;
}


void ModuleMainMenu::ClearData()
{
	IModule::ClearData();
	_isInit = false;
	_isFirst = true;
	_completeTime = 0;
	_isDone = false;
}


void ModuleMainMenu::Execute()
{
	_DEBUG_BREAK_IF(UManager == NULL)	
	_DEBUG_BREAK_IF(CntrlManager == NULL)	

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

	// Обновляем камеру
	ICameraSceneNode* camera = SceneManager->getActiveCamera();	
	vector3df camPos = camera->getPosition();
	vector3df camTarget = camera->getTarget();
	vector3df upVec = camera->getUpVector();
	vector3df offset = camera->getAbsolutePosition() - camera->getTarget();


	matrix4 m1;
	m1.setRotationDegrees(vector3df(0, GetChangeValue(10), 0));
	m1.transformVect(offset);

	camera->setPosition(camTarget + offset); 

	UpdateLanguage();

	if(CntrlManager->GetCurrentPackageName() == "MainMenu.controls")
	{
		UpdateMainMenu();
	} 
	else if(CntrlManager->GetCurrentPackageName() == "Settings.controls")
	{
		UpdateSettings();
	}
	else if(CntrlManager->GetCurrentPackageName() == "StageSelection.controls")
	{
		UpdateStageSelect(0);
	}
}

void ModuleMainMenu::UpdateSettings()
{
	ControlButton* soundOn = dynamic_cast<ControlButton *>(CntrlManager->GetControlByName(stringc("SoundOn")));
	ControlImage* soundOnText = dynamic_cast<ControlImage *>(CntrlManager->GetControlByName(stringc("TextSoundOn")));
	if(soundOn != NULL)
	{
		ControlImage* soundOffText = dynamic_cast<ControlImage *>(CntrlManager->GetControlByName(stringc("TextSoundOff")));
		ControlButton* soundOff = dynamic_cast<ControlButton *>(CntrlManager->GetControlByName(stringc("SoundOff")));
		ControlImage* musikOnText = dynamic_cast<ControlImage *>(CntrlManager->GetControlByName(stringc("TextMusicOn")));
		ControlButton* musikOn = dynamic_cast<ControlButton *>(CntrlManager->GetControlByName(stringc("MusicOn")));
		ControlImage* musikOffText = dynamic_cast<ControlImage *>(CntrlManager->GetControlByName(stringc("TextMusicOff")));
		ControlButton* musikOff = dynamic_cast<ControlButton *>(CntrlManager->GetControlByName(stringc("MusicOff")));
		ControlButton* backBtn = dynamic_cast<ControlButton *>(CntrlManager->GetControlByName(stringc("Back")));
		if(backBtn->GetButtonState() == BUTTON_STATE_UP)
		{
			CntrlManager->SetControlPackage(stringc("MainMenu.controls"));
		}

		stringc sndEnabled = SharedParams.Settings->GetValue(stringc("SoundEnable"));
		stringc musikEnabled = SharedParams.Settings->GetValue(stringc("MusicEnable"));
		if(sndEnabled == "true")
		{
			soundOff->IsVisible = false;
			soundOffText->IsVisible = false;
			soundOn->IsVisible = true;
			soundOnText->IsVisible = true;
		}
		else 
		{
			soundOff->IsVisible = true;
			soundOffText->IsVisible = true;
			soundOn->IsVisible = false;
			soundOnText->IsVisible = false;
		}

		if(musikEnabled == "true")
		{
			musikOff->IsVisible = false;
			musikOffText->IsVisible = false;
			musikOn->IsVisible = true;
			musikOnText->IsVisible = true;
		}
		else 
		{
			musikOff->IsVisible = true;
			musikOffText->IsVisible = true;
			musikOn->IsVisible = false;
			musikOnText->IsVisible = false;
		}


		if(soundOff->IsVisible && soundOff->GetButtonState() == BUTTON_STATE_UP)
		{
			soundOff->IsVisible = false;
			soundOn->IsVisible = true;
			SharedParams.Settings->SetTextSetting(stringc("SoundEnable"), "true");
			UserSettingsWorker::Save(SharedParams, SharedParams.Settings);
			SndManager->SetEnableSound(true);
		}
		else if(soundOn->IsVisible && soundOn->GetButtonState() == BUTTON_STATE_UP)
		{
			soundOff->IsVisible = true;
			soundOn->IsVisible = false;
			SharedParams.Settings->SetTextSetting(stringc("SoundEnable"), "false");
			UserSettingsWorker::Save(SharedParams, SharedParams.Settings);
			SndManager->SetEnableSound(false);
		}
		if(musikOff->IsVisible && musikOff->GetButtonState() == BUTTON_STATE_UP)
		{
			musikOff->IsVisible = false;
			musikOn->IsVisible = true;
			SharedParams.Settings->SetTextSetting(stringc("MusicEnable"), "true");
			UserSettingsWorker::Save(SharedParams, SharedParams.Settings);
			SndManager->SetEnableMusic(true);
			SndManager->PlayMusic("Units/Music/MusicMenu.mp3", true, true);
		}
		else if(musikOn->IsVisible && musikOn->GetButtonState() == BUTTON_STATE_UP)
		{
			musikOff->IsVisible = true;
			musikOn->IsVisible = false;
			SharedParams.Settings->SetTextSetting(stringc("MusicEnable"), "false");
			UserSettingsWorker::Save(SharedParams, SharedParams.Settings);
			SndManager->StopAll();
			SndManager->SetEnableMusic(false);
		}
	}
}

void ModuleMainMenu::UpdateMissionSelect()
{
	ControlButton* earth = dynamic_cast<ControlButton *>(CntrlManager->GetControlByName(stringc("Earth")));
	ControlButton* animals = dynamic_cast<ControlButton *>(CntrlManager->GetControlByName(stringc("Animals")));
	ControlButton* magic = dynamic_cast<ControlButton *>(CntrlManager->GetControlByName(stringc("Magic")));
	ControlButton* back = dynamic_cast<ControlButton *>(CntrlManager->GetControlByName(stringc("Back")));
	ControlImage* lockAnimals = dynamic_cast<ControlImage *>(CntrlManager->GetControlByName(stringc("LockAnimals")));
	ControlImage* lockMagic = dynamic_cast<ControlImage *>(CntrlManager->GetControlByName(stringc("LockMagic")));
	bool animalsAvail = (SharedParams.Settings->GetValue("MissionAnimals") == "true");
	bool magicAvail = (SharedParams.Settings->GetValue("MissionMagic") == "true");
	if(animalsAvail)
	{
		lockAnimals->IsVisible = false;
	}
	if(magicAvail)
	{
		lockMagic->IsVisible = false;
	}
	if(earth->GetButtonState() == BUTTON_STATE_UP)
	{
		CntrlManager->SetControlPackage(stringc("StageSelectEarth.controls"));
	}
	else if(animals->GetButtonState() == BUTTON_STATE_UP)
	{
		if(animalsAvail)
			CntrlManager->SetControlPackage(stringc("StageSelectAnimals.controls"));
	}
	else if(magic->GetButtonState() == BUTTON_STATE_UP)
	{
		if(magicAvail)
			CntrlManager->SetControlPackage(stringc("StageSelectMagic.controls"));
	}
	else if(back->GetButtonState() == BUTTON_STATE_UP)
	{
		CntrlManager->SetControlPackage(stringc("MainMenu.controls"));
	}
}

void ModuleMainMenu::UpdateMainMenu()
{
}

void ModuleMainMenu::UpdateStageSelect(int stageOffset)
{
	stringc stage = SharedParams.Settings->GetValue("Stage");
	//stage = stage.subString(5, stage.size() - 5);
	stringc stageNrStr;
	for (u32 iCh = 0; iCh < stage.size() ; iCh++)
	{	
		char c = stage[iCh];		
		if(strchr("0123456789", c) != 0)// Это число
		{
			stageNrStr += c;
		}
	}
	
	int stageNr = (s32)core::fast_atof(stageNrStr.c_str()) - stageOffset;
	if(stageNr <= 0) 
	{
		stageNr = 1;
	}
	
	for(int iStage = 1; iStage <= 30; iStage++)
	{
		stringc lockName = stringc("Lock") + stringc(iStage);
		IControl* cntrl = CntrlManager->GetControlByName(lockName);
		if(cntrl != NULL)
		{
			if(iStage <= stageNr)
				cntrl->IsVisible = false;
			else
				cntrl->IsVisible = true;
		}

		stringc stageText = stringc("Text") + stringc(iStage);
		cntrl = CntrlManager->GetControlByName(stageText);
		if(cntrl != NULL)
		{
			if(iStage <= stageNr)
				cntrl->IsVisible = true;
			else
				cntrl->IsVisible = false;
		}

		stageText += "_1";
		cntrl = CntrlManager->GetControlByName(stageText);
		if(cntrl != NULL)
		{
			if(iStage <= stageNr)
				cntrl->IsVisible = true;
			else
				cntrl->IsVisible = false;
		}
	}

	for(int iStage = 1; iStage <= stageNr; iStage++)
	{
		stringc stageBtnName = stringc("Stage") + stringc(iStage);
		ControlButton* stageBtn = dynamic_cast<ControlButton *>(CntrlManager->GetControlByName(stageBtnName));
		if(stageBtn != NULL)
		{
			if(stageBtn->GetButtonState() == BUTTON_STATE_UP)
			{

				// Загружаем стадию
				int iStageOff = iStage + stageOffset;
				stringc stageName = stringc("Stage") + stringc(iStageOff);
				
				
				if(iStageOff == 3 ||
					iStageOff == 6 || 
					iStageOff == 7 ||
					iStageOff == 10 ||
					iStageOff == 12 ||
					iStageOff == 14 ||
					iStageOff == 15 ||
					iStageOff == 17 ||
					iStageOff == 20)
				{
					// No market

					SndManager->StopAll();


					stageName += ".stage";

					_isDone = true;

					ModuleHelper::DrawLoading(&SharedParams);

					EventBase->PostEvent(Event::ID_MANAGER, MANAGER_EVENT_AD_SHOW);

					EventParameters_t params;
					params.StrVar = stageName;
					EventBase->PostEvent(Event::ID_MANAGER, MANAGER_EVENT_STAGE_LOAD, params);
					return;
				}
				else 
				{
					stringc menuParam(iStageOff);
					SetGlobalParameter("MainMenuStageNr", menuParam);
					CntrlManager->SetControlPackage(stringc("Market.controls"));
					return;
				}				
			}
		}
	}


	ControlButton* back = dynamic_cast<ControlButton *>(CntrlManager->GetControlByName(stringc("Back")));
	if(back->GetButtonState() == BUTTON_STATE_UP)
	{
		CntrlManager->SetControlPackage(stringc("MissionSelection.controls"));
	}
}

void ModuleMainMenu::UpdateLanguage()
{
	return;

	stringc path = ModuleHelper::GetLangTextPath(&SharedParams);

	ControlImage* cntrl;
	if(CntrlManager->GetCurrentPackageName() == "MainMenu.controls")
	{
		cntrl = dynamic_cast<ControlImage*>(CntrlManager->GetControlByName("TextStart"));
		cntrl->Init(path + stringc("Start.png"), cntrl->GetPosition());
		cntrl = dynamic_cast<ControlImage*>(CntrlManager->GetControlByName("TextSettings"));
		cntrl->Init(path + stringc("Settings.png"), cntrl->GetPosition());
	}
	else if(CntrlManager->GetCurrentPackageName() == "Settings.controls")
	{
		cntrl = dynamic_cast<ControlImage*>(CntrlManager->GetControlByName("TextBack"));
		cntrl->Init(path + stringc("Back.png"), cntrl->GetPosition());
		cntrl = dynamic_cast<ControlImage*>(CntrlManager->GetControlByName("TextMusicOn"));
		cntrl->Init(path + stringc("MusicOn.png"), cntrl->GetPosition());
		cntrl = dynamic_cast<ControlImage*>(CntrlManager->GetControlByName("TextMusicOff"));
		cntrl->Init(path + stringc("MusicOff.png"), cntrl->GetPosition());
		cntrl = dynamic_cast<ControlImage*>(CntrlManager->GetControlByName("TextSoundOn"));
		cntrl->Init(path + stringc("SoundOn.png"), cntrl->GetPosition());
		cntrl = dynamic_cast<ControlImage*>(CntrlManager->GetControlByName("TextSoundOff"));
		cntrl->Init(path + stringc("SoundOff.png"), cntrl->GetPosition());
		//cntrl = dynamic_cast<ControlImage*>(CntrlManager->GetControlByName("TextLanguage"));
		//cntrl->Init(path + stringc("Language.png"), cntrl->GetPosition());
	}
	else if(CntrlManager->GetCurrentPackageName() == "About.controls")
	{
		cntrl = dynamic_cast<ControlImage*>(CntrlManager->GetControlByName("TextBack"));
		cntrl->Init(path + stringc("Back.png"), cntrl->GetPosition());
	}
	else if(CntrlManager->GetCurrentPackageName() == "MissionSelect.controls")
	{
		cntrl = dynamic_cast<ControlImage*>(CntrlManager->GetControlByName("TextSelectMission"));
		cntrl->Init(path + stringc("SelectMission.png"), cntrl->GetPosition());
	}
	else if(CntrlManager->GetCurrentPackageName() == "StageSelectEarth.controls")
	{
		cntrl = dynamic_cast<ControlImage*>(CntrlManager->GetControlByName("TextSelectStage"));
		cntrl->Init(path + stringc("SelectStage.png"), cntrl->GetPosition());
	}
	else if(CntrlManager->GetCurrentPackageName() == "StageSelectAnimals.controls")
	{
		cntrl = dynamic_cast<ControlImage*>(CntrlManager->GetControlByName("TextSelectStage"));
		cntrl->Init(path + stringc("SelectStage.png"), cntrl->GetPosition());
	}
	else if(CntrlManager->GetCurrentPackageName() == "StageSelectMagic.controls")
	{
		cntrl = dynamic_cast<ControlImage*>(CntrlManager->GetControlByName("TextSelectStage"));
		cntrl->Init(path + stringc("SelectStage.png"), cntrl->GetPosition());
	}
}






