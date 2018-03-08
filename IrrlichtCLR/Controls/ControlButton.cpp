#include "ControlButton.h"
#include "Core/Xml/XMLHelper.h"
#include "Core/TextureWorker.h"
#include "ControlManager.h"

//#define DEBUG_CONTROL_BUTTON

ControlButton::ControlButton(SharedParams_t params) : IControl(params),
	OnClickDown(), 
	OnClickUp(),
	OnClickSound()
{
	_texture = NULL;
	_textureA = NULL;
	_isSelected = false;
	_holdTime = 300;
	_startPushTime = 0;
	IsVisible = true;	

	//_parser = new ParserAS();
	_parser = NULL;
}

ControlButton::~ControlButton(void)
{
	if(_parser != NULL)
	{
		delete _parser;
	}
	_parser = NULL;
}

void ControlButton::Init(stringc texturePath, 
						 stringc texturePathActivate, 
						 position2di position,
						 u32 holdTime)
{
	_holdTime = holdTime;
	if(_holdTime == 0) _holdTime = 300;

	_position = position;

	_controlButtonDesc.BtnState = BUTTON_STATE_NONE;
	_controlButtonDesc.Name = Name;
	
	_texture = TextureWorker::GetTexture(Driver, texturePath,true);
	if(_texture != NULL)
	{
		_sizeTexture = _texture->getOriginalSize();
	}
	if(texturePathActivate != "")
	{
		_textureA = TextureWorker::GetTexture(Driver, texturePathActivate, true);
		if(_textureA != NULL)
		{
			_sizeTextureA = _textureA->getOriginalSize();
		}
	}

}

void ControlButton::SetControlManager( ControlManager* manager )
{
	IControl::SetControlManager(manager);
}


void ControlButton::Update(reciverInfo_t *reciverInfo) 
{
	if(!IsVisible) return;
	if(UseClip)
	{
		position2di secondPos = _position + _sizeTexture;
		if (!Clip.isPointInside(_position) && !Clip.isPointInside(secondPos))
			return;
	}

	_isSelected = false;

	if(_startPushTime == 0) _startPushTime = GetNowTime();
	u32 timeStampPressed = GetNowTime() - _startPushTime;

	for(u32 i=0;i<MAX_USER_ACTION_COUNT;i++)
	{
		int state = reciverInfo->StateTouch[i];
		if(state == FAIL_VALUE)
			continue;
#ifdef IPHONE_COMPILE
		if(state == irr::ETOUCH_NONE)
			continue;
#endif
		int x = reciverInfo->XTouch[i];
		int y = reciverInfo->YTouch[i];
#ifdef IPHONE_COMPILE
		bool realesed = (state == irr::ETOUCH_ENDED || state == irr::ETOUCH_CANCELLED);
		bool isDown = (state == irr::ETOUCH_BEGAN);
#else	
		bool realesed = (state == irr::EMIE_LMOUSE_LEFT_UP);
		bool isDown = (state == irr::EMIE_LMOUSE_PRESSED_DOWN);
#endif	
		if(x>(_position.X) && x<(_position.X+_sizeTexture.Width) 
			&& (y>_position.Y) && (y<(_position.Y+_sizeTexture.Height)))
		{
			_isSelected = true;
			if(realesed)
			{
				_controlButtonDesc.BtnState = BUTTON_STATE_UP;

#ifdef DEBUG_CONTROL_BUTTON
				printf("[ControlButton] Button[%s] state [up] \n",_controlButtonDesc.Name.c_str());
#endif
				ExecuteExpression(OnClickUp);
			}
			else
			{				
				if(timeStampPressed > _holdTime)
				{
					#ifdef DEBUG_CONTROL_BUTTON
					printf("[ControlButton] Button[%s] state [hold] \n",_controlButtonDesc.Name.c_str());
					#endif
					_controlButtonDesc.BtnState = BUTTON_STATE_HOLD;
				} 
				else
				{		
					if(isDown)
					{
						#ifdef DEBUG_CONTROL_BUTTON
						printf("[ControlButton] Button[%s] state [down] \n",_controlButtonDesc.Name.c_str());
						#endif
						_controlButtonDesc.BtnState = BUTTON_STATE_DOWN;
						ExecuteExpression(OnClickDown);
					}
					else
					{						
						#ifdef DEBUG_CONTROL_BUTTON
						printf("[ControlButton] Button[%s] state [press] \n",_controlButtonDesc.Name.c_str());
						#endif
						_controlButtonDesc.BtnState = BUTTON_STATE_PRESSED;
					}
				}
			}
			break;
		}
	}	
	if(!_isSelected)
	{
		_controlButtonDesc.BtnState = BUTTON_STATE_NONE;
		_startPushTime = 0;
		return;
	} 
	else 
	{
		EventParameters_t params;
		params.CommonParam = &_controlButtonDesc;		
		EventBase->PostEvent(Event::ID_UNIT_MANAGER, EVENT_CONTROL_BUTTON, params);
	}
}

void ControlButton::Draw()
{
	if(!IsVisible) return;

	recti* clip = (UseClip) ? &Clip : NULL;

	if(_isSelected && _textureA != NULL)
	{		
		Driver->draw2DImage(
			_textureA, 
			_position, 
			recti(0, 0, _sizeTextureA.Width,_sizeTextureA.Height),
			clip, 
			video::SColor(255,255,255,255), 
			true);	
	}
	else if(_texture != NULL) 
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

// Получить границы контрола
irr::core::recti ControlButton::GetBounds()
{
	return recti(_position, _sizeTexture);
}

// Выполнить выражение
void ControlButton::ExecuteExpression( stringc& expr )
{
	/*if(expr != "")
	{	
		_parser->GetUserData()->ControlManagerData = CntrlManager;
		_parser->GetUserData()->BaseData = CntrlManager;
		_parser->GetUserData()->ControlOwnerData = this;
		_parser->SetCode(expr);
		_parser->Execute();
	}*/
}


// Установить позицию контрола
void ControlButton::SetPosition(position2di newPosition)
{
	_position = newPosition;
}

// Получить позицию контрола
irr::core::position2di ControlButton::GetPosition()
{
	position2di position = _position;
	return position;
}

// Получить состояние кнопки
CONTROL_BTN_STATE ControlButton::GetButtonState()
{
	return _controlButtonDesc.BtnState;
}
