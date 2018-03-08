#pragma once
#include "UnitInstanceStandard.h"
#include "../UnitGenInstance/UnitGenInstanceEmpty.h"
#include "../UnitPart/UnitMapping/UnitMappingBase.h"

class UnitManager;

class UnitInstanceEmpty : public UnitInstanceBase
{
public:
	UnitInstanceEmpty(
		SharedParams_t params, 
		UnitGenInstanceEmpty *genInstance, 
		UnitInstanceBase* parent, 
		UnitManager* unitManager,
		UnitInstanceBase* creator);
	virtual ~UnitInstanceEmpty(void);
	
	// Обновить данные
	void Update();
	
	// Обработка событий
	void HandleEvent(core::array<Event_t*>& events);

};
