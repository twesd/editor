#include "UnitClause.h"
#include "UnitBehavior.h"
#include "UnitEvent/UnitEventOperator.h"

UnitClause::UnitClause(SharedParams_t params) : Base(params),
	_unitEvents(), _parser()
{
	_parameters = new UnitParameters(params, false);
	_globalParameters = new UnitParameters(params, true);
	_behavior = NULL;
	_behaviorParameters = NULL;
}

UnitClause::~UnitClause(void)
{
	if(_parameters != NULL) _parameters->drop();
	if(_globalParameters != NULL) _globalParameters->drop();

	for(u32 i = 0; i < _unitEvents.size(); i++)
		_unitEvents[i]->drop();
}

// Добавить параметр
void UnitClause::AddParameter(bool isGlobal, stringc name, stringc value)
{
	if(isGlobal)
		_globalParameters->Set(name, value, true);
	else
		_parameters->Set(name, value, true);
}

// Добавить событие
// Объект захватывается (grab) в данном методе
void UnitClause::AddUnitEvent(UnitEventBase* unitEvent)
{
	_DEBUG_BREAK_IF(_behavior == NULL)
	unitEvent->SetBehavior(_behavior);
	_unitEvents.push_back(unitEvent);
	unitEvent->grab();
}

// Выполняются ли условия
bool UnitClause::IsApprove(core::array<Event_t*>& events)
{
	// Проверяем параметры...
	//
	u32 localCount = _parameters->Count();
	if(localCount > 0)
	{
		bool paramApproved = true;
		for (u32 i = 0; i < localCount; i++)
		{
			Parameter* param = _parameters->GetByIndex(i);			
			Parameter* paramBehavior = _behaviorParameters->FindParameter(param->Name);
			if (paramBehavior == NULL)
			{
				if(param->Value.size() == 0)
				{
					// пустое значение задано
					paramApproved = true;
				}
				else 
				{
					paramApproved = false;
				}				
				break;
			}
			if (param->Value[0] == '{')
			{
				_parser.SetVariable("ThisParam", paramBehavior->Value);
				// Убираем скобки {...}
				_parser.SetExpression(param->Value.subString(1, param->Value.size() - 2));
				if(_parser.EvalAsFloat() == 0)
				{
					paramApproved = false;
					break;
				}
			}
			else if (param->Value != paramBehavior->Value)
			{
				paramApproved = false;
				break;
			}
		}
		if(!paramApproved) return false;
	}
	u32 globalCount = _globalParameters->Count();
	if (globalCount > 0)
	{
		bool paramApproved = true;		
		for (u32 i = 0; i < globalCount; i++)
		{
			Parameter* param = _globalParameters->GetByIndex(i);
			if(!HasGlobalParameter(param->Name) || 
				(param->Value != GetGlobalParameter(param->Name)))
			{
				paramApproved = false;
				break;			
			}
		}
		if(!paramApproved) return false;
	}

	// Проверяем события
	//
	if(_unitEvents.size() > 0)
	{
		bool eventsApprove = true;
		OperatorTypeEnum opType = OPERATOR_TYPE_AND;
		for(u32 i = 0; i < _unitEvents.size(); i++)
		{
			UnitEventBase* unitEvent = _unitEvents[i];
			if(unitEvent->GetEventType() == UNIT_EVENT_TYPE_OPERATOR)
			{
				UnitEventOperator* unitEventOp = (UnitEventOperator*)unitEvent;
				opType = unitEventOp->Operator;
				continue;
			}

			bool evtRes = unitEvent->IsApprove(events);
			if(opType == OPERATOR_TYPE_AND)
				eventsApprove = eventsApprove && evtRes;
			else if(opType == OPERATOR_TYPE_OR)
				eventsApprove = eventsApprove || evtRes;

			opType = OPERATOR_TYPE_AND;
		}
		if(!eventsApprove) return false;
	}

	return true;	
}

// Устаноить поведение, которому принадлежит данное условие
// Установка ссылки на объект
void UnitClause::SetBehavior( UnitBehavior* behavior )
{
	_behavior = behavior;
	_behaviorParameters = _behavior->GetParameters();
}

// Начало действия
void UnitClause::Begin()
{
	// Оповещаем события
	//
	if(_unitEvents.size() > 0)
	{
		for(u32 i = 0; i < _unitEvents.size(); i++)
		{
			UnitEventBase* unitEvent = _unitEvents[i];
			unitEvent->Begin();
		}
	}
}

// Обновление данных
void UnitClause::Update( scene::ISceneNode* node, core::array<Event_t*>& events )
{
	
}

