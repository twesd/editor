#include "StageManager.h"
#include "Core/Xml/XMLFileDocument.h"
#include "Core/NodeWorker.h"
#include "Core/TextureWorker.h"
#include "Core/FileUtility.h"
#include "../Controls/LoaderControlManager.h"

#ifdef DEBUG_VISUAL_DATA
#include "Debug/DebugSettings.h"
#include "Debug/DebugInfo.h"
#endif

StageManager::StageManager(
	SharedParams_t params, 
	ModuleManager* moduleManager,
	SoundManager* soundManager,
	InAppPurchases* inAppManager,
	float controlsScale,
	position2di controlsOffset) : Base(params)
{
	_enableUnitManager = true;
	_enableDrawAll = true;
	_moduleManager = moduleManager;
	_soundManager = soundManager;
	_inAppManager = inAppManager;
	_isFirst = true;
	_controlsScale = controlsScale;
	_controlsOffset = controlsOffset;

	_controlManager = new ControlManager(params, controlsScale, controlsOffset);
	_unitManager = new UnitManager(params);
	_xmlCache = new XMLCache(params);
	_scriptCache = new ScriptCache(params);


	// Устанавливаем доп. параметры
	//
	_unitManager->SetXmlCache(_xmlCache);
	_unitManager->SetScriptCache(_scriptCache);
	_unitManager->SetModuleManager(_moduleManager);
	_unitManager->SetSoundManager(_soundManager);
	_controlManager->SetXmlCache(_xmlCache);
	_controlManager->SetScriptCache(_scriptCache);

	_moduleManager->SetUnitManager(_unitManager);
	_moduleManager->SetControlManager(_controlManager);
	_moduleManager->SetSoundManager(_soundManager);
	_moduleManager->SetInAppManager(_inAppManager);
}

StageManager::~StageManager(void)
{
	_moduleManager->ClearData();
	if(_controlManager != NULL)
	{
		_controlManager->drop();
	}
	if(_unitManager != NULL)
	{
		_unitManager->drop();
	}
	if(_xmlCache != NULL)
	{
		_xmlCache->drop();
	}
	if(_scriptCache != NULL)
	{	
		_scriptCache->drop();
	}
}

void StageManager::Update(reciverInfo_t *reciverInfo)
{
	Driver->beginScene(true, true, video::SColor(255,0,0,0));

#ifdef DEBUG_VISUAL_DATA
	DebugSettings* dbgSettings = DebugSettings::GetInstance();
	if (dbgSettings->DynamicDebugInfoMode)
	{
		DebugInfo::GetInstance(SharedParams)->Update(reciverInfo, _unitManager);
		SceneManager->drawAll();
		Driver->endScene();
		return;
	}
#endif

	if(_isFirst)
	{
		// В первый раз производим два обновления
		//
		_controlManager->Update(reciverInfo);
		if(_enableUnitManager)
		{
			_unitManager->Update();
		}
		_isFirst = false;
	}

	// Отрисовка происходит вначале,
	// для того, чтобы аниматоры отработали для действий, которые исполняются один такт.
	// Например Transform - LINE
	if(_enableDrawAll)
		SceneManager->drawAll();
	_controlManager->Draw();

	_controlManager->Update(reciverInfo);
	if(_enableUnitManager)
	{
		_unitManager->Update();
	}

	HandleEvent();

	Driver->endScene();
}


void StageManager::LoadStage(stringc path)
{ 
	stringc root = FileSystem->getFileDir(path) + "/";

	XMLFileDocument fileDoc(path, FileSystem);
	rapidxml::xml_document<>* doc = fileDoc.GetDocument();
	rapidxml::xml_node<>* rootNode = doc->first_node();	

	for (rapidxml::xml_node<>* sourceNode = rootNode->first_node(); sourceNode; sourceNode = sourceNode->next_sibling())
	{
		if(strcmp(sourceNode->name(), "TreeView") == 0)
		{
			_unitManager->LoadUnitGensFromXmlTag(sourceNode, root);
		}
		else if(strcmp(sourceNode->name(), "ControlPackages") == 0)
		{			
			core::array<ControlPackage> packages = 
				LoaderControlManager::LoadControlPackagesFromXmlTag(
					SharedParams,
					sourceNode, 
					root, 
					_controlManager,
					_moduleManager,
					_soundManager,
					_scriptCache,
					_controlsScale,
					_controlsOffset);
			_controlManager->SetPackages(packages);
		}
		else if(strcmp(sourceNode->name(), "StartGlobalParameters") == 0)
		{
			for (rapidxml::xml_node<>* paramNode = sourceNode->first_node(); 
				paramNode; paramNode = paramNode->next_sibling())
			{	
				_DEBUG_BREAK_IF(strcmp(paramNode->name(), "Parameter") != 0)
				stringc paramName = stringc(paramNode->first_node("Name")->value());
				stringc paramVal = stringc(paramNode->first_node("Value")->value());
				SetGlobalParameter(paramName, paramVal);
			}	
		}
		else if(strcmp(sourceNode->name(), "CacheModelPaths") == 0)
		{
			// Загружаем модели в кэш
			//

			for (rapidxml::xml_node<>* chNode = sourceNode->first_node(); chNode; chNode = chNode->next_sibling())
			{
				if(strcmp(chNode->name(), "string") == 0)
				{
					stringc path = root + stringc(chNode->value());
					path = FileUtility::GetUpdatedFileName(path);
					IAnimatedMeshSceneNode* node = SceneManager->addAnimatedMeshSceneNode(
						SceneManager->getMesh(path.c_str()));
					node->remove();
				}
			}
		}
		else if(strcmp(sourceNode->name(), "CacheTexturePaths") == 0)
		{
			// Загружаем текстуры в кэш
			for (rapidxml::xml_node<>* chNode = sourceNode->first_node(); chNode; chNode = chNode->next_sibling())
			{
				if(strcmp(chNode->name(), "string") == 0)
				{
					stringc path = root + stringc(chNode->value());
					path = FileUtility::GetUpdatedFileName(path);
					TextureWorker::GetTexture(Driver, path, false);
				}
			}
		}
		else if(strcmp(sourceNode->name(), "CacheScripts") == 0)
		{
			// Загружаем модели в кэш
			//
			for (rapidxml::xml_node<>* chNode = sourceNode->first_node(); chNode; chNode = chNode->next_sibling())
			{
				if(strcmp(chNode->name(), "string") == 0)
				{
					stringc path = root + stringc(chNode->value());	
					path = FileUtility::GetUpdatedFileName(path);
					_scriptCache->AddItem(path);
				}
			}
		}
		else if(strcmp(sourceNode->name(), "CacheXmlFiles") == 0)
		{
			// Загружаем файлы в кэш
			//
			for (rapidxml::xml_node<>* chNode = sourceNode->first_node(); chNode; chNode = chNode->next_sibling())
			{
				if(strcmp(chNode->name(), "string") == 0)
				{
					stringc path = root + stringc(chNode->value());			
					path = FileUtility::GetUpdatedFileName(path);
					_xmlCache->AddItem(path);
				}
			}
		}
		else if(strcmp(sourceNode->name(), "StartCameraName") == 0)
		{
			// Устанавливаем начальную камеру
			_unitManager->SetStartCameraName(sourceNode->value());
		}
		else if(strcmp(sourceNode->name(), "ContainerStage") == 0 ||
			strcmp(sourceNode->name(), "UnitBehaviorPaths") == 0 ||
			strcmp(sourceNode->name(), "Camera") == 0)
		{
			// Пропускаем
			continue;
		}
		else
		{
			_DEBUG_BREAK_IF(true)
		}
	}

	// Иницилизация стадии
	InitStage();
}

// Иницилизация стадии
void StageManager::InitStage()
{
	// Обязательно необходимо делать проверку на stop, 
	// так как в итоге может получится некорректное поведение.
	if(Timer->isStopped())
		Timer->start();
	// Устанавливаем начальное время
	Device->run();
	Timer->setTime(0);
}

void StageManager::HandleEvent()
{
	core::array<Event_t*> events;
	Event_t* eventInst;
	while(!EventBase->GetEvent(Event::ID_STAGE_MANAGER,&eventInst))
	{
		if (eventInst->EventId == STAGE_MANAGER_EVENT_STOP_UNIT_MANAGER)
		{
			_enableUnitManager = false;	
		}
		else if (eventInst->EventId == STAGE_MANAGER_EVENT_START_UNIT_MANAGER)
		{
			_enableUnitManager = true;	
		}
		else if (eventInst->EventId == STAGE_MANAGER_EVENT_STOP_SCENE_MANAGER)
		{
			_enableDrawAll = false;	
		}
		else if (eventInst->EventId == STAGE_MANAGER_EVENT_START_SCENE_MANAGER)
		{
			_enableDrawAll = true;	
		}		
	}	
	EventBase->RemoveNotUsedEvent();
}

