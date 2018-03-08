#pragma once
#include "UnitEventBase.h"
#include "../UnitSelectSceneNodes/UnitSelectSceneNodeBase.h"

class UnitEventSelection : public UnitEventBase
{
public:
	UnitEventSelection(SharedParams_t params);
	virtual ~UnitEventSelection(void);

	// Добавить выборку
	// Захват ресурса grab
	void AddSelector(UnitSelectSceneNodeBase* selector);

	// Начало выполнения
	virtual void Begin();

	// Выполняется ли условие
	virtual bool IsApprove(core::array<Event_t*>& events);

	// Количество выбранных объектов
	int Count;

private:
	// Выборка моделий
	core::array<UnitSelectSceneNodeBase*> _selectors;

	bool _isInit;
};

