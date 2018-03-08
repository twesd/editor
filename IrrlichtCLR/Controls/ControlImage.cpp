#include "ControlImage.h"
#include "Core/TextureWorker.h"

ControlImage::ControlImage(SharedParams_t params) : IControl(params)
{
	IsVisible = true;
	_texture = NULL;
}

ControlImage::~ControlImage(void)
{
}

void ControlImage::Init(stringc texturePath, position2di position)
{
	_texture = TextureWorker::GetTexture(Driver, texturePath, true);
	if(_texture == NULL)
	{
		return;
	}
	_sizeTexture = _texture->getOriginalSize();
	_position = position;	
}

void ControlImage::Update(reciverInfo_t *reciverInfo) 
{
	if(!IsVisible) return;
}

void ControlImage::Draw()
{
	if(!IsVisible || _texture == NULL) 
	{
		return;
	}
	recti* clip = (UseClip) ? &Clip : NULL;
	if(_texture != NULL) 
	{
		Driver->draw2DImage(
			_texture, 
			_position, 
			recti(0, 0, _sizeTexture.Width,_sizeTexture.Height),
			clip, 
			video::SColor(255,255,255,255), 
			true);		
	}
}

void ControlImage::SetPosition(position2di newPosition)
{
	_position = newPosition;

}

// Получить границы контрола
irr::core::recti ControlImage::GetBounds()
{
	return recti(_position, _sizeTexture);
}

// Получить позицию контрола
irr::core::position2di ControlImage::GetPosition()
{
	position2di position = _position;
	return position;
}
