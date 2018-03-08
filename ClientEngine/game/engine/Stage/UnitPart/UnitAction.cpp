#include "UnitAction.h"
#include "../UnitInstance/UnitInstanceStandard.h"
#include "../../Core/Animators/IExtendAnimator.h"
#include "../Paths/PathAnimator.h"
#include "../Paths/PathSceneNodeAnimator.h"
#include "../../ParserExtensions/ParserExtensionUnits.h"

UnitAction::UnitAction(SharedParams_t params) : Base(params),
	Name(), FilterEvent(), _executes()
{
	Priority = 0;
	_animation = NULL;
	_clause = NULL;
	_breakClause = NULL;
	UnitInstance = NULL;
	NoChangeCurrentAction = false;
	_parser = NULL;
}

UnitAction::~UnitAction(void)
{
	if(_clause != NULL) _clause->drop();
	if(_breakClause != NULL) _breakClause->drop();

	for(u32 i =0; i < _executes.size(); i++)
	{
		_executes[i]->drop();
	}
}

// Назначить условия
void UnitAction::SetClause(UnitClause* clause)
{
	if(_clause != NULL) _clause->drop();
	_clause = clause;
	if(_clause == NULL) return;
	_clause->grab();
}


// Назначить условия отмены
void UnitAction::SetBreak( UnitActionBreak* breakClause, ScriptCache* scriptCache )
{
	if(_breakClause != NULL) 
	{
		_breakClause->drop();
	}
	_breakClause = breakClause;
	if(breakClause == NULL) 
	{
		return;
	}
	_breakClause->grab();

	if(_breakClause->ScriptFileName != "")
	{
		_parser = scriptCache->GetItem(_breakClause->ScriptFileName);
	}
}


// Добавить действие
void UnitAction::AddExecute( ExecuteBase* execute )
{
	_DEBUG_BREAK_IF(UnitInstance != NULL)
	_DEBUG_BREAK_IF(execute == NULL)
	_executes.push_back(execute);
	execute->SetUnitAction(this);
	execute->grab();
}

// Установить экземпляр юнита
void UnitAction::SetUnitInstance( UnitInstanceStandard* unitInstance )
{
	_DEBUG_BREAK_IF(unitInstance == NULL)
	_DEBUG_BREAK_IF(UnitInstance != NULL)

	UnitInstance = unitInstance;
	
	for (u32 i = 0; i < _executes.size() ; i++)
	{
		_executes[i]->SetUnitInstance(UnitInstance);
	}
}


// Выполняются ли условия
bool UnitAction::IsApprove( core::array<Event_t*>& events )
{
	return _clause->IsApprove(events);
}

// Начало действия
void UnitAction::Begin()
{
	for (u32 i = 0; i < _executes.size() ; i++)
	{
		_executes[i]->Begin();
	}
}

// Выполнить действия
void UnitAction::Execute(scene::ISceneNode* node, core::array<Event_t*>& events)
{	
	// Назначаем анимацию и трансформацию
	if (_animation != NULL)
	{
		_animation->Execute(node);
	}

	// Выполняем дополнительные действия	
	//
	Update(node, events);
}


// Обновление данных
void UnitAction::Update(scene::ISceneNode* node, core::array<Event_t*>& events)
{
	// Обновляем дополнительные действия
	//
	for (u32 i = 0; i < _executes.size() ; i++)
	{
		ExecuteBase* executeItem = _executes[i];
		executeItem->Update(node, events);

		if(executeItem->IsCanRun && !executeItem->IsRunned)
		{
			executeItem->Run(node, events);
		}
	}
}

// Необходимо ли отменить текущее поведение
bool UnitAction::NeedBreak( core::array<Event_t*>& events, bool animationEnd, ISceneNode* node )
{	
	if(_breakClause->StartClauseNotApproved ||
		_breakClause->StartClauseApproved)
	{
		bool eventApproved = _clause->IsApprove(events);
		// Отмена, если начальные условия выполняются
		if(_breakClause->StartClauseApproved && eventApproved) 
		{
			return true;
		}
		// Отмена, если начальные условия не выполняются
		if(_breakClause->StartClauseNotApproved && !eventApproved) 
		{
			return true;
		}
	}

	// Отмена, если поведение должно завершится после выполнения
	if(_breakClause->IsExecuteOnly)
	{
		return true;
	}
	// Отмена, если обноружено завершение анимации
	if(_breakClause->AnimationEnd && animationEnd)
	{
		return true;
	}
	// Отмена, если завершился аниматор
	if(_breakClause->AnimatorEnd != UnitActionBreak::AnimatorNone)
	{
		const core::list<ISceneNodeAnimator*>& animators = node->getAnimators();
		core::list<ISceneNodeAnimator*>::ConstIterator it = animators.begin();
		for (; it!=animators.end(); ++it)
		{
			ISceneNodeAnimator* animator = *it;
			IExtendAnimator* animatorEx = dynamic_cast<IExtendAnimator*>(animator);
			if (animatorEx != NULL && animatorEx->AnimationEnd())
			{
				if (_breakClause->AnimatorEnd == UnitActionBreak::AnimatorAny)
				{
					return true;
				}
				if (_breakClause->AnimatorEnd == UnitActionBreak::AnimatorMoveToPoint)
				{
					if (dynamic_cast<PathAnimator*>(animator) != NULL)
					{
						return true;
					}
				}
				else if(_breakClause->AnimatorEnd == UnitActionBreak::AnimatorMoveToSceneNode)
				{
					if (dynamic_cast<PathSceneNodeAnimator*>(animator) != NULL)
					{
						return true;
					}
				}
				else
				{
					_DEBUG_BREAK_IF(true)
				}
			}
		}
	}
	if(_parser != NULL)
	{
		_parser->GetUserData()->UnitInstanceData = UnitInstance;
		_parser->GetUserData()->BaseData = UnitInstance;
		return (_parser->EvalAsBool() != 0);
	}
	return false;
}

// Установить анимацию
void UnitAction::SetAnimation( UnitAnimation* unitAnimation )
{
	_animation = unitAnimation;
}

// Имеет ли поведение анимацию
bool UnitAction::HasAnimation()
{
	return (_animation != NULL);
}

// Получить экземпляр юнита которому принадлежит поведения
UnitInstanceStandard* UnitAction::GetUnitInstance()
{
	return UnitInstance;
}

UnitClause* UnitAction::GetUnitClause()
{
	return _clause;
}
