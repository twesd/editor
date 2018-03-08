#pragma once
#include "irrlicht.h"
#include "../../include/configureCompile.h"
#include "Parameter.h"

using namespace irr;
using namespace core;
using namespace scene;

class UserSettings
{
public:
	UserSettings();

	// Получить значение как string
	stringc GetValue(stringc name);

	// Получить значение как string
	f32 GetValueAsFloat(stringc name);

	// Утсановить текстовую настройку
	void SetTextSetting(stringc name, stringc paramValue);

	// Получить все параметры
	core::array<Parameter> GetParameters();

	// Очистить все параметры
	void Clear();

private:
	core::array<Parameter> _parameters;
};
