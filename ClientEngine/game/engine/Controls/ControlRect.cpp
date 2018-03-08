#include "ControlRect.h"
#include "Core/TextureWorker.h"

ControlRect::ControlRect(SharedParams_t params) : IControl(params)
{
	IsVisible = true;
	_color = 0;
	_outline = true;
}

ControlRect::~ControlRect(void)
{
}

void ControlRect::Init(stringc name, position2di position, int width, int height, u32 color, bool outline)
{
	Name = name;
	_position = position;
	_size.Width = width;
	_size.Height = height;
	_color = color;
	_outline = outline;
}

void ControlRect::Update(reciverInfo_t *reciverInfo) 
{
	if(!IsVisible) return;
}

void ControlRect::Draw()
{
	if(!IsVisible) 
	{
		return;
	}
	core::rect<s32> rectImg;
	rectImg.LowerRightCorner.X = _position.X + _size.Width;
	rectImg.LowerRightCorner.Y = _position.Y + _size.Height;
	rectImg.UpperLeftCorner.X = _position.X;
	rectImg.UpperLeftCorner.Y = _position.Y;
	if(_outline)
		Driver->draw2DRectangleOutline(rectImg, video::SColor(_color));
	else 
		Driver->draw2DRectangle(video::SColor(_color), rectImg);
}

void ControlRect::SetPosition(position2di newPosition)
{
	_position = newPosition;

}

// Получить границы контрола
irr::core::recti ControlRect::GetBounds()
{
	return recti();
}

// Получить позицию контрола
irr::core::position2di ControlRect::GetPosition()
{
	position2di position = _position;
	return position;
}
