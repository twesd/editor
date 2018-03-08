#include "Parameter.h"
#include "Parsers/ParserExpression.h"

Parameter::Parameter() : Name(), Value()
{
}

// Изменить параметр
void Parameter::Change( stringc& newValue )
{
	// Если новое значение является выражением, то ...
	if (newValue[0] == '{')
	{
 		ParserExpression parser;
		// Убираем скобки {...}
		parser.SetExpression(newValue.subString(1, newValue.size() - 2));
		parser.SetVariable("ThisParam", Value);
		Value = parser.Eval();
	}
	else
	{
		Value = newValue;
	}
}

// Параметр имеет значение в виде числа
bool Parameter::IsNumber() const
{
	return IsNumberString(Value);
}

// Получить значение как float
float Parameter::GetAsFloat() const
{
	return core::fast_atof(Value.c_str());
}

// Строка имеет значение в виде числа
bool Parameter::IsNumberString( const stringc& str ) const
{
	if(str.size() == 0) return false;
	for (u32 i = 0; i < str.size() ; i++)
	{
		if(strchr("-0123456789.", str[i]) == NULL)
			return false;
	}
	return true;
}
