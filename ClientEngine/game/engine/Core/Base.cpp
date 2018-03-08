
#include "Base.h"


Base::Base(SharedParams_t params)
{
	Device =	params.Device;
	Driver	=	params.Driver;
	SceneManager = params.SceneManager;
	Timer	=	params.Timer;
	EventBase	=	(Event *)params.Event;
	//GUIEnv = params.guiEnv;
	FileSystem = params.FileSystem;
#ifdef DEBUG_MESSAGE_PRINT
	if(!Device || !Driver || !SceneManager || !EventBase)
		printf("[ERROR]<Base::Base>(nul pointer)\n");
#endif
	ButtonState = -1;
	SharedParams = params;
}


Base::~Base()
{
}

void Base::SetGameParameter(const stringc& name,stringc value)
{
	if(name.size() == 0) 
	{
		return;
	}

	for(u32 i = 0; i < SharedParams.GameParams->size(); i++)
	{
		if(SharedParams.GameParams[0][i].Name == name)
		{
			SharedParams.GameParams[0][i].Value = value;			
			return;
		}
	}

	// Если ничего не найдено
	Parameter item;
	item.Name = name;
	item.Value = value;
	SharedParams.GameParams->push_back(item);
}

stringc Base::GetGameParameter(const stringc& name)
{
	for(u32 i = 0; i < SharedParams.GameParams->size(); i++)
	{
		if(SharedParams.GameParams[0][i].Name == name)
		{
			return SharedParams.GameParams[0][i].Value;			
		}
	}	
	return "";
}

void Base::SetGlobalParameter(const stringc& name,stringc value)
{
	if(name.size() == 0) 
	{
		return;
	}

	for(u32 i = 0; i < SharedParams.GlobalParams->size(); i++)
	{
		if(SharedParams.GlobalParams[0][i].Name == name)
		{
			SharedParams.GlobalParams[0][i].Value = value;			
			return;
		}
	}

	// Если ничего не найдено
	Parameter item;
	item.Name = name;
	item.Value = value;
	SharedParams.GlobalParams->push_back(item);
}

stringc Base::GetGlobalParameter(const stringc& name)
{
	for(u32 i = 0; i < SharedParams.GlobalParams->size(); i++)
	{
		if(SharedParams.GlobalParams[0][i].Name == name)
		{
			return SharedParams.GlobalParams[0][i].Value;			
		}
	}	
	return "";
}

f32 Base::GetGlobalParameterAsFloat(const stringc& name)
{
	stringc val = GetGlobalParameter(name);
	if(val == "") return 0;
	return core::fast_atof(val.c_str());
}

bool Base::GetGlobalParameterAsBool(const stringc& name)
{
	stringc val = GetGlobalParameter(name);
	return (val == "true");
}

bool Base::HasGlobalParameter(stringc name)
{	
	s32 pos = name.find("=");
	stringc val;
	if(pos > 0)
	{
		val = name.subString(pos+1,name.size());
		name = name.subString(0,pos);
	}
	for(u32 i = 0; i < SharedParams.GlobalParams->size(); i++)
	{
		if(SharedParams.GlobalParams[0][i].Name == name)
		{
			if(val != "" && val != SharedParams.GlobalParams[0][i].Value)
				return false;
			return true;			
		}
	}	
	return false;
}

void Base::ResetGlobalParameters()
{
	SharedParams.GlobalParams->clear();
}

// Получить настройки
UserSettings* Base::GetUserSettings()
{
	return SharedParams.Settings;
}

// Получить общие параметры
SharedParams_t Base::GetSharedParams()
{
	return SharedParams;
}

// Получить объект событий
Event* Base::GetEvent()
{
	return EventBase;
}

/// <summary>
/// Возращает изменение. Считаем что переменная должна измениться на Val за 1 сек.
/// </summary>
f32 Base::GetChangeValue(f32 val,u32 time)
{
	if(time == 0) return 0;
	f32 coeff = (f32)(*SharedParams.TimeDiff) / (f32)time;
	return val * coeff;
}
