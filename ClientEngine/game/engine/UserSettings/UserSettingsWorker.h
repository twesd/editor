#pragma once
#include "../Core/Base.h"

class UserSettingsWorker
{
public:
	// Сохранить настройки
	static void Save(SharedParams_t sharedParams,UserSettings* settings);

	// Загрузить настройки
	static void Load(SharedParams_t sharedParams,UserSettings* settings);
};
