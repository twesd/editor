#include "UnitManager.h"
#include "loaders/LoaderUnitGenInstance.h"
#include "unitInstance/UnitInstanceEnv.h"
#include "unitInstance/UnitInstanceStandard.h"
#include "unitInstance/UnitInstanceBillboard.h"
#include "unitInstance/UnitInstanceCamera.h"
#include "unitInstance/UnitInstanceEmpty.h"

UnitManager::UnitManager(SharedParams_t params) : Base(params),
	_genInstances(), _instances()
{
	_xmlCache = NULL;
	_scriptCache = NULL;
	_moduleManager = NULL;
	_soundManager = NULL;
}

UnitManager::~UnitManager(void)
{
	for(u32 i = 0; i < _genInstances.size(); i++)
	{
		_genInstances[i]->drop();
	}
	for (u32 i = 0; i < _instances.size(); i++)
	{
		_instances[i]->drop();
	}
	for (u32 i = 0; i < _timers.size(); i++)
	{
		delete _timers[i];
	}
}

// Загрузка описания юнита <TreeView>
void UnitManager::LoadUnitGensFromXmlTag(rapidxml::xml_node<>* sourceNode, stringc root)
{
	_genInstances = LoaderUnitGenInstance::LoadUnitGensFromXmlTag(SharedParams, sourceNode, root);
}

void UnitManager::Update()
{
	// Обновление таймеров
	UpdateTimers();

	// Обработка создания новых юнитов
	UpdateGenInstances();

	// Обработка событий
	HandleEvent();

	// Обновление поведения юнитов
	UpdateInstances();
}

// Обновление таймеров
void UnitManager::UpdateTimers()
{
	for (u32 i = 0; i < _timers.size(); i++)
	{
		_timers[i]->Tick();
	}
}

// Обработка создания новых юнитов
void UnitManager::UpdateGenInstances()
{
	bool done = false;
	while(!done)
	{
		done = true;
		for(u32 i = 0; i < _genInstances.size(); i++)
		{
			UnitGenInstanceBase* genInstance = _genInstances[i];
			if(!genInstance->NeedCreate()) continue;
			
			CreateNewUnit(genInstance, NULL);

			if(genInstance->CanDispose())
			{	
				_genInstances[i]->drop();
				_genInstances.erase(i);
				done = false;
				break;
			}
		}
	}
}

// Обработка событий
void UnitManager::HandleEvent()
{
	core::array<Event_t*> events;
	Event_t* eventInst;
	while(!EventBase->GetEvent(Event::ID_UNIT_MANAGER,&eventInst))
	{
		events.push_back(eventInst);
	}	
	for (u32 i = 0; i < _instances.size(); i++)
	{
		_instances[i]->HandleEvent(events);
	}
	EventBase->RemoveNotUsedEvent();
}

// Обновить юниты
void UnitManager::UpdateInstances()
{
	for (u32 i = 0; i < _instances.size(); i++)
	{
		UnitInstanceBase* instance = _instances[i];
		instance->Update();
	}

	// Удаление юнитов
	//
	bool done = false;
	while(!done)
	{
		done = true;
		for (u32 i = 0; i < _instances.size(); i++)
		{
			UnitInstanceBase* instance = _instances[i];
			DeleteErasedChilds(instance);
			if(instance->GetIsErased())
			{
				instance->drop();
				_instances.erase(i);
				done = false;
				break;
			}
		}
	}	
}

void UnitManager::DeleteErasedChilds(UnitInstanceBase* instance)
{
	// Удаление юнитов
	//
	core::array<UnitInstanceBase*> childs = instance->GetChilds();
	if(childs.size() == 0) return;
	for (u32 i = 0; i < childs.size(); i++)
	{
		DeleteErasedChilds(childs[i]);
		if(childs[i]->GetIsErased())
		{
			instance->RemoveChild(childs[i]);
		}
	}	
}

UnitInstanceBase* UnitManager::GetInstanceBySceneNode( ISceneNode* node )
{
	if(node == NULL)
	{
		return NULL;
	}
	for (u32 i = 0; i < _instances.size(); i++)
	{
		if(_instances[i]->SceneNode == node)
		{
			return _instances[i];
		}
		UnitInstanceBase* unit = GetChildInstanceBySceneNode(_instances[i], node);
		if(unit != NULL)
		{
			return unit;
		}
	}
	return NULL;
}

UnitInstanceBase* UnitManager::GetChildInstanceBySceneNode(UnitInstanceBase* instance, ISceneNode* node )
{
	core::array<UnitInstanceBase*> childs = instance->GetChilds();
	for (u32 i = 0; i < childs.size(); i++)
	{
		if(childs[i]->SceneNode == node)
		{
			return childs[i];
		}
		UnitInstanceBase* unit = GetChildInstanceBySceneNode(childs[i], node);
		if(unit != NULL)
		{
			return unit;
		}
	}
	return NULL;
}

// Получить юниты по имени
UnitInstanceBase* UnitManager::GetInstanceByName(const stringc& name )
{
	if(name.size() == 0)
	{
		return NULL;
	}
	core::array<UnitInstanceBase*> res;
	for (u32 i = 0; i < _instances.size(); i++)
	{
		if(_instances[i]->Name == name)
		{
			return _instances[i];
		}
		UnitInstanceBase* unit = GetChildInstanceByName(_instances[i], name);
		if(unit != NULL)
		{
			return unit;
		}
	}
	return NULL;
}

UnitInstanceBase* UnitManager::GetChildInstanceByName(UnitInstanceBase* instance, const stringc& name )
{
	core::array<UnitInstanceBase*> childs = instance->GetChilds();
	for (u32 i = 0; i < childs.size(); i++)
	{
		if(childs[i]->Name == name)
		{
			return childs[i];
		}
		UnitInstanceBase* unit = GetChildInstanceByName(childs[i], name);
		if(unit != NULL)
		{
			return unit;
		}
	}
	return NULL;
}

core::array<UnitInstanceBase*> UnitManager::GetInstances()
{
	return _instances;
}

// Создать новый юнит
void UnitManager::CreateNewUnit( UnitGenInstanceBase* genInstance, UnitInstanceBase* creator )
{
	if(genInstance->GetType() == UnitGenInstanceBase::Env)
	{
		UnitInstanceEnv* instance = new UnitInstanceEnv(SharedParams, 
			(UnitGenInstanceEnv*)genInstance, NULL, creator);
		_instances.push_back(instance);
	}
	else if(genInstance->GetType() == UnitGenInstanceBase::Standard)
	{
		UnitInstanceBase* instance = new UnitInstanceStandard(SharedParams,
			(UnitGenInstanceStandard*)genInstance, NULL, this, _soundManager, creator);
		_instances.push_back(instance);				
	}
	else if(genInstance->GetType() == UnitGenInstanceBase::Billboard)
	{
		UnitInstanceBillboard* instance = new UnitInstanceBillboard(
			SharedParams, (UnitGenInstanceBillboard*)genInstance, NULL, creator);
		_instances.push_back(instance);				
	}
	else if(genInstance->GetType() == UnitGenInstanceBase::Empty)
	{
		UnitInstanceBase* instance = new UnitInstanceEmpty(SharedParams,
			(UnitGenInstanceEmpty*)genInstance, NULL, this, creator);
		_instances.push_back(instance);				
	}	
	else if(genInstance->GetType() == UnitGenInstanceBase::Camera)
	{
		UnitInstanceCamera* instance = new UnitInstanceCamera(SharedParams,
			(UnitGenInstanceCamera*)genInstance, NULL, this, creator);
		if (instance->Name == _startCameraName)
		{
			// Активируем стратовую камеру
			instance->Activate();
		}
		_instances.push_back(instance);				
	}
	else
	{
		_DEBUG_BREAK_IF(true)
	}
}

// Существует ли заданный юнит
bool UnitManager::GetIsUnitInstanceExist( UnitInstanceBase* instance )
{
	for (u32 i = 0; i < _instances.size(); i++)
	{
		UnitInstanceBase* inst = _instances[i];
		if(inst == instance)
			return true;
		core::array<UnitInstanceBase*> childs = inst->GetChilds();
		for (u32 iChild = 0; iChild < childs.size(); iChild++)
		{
			if(childs[iChild] == instance)
				return true;
		}
	}
	return false;
}

// Установить объект кэширования xml файлов
void UnitManager::SetXmlCache( XMLCache* xmlCache )
{
	_DEBUG_BREAK_IF(xmlCache == NULL)
	_xmlCache = xmlCache;
}

// Получить объект кэширования xml файлов
XMLCache* UnitManager::GetXmlCache()
{
	return _xmlCache;
}

void UnitManager::SetScriptCache( ScriptCache* scriptCache )
{
	_scriptCache = scriptCache;
}

ScriptCache* UnitManager::GetScriptCache()
{
	return _scriptCache;
}

void UnitManager::SetStartCameraName( stringc cameraName )
{
	_startCameraName = cameraName;
}

void UnitManager::SetModuleManager( ModuleManager* moduleManager )
{
	_DEBUG_BREAK_IF(moduleManager == NULL)
	_moduleManager = moduleManager;
}

ModuleManager* UnitManager::GetModuleManager()
{
	return _moduleManager;
}

void UnitManager::SetSoundManager( SoundManager* soundManager )
{
	_DEBUG_BREAK_IF(soundManager == NULL)
	_soundManager = soundManager;
}

TimerBase* UnitManager::GetTimerByName( const stringc& name, bool createNewIfNotExist)
{
	for (u32 i = 0; i < _timers.size(); i++)
	{
		if (_timers[i]->Name == name)
		{
			return _timers[i];
		}
	}
	if (createNewIfNotExist)
	{
		TimerBase* t = new TimerBase(name, Timer);
		_timers.push_back(t);
		return t;
	}
	return NULL;
}


