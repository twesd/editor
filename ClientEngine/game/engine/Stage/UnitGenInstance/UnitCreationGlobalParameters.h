#pragma once
#include "UnitCreationBase.h"
#include "Core/CompareType.h"
#include "Core/NodeWorker.h"

class UnitCreationGlobalParameters : public UnitCreationBase
{
public:
	UnitCreationGlobalParameters(SharedParams_t params);
	virtual ~UnitCreationGlobalParameters(void);
	
	// Необходимо ли создать юнит
	bool NeedCreate();

	// Можно ли удалить данное условие создания
	bool CanDispose();

	// Юнит создан, все условия выполнены
	void UnitCreated();

	core::array<Parameter> Parameters;
};
