#pragma once
#include "../../Core/Base.h"

class UnitParameters : public Base
{
public:
	UnitParameters(SharedParams_t params, bool isGlobal);

	~UnitParameters();

	// Установить параметр
	void Set(stringc& name, stringc& value, bool ignoreExpr = false);
	
	// Получить параметр
	stringc Get(stringc name);

	// Имеется ли заданный параметр
	bool HasParameter(stringc name);

	// Имеется ли заданный параметр, и совподают ли значения
	bool HasAndEqualParameter(Parameter* parameter);

	// Получить количество
	u32 Count();

	// Получить параметр
	Parameter* GetByIndex(u32 index);

	// Поиск параметра по имени
	Parameter* FindParameter(stringc& name);

private:	
	// Являются ли параметры глобальными
	bool _isGlobal;

	core::array<Parameter*> _parameters;
};
