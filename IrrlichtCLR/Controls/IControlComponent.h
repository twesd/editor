#pragma once

#include "../Core/Base.h"
#include "ControlEvent.h"

// Тип элемента управления
enum CONTROL_COMPONENT_TYPE
{
	CONTROL_COMPONENT_TYPE_SCROLL
};

class IControlComponent : public Base
{
public:
	IControlComponent(SharedParams_t params) : Base(params)
	{
	};
	virtual ~IControlComponent() { };

	virtual CONTROL_COMPONENT_TYPE GetControlType() = 0;

	// Обработка данных
	virtual void Update(reciverInfo_t *reciverInfo) = 0;
	
	// Отрисовка элементов 
	virtual void Draw() = 0;

	virtual void SetVisible(bool visible){
		IsVisible = visible;
	};

	virtual bool GetVisible() {
		return IsVisible;
	};

protected:
	// Видим ли контрол
	bool IsVisible;
};
