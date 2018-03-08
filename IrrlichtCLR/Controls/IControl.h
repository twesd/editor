#pragma once

#include "Core/Base.h"
#include "ControlEvent.h"

// Тип элемента управления
enum CONTROL_TYPE
{
	CONTROL_TYPE_UNKNOWN,
	// Кнопка
	CONTROL_TYPE_BUTTON,
	// Клики по сцене
	CONTROL_TYPE_TAPSCENE,
	// Контрол действий
	CONTROL_TYPE_BEHAVIOR,
	// Контрол изображение
	CONTROL_TYPE_IMAGE,
	// Текст
	CONTROL_TYPE_TEXT,
	// Круг
	CONTROL_TYPE_CIRCLE,
	// Панель
	CONTROL_TYPE_PANEL

};

class ControlManager;

class IControl : public Base
{
public:
	IControl(SharedParams_t params) : Base(params), 
		Parameters()
	{
		IsVisible = true;
		CntrlManager = NULL;
		UseClip = false;
	};
	virtual ~IControl() { };

	//Возращает тип элемента управления
	virtual CONTROL_TYPE GetControlType() = 0;

	// Обработка данных
	virtual void Update(reciverInfo_t *reciverInfo) = 0;
	
	// Отрисовка элементов 
	virtual void Draw() = 0;

	// Получить границы контрола
	virtual recti GetBounds() = 0;

	// Получить позицию контрола
	virtual position2di GetPosition() = 0;

	// Установить позицию контрола
	virtual void SetPosition(position2di position) = 0;

	// Установить область обрезки
	virtual void SetClip(recti& clip)
	{
		Clip = clip;
		UseClip = true;
	};

	// Удалить область обрезки
	virtual void RemoveClip()
	{
		UseClip = false;
		Clip = recti();
	}

	virtual void SetControlManager(ControlManager* manager)
	{
		CntrlManager = manager;
	}

	// Индентификатор
	stringc Id;

	// Имя элемента управления
	stringc Name;
	
	// Видим ли контрол
	bool IsVisible;

	// Параметры контрола
	core::array<Parameter> Parameters;

	// ---------------- События ------------------------ 
	
	// Событие начала отображения пакета контролов 
	stringc OnPackageShow;

protected:
	ControlManager* CntrlManager;

	// Использовать область обрезки
	bool UseClip;

	// Область обрезки
	recti Clip;
};
