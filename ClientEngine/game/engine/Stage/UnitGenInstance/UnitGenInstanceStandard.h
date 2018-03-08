#pragma once
#include "UnitGenInstanceBase.h"

class UnitGenInstanceStandard : public UnitGenInstanceBase
{
public:
	UnitGenInstanceStandard(SharedParams_t params);
	virtual ~UnitGenInstanceStandard(void);

	// Получить тип
	virtual GenInstanceType GetType()
	{
		return UnitGenInstanceBase::Standard;
	}

	// Необходимо ли удалить описание. Случай когда юниты больше создаватья не будут
	virtual bool CanDispose();

	// Описание поведения юнита
	stringc PathBehavior;

	// Начальный скрипт
	stringc StartScriptFileName;

	// Наименование кости скелета к которому надо прикрепить модель
	stringc JointName;
};
