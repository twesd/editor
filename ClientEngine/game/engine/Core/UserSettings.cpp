#include "UserSettings.h"


UserSettings::UserSettings() : _parameters()
{

}

// Получить значение как string
stringc UserSettings::GetValue(stringc name)
{
	for (u32 i = 0; i < _parameters.size() ; i++)
	{
		if(_parameters[i].Name == name)
			return _parameters[i].Value;
	}
	return "";
}

// Получить значение как string
f32 UserSettings::GetValueAsFloat(stringc name)
{
	return core::fast_atof(GetValue(name).c_str());
}

// Утсановить текстовую настройку
void UserSettings::SetTextSetting( stringc name, stringc paramValue )
{
	for (u32 i = 0; i < _parameters.size() ; i++)
	{
		if(_parameters[i].Name == name)
		{
			_parameters[i].Value = paramValue;
			return;
		}
	}
	Parameter param;
	param.Name = name;
	param.Value = paramValue;
	_parameters.push_back(param);
}

core::array<Parameter> UserSettings::GetParameters()
{
	return _parameters;
}

void UserSettings::Clear()
{
	_parameters.clear();
}
