#pragma once
#include "global.h"
#include "Event.h"

class Base : public virtual IReferenceCounted
{
public:
	Base(SharedParams_t params);
	virtual ~Base();

	void SetGameParameter(const stringc& name,stringc value);

	stringc GetGameParameter(const stringc& name);

	// Установка глобального параметра
	void SetGlobalParameter(const stringc& name,stringc value);

	stringc GetGlobalParameter(const stringc& name);

	f32 GetGlobalParameterAsFloat(const stringc& name);

	bool GetGlobalParameterAsBool(const stringc& name);

	bool HasGlobalParameter(stringc name);

	void ResetGlobalParameters();	

	// Получить настройки
	UserSettings* GetUserSettings();

	// Получить общие параметры
	SharedParams_t GetSharedParams();

	// Получить объект событий
	Event* GetEvent();

	///////////////////////////// Дополнительные функции ///////////////////////////////////

	inline u32 GetNowTime()
	{
		return Timer->getTime();
	}

	//Возращает изменение. Считаем что переменная должна измениться на Val за 1 сек.
	f32 GetChangeValue(f32 val,u32 time = 1000);

	
protected:
	///////////////////////////// Свойства ///////////////////////////////////
	SharedParams_t			SharedParams;
	int						ButtonState;

	//irrlicht members
	IrrlichtDevice*			Device;
	video::IVideoDriver*	Driver;
	scene::ISceneManager*	SceneManager;
	//gui::IGUIEnvironment*	GUIEnv;
	io::IFileSystem*		FileSystem;
	ITimer*					Timer;

	Event*					EventBase;
};
