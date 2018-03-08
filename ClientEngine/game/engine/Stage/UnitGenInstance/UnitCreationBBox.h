#pragma once
#include "UnitCreationBase.h"
#include "Core/CompareType.h"
#include "Core/NodeWorker.h"

class UnitCreationBBox : public UnitCreationBase
{
public:
	UnitCreationBBox(SharedParams_t params);
	virtual ~UnitCreationBBox(void);
	
	// Необходимо ли создать юнит
	bool NeedCreate();

	// Можно ли удалить данное условие создания
	bool CanDispose();

	// Юнит создан, все условия выполнены
	void UnitCreated();

	// Получить модель по индентификатору
	int FilterNodeId;

	// Количество моделий
	int CountNodes;

	// Расстояние
	aabbox3df Boundbox;
};
