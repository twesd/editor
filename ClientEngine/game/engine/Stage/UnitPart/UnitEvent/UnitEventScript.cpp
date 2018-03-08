#include "UnitEventScript.h"
#include "../../UnitInstance/UnitInstanceStandard.h"
#include "../../../ParserExtensions/ParserExtensionUnits.h"

UnitEventScript::UnitEventScript(SharedParams_t params) : UnitEventBase(params)
{
	_parser = NULL;
}

UnitEventScript::~UnitEventScript(void)
{
}

// Выполняется ли условие
bool UnitEventScript::IsApprove( core::array<Event_t*>& events )
{
	_parser->GetUserData()->UnitInstanceData = Behavior->GetUnitInstance();
	_parser->GetUserData()->BaseData = Behavior->GetUnitInstance();	
	return _parser->EvalAsBool();
}

void UnitEventScript::SetParser( ParserAS* parser )
{
	_parser = parser;
}

