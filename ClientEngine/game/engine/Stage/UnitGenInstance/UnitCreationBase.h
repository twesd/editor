#pragma once
#include "../../Core/Base.h"

class UnitGenInstanceBase;

class UnitCreationBase : public Base
{
public:
	UnitCreationBase(SharedParams_t params);
	virtual ~UnitCreationBase(void);

	// Необходимо ли создать юнит
	virtual bool NeedCreate() = 0;
	// Можно ли удалить данное условие создания
	virtual bool CanDispose() = 0;
	// Юнит создан, все условия выполнены
	virtual void UnitCreated() = 0;

	virtual void SetGenInstance(UnitGenInstanceBase* genInstance);

protected:
	UnitGenInstanceBase* GenInstance;
};
