#include "ControlComponentScroll.h"
#include "Core/Xml/XMLHelper.h"
#include "Core/TextureWorker.h"

ControlComponentScroll::ControlComponentScroll(SharedParams_t params) : IControlComponent(params)
{
	//IsVisible = true;	
	_isInit = false;
	_textureBody = NULL;
	_textureBackground =  NULL;
	_isActivateState = false;
	_alphaDefVal = 0;
	_alpha = _alphaDefVal;
}

ControlComponentScroll::~ControlComponentScroll(void)
{
	
}


void ControlComponentScroll::Init()
{
	_textureBody = TextureWorker::GetTexture(Driver, TextureBody);
	_DEBUG_BREAK_IF(_textureBody == NULL)
	if (TextureBackground != "")
		_textureBackground = TextureWorker::GetTexture(Driver, TextureBackground);	
	_isInit = true;
	_isActivateState = false;
	_alpha = _alphaDefVal;
}


void ControlComponentScroll::Update(reciverInfo_t *reciverInfo) 
{
	bool isFistTime = !_isInit;

	// Если компонент ещё не иницилизирован
	if(!_isInit) Init();
	
	if(IsVertical)
	{
		s32 oldY = _bodyBounds.LowerRightCorner.Y;
		dimension2di textureBodySize = _textureBody->getOriginalSize();
		dimension2di textureBgSize;
		if(_textureBackground)
			textureBgSize = _textureBackground->getOriginalSize();
		else
			textureBgSize = textureBodySize;
		s32 textureOffset = (textureBgSize.Width - textureBodySize.Width) / 2;
		// Получаем соотношение внутреннего и внешнего контэйнера
		f32 coeff = ((f32) _outerContainerBounds.getHeight() / _innerContainerBounds.getHeight());
		// Получаем высоту бегунка
		s32 bgHeight = (s32)((f32)_outerContainerBounds.getHeight() * coeff + 0.5f);
		_bodyBounds.UpperLeftCorner.X = _outerContainerBounds.LowerRightCorner.X - textureBodySize.Width - textureOffset;
		_bodyBounds.UpperLeftCorner.Y = _outerContainerBounds.UpperLeftCorner.Y + (s32)(_offset.Y * coeff + 0.5f);
		_bodyBounds.LowerRightCorner.X = _outerContainerBounds.LowerRightCorner.X;
		_bodyBounds.LowerRightCorner.Y = _bodyBounds.UpperLeftCorner.Y + bgHeight;

		if (_bodyBounds.LowerRightCorner.Y != oldY && !isFistTime)
		{
			// Применяем эффект активации
			_isActivateState = true;
			_timeEndAppear = GetNowTime() + 1000;
			_alpha = 255;
		}
	}
}

/// <summary>
/// Отрисовка объекта
/// </summary>
void ControlComponentScroll::Draw()
{
	if(!_isInit || !IsVisible) return;	
	
	// Обновляем прозрачность
	// 
	if (_isActivateState)
	{
		if (GetNowTime() > _timeEndAppear)
		{
			_isActivateState = false;
		}
		_alpha = 255;
	}
	if (_alpha > _alphaDefVal)
	{
		_alpha -= GetChangeValue(500.0f);
	}
	if (_alpha < _alphaDefVal) _alpha = _alphaDefVal;
	if (_alpha > 255) _alpha = 255;

	if (IsVertical)
	{
		int count;
		position2di pos;

		if (_textureBackground)
		{
			// Рисуем задний фон
			//
			dimension2di bgSize = _textureBackground->getOriginalSize();
			count = (_outerContainerBounds.getHeight() / bgSize.Height) + 1;			
			pos.X = _outerContainerBounds.LowerRightCorner.X - bgSize.Width;
			pos.Y = _outerContainerBounds.UpperLeftCorner.Y; 
			for (s32 i = 0; i < count; i++)
			{
				Driver->draw2DImage(
					_textureBackground, 
					pos, 
					recti(position2di(), _textureBackground->getSize()),
					&_outerContainerBounds, 
					video::SColor((s32)_alpha,255,255,255), 
					true);
				pos.Y += bgSize.Height;
			}
		}

		// Рисуем бегунок
		//
		dimension2di bodyTextureSize = _textureBody->getOriginalSize();
		count = (_bodyBounds.getHeight() / bodyTextureSize.Height) + 1;
		pos = _bodyBounds.UpperLeftCorner;
		for (s32 i = 0; i < count; i++)
		{
			Driver->draw2DImage(
				_textureBody, 
				pos, 
				recti(position2di(), _textureBody->getSize()),
				&_bodyBounds, 
				video::SColor((s32)_alpha,255,255,255), 
				true);
			pos.Y += bodyTextureSize.Height;
		}
	}
	else
	{
		// TODO: не реализовано
	}
	
	
}

void ControlComponentScroll::SetContainerBounds( recti outerBounds, recti innerBounds )
{
	_outerContainerBounds = outerBounds;
	_innerContainerBounds = innerBounds;

	if(IsVertical)
	{
		if (outerBounds.getHeight() == innerBounds.getHeight())
		{
			IsVisible = false;
		}
		else
		{
			IsVisible = true;
		}
	}
	else
	{
		if (outerBounds.getWidth() == innerBounds.getWidth())
		{
			IsVisible = false;
		}
		else
		{
			IsVisible = true;
		}
	}
}

void ControlComponentScroll::SetOffset( position2di offset )
{
	_offset = offset;
}

