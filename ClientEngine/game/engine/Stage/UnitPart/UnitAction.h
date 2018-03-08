#pragma once
#include "UnitAnimation.h"
#include "UnitClause.h"
#include "UnitExecute/ExecuteBase.h"
#include "UnitBreak/UnitActionBreak.h"
#include "../../Sounds/SoundManager.h"
#include "../../Core/Parsers/ParserAS.h"
#include "../../Core/Parsers/ScriptCache.h"

class UnitInstanceStandard;

class UnitAction : public Base
{
public:
	UnitAction(SharedParams_t params);
	virtual ~UnitAction(void);

	// Назначить условия при которых возможно исполнение
	void SetClause(UnitClause* clause);

	// Назначить условия отмены
	void SetBreak(UnitActionBreak* breakClause, ScriptCache* scriptCache);

	// Установить экземпляр юнита
	virtual void SetUnitInstance(UnitInstanceStandard* unitInstance);

	// Получить экземпляр юнита
	UnitInstanceStandard* GetUnitInstance();

	UnitClause* GetUnitClause();

	// Добавить действие
	void AddExecute(ExecuteBase* execute);

	// Выполняются ли условия
	bool IsApprove(core::array<Event_t*>& events);

	// Начало действия
	virtual void Begin();

	// Выполнить действия
	void Execute(scene::ISceneNode* node, core::array<Event_t*>& events);

	// Обновление данных
	virtual void Update(scene::ISceneNode* node, core::array<Event_t*>& events);

	// Установить анимацию
	void SetAnimation(UnitAnimation* unitAnimation);
	
	// Необходимо ли отменить текущее поведение
	virtual bool NeedBreak( core::array<Event_t*>& events, bool animationEnd, ISceneNode* node );

	// Имеет ли действие анимацию
	bool HasAnimation();

	// Индентификатор
	stringc Id;

	// Имя
	stringc Name;

	// Приоритет
	int Priority;

	// Неизменять текущее действие
	bool NoChangeCurrentAction;

	// Фильтр для событий
	stringc FilterEvent;

protected:

	// Экземпляр юнита которому принадлежит поведения
	UnitInstanceStandard* UnitInstance;

private:
	// Описание действия
	UnitAnimation* _animation;

	// Условия события
	UnitClause* _clause;

	// Условия отмены действия
	UnitActionBreak* _breakClause;

	// Действия
	core::array<ExecuteBase*> _executes;

	ParserAS* _parser;
};
