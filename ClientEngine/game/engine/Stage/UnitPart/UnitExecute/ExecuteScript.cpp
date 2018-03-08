#include "ExecuteScript.h"
#include "../../UnitInstance/UnitInstanceStandard.h"
#include "../../../ParserExtensions/ParserExtensionUnits.h"

ExecuteScript::ExecuteScript(SharedParams_t params) : ExecuteBase(params),
	_parser(NULL), _unitInstance(NULL)
{
}

ExecuteScript::~ExecuteScript()
{
}

// Выполнить действие
void ExecuteScript::Run( scene::ISceneNode* node, core::array<Event_t*>& events )
{
	ExecuteBase::Run(node, events);

	_parser->GetUserData()->UnitInstanceData = _unitInstance;
	_parser->GetUserData()->BaseData = _unitInstance;
	_parser->Execute();
}

// Установить экземпляр юнита
void ExecuteScript::SetUnitInstance( UnitInstanceStandard* unitInstance )
{
	_unitInstance = unitInstance;
}

void ExecuteScript::SetParser(ParserAS* parser)
{
	_parser = parser;
}
