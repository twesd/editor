#pragma once
#include "UnitEventBase.h"
#include "../UnitParameters.h"
#include "Core/CompareType.h"
#include "Core/NodeWorker.h"

class UnitEventChildUnit : public UnitEventBase
{
public:
	UnitEventChildUnit(SharedParams_t params);
	virtual ~UnitEventChildUnit(void);

	// Начало выполнения
	virtual void Begin();

	// Выполняется ли условие
	virtual bool IsApprove(core::array<Event_t*>& events);

	// Установить основную директорию
	void SetRoot(stringc root);

	// Путь до файла поведений
	stringc ChildPath;

	// Описание локальных параметров, которые будут проверяться
	UnitParameters* Parameters;

private:
	stringc _root;
};

