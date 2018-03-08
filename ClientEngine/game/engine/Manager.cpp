#include "Manager.h"

#include "ParserExtensions/ParserExtensionCommon.h"
#include "UserSettings/UserSettingsWorker.h"
#include "Core/Xml/XMLFileDocument.h"
#include "RateApp/RateAppManager.h"
#include "Core/TextureWorker.h"


#include "../Modules/ModuleStandardControl.h"
#include "../Modules/ModuleTapAndShot.h"
#include "../Modules/ModuleFightControl.h"
#include "../Modules/ModuleMainMenu.h"
#include "../Modules/ModuleMarket.h"
#include "../Modules/ModuleFly.h"
#include "../Modules/ModuleEndGame.h"
#include "../Modules/ModuleTutorial1.h"
#include "../Modules/ModuleTranslate.h"
#include "../Modules/ModuleHelper.h"

//#define DEBUG_SLOW_TIME

Manager::Manager(
	SharedParams_t params, 
	bool useAd, 
	float controlScale, 
	float reciverScale,
	position2di controlsOffset) : Base(params)
{
#ifdef DEBUG_MESSAGE_PRINT
	if(!Device || !Driver || !SceneManager || !EventBase)
		printf("[ERROR]<Manager::Manager>(nul pointer)\n");
#endif

	_reciver = new Reciver(reciverScale);
	_lastTime = 0;
	_controlsScale = controlScale;
	_controlsOffset = controlsOffset;
	
	// Загрузка настроек пользователя
	UserSettingsWorker::Load(params, params.Settings);
	if (params.Settings->GetValue("Language") == "")
	{
		params.Settings->SetTextSetting("Language", "en");
	}

#ifdef DEBUG_VISUAL_DATA
	// Создаём декорадтор для возвращаемых значений
	_dbgReciver = new DebugReciver(_reciver, SceneManager, this);
	Device->setEventReceiver(_dbgReciver);
	DebugSettings::LoadFromFile(params, "Debug/DebugSettings.xml");
#else
	Device->setEventReceiver(_reciver);
#endif
	Device->getLogger()->setLogLevel(ELL_INFORMATION);
	//Device->getLogger()->setLogLevel(ELL_WARNING);
	//Device->getLogger()->setLogLevel(ELL_NONE);

	
	ModuleHelper::DrawLoading(&SharedParams);

	_stageManager = NULL;

	// Иницилизация звука
	//
	_soundManager = new SoundManager(SharedParams);

	_inAppManager = new InAppPurchases(SharedParams);

	if(useAd)
	{
		_adManager = new AdManager(SharedParams);
	}
	else 
	{
		_adManager = NULL;
	}
	
	// Иницилизация доп. модулей
	//
	_moduleManager = new ModuleManager(SharedParams);
	_moduleManager->AddModule(new ModuleStandardControl(SharedParams));
	_moduleManager->AddModule(new ModuleTapAndShot(SharedParams));
	_moduleManager->AddModule(new ModuleFly(SharedParams));
	_moduleManager->AddModule(new ModuleFightControl(SharedParams));	
	_moduleManager->AddModule(new ModuleMainMenu(SharedParams));
	_moduleManager->AddModule(new ModuleMarket(SharedParams));
	_moduleManager->AddModule(new ModuleTutorial1(SharedParams));
	_moduleManager->AddModule(new ModuleEndGame(SharedParams));
	_moduleManager->AddModule(new ModuleTranslate(SharedParams));
	
	// Регистрация основных функций скрипта
	ParserExtensionCommon_RegisterFunctions(_parser.GetEngine());
}

Manager::~Manager()
{
	if(_stageManager)
	{
		delete _stageManager;
	}

	Clear();

#ifdef DEBUG_VISUAL_DATA
	if(_dbgReciver != NULL)
	{
		delete _dbgReciver;
	}
#endif
	if(_reciver != NULL)
	{
		delete _reciver;
	}
	if(_moduleManager != NULL) 
	{
		delete _moduleManager;
	}
	if(_soundManager != NULL)
	{
		delete _soundManager;
	}
	if(_inAppManager != NULL)
	{
		delete _inAppManager;
	}
	if(_adManager != NULL)
	{
		delete _adManager;
	}

	Device->drop();

	if (SharedParams.Event)
	{
		delete (Event*)SharedParams.Event;
	}
	if (SharedParams.GlobalParams)
	{
		delete SharedParams.GlobalParams;
	}
	if (SharedParams.GameParams)
	{
		delete SharedParams.GameParams;
	}
	if (SharedParams.TimeDiff)
	{
		delete SharedParams.TimeDiff;	
	}
}

void Manager::Init()
{
	int winWidth = SharedParams.WindowSize.Width;
	int winHeight = SharedParams.WindowSize.Height;
	stringc mainFileName = "main.game";
	
	LoadMain(mainFileName);
	
	// Определяем ииндекс текущей стадии
	//
	_currentStageIndex = -1;
	for (u32 i = 0; i < _stageItems.size() ; i++)
	{
		if(_stageItems[i].IsStartStage)
		{
			_currentStageIndex = i;
			break;
		}
	}
	_DEBUG_BREAK_IF(_currentStageIndex < 0)
	
	// Загрузка начальной стадии
	LoadStage(FileSystem->getFileBasename(_stageItems[_currentStageIndex].Path));
}

SharedParams_t Manager::CreateSharedParams(
	IrrlichtDevice *device,
	UserSettings *setting,
	core::dimension2d<s32> windowSize)
{
	SharedParams_t sharedParams;
	sharedParams.Event	=	(void*) new Event();
	sharedParams.Device	=	device;
	sharedParams.Driver	=	device->getVideoDriver();
	sharedParams.SceneManager	=	device->getSceneManager();		
	sharedParams.Settings	=	setting;
	sharedParams.Timer	=	device->getTimer();		
	sharedParams.FileSystem = device->getFileSystem();	
	sharedParams.TimeDiff = new u32[1];
	sharedParams.TimeDiff[0] = 0;
	sharedParams.GlobalParams = new core::array<Parameter>();
	sharedParams.GameParams = new core::array<Parameter>();

	sharedParams.WindowSize = windowSize;

	return sharedParams;

}


void Manager::Update()
{	
	Device->run();
	
#ifdef DEBUG_SLOW_TIME
	// Замедление времени
	//
	Device->getTimer()->setSpeed(0.3f);
#endif


	GetReciverInfo();

	s32 tDiff = GetNowTime() - _lastTime;
	if(tDiff < 0) 
	{
		_lastTime = GetNowTime();
		tDiff = 0;
	}
	else if(tDiff > 80)
	{
		// Минимум 13 fps
		// Делается для того, чтоб не было резких скачков
		tDiff = 70;
		Timer->setTime(_lastTime + 70);
	}

	// Обновляем время
	*SharedParams.TimeDiff = tDiff;
	_lastTime = GetNowTime();

	_stageManager->Update(&_reciverInfo);
	_soundManager->Update();

	HandleEvent();
	
	ClearReciverInfo();	


#ifdef  IPHONE_COMPILE	
	static int extraCounter = 0;
	extraCounter++;
	if(extraCounter > 20)
	{
		extraCounter = 0;
		//s32 fps = Driver->getFPS();
		//printf("fps[%d] \n",fps);
		//u32 drawcount = Driver->getPrimitiveCountDrawn();
		//printf("drawcount[%d] \n",drawcount);
	}
#else
	core::stringw	strWinCaption;
	s32 fps = Driver->getFPS();
	core::vector3df pos;
	strWinCaption		=	L"FPS::";
	strWinCaption		+=	fps;
	strWinCaption		+=  "   ";
	strWinCaption		+=  Driver->getPrimitiveCountDrawn();

	scene::ICameraSceneNode *camera = SceneManager->getActiveCamera();
	if(camera != NULL)
	{
		char capCam[300];
		vector3df cPos = camera->getPosition();
		vector3df cTarget = camera->getTarget();
		sprintf(capCam, " CAM:: %.2f, %.2f, %.2f T: %.2f, %.2f, %.2f",
			cPos.X,
			cPos.Y,
			cPos.Z,
			cTarget.X,
			cTarget.Y,
			cTarget.Z);

		strWinCaption		+=	stringw(capCam);

		Device->setWindowCaption(strWinCaption.c_str());
	}
#endif
	//s32 fps = Driver->getFPS();
	//printf("fps[%d] \n",fps);	
}



void Manager::HandleEvent()
{
	Event_t* eventInst;
	while(!EventBase->GetEvent(Event::ID_MANAGER, &eventInst))
	{
		if (eventInst->EventId == MANAGER_EVENT_STAGE_COMPLETE)
		{
			stringc scriptCode = _stageItems[_currentStageIndex].ScriptOnComplete;
			_parser.GetUserData()->BaseData = this;
			_parser.SetCode(scriptCode);
			_parser.Execute();
		}
		else if (eventInst->EventId == MANAGER_EVENT_STAGE_LOAD)
		{
			// Загрузка новой стадии
			stringc stagePath = eventInst->Params.StrVar;
			LoadStage(stagePath);
		}
		else if (eventInst->EventId == MANAGER_EVENT_STAGE_RESTART)
		{
			// Перезапуск стадии
			LoadStage(FileSystem->getFileBasename(_stageItems[_currentStageIndex].Path));
		}
		else if (eventInst->EventId == MANAGER_EVENT_AD_SHOW)
		{
			if (_adManager != NULL)
			{
				// Показать рекламу
				_adManager->ShowAd(5000);
			}
 		}
		else if (eventInst->EventId == MANAGER_EVENT_AD_HIDE)
		{
			if (_adManager != NULL)
			{
				// Скрыть рекламу
				_adManager->HideAd();
			}
		}
		else if (eventInst->EventId == MANAGER_EVENT_RATE_APP)
		{
			RateAppManager rateManager;	
			stringc appId = eventInst->Params.StrVar;
			rateManager.RateApp(appId);
		}
		
	}
	EventBase->RemoveNotUsedEvent();
}


void Manager::Clear(){	

	if(EventBase) EventBase->Clear();

	SceneManager->getRootSceneNode()->removeAnimators();
	SceneManager->getRootSceneNode()->removeAll();
	SceneManager->clear();
	SceneManager->getMeshCache()->clear();
	Driver->removeAllTextures();
}

void Manager::GetReciverInfo()
{
	_reciverInfo.XTouch[0] = _reciver->XTouch;
	_reciverInfo.YTouch[0] = _reciver->YTouch;
	_reciverInfo.StateTouch[0] = _reciver->StateTouch;
	_reciverInfo.XTouch[1] = _reciver->XTouchSecond;
	_reciverInfo.YTouch[1] = _reciver->YTouchSecond;
	_reciverInfo.StateTouch[1] = _reciver->StateTouchSecond;
	_reciverInfo.Xaccel = _reciver->Xaccel;
	_reciverInfo.Yaccel = _reciver->Yaccel;
	_reciverInfo.Zaccel = _reciver->Zaccel;
#ifndef IPHONE_COMPILE
	_reciverInfo.MousePressed = _reciver->MousePressed;
#endif
}

void Manager::ClearReciverInfo()
{
#ifdef IPHONE_COMPILE
	if(_reciverInfo.StateTouch[0] == irr::ETOUCH_CANCELLED || _reciverInfo.StateTouch[0] == ETOUCH_ENDED)
	{
		if(_reciverInfo.StateTouch[0] == _reciver->StateTouch)
		{
			_reciver->XTouch = FAIL_VALUE;
			_reciver->YTouch = FAIL_VALUE;
			_reciver->StateTouch = irr::ETOUCH_NONE;
		}
	}
	else if(_reciverInfo.StateTouch[0] == irr::ETOUCH_BEGAN)
	{
		if(_reciverInfo.StateTouch[0] == _reciver->StateTouch)
		{
			_reciver->StateTouch = irr::ETOUCH_NONE;
		}
	}
	if(_reciverInfo.StateTouch[1] == irr::ETOUCH_CANCELLED || _reciverInfo.StateTouch[1] == ETOUCH_ENDED)
	{
		if(_reciverInfo.StateTouch[1] == _reciver->StateTouchSecond)
		{
			_reciver->XTouchSecond = FAIL_VALUE;
			_reciver->YTouchSecond = FAIL_VALUE;
			_reciver->StateTouchSecond = irr::ETOUCH_NONE;
		}
	}
	else if(_reciverInfo.StateTouch[1] == irr::ETOUCH_BEGAN)
	{
		if(_reciverInfo.StateTouch[1] == _reciver->StateTouchSecond)
		{
			_reciver->StateTouchSecond = irr::ETOUCH_NONE;
		}
	}
#else
	//printf("StateTouch %d \n", );
	if(_reciverInfo.StateTouch[1] == irr::EMIE_LMOUSE_LEFT_UP)
	{
		_reciver->XTouchSecond = FAIL_VALUE;
		_reciver->YTouchSecond = FAIL_VALUE;
		_reciver->StateTouchSecond = FAIL_VALUE;
	}
	else if(_reciverInfo.StateTouch[1] == irr::EMIE_LMOUSE_PRESSED_DOWN)
	{
		_reciver->StateTouchSecond = irr::EMIE_MOUSE_MOVED;
	}

	if(_reciverInfo.StateTouch[0] == irr::EMIE_LMOUSE_LEFT_UP)
	{
		_reciver->XTouch = FAIL_VALUE;
		_reciver->YTouch = FAIL_VALUE;
		_reciver->StateTouch = FAIL_VALUE;
	}
	else if(_reciverInfo.StateTouch[0] == irr::EMIE_LMOUSE_PRESSED_DOWN)
	{
		_reciver->StateTouch = irr::EMIE_MOUSE_MOVED;
	}

#endif
}

// Загрузка стадии
void Manager::LoadStage(stringc stageName)
{
	TestFlightManager::Log(stringc("Load stage : ") + stringc(stageName));
	
	ModuleHelper::DrawLoading(&SharedParams);

	// Ищем путь до стадии
	stringc path;
	for (u32 i = 0; i < _stageItems.size() ; i++)
	{
		stringc basePath = FileSystem->getFileBasename(_stageItems[i].Path);
		if(basePath == stageName)
		{
			path = _stageItems[i].Path;
			_currentStageIndex = i;
			break;
		}
	}	
	_DEBUG_BREAK_IF(path == "")

	if(_stageManager != NULL) 
	{
		delete _stageManager;
	}
	
	// Очистка глобальных параметров
	ResetGlobalParameters();

	Clear();
	
	_stageManager = new StageManager(
		SharedParams, _moduleManager, 
		_soundManager, _inAppManager, 
		_controlsScale, _controlsOffset);
	_stageManager->LoadStage(path);
	
	// Устанавливаем начальное время
	Device->run();
	Timer->setTime(0);
	_lastTime = 0;

	if (_adManager != NULL)
	{
		_adManager->HideAd();
	}
}

// Загрузка основного файла
void Manager::LoadMain( stringc path )
{
	stringc root = FileSystem->getFileDir(path) + "/";

	XMLFileDocument fileDoc(path, FileSystem);
	rapidxml::xml_document<>* doc = fileDoc.GetDocument();
	rapidxml::xml_node<>* rootNode = doc->first_node();	

	core::array<rapidxml::xml_node<>*> tagNodes = 
		XMLHelper::SelectAllByNodeName(rootNode, "Tag");
	for(u32 iNode = 0; iNode < tagNodes.size(); iNode++)
	{
		rapidxml::xml_node<>* tagNode = tagNodes[iNode];
		rapidxml::xml_attribute<>* attr = tagNode->first_attribute("xsi:type");
		if(strcmp(attr->value(), "StageItem") == 0)
		{
			StageItem stageItem;
			for (rapidxml::xml_node<>* chNode = tagNode->first_node(); chNode; chNode = chNode->next_sibling())
			{
				if(strcmp(chNode->name(), "Path") == 0)
				{
					stageItem.Path = chNode->value();
				}
				else if(strcmp(chNode->name(), "ScriptOnComplete") == 0)
				{
					stageItem.ScriptOnComplete = chNode->value();
				}
				else if(strcmp(chNode->name(), "IsStartStage") == 0)					
				{
					stageItem.IsStartStage = XMLHelper::GetTextAsBool(chNode);
				}
				else
				{
					_DEBUG_BREAK_IF(true)
				}
			}
			_stageItems.push_back(stageItem);
		}
		else
		{
			_DEBUG_BREAK_IF(true)
		}
	}

}



