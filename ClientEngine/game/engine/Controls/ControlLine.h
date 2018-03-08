#pragma once
#include "IControl.h"
#include "../Core/Parsers/ParserAS.h"

class ControlLine : public IControl
{
public:
	ControlLine(SharedParams_t params);
	virtual ~ControlLine(void);

	void Init(stringc name, vector2di startPnt, vector2di endPnt, int width, u32 color);

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
	vector2di _startPnt;	

	vector2di _endPnt;	

	int _width;

	u32 _color;
};
