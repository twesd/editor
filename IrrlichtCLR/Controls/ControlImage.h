#pragma once
#include "IControl.h"
#include "../Core/Parsers/ParserAS.h"

class ControlImage : public IControl
{
public:
	ControlImage(SharedParams_t params);
	virtual ~ControlImage(void);

	void Init(stringc texturePath, position2di position);

	///////////////// IControl ////////////////////

	virtual CONTROL_TYPE GetControlType()
	{
		return CONTROL_TYPE_IMAGE;
	};
	
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

	/////////////////////////////////////

private:
	position2di _position;

	video::ITexture	*_texture;
	dimension2d<s32> _sizeTexture;
};
