#pragma once
#include "UnitCreationBase.h"
#include "Core/CompareType.h"
#include "Core/NodeWorker.h"

class UnitCreationDistance : public UnitCreationBase
{
public:
	UnitCreationDistance(SharedParams_t params);
	virtual ~UnitCreationDistance(void);
	
	// Необходимо ли создать юнит
	bool NeedCreate();

	// Можно ли удалить данное условие создания
	bool CanDispose();

	// Юнит создан, все условия выполнены
	void UnitCreated();

	// Получить модель по индентификатору
	int FilterNodeId;

	// Расстояние
	f32 Distance;

	// Тип сравнения
	CompareTypeEnum CompareType;

	// Учитывать размеры юнитов
	bool UseUnitSize;

	// Количество моделий
	int CountNodes;
};
