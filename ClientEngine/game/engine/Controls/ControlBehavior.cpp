#include "ControlBehavior.h"
#include "../ParserExtensions/ParserExtensionControls.h"
#include "../ParserExtensions/ParserExtensionUnits.h"

ControlBehavior::ControlBehavior(SharedParams_t params, ControlManager* manager) : IControl(params),
	_parser(NULL), _moduleName(), _moduleManager(NULL)
{

}

ControlBehavior::~ControlBehavior(void)
{
}

void ControlBehavior::Update(reciverInfo_t *reciverInfo) 
{
	if(_moduleName != "")
	{
		_moduleManager->Execute(_moduleName);
	}
	else
	{
		_parser->GetUserData()->ControlManagerData = CntrlManager;
		_parser->GetUserData()->ControlOwnerData = this;
		_parser->GetUserData()->BaseData = CntrlManager;
		_parser->Execute();
	}	
}

void ControlBehavior::Draw()
{
}


// Получить границы контрола
irr::core::recti ControlBehavior::GetBounds()
{
	return recti(0,0,0,0);
}

// Добавить действие
void ControlBehavior::SetParser( ParserAS* parser )
{
	_parser = parser;
}

// Получить позицию контрола
// Всегда возвращает position2di(0, 0)
irr::core::position2di ControlBehavior::GetPosition()
{
	return position2di(0, 0);
}

void ControlBehavior::SetPosition( position2di newPosition )
{
	// ничего не делаем
}

void ControlBehavior::SetModule( stringc name, ModuleManager* moduleManager )
{
	_DEBUG_BREAK_IF(name == "")
	_DEBUG_BREAK_IF(moduleManager == NULL)
	_moduleManager = moduleManager;
	_moduleName = name;
}

