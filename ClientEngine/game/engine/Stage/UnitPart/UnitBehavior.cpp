#include "UnitBehavior.h"
#include "../UnitInstance/UnitInstanceStandard.h"
#include "../../ParserExtensions/ParserExtensionUnits.h"

//#define DEBUG_BEHAVIOR

UnitBehavior::UnitBehavior(SharedParams_t params) : Base(params),
	_actions(), 
	_candidateActions(), 
	_exprParametersExt(),
	OwnerPath(),
	_script()
{
	_currentAction = NULL;
	UnitModel = NULL;
	_node = NULL;
	_parser = NULL;
	_animationEnd = false;
	NodeId = 0;

	_parameters = new UnitParameters(params, false);
	_actionUpdater = new UnitActionUpdater(params);
}

UnitBehavior::~UnitBehavior(void)
{
	// Этой установки не надо, так как drop вызывется в ISceneNode при удалении
	// Оставил, чтобы следить затем чтобы UnitBehavior убивался с первого drop
	if(_node != NULL && _node->getType() == ESNT_ANIMATED_MESH)
	{
		IAnimatedMeshSceneNode* animNode = (IAnimatedMeshSceneNode*)_node;
		animNode->setAnimationEndCallback(NULL);
	}
	
	for(u32 i = 0; i < _animations.size(); i++)
	{
		_animations[i]->drop();
	}

	for(u32 i = 0; i < _actions.size(); i++)
	{
		_actions[i]->drop();
	}

	for(u32 i = 0; i < _exprParametersExt.size(); i++)
	{
		delete _exprParametersExt[i];
	}

	_parameters->drop();
	_actionUpdater->drop();
}

// Добавить описания
void UnitBehavior::SetActions(core::array<UnitAction*> actions)
{
	// Повторное назначение действий запрещено
	_DEBUG_BREAK_IF(_actions.size() != 0)

	for(u32 i = 0; i < actions.size(); i++)
	{
		_actions.push_back(actions[i]);
	}
}

// Добавить доступные анимации
void UnitBehavior::SetAnimations(core::array<UnitAnimation*> animations)
{
	// Повторное назначение анимаций запрещено
	_DEBUG_BREAK_IF(_animations.size() != 0)

	for(u32 i = 0; i < animations.size(); i++)
	{
		_animations.push_back(animations[i]);
	}
}


// Добавить условие для внешнего параметра
void UnitBehavior::AddExprParameterExt( UnitExprParameter* exprParam )
{
	_exprParametersExt.push_back(exprParam);
}

// Добавить условие для внешнего параметра
void UnitBehavior::AddStartParameter( stringc name, stringc val )
{
	_parameters->Set(name, val);
}

// Задать скрипт
void UnitBehavior::SetParser(ParserAS* parser)
{
	_parser = parser;
}

// Установить экземпляр юнита которому принадлежит поведения
void UnitBehavior::SetUnitInstance( UnitInstanceStandard* unitInstance )
{
	_unitInstance = unitInstance;
	for(u32 i = 0; i < _actions.size(); i++)
	{
		_actions[i]->SetUnitInstance(unitInstance);
	}
	for(u32 i = 0; i < _exprParametersExt.size(); i++)
	{
		_exprParametersExt[i]->SetUnitInstance(unitInstance);
	}
	_actionUpdater->SetBehavior(unitInstance->GetBehavior());
}

// Завершение загрузки
void UnitBehavior::LoadComplete()
{
	// Говорим основным условиям о завершении загрузки
	for(u32 i = 0; i < _actions.size(); i++)
	{
		_actions[i]->GetUnitClause()->Begin();
	}
}

// Обработка событий
void UnitBehavior::HandleEvent( core::array<Event_t*>& events )
{
	// Обновляем модуль
	if (ModuleName.size() > 0)
	{
		_moduleManager->Execute(ModuleName);
	}

	// Обновляем данные
	if(_currentAction != NULL)
	{
		_currentAction->Update(_node, events);
	}

	// Проверка на отмену действия
	if(_actionUpdater->CurrentActionNeedBreak(_currentAction, events))
	{
		_currentAction = NULL;
	}

	// Получение списка кандидатов
	//
	for(u32 i = 0; i < _actions.size(); i++)
	{
		UnitAction* action = _actions[i];
		if(action->IsApprove(events))
		{
			_candidateActions.push_back(action);
		}
	}
	if(_candidateActions.size() == 0)
		return;

	// Выбор кандидата с высшим приоритетом
	//
	UnitAction* bestAction = _candidateActions[0];
	for(u32 i = 0; i < _candidateActions.size(); i++)
	{
		UnitAction* action = _candidateActions[i];
		if(action->Priority > bestAction->Priority)
		{
			bestAction = action;
		}
	}
	// Очищаем список кандидатов
	_candidateActions.set_used(0);

	_actionUpdater->ApplyAction(_node, bestAction, &_currentAction, events);

	if(_parser != NULL)
	{
		_parser->GetUserData()->BaseData = _unitInstance;
		_parser->GetUserData()->UnitInstanceData = _unitInstance;
		_parser->Execute();
	}	
}

// Отменить текущие действие
bool UnitBehavior::CurrentActionNeedBreak( core::array<Event_t*>& events )
{
	return _actionUpdater->CurrentActionNeedBreak(_currentAction, events);
}


// Применить действие
bool UnitBehavior::ApplyAction( stringc actionName )
{
	UnitAction* action = GetActionByName(actionName);
	if(action == NULL) return false;
	core::array<Event_t*> events;
	return _actionUpdater->ApplyAction(_node, action, &_currentAction, events);
}

// Завершение анимации
void UnitBehavior::OnAnimationEnd( scene::IAnimatedMeshSceneNode* node )
{
#ifdef DEBUG_BEHAVIOR
	if(_currentAction != NULL)
		printf("[UnitBehavior(%s)] Behavior[%s] end \n",OwnerPath.c_str(), _currentAction->Name.c_str());
#endif
	_animationEnd = true;
}

// Назначить модель
void UnitBehavior::SetSceneNode( ISceneNode* node )
{
	if (_node != NULL && _node->getType() == ESNT_ANIMATED_MESH)	
	{
		IAnimatedMeshSceneNode* animNode = (IAnimatedMeshSceneNode*)_node;
		animNode->setAnimationEndCallback(NULL);	
	}
	_node = node;
	if(node != NULL)
	{
		if (node->getType() == ESNT_ANIMATED_MESH)	
		{
			IAnimatedMeshSceneNode* animNode = (IAnimatedMeshSceneNode*)node;
			animNode->setAnimationEndCallback(this);	
		}		
		node->setID(NodeId);
	}
}

// Получить модель
ISceneNode* UnitBehavior::GetSceneNode()
{
	_DEBUG_BREAK_IF(_node == NULL)
	return _node;
}

// Получить текущее действие
UnitAction* UnitBehavior::GetCurrentAction()
{
	return _currentAction;
}

// Получить экземпляр юнита
UnitInstanceStandard* UnitBehavior::GetUnitInstance()
{
	return _unitInstance;
}

// Добавить претендента на след. действие
void UnitBehavior::AddCandidateNextAction( stringc actionName )
{
	UnitAction* action = GetActionByName(actionName);
	_DEBUG_BREAK_IF(action == NULL)
	_candidateActions.push_back(action);
}

// Получить действие по имени
UnitAction* UnitBehavior::GetActionByName( stringc &actionName )
{
	for(u32 i = 0; i < _actions.size(); i++)
	{
		UnitAction* action = _actions[i];
		if (action->Name == actionName)
		{
			return action;
		}
	}	
	return NULL;

}


// Получить параметры
UnitParameters* UnitBehavior::GetParameters()
{
	return _parameters;
}

const Parameter* UnitBehavior::GetParameter( stringc name )
{
	Parameter* unitParam = _parameters->FindParameter(name);
	return unitParam;
}

// Изменить параметр
bool UnitBehavior::SetParameter( stringc name, stringc val )
{
	Parameter* unitParam = _parameters->FindParameter(name);
	if(unitParam == NULL)
	{
		_parameters->Set(name, val);
		unitParam = _parameters->FindParameter(name);
	}
	_DEBUG_BREAK_IF(unitParam == NULL)

	UnitExprParameter* exprParam = NULL;
	for (u32 i = 0; i < _exprParametersExt.size() ; i++)
	{
		if (name == _exprParametersExt[i]->Name)
		{
			exprParam = _exprParametersExt[i];
			break;
		}
	}

	if(exprParam != NULL)
	{
		return exprParam->ChangeParameter(unitParam, val);
	}

	unitParam->Change(val);
	return true;
}

// Завершина ли анимация
bool UnitBehavior::GetAnimationEnd()
{
	return _animationEnd;
}

// Пометить, что начилась новая анимация
void UnitBehavior::NotifyAnimationStart()
{
	_animationEnd = false;
}

void UnitBehavior::SetModuleManager( ModuleManager* moduleManager )
{
	_DEBUG_BREAK_IF(moduleManager == NULL)
	_moduleManager = moduleManager;
}

UnitAnimation* UnitBehavior::GetAnimationById( stringc& animId )
{
	if (animId == "")
	{
		return NULL;
	}
	
	for(u32 i = 0; i < _animations.size(); i++)
	{
		if (_animations[i]->Id == animId)
		{
			return _animations[i];
		}		
	}
	return NULL;
}





