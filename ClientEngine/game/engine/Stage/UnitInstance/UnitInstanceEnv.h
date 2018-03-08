#pragma once
#include "UnitInstanceBase.h"
#include "../UnitGenInstance/UnitGenInstanceEnv.h"

class UnitInstanceEnv : public UnitInstanceBase
{
public:
	UnitInstanceEnv(SharedParams_t params, 
		UnitGenInstanceEnv* genInstance,
		UnitInstanceBase* parent, 
		UnitInstanceBase* creator);
	virtual ~UnitInstanceEnv(void);

	// Обновить данные
	void Update();

	// Обработка событий
	void HandleEvent(core::array<Event_t*>& events);
};
