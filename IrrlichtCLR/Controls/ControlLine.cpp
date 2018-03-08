#include "ControlLine.h"
#include "Core/TextureWorker.h"

ControlLine::ControlLine(SharedParams_t params) : IControl(params)
{
	IsVisible = true;
	_color = 0;
}

ControlLine::~ControlLine(void)
{
}

void ControlLine::Init(stringc name, vector2di startPnt, vector2di endPnt, int width, u32 color)
{
	Name = name;
	_startPnt = startPnt;
	_endPnt = endPnt;
	_width = width;
	_color = color;
}

void ControlLine::Update(reciverInfo_t *reciverInfo) 
{
	if(!IsVisible) return;
}

void ControlLine::Draw()
{
	if(!IsVisible) 
	{
		return;
	}
	position2di startPos = position2di(_startPnt.X, _startPnt.Y);
	position2di endPos = position2di(_endPnt.X, _endPnt.Y);
	Driver->draw2DLine(startPos, endPos, video::SColor(_color));
}

void ControlLine::SetPosition(position2di newPosition)
{

}

// Получить границы контрола
irr::core::recti ControlLine::GetBounds()
{
	return recti();
}

// Получить позицию контрола
irr::core::position2di ControlLine::GetPosition()
{
	return position2di();
}
