#include "UnitInstanceStandard.h"
#include "../Loaders/LoaderBehaviors.h"
#include "../../Controls/ControlEvent.h"
#include "../UnitManager.h"
#include "Core/NodeWorker.h"
#ifdef DEBUG_VISUAL_DATA
#include "../Debug/DebugNode.h"
#include "../Debug/DebugSettings.h"
#endif

UnitInstanceStandard::UnitInstanceStandard(
	SharedParams_t params, 
	UnitGenInstanceStandard *genInstance, 
	UnitInstanceBase* parent,
	UnitManager* unitManager,
	SoundManager* soundManager,
	UnitInstanceBase* creator) : 
		UnitInstanceBase(params, genInstance, parent, creator), 
		_mappings()
{
	SceneNode = NULL;
	_startScript = NULL;
	_isFirstHandle = true;
	_unitManager = unitManager;
	_soundManager = soundManager;

	XMLCache* xmlCache  = unitManager->GetXmlCache();
	ScriptCache* scriptCache = unitManager->GetScriptCache();
	ModuleManager* moduleManager = unitManager->GetModuleManager();
	_behavior = LoaderBehaviors::LoadFromFile(
		params, 
		xmlCache, 
		scriptCache, 
		moduleManager,
		soundManager,
		genInstance->PathBehavior);
	if(_behavior->UnitModel != NULL)
	{
		SceneNode = _behavior->UnitModel->LoadSceneNode();

		if(parent != NULL && parent->SceneNode != NULL)
		{
			// Если указано привязать к кости, то
			if (genInstance->JointName.size() > 0)
			{
				if(parent->SceneNode->getType() == ESNT_ANIMATED_MESH)
				{
					IAnimatedMeshSceneNode* animNodeParent = (IAnimatedMeshSceneNode*)parent->SceneNode;
					IBoneSceneNode* jointNode = NULL;
					// Перебираем возможные имена кости
					//
					jointNode = animNodeParent->getJointNode(genInstance->JointName.c_str());
					if (jointNode == NULL)
					{
						jointNode = animNodeParent->getJointNode(
							(stringc("Anim_MatrixFrame_") + genInstance->JointName).c_str());
						if (jointNode == NULL)
						{
							jointNode = animNodeParent->getJointNode(
								(stringc("Frame_") + genInstance->JointName).c_str());
						}
					}
					if (jointNode == NULL)
					{
						_DEBUG_BREAK_IF(true)
							return;
					}
					jointNode->addChild(SceneNode);
				}
				else
				{
					_DEBUG_BREAK_IF(true)
				}
			}
			else
			{
				SceneNode->setParent(parent->SceneNode);
			}
		}
		
		genInstance->ApplyStartParameters(SceneNode);
		_behavior->SetSceneNode(SceneNode);
		SceneNode->updateAbsolutePosition();
		// По умолчанию делаем модель невидимой, 
		// во время пeрвой обработки событий делаем её видимой
		SceneNode->setVisible(false);


		vector3df sdf = SceneNode->getAbsolutePosition();
		sdf = SceneNode->getAbsolutePosition();
	}

	_behavior->SetUnitInstance(this);
	_behavior->LoadComplete();

	// Если задан начальный скрипт, то выполняем его
	if(genInstance->StartScriptFileName != "")
	{
		_startScript = scriptCache->GetItem(genInstance->StartScriptFileName);
	}

	if(SceneNode != NULL)
		SceneNode->setName(genInstance->PathBehavior);

#ifdef DEBUG_VISUAL_DATA	
	if(SceneNode != NULL)
	{
		int idSceneNode = SceneNode->getID();	
		if(DebugSettings::GetInstance()->ContainsNodeId(idSceneNode))
		{
			DebugNode* debugNode = new DebugNode(params, SceneNode);
			debugNode->SetInstance(this);
			debugNode->setID(0);
			debugNode->drop();
		}
	}
#endif
}

UnitInstanceStandard::~UnitInstanceStandard(void)
{
	// Очистка связей
	for (u32 i = 0; i < _mappings.size() ; i++)
	{
		_mappings[i]->drop();
	}
	_mappings.clear();

	if(_behavior->UnitModel != NULL)
		_behavior->UnitModel->drop();
	_behavior->SetSceneNode(NULL);	
	_behavior->drop();	

	for (u32 i = 0; i < _timers.size(); i++)
	{
		delete _timers[i];
	}
}

// Установить модель
void UnitInstanceStandard::SetSceneNode( ISceneNode* node )
{
	SceneNode = node;

	_behavior->SetSceneNode(SceneNode);

	if(Parent != NULL)
		Parent->SceneNode->addChild(SceneNode);
}


// Обновить данные
void UnitInstanceStandard::Update()
{
	UpdateTimers();

	// Обновляем дочерние юниты
	for (u32 i = 0; i < Childs.size() ; i++)
	{
		Childs[i]->Update();
	}	

	// Обновляем связи
	for (u32 i = 0; i < _mappings.size() ; i++)
	{
		_mappings[i]->Update();
	}
}

// Обработка событий
void UnitInstanceStandard::HandleEvent( core::array<Event_t*>& events )
{
	if(_isFirstHandle)
	{
		if(SceneNode != NULL)
			SceneNode->setVisible(true);
		_isFirstHandle = false;
	}

	// Выполняем начальный скрипт
	if(_startScript != NULL)
	{
		_startScript->GetUserData()->UnitInstanceData = this;
		_startScript->GetUserData()->BaseData = this;
		_startScript->Execute();
		_startScript = NULL;
	}

	_behavior->HandleEvent(events);

	for (u32 i = 0; i < Childs.size() ; i++)
	{
		Childs[i]->HandleEvent(events);
	}
}

// Создать дочерний юнит
void UnitInstanceStandard::CreateChild(UnitGenInstanceStandard genInstance, bool allowSeveralInstances)
{
	if(!allowSeveralInstances)
	{
		for (u32 i = 0; i < Childs.size() ; i++)
		{
			UnitInstanceStandard* unitStandard = dynamic_cast<UnitInstanceStandard*>(Childs[i]);
			if(unitStandard != NULL && unitStandard->GetBehaviorPath() == genInstance.PathBehavior &&
				!unitStandard->GetIsErased())
			{
				return;
			}
		}
	}
	UnitInstanceStandard* childInstance = new UnitInstanceStandard(
		SharedParams, &genInstance, this, _unitManager, _soundManager, this);
	Childs.push_back(childInstance);
}

// Получить путь до файла поведений
stringc UnitInstanceStandard::GetBehaviorPath()
{
	return _behavior->OwnerPath;
}

// Получить дочерний узел
UnitInstanceStandard* UnitInstanceStandard::GetChildByPath( stringc behaviorPath )
{
	bool done = false;
	while(!done)
	{
		done = true;
		for (u32 i = 0; i < Childs.size() ; i++)
		{
			UnitInstanceStandard* unitStandard = dynamic_cast<UnitInstanceStandard*>(Childs[i]);
			if(unitStandard != NULL && unitStandard->GetBehaviorPath() == behaviorPath)
			{
				return unitStandard;
			}
		}
	}
	return NULL;
}

// Удалить дочерний узел
void UnitInstanceStandard::DeleteChild( stringc behaviorPath )
{
	for (u32 i = 0; i < Childs.size() ; i++)
	{
		UnitInstanceStandard* unitStandard = dynamic_cast<UnitInstanceStandard*>(Childs[i]);
		if(unitStandard != NULL && unitStandard->GetBehaviorPath() == behaviorPath)
		{
			unitStandard->Erase();
		}
	}
}

// Создать внешний юнит
void UnitInstanceStandard::CreateExternalUnit( UnitGenInstanceStandard genInstance, bool allowSeveralInstances )
{
	SceneNode->updateAbsolutePosition();

	vector3df rotation = NodeWorker::GetAbsoluteRotation(SceneNode);
	genInstance.StartRotation += rotation;

	// TODO : для поворота добавить опцию - относительно ентра родителя вращать или своего
	vector3df pos = SceneNode->getAbsolutePosition();
	genInstance.StartPosition += pos;
	genInstance.StartPosition.rotateXZBy(-genInstance.StartRotation.Y, pos);

	_unitManager->CreateNewUnit(&genInstance, this);
}


// Получить описание поведения
UnitBehavior* UnitInstanceStandard::GetBehavior()
{
	return _behavior;
}

// Получить менджер юнитов
UnitManager* UnitInstanceStandard::GetUnitManager()
{
	return _unitManager;
}

// Добавить связь
void UnitInstanceStandard::AddMapping( UnitMappingBase* mapping )
{
	_mappings.push_back(mapping);
	mapping->grab();
}

TimerBase* UnitInstanceStandard::GetTimerByName( const stringc& name, bool createNewIfNotExist )
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

void UnitInstanceStandard::UpdateTimers()
{
	for (u32 i = 0; i < _timers.size(); i++)
	{
		_timers[i]->Tick();
	}
}


