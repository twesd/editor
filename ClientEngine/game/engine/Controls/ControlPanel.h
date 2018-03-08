#pragma once
#include "IControl.h"
#include "ControlComponentScroll.h"

class ControlPanel : public IControl
{
public:
	ControlPanel(SharedParams_t params);
	virtual ~ControlPanel(void);

	///////////////// IControl ////////////////////
	
	CONTROL_TYPE GetControlType()
	{
		return CONTROL_TYPE_PANEL;
	};
	
	void SetControlManager(ControlManager* manager);

	// Обработка данных
	void Update(reciverInfo_t *reciverInfo);

	// Отрисовка
	void Draw();

	// Установить позицию контрола
	void SetPosition(position2di newPosition);
		
	// Получить позицию контрола
	position2di GetPosition();

	// Получить границы контрола
	recti GetBounds();

	/// <summary>
	/// Установить область обрезки
	/// </summary>
	void SetClip(recti& clip);

	/////////////////////////////////////

	void SetSize(dimension2di size);

	void SetVertScroll(ControlComponentScroll* scroll);

	void SetHorzScroll(ControlComponentScroll* scroll);

	/// <summary>
	/// Добавить дочерний элемент
	/// </summary>
	/// <param name="control">Элемент который необходимо добавить</param>
	void AddChild(IControl* control);

	/// <summary>
	/// Удалить дочерний элемент
	/// </summary>
	/// <param name="control">Элемент который необходимо удалить.</param>
	void RemoveChild(IControl* control);

	/// <summary>
	/// Преобразовать в координаты панели
	/// </summary>
	position2di ConvertToClientPosition(position2di sourcePos);

	/// <summary>
	/// Список индентификаторов дочерних элементов, используется при загрузке
	/// </summary>
	core::array<stringc> ChildrenIds;

private:

	// Иницилизация
	void Init();

	// Флаг иницилизации компонента
	bool _isInit;

	core::array<IControl*> _controls;

	ControlComponentScroll* _scroll;

	// Границы обработки
	core::recti _outerBounds;

	// Внутренний размер
	core::recti _innerBounds;

	position2di _limitScrollOffset;

	// Отступ от верхнего левого угла
	position2di _scrollOffset;

	/// <summary>
	/// Пользователь начал перетаскивание
	/// </summary>
	bool _isStartScrolling;

	/// <summary>
	/// Последняя позиция при перетаскивании
	/// </summary>
	position2di _lastDragPos;

	ControlComponentScroll* _horzScrollComp;

	ControlComponentScroll* _vertScrollComp;
};
