#include "UnitExprParameter.h"
#include "../UnitInstance/UnitInstanceStandard.h"
#include "../../ParserExtensions/ParserExtensionUnits.h"

UnitExprParameter::UnitExprParameter() :
	Name(), _expression(), _parserExpr()
{
	_parser = NULL;
	_unitInstance = NULL;
}


void UnitExprParameter::SetUnitInstance( UnitInstanceStandard* unitInstance )
{
	_unitInstance = unitInstance;
}


// Изменить параметр
bool UnitExprParameter::ChangeParameter( Parameter* unitParam, stringc newValue )
{
	// Если новое значение является выражением, то ...
	if (newValue[0] == '{')
	{
		// Убираем скобки {...}
		_parserExpr.SetExpression(newValue.subString(1, newValue.size() - 2));		
		_parserExpr.SetVariable("ThisParam", unitParam->Value);
		newValue = _parserExpr.Eval();
	}

	ParserUserData* userData = _parser->GetUserData();
	userData->BaseData = _unitInstance;
	userData->UnitInstanceData = _unitInstance;

	_parser->SetFuncDecl("string main(string &in, string &in)");
	std::string newVal(newValue.c_str());
	std::string oldValue(unitParam->Value.c_str());

	_parser->SetArgString(0, &newVal); 
	_parser->SetArgString(1, &oldValue);

	_parser->ExecuteCustom();
	stringc resultVal = _parser->GetReturnAsString();
	unitParam->Change(resultVal);	
	return true;
}

void UnitExprParameter::SetParser(ParserAS* parser)
{
	_parser = parser;
}
