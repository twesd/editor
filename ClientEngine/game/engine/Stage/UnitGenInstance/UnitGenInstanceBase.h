#pragma once
#include "UnitCreationBase.h"
#include "UnitCreationTimer.h"
#include "../unitPart/UnitBehavior.h"

class UnitGenInstanceBase : public Base
{
public:
	UnitGenInstanceBase(SharedParams_t params);
	virtual ~UnitGenInstanceBase(void);

	// Типы юнитов
	enum GenInstanceType
	{
		Env = 0,
        Standard,
		Billboard,
		Camera,
		Empty
	};

	// Получить тип
	virtual GenInstanceType GetType() = 0;

	// Необходимо ли удалить описание. Случай когда юниты больше создаватья не будут
	virtual bool CanDispose() = 0;

	// Необходимо ли создать юнит
	virtual bool NeedCreate();

	// Применить начальные параметры к объекту сцены
	virtual void ApplyStartParameters(ISceneNode* node);

	// Имя
	stringc UnitName;

	// Начальная позиция
	vector3df StartPosition;
	
	// Начальный поворот
	vector3df StartRotation;

	// Описания создания юнита
	core::array<UnitCreationBase*> Creations;

};
