#pragma once
#include "UnitGenInstanceBase.h"

class UnitGenInstanceEnv : public UnitGenInstanceBase
{
public:
	UnitGenInstanceEnv(SharedParams_t params);
	virtual ~UnitGenInstanceEnv(void);

	// Получить тип
	virtual GenInstanceType GetType()
	{
		return UnitGenInstanceBase::Env;
	}

	// Необходимо ли удалить описание. Случай когда юниты больше создаватья не будут
	virtual bool CanDispose()
	{
		return true;
	}
	
	// Необходимо ли создать юнит
	virtual bool NeedCreate()
	{
		return true;
	}

	// Путь до модели
	stringc ModelPath;

	s32 NodeId;

};
