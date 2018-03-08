#pragma once
#include "../../Core/Base.h"

class UnitAnimation : public Base
{
public:
	UnitAnimation(SharedParams_t params);
	virtual ~UnitAnimation(void);
	
	// Выполнить анимацию
	void Execute( scene::ISceneNode* node );
	
	// Индентификатор
	stringc Id;

	// Наименование анимации
	stringc Name;

	// Начальный кадр
	int StartFrame;

	// Конечный кадр
	int EndFrame;

	// Скорость анимации
	int Speed;

	// Повторять движение
	bool Loop;

};
