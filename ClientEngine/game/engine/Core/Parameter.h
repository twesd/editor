#pragma once
#include "irrlicht.h"
#include "../../include/configureCompile.h"

using namespace irr;
using namespace core;
using namespace scene;

class Parameter
{
public:
	Parameter();

	// Изменить параметр
	void Change( stringc& newValue );

	// Параметр имеет значение в виде числа
	bool IsNumber() const;

	// Получить значение как float
	float GetAsFloat() const;

	// Имя параметра
	stringc Name;

	// Значение параметра
	stringc Value;

private:
	// Строка имеет значение в виде числа
	bool IsNumberString(const stringc& str) const;
};
