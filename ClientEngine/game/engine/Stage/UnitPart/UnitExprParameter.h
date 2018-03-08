#pragma once
#include "../../Core/Base.h"
#include "../../Core/Parsers/ParserExpression.h"
#include "../../Core/Parsers/ParserAS.h"

class UnitInstanceStandard;

class UnitExprParameter
{
public:
	UnitExprParameter();

	// Установить экземпляр юнита
	void SetUnitInstance(UnitInstanceStandard* unitInstance);

	void SetParser(ParserAS* parser);

	// Изменить параметр
	bool ChangeParameter( Parameter* unitParam, stringc newValue );

	// Имя параметра
	stringc Name;
	
private:
	ParserExpression _parserExpr;
	ParserAS* _parser;
	
	// Выражение для параметра
	stringc _expression;

	UnitInstanceStandard* _unitInstance;
};
