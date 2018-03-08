#include "UnitBlockAction.h"
#include "../UnitInstance/UnitInstanceStandard.h"
#include "../../Core/Animators/IExtendAnimator.h"
#include "../Paths/PathAnimator.h"

UnitBlockAction::UnitBlockAction(SharedParams_t params) : UnitAction(params),
	_actions(), _candidateActions(), _currentAction(NULL)
{
	_actionUpdater = new UnitActionUpdater(params);
}

UnitBlockAction::~UnitBlockAction(void)
{
	for(u32 i = 0; i < _actions.size(); i++)
	{
		_actions[i]->drop();
	}

	_actionUpdater->drop();
}

// Добавить действие
void UnitBlockAction::AddAction( UnitAction* action )
{
	_actions.push_back(action);
}

// Обновление данных
void UnitBlockAction::Update( scene::ISceneNode* node, core::array<Event_t*>& events )
{
	UnitAction::Update(node, events);

	// Обновляем данные
	if(_currentAction != NULL)
	{
		_currentAction->Update(node, events);
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
	{
		return;
	}

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

	_actionUpdater->ApplyAction(node, bestAction, &_currentAction, events);
}

// Необходимо ли отменить текущее поведение
bool UnitBlockAction::NeedBreak( core::array<Event_t*>& events, bool animationEnd, ISceneNode* node )
{
	if(UnitAction::NeedBreak(events, animationEnd, node))
	{
		return true;
	}	
	if (_currentAction == NULL)
	{
		return true;
	}
	return false;
}

// Установить экземпляр юнита
void UnitBlockAction::SetUnitInstance( UnitInstanceStandard* unitInstance )
{
	UnitAction::SetUnitInstance(unitInstance);

	_actionUpdater->SetBehavior(unitInstance->GetBehavior());
	for (u32 i = 0; i < _actions.size() ; i++)
	{
		_actions[i]->SetUnitInstance(unitInstance);
	}
}

// Начало действия
void UnitBlockAction::Begin()
{
	UnitAction::Begin();

	_currentAction = NULL;
	_candidateActions.clear();
}

// Получить текущее действие
UnitAction* UnitBlockAction::GetCurrentAction()
{
	return _currentAction; 
}
