#pragma once
#include "IControl.h"
#include "../Core/Parsers/ParserAS.h"

class ControlRect : public IControl
{
public:
	ControlRect(SharedParams_t params);
	virtual ~ControlRect(void);

	void Init(stringc name, position2di position, int width, int height, u32 color, bool outline);

	///////////////// IControl ////////////////////

	virtual CONTROL_TYPE GetControlType()
	{
		return CONTROL_TYPE_UNKNOWN;
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
	dimension2d<s32> _size;
	u32 _color;
	bool _outline;
};
