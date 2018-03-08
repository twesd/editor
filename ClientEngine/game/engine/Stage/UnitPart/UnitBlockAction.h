#pragma once
#include "UnitAction.h"
#include "UnitActionUpdater.h"

class UnitInstanceStandard;

class UnitBlockAction : public UnitAction
{
public:
	UnitBlockAction(SharedParams_t params);
	virtual ~UnitBlockAction(void);
	
	// Добавить действие
	void AddAction(UnitAction* action);

	// Установить экземпляр юнита
	virtual void SetUnitInstance(UnitInstanceStandard* unitInstance);

	// Начало действия
	virtual void Begin();

	// Обновление данных
	virtual void Update(scene::ISceneNode* node, core::array<Event_t*>& events);

	// Необходимо ли отменить текущее поведение	
	virtual bool NeedBreak( core::array<Event_t*>& events, bool animationEnd, ISceneNode* node );

	// Получить текущее действие
	UnitAction* GetCurrentAction();
private:
	core::array<UnitAction*> _actions;

	// Кандидаты на след. действие
	core::array<UnitAction*> _candidateActions;

	// Текущие действие
	UnitAction* _currentAction;

	// Класс для работы с действиями
	UnitActionUpdater* _actionUpdater;

};

