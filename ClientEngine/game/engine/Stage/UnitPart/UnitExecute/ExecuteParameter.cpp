#include "ExecuteParameter.h"
#include "../../UnitInstance/UnitInstanceStandard.h"

ExecuteParameter::ExecuteParameter(SharedParams_t params) : 
	ExecuteBase(params)
{
	_behavior = NULL;
}

ExecuteParameter::~ExecuteParameter()
{
}

// Добавить параметр
void ExecuteParameter::AddParameter( stringc name, stringc val )
{
	Parameter parameter;
	parameter.Name = name;
	parameter.Value = val;
	_parameters.push_back(parameter);
}

// Выполнить действие
void ExecuteParameter::Run(scene::ISceneNode* node, core::array<Event_t*>& events)
{
	ExecuteBase::Run(node, events);

	if(IsGlobal)
	{
		Parameter param;
		for (u32 i = 0; i < _parameters.size() ; i++)
		{			
			param.Value = GetGlobalParameter(_parameters[i].Name);
			param.Change(_parameters[i].Value);
			SetGlobalParameter(_parameters[i].Name, param.Value);
		}
	}
	else
	{
		UnitParameters* behaviorParams = _behavior->GetParameters();
		for (u32 i = 0; i < _parameters.size() ; i++)
		{
			behaviorParams->Set(_parameters[i].Name, _parameters[i].Value);
		}
	}
}

void ExecuteParameter::SetBehavior( UnitBehavior* behavior )
{
	_behavior = behavior;
}
