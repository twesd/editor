#include "ControlBehavior.h"

ControlBehavior::ControlBehavior(SharedParams_t params, ControlManager* manager) : IControl(params),
	_parser(NULL), _moduleName()
{

}

ControlBehavior::~ControlBehavior(void)
{
}

void ControlBehavior::Update(reciverInfo_t *reciverInfo) 
{
	
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
