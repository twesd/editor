#pragma once

class IExtendAnimator
{
public:
	//Сброс в начальные установки
	virtual void ResetState() = 0;
	//Закончена ли анимация
	virtual bool AnimationEnd() = 0;
	//Устанавливает дополнительные параметры
	virtual void SetUserParams(void* params) = 0;
};
