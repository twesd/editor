#pragma once
#include "IControlComponent.h"


class ControlComponentScroll : IControlComponent
{
public:
	ControlComponentScroll(SharedParams_t params);
	virtual ~ControlComponentScroll(void);

	CONTROL_COMPONENT_TYPE GetControlType(){
		return CONTROL_COMPONENT_TYPE_SCROLL;
	};

	void Update(reciverInfo_t *reciverInfo);

	void Draw();

	/// <summary>
	/// Установить границы контэйнера
	/// </summary>
	/// <param name="outerBounds">The outer bounds.</param>
	/// <param name="innerBounds">The inner bounds.</param>
	void SetContainerBounds(recti outerBounds, recti innerBounds);

	/// <summary>
	/// Установить сдвиг
	/// </summary>
	/// <param name="offset">The offset.</param>
	void SetOffset(position2di offset);

	/// <summary>
	///  Текстура бегунка
	/// </summary>
	stringc TextureBody;

	/// <summary>
	/// Текстура заднего фона
	/// </summary>
	stringc TextureBackground;

	/// <summary>
	/// Является ли скролл вертикальным
	/// </summary>
	bool IsVertical;

private:
	// Иницилизация
	void Init();

	// Иницилизирован ли компонент
	bool _isInit;

	recti _outerContainerBounds;

	recti _innerContainerBounds;

	// Границы и положение бегунка
	recti _boundsBody;

	position2di _offset;

	position2di _positionBody;

	/// <summary>
	/// Границы бегунка
	/// </summary>
	recti _bodyBounds;

	/// <summary>
	/// Время завершения активности скролла
	/// </summary>
	u32 _timeEndAppear;

	bool _isActivateState;

	f32 _alpha;

	/// <summary>
	/// Значение прозрачности по умолчанию
	/// </summary>
	f32 _alphaDefVal;

	video::ITexture* _textureBody;

	video::ITexture* _textureBackground;
};
