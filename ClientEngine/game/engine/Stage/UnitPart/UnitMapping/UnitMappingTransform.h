#pragma once

#include "Core/CompareType.h"
#include "Core/Parsers/ParserExpression.h"
#include "UnitMappingBase.h"
#include "../UnitParameters.h"

class UnitMappingTransform : public UnitMappingBase
{
public:
	UnitMappingTransform(SharedParams_t params);
	virtual ~UnitMappingTransform(void);	
	
	// Выполнить обработку
	virtual void Update( );

	// Установить внешние параметры
	void SetExternalParameters(UnitParameters* parameters);

	stringc ScaleX;

	stringc ScaleY;

	stringc ScaleZ;

	stringc PositionX;

	stringc PositionY;

	stringc PositionZ;

	stringc RotationX;

	stringc RotationY;

	stringc RotationZ;

private:
	// Задать необходимые параметры
	void SetVariablesFromExpr(UnitParameters* parameters, stringc& expr);

	// Внешние параметры
	UnitParameters* _externalParameters;

	ParserExpression _parserExpr;
};
