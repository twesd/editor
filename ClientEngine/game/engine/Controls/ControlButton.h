#pragma once
#include "IControl.h"
#include "../Core/Parsers/ParserAS.h"
#include "../Sounds/SoundManager.h"

class ControlButton : public IControl
{
public:
	ControlButton(SharedParams_t params, SoundManager* soundManager);
	virtual ~ControlButton(void);

	void Init(
		stringc texturePath, 
		stringc texturePathActivate, 
		position2di position, 				
		u32 holdTime);	


	///////////////// IControl ////////////////////

	CONTROL_TYPE GetControlType()
	{
		return CONTROL_TYPE_BUTTON;
	};

	void SetControlManager(ControlManager* manager);

	// Обработка данных
	void Update(reciverInfo_t *reciverInfo);

	// Отрисовка
	void Draw();
		
	// Получить состояние кнопки
	CONTROL_BTN_STATE GetButtonState();

	// Установить позицию контрола
	void SetPosition(position2di newPosition);
		
	// Получить позицию контрола
	position2di GetPosition();

	// Получить границы контрола
	recti GetBounds();

	/////////////////////////////////////

	stringc OnClickSound;

	// Выражение по событию нажатия кнопки
	stringc OnClickDown;

	// Выражение по событию нажатия кнопки
	stringc OnClickUp;

private:
	SoundManager* _soundManager;

	// Выполнить выражение
	void ExecuteExpression( stringc& expr );

	ControlButtonDesc_t _controlButtonDesc;
	
	stringc _action;
	
	position2di _position;	
	
	bool _isSelected;
	
	//Время перехода в состояние Hold
	u32 _holdTime;
	
	//Время начала нажатия на кнопку
	u32 _startPushTime;

	video::ITexture	*_texture;
	video::ITexture	*_textureA;
	dimension2d<s32> _sizeTexture;
	dimension2d<s32> _sizeTextureA;

	ParserAS* _parser;
};
