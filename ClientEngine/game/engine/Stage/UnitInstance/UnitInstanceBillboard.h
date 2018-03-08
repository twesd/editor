#pragma once
#include "UnitInstanceBase.h"
#include "../UnitGenInstance/UnitGenInstanceBillboard.h"

class UnitInstanceBillboard : public UnitInstanceBase
{
public:
	UnitInstanceBillboard(
		SharedParams_t params, 
		UnitGenInstanceBillboard* genInstance,
		UnitInstanceBase* parent, 
		UnitInstanceBase* creator);
	virtual ~UnitInstanceBillboard(void);

	// Обновить данные
	void Update();

	// Обработка событий
	void HandleEvent(core::array<Event_t*>& events);
};
