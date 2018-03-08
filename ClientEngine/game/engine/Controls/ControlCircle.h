#pragma once
#include "IControl.h"

class ControlCircle : public IControl
{
public:
	ControlCircle(SharedParams_t params);
	virtual ~ControlCircle(void);
	
	CONTROL_TYPE GetControlType()
	{
		return CONTROL_TYPE_CIRCLE;
	};

	void Init(
		stringc texturePath, 
		stringc texturePathCenter, 
		position2di position,
		float controlsScale);	

	void SetControlManager(ControlManager* manager);

	///////////////// IControl ////////////////////
		
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

	virtual void SetControlsScale(float scale);

	/////////////////////////////////////

	// Получить угол
	float GetCenterAngle();

	CONTROL_CIRCLE_STATE GetState();

private:
	// Установить начальную позицию
	void SetDefault();

	// Заполнить данные для события
	void fillCirleDesc(CONTROL_CIRCLE_STATE state);
	
	ControlCircleDesc_t _controlCircleDesc;
	
	stringc _action;
	
	position2di _position;	

	position2di _positionCenter;	

	// Позиция центра по умолчанию
	position2di _defaultPositionCenter;	
	
	// Индекс выбранного тача
	int _selectIndex;

	video::ITexture	*_texture;
	video::ITexture	*_textureCenter;
	dimension2d<s32> _sizeTexture;
	dimension2d<s32> _sizeTextureCenter;

	// Менеджер контролов
	ControlManager* _controlManager;
};
