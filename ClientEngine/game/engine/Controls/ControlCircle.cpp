#include "ControlCircle.h"
#include "Core/TextureWorker.h"
#include "Core/GeometryWorker.h"

ControlCircle::ControlCircle(SharedParams_t params) : IControl(params),
	_position(),
	_positionCenter()
{
	_texture = NULL;
	_textureCenter = NULL;
	_selectIndex = -1;
	IsVisible = true;	

	_controlCircleDesc.Angle = 0;
	_controlCircleDesc.State = CONTROL_CIRCLE_STATE_NONE;
	_controlCircleDesc.Name = stringc();
}

ControlCircle::~ControlCircle(void)
{

}

void ControlCircle::Init(
						 stringc texturePath, 
						 stringc texturePathCenter, 
						 position2di position,
						 float controlsScale)
{

	_position = position;

	_controlCircleDesc.State = CONTROL_CIRCLE_STATE_NONE;
	_controlCircleDesc.Name = Name;
	
	_texture = TextureWorker::GetTexture(Driver, texturePath,true);
	_DEBUG_BREAK_IF(_texture == NULL)

	_sizeTexture = _texture->getOriginalSize();
	
	_textureCenter = TextureWorker::GetTexture(Driver, texturePathCenter,true);
	_DEBUG_BREAK_IF(_textureCenter == NULL)
	_sizeTextureCenter = _textureCenter->getOriginalSize();

	s32 offsetX = (s32)(((_sizeTexture.Width / 2) - (_sizeTextureCenter.Width / 2)) * controlsScale);
	_defaultPositionCenter.X = _position.X + offsetX;
	s32 offsetY = (s32)(((_sizeTexture.Height / 2) - (_sizeTextureCenter.Height / 2)) * controlsScale);
	_defaultPositionCenter.Y = _position.Y + offsetY;

	SetDefault();
}

void ControlCircle::SetControlManager( ControlManager* manager )
{
	IControl::SetControlManager(manager);
}

// Установить начальную позицию
void ControlCircle::SetDefault()
{
	_positionCenter = _defaultPositionCenter;
	_controlCircleDesc.Angle = 0;
	_controlCircleDesc.State = CONTROL_CIRCLE_STATE_NONE;
}

void ControlCircle::SetControlsScale( float scale )
{
	ControlsScale = scale;
	SetDefault();
}

void ControlCircle::Update(reciverInfo_t *reciverInfo) 
{
	if(!IsVisible)
	{
		return;
	}

	for(u32 i=0;i<MAX_USER_ACTION_COUNT;i++)
	{
		int state = reciverInfo->StateTouch[i];
		int x = reciverInfo->XTouch[i];
		int y = reciverInfo->YTouch[i];
#ifdef IPHONE_COMPILE
		bool realesed = (state == irr::ETOUCH_ENDED || state == irr::ETOUCH_CANCELLED);
#else	
		bool realesed = (state == irr::EMIE_LMOUSE_LEFT_UP);
#endif	
		if(realesed && _selectIndex >= 0)
		{
			_selectIndex = -1;
			SetDefault();
			break;
		}
		// Если ничего не выбрано
		if (_selectIndex < 0)
		{
			if(x>(_position.X) && x<(_position.X+_sizeTexture.Width) 
				&& (y>_position.Y) && (y<(_position.Y+_sizeTexture.Height)))
			{
				_selectIndex = i;
				_positionCenter.X = x - (_sizeTextureCenter.Width / 2);
				_positionCenter.Y = y - (_sizeTextureCenter.Height / 2);
				fillCirleDesc(CONTROL_CIRCLE_STATE_DOWN);
				break;
			}
		}
		else if (_selectIndex == i)
		{
			_positionCenter.X = x - (_sizeTextureCenter.Width / 2);
			_positionCenter.Y = y - (_sizeTextureCenter.Height / 2);
			CONTROL_CIRCLE_STATE state = (realesed) ? 
				CONTROL_CIRCLE_STATE_UP : CONTROL_CIRCLE_STATE_PRESSED;
			fillCirleDesc(state);
			break;
		}		
	}	
	if(_selectIndex < 0)
	{
		return;
	} 
	else 
	{
		EventParameters_t params;
		params.CommonParam = &_controlCircleDesc;		
		EventBase->PostEvent(Event::ID_UNIT_MANAGER, EVENT_CONTROL_CIRCLE, params);
	}
}

void ControlCircle::Draw()
{
	if(!IsVisible) return;

	recti* clip = (UseClip) ? &Clip : NULL;

	if (ControlsScale != 1.0f)
	{
		recti destRect(
			_position.X, _position.Y, 
			(s32) (_position.X + _sizeTexture.Width * ControlsScale), 
			(s32) (_position.Y + _sizeTexture.Height * ControlsScale));

		Driver->draw2DImage(
			_texture, 
			destRect, 
			recti(0, 0, _sizeTexture.Width,_sizeTexture.Height),
			clip,
			0, 
			true);	

		recti destRectCenter(
			_positionCenter.X, _positionCenter.Y, 
			(s32) (_positionCenter.X + _sizeTextureCenter.Width * ControlsScale), 
			(s32) (_positionCenter.Y + _sizeTextureCenter.Height * ControlsScale));


		Driver->draw2DImage(
			_textureCenter, 
			destRectCenter, 
			recti(0, 0, _sizeTextureCenter.Width,_sizeTextureCenter.Height),
			clip, 
			0, 
			true);
	}
	else 
	{
		Driver->draw2DImage(
			_texture, 
			_position, 
			recti(0, 0, _sizeTexture.Width,_sizeTexture.Height),
			clip,
			video::SColor(255,255,255,255), 
			true);	

		Driver->draw2DImage(
			_textureCenter, 
			_positionCenter, 
			recti(0, 0, _sizeTextureCenter.Width,_sizeTextureCenter.Height),
			clip, 
			video::SColor(255,255,255,255), 
			true);			
	}
}

// Получить границы контрола
irr::core::recti ControlCircle::GetBounds()
{
	if (ControlsScale != 1.0f)
	{
		dimension2di size;
		size.Width = (s32)(_sizeTexture.Width * ControlsScale);
		size.Height = (s32)(_sizeTexture.Height * ControlsScale);
		return recti(_position, size);
	}
	else 
	{
		return recti(_position, _sizeTexture);
	}
}



// Установить позицию контрола
void ControlCircle::SetPosition(position2di newPosition)
{
	_position = newPosition;
}

// Получить позицию контрола
irr::core::position2di ControlCircle::GetPosition()
{
	position2di position = _position;
	return position;
}

// Получить состояние кнопки
CONTROL_CIRCLE_STATE ControlCircle::GetState()
{
	return _controlCircleDesc.State;
}


// Заполнить данные для события
void ControlCircle::fillCirleDesc(CONTROL_CIRCLE_STATE state)
{
	_controlCircleDesc.Angle = GeometryWorker::GetAngle(
		vector3df((f32)_defaultPositionCenter.X, 0.0f, (f32)_defaultPositionCenter.Y),
		vector3df((f32)_positionCenter.X, 0.0f, (f32)_positionCenter.Y));
	_controlCircleDesc.Angle = 360.0f - _controlCircleDesc.Angle;
	_controlCircleDesc.State = state;
}

float ControlCircle::GetCenterAngle()
{
	return _controlCircleDesc.Angle;
}


