#include "ControlPanel.h"
#include "ControlManager.h"

//#define DEBUG_CONTROL_COMPONENT_SCROLL

ControlPanel::ControlPanel(SharedParams_t params) : IControl(params),
	ChildrenIds(), _controls()
{
	IsVisible = true;	
	_isInit = false;
	_scroll = NULL;
	_horzScrollComp = NULL;
	_vertScrollComp = NULL;
	_isStartScrolling = false;
}

ControlPanel::~ControlPanel(void)
{
	if (_horzScrollComp != NULL)
		delete _horzScrollComp;
	if (_vertScrollComp != NULL)
		delete _vertScrollComp;
}


void ControlPanel::Init()
{	
	_DEBUG_BREAK_IF(_isInit)

	// Получаем контролы и получаем внутренний размер
	//	
	s32 xMax = _outerBounds.LowerRightCorner.X;
	s32 yMax = _outerBounds.LowerRightCorner.Y;
	for (u32 i = 0; i < ChildrenIds.size() ; i++)
	{
		IControl* control = CntrlManager->GetControlById(ChildrenIds[i]);
		_DEBUG_BREAK_IF(control == NULL)
		_controls.push_back(control);	

		recti sizeCntrl = control->GetBounds();
		if (xMax < sizeCntrl.LowerRightCorner.X)
			xMax = sizeCntrl.LowerRightCorner.X;
		if (yMax < sizeCntrl.LowerRightCorner.Y)
			yMax = sizeCntrl.LowerRightCorner.Y;
	}
	// Устанавливаем внутренний размер
	_innerBounds = recti(_outerBounds.UpperLeftCorner.X, _outerBounds.UpperLeftCorner.Y, xMax, yMax);
	_limitScrollOffset.X = xMax - _outerBounds.LowerRightCorner.X;
	_limitScrollOffset.Y = yMax - _outerBounds.LowerRightCorner.Y;

	_isInit = true;

	// Устанавливаем область отрисовки
	// 
	for (u32 iCntrl = 0; iCntrl < _controls.size(); iCntrl++)
	{
		IControl* control = _controls[iCntrl];
		control->SetClip(_outerBounds);
	}

	// Иницлизируем компоненты скролла
	_vertScrollComp->SetContainerBounds(_outerBounds, _innerBounds);
	_horzScrollComp->SetContainerBounds(_outerBounds, _innerBounds);
}

void ControlPanel::SetControlManager( ControlManager* manager )
{
	IControl::SetControlManager(manager);
}


void ControlPanel::Update(reciverInfo_t *reciverInfo) 
{
	// TODO: fast fix
	return;
	
	
	if(!IsVisible) return;
	if(!_isInit)
		Init();


	position2di touchPos;
	bool touchDetected = false;
	position2di oldOffset = _scrollOffset;

	for(u32 i=0;i<MAX_USER_ACTION_COUNT;i++)
	{
		int state = reciverInfo->StateTouch[i];
		if(state == FAIL_VALUE) continue;
		touchPos.X = reciverInfo->XTouch[i];
		touchPos.Y = reciverInfo->YTouch[i];
#ifdef IPHONE_COMPILE
		bool realesed = (state == irr::ETOUCH_ENDED || state == irr::ETOUCH_CANCELLED);
#else	
		bool realesed = (state == irr::EMIE_LMOUSE_LEFT_UP);
#endif	
		if (_outerBounds.isPointInside(touchPos))
		{
			touchDetected = true;		

#ifdef DEBUG_CONTROL_COMPONENT_SCROLL
			if(!_isStartScrolling)
				printf("[ControlPanel] Start Scroll \n");
#endif
			if(!_isStartScrolling)
			{
				_isStartScrolling = true;
				_lastDragPos = touchPos;
				break;
			}
	

			if(realesed)
			{
				_isStartScrolling = false;
#ifdef DEBUG_CONTROL_COMPONENT_SCROLL
				if(!_isStartScrolling)
					printf("[ControlPanel] End Scroll \n");
#endif
			}
			else
			{
				if (_isStartScrolling)
				{
					position2di newOffset = (_lastDragPos - touchPos);
					_scrollOffset.X += (newOffset.X / 2);
					_scrollOffset.Y += (newOffset.Y / 2);
					if(_scrollOffset.X < 0) 
						_scrollOffset.X = 0;
					else if(_scrollOffset.X > _limitScrollOffset.X)
						_scrollOffset.X = _limitScrollOffset.X;
					if(_scrollOffset.Y < 0) 
						_scrollOffset.Y = 0;
					else if(_scrollOffset.Y > _limitScrollOffset.Y)
						_scrollOffset.Y = _limitScrollOffset.Y;
				}
			}

			_lastDragPos = touchPos;
		}
	}

	if (!touchDetected)
	{
		_isStartScrolling = false;
	}

	position2di destOffset = _scrollOffset - oldOffset;

#ifdef DEBUG_CONTROL_COMPONENT_SCROLL
	if(_isStartScrolling)
	{
		printf("[ControlPanel] destOffset [%d][%d] \n", destOffset.X, destOffset.Y);
	}
#endif
	// Обновляем видимость контролов
	// 
	for (u32 iCntrl = 0; iCntrl < _controls.size(); iCntrl++)
	{
		IControl* control = _controls[iCntrl];
		// Обновляем позицию
		position2di pos = control->GetPosition();
		pos -= destOffset;
		control->SetPosition(pos);
	}

	// Обновляем компоненты
	// 
	_vertScrollComp->SetOffset(_scrollOffset);
	_horzScrollComp->SetOffset(_scrollOffset);

	_vertScrollComp->Update(reciverInfo);
	_horzScrollComp->Update(reciverInfo);
}

void ControlPanel::Draw()
{
	if(!IsVisible) return;
	_vertScrollComp->Draw();
	_horzScrollComp->Draw();
}

// Получить границы контрола
irr::core::recti ControlPanel::GetBounds()
{
	return _outerBounds;
}

// Установить позицию контрола
void ControlPanel::SetPosition(position2di newPosition)
{
	_outerBounds = recti(newPosition, _outerBounds.getSize());
}

// Получить позицию контрола
irr::core::position2di ControlPanel::GetPosition()
{
	position2di position = _outerBounds.UpperLeftCorner;
	return position;
}

void ControlPanel::SetSize( dimension2di size )
{
	_outerBounds = recti(_outerBounds.UpperLeftCorner, size);
}

void ControlPanel::SetVertScroll( ControlComponentScroll* scroll )
{
	_vertScrollComp = scroll;
}

void ControlPanel::SetHorzScroll( ControlComponentScroll* scroll )
{
	_horzScrollComp = scroll;
}

/// <summary>
/// Установить область обрезки
/// </summary>
void ControlPanel::SetClip( recti& clip )
{

}

/// <summary>
/// Добавить дочерний элемент
/// </summary>
/// <param name="child">Элемент который необходимо добавить</param>
void ControlPanel::AddChild( IControl* control )
{
	// TODO: fast fix
	return;
	for (u32 i = 0; i < _controls.size() ; i++)
	{
		if (_controls[i] == control)
		{
			return;
		}
	}
	_controls.push_back(control);
	control->SetClip(_outerBounds);
}

/// <summary>
/// Удалить дочерний элемент
/// </summary>
/// <param name="child">Элемент который необходимо удалить.</param>
void ControlPanel::RemoveChild( IControl* control )
{
	for (u32 i = 0; i < _controls.size() ; i++)
	{
		if (_controls[i] == control)
		{
			_controls.erase(i);
			// Отменяем область обрезки
			control->RemoveClip();
			break;
		}
	}
}

/// <summary>
/// Преобразовать в координаты панели
/// </summary>
position2di ControlPanel::ConvertToClientPosition( position2di sourcePos )
{
	return sourcePos - _scrollOffset;
}
