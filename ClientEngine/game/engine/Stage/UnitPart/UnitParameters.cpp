#include "UnitParameters.h"

UnitParameters::UnitParameters(SharedParams_t params, bool isGlobal) : Base(params),
	_isGlobal(isGlobal), _parameters()
{
}


UnitParameters::~UnitParameters()
{
	for (u32 i = 0; i < _parameters.size() ; i++)
	{
		delete _parameters[i];
	}
}

void UnitParameters::Set(stringc& name, stringc& value, bool ignoreExpr)
{	
	//SetGlobalParameter(name,value);
	Parameter* parameter = FindParameter(name);
	if (parameter == NULL)
	{
		// Добавляем новый параметр
		Parameter* newParam = new Parameter();
		newParam->Name = name;
		_parameters.push_back(newParam);
		parameter = _parameters.getLast();
	}
	// Если задано игнорировать выражение в значении параметра, то ...
	if (ignoreExpr)
	{
		parameter->Value = value;
	}
	else
	{
		parameter->Change(value);
	}
	
}

// Получить параметр
irr::core::stringc UnitParameters::Get( stringc name )
{
	Parameter* parameter = FindParameter(name);
	if (parameter != NULL)
	{
		return parameter->Value;
	}
	return "";
}

// Поиск параметра по имени
Parameter* UnitParameters::FindParameter( stringc& name )
{
	for (u32 i = 0; i <  _parameters.size(); i++)
	{
		if (_parameters[i]->Name == name)
		{
			return _parameters[i];
		}
	}
	return NULL;
}

// Имеется ли заданный параметр
bool UnitParameters::HasParameter( stringc name )
{
	return (FindParameter(name) != NULL);
}

// Получить количество
irr::u32 UnitParameters::Count()
{
	return _parameters.size();
}

// Получить параметр
Parameter* UnitParameters::GetByIndex( u32 index )
{
	return _parameters[index];
}

// Имеется ли заданный параметр, и совподают ли значения
bool UnitParameters::HasAndEqualParameter( Parameter* parameter )
{
	for (u32 i = 0; i <  _parameters.size(); i++)
	{
		if (_parameters[i]->Name == parameter->Name)
		{
			return (_parameters[i]->Value == parameter->Value);
		}
	}
	return false;
}





