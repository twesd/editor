#pragma once

#include "../Core/Base.h"

// Тип элемента управления
enum EVENT_CONTROL
{
	// Событие от кнопки
	EVENT_CONTROL_BUTTON = 2000,
	// Событие от клика по сцене
	EVENT_CONTROL_TAPSCENE,
	// Событие от круга
	EVENT_CONTROL_CIRCLE
};

// Состояние кнопки
enum CONTROL_BTN_STATE
{
	BUTTON_STATE_NONE = 0,
	BUTTON_STATE_DOWN,
	BUTTON_STATE_PRESSED,
	BUTTON_STATE_UP,
	BUTTON_STATE_HOLD
};

//Описание события кнопки
typedef struct 
{
	stringc Name;
	CONTROL_BTN_STATE BtnState;
} ControlButtonDesc_t;

// Состояние кнопки
enum CONTROL_CIRCLE_STATE
{
	CONTROL_CIRCLE_STATE_NONE = 0,
	CONTROL_CIRCLE_STATE_DOWN,
	CONTROL_CIRCLE_STATE_PRESSED,
	CONTROL_CIRCLE_STATE_UP
};

//Описание события элемента движения
typedef struct 
{
	stringc Name;
	f32 Angle;
	CONTROL_CIRCLE_STATE State;
} ControlCircleDesc_t;

//Описание события элемента нажатия по экрану
typedef struct 
{
	// Наименование контрола
	stringc Name;
	// Выбранная модель
	ISceneNode* Node;
	// Точка нажатия на экране
	position2di ScreenPoint; 
	// Линия в игровом пространстве 
	line3df SceneRay;
	// Состояние нажатия
	CONTROL_BTN_STATE State;
} ControlTapSceneDesc_t;
