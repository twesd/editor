#pragma once
#include "UnitEventBase.h"
#include "../../../Controls/ControlEvent.h"

class UnitEventControlTapScene : public UnitEventBase
{
public:
	UnitEventControlTapScene(SharedParams_t params);
	virtual ~UnitEventControlTapScene(void);

	// Выполняется ли условие
	virtual bool IsApprove(core::array<Event_t*>& events);
	
	// Состояние кнопки (CONTROL_BTN_STATE)
	CONTROL_BTN_STATE TapSceneState;

	// Наименование контрола
	stringc TapSceneName;

	// Игнорировать модель
	bool IgnoreNode;

	// Модели должны совпадать
	bool IdentNode;

	// Фильтр модели
	int FilterId;

	// Сохранить модель в данных
	stringc DataName;

};
