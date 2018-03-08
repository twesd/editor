#pragma once
#include "UnitGenInstanceStandard.h"

class UnitGenInstanceEmpty : public UnitGenInstanceBase
{
public:
	UnitGenInstanceEmpty(SharedParams_t params);
	virtual ~UnitGenInstanceEmpty(void);

	// Получить тип
	virtual GenInstanceType GetType()
	{
		return UnitGenInstanceBase::Empty;
	}

	// Необходимо ли удалить описание. Случай когда юниты больше создаватья не будут
	virtual bool CanDispose();

	vector3df Scale;

	int NodeId;
};
