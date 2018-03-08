#pragma once
#include "UnitParameters.h"
#include "UnitEvent/UnitEventBase.h"
#include "../../Core/Parsers/ParserExpression.h"

class UnitBehavior;

class UnitClause : public Base
{
public:
	UnitClause(SharedParams_t params);
	virtual ~UnitClause(void);
	
	// Добавить параметр
	void AddParameter(bool isGlobal, stringc name, stringc value);
	
	// Добавить событие
	// Объект захватывается (grab) в данном методе
	void AddUnitEvent(UnitEventBase* unitEvent);
	
	// Выполняются ли условия
	bool IsApprove(core::array<Event_t*>& events); 

	// Устаноить поведение, которому принадлежит данное условие
	// Установка ссылки
	void SetBehavior(UnitBehavior* behavior);

	// Начало выполнения действия
	void Begin();

	// Обновление данных
	void Update( scene::ISceneNode* node, core::array<Event_t*>& events );

private:

	// Параметры юнита
	UnitParameters* _behaviorParameters;

	// Описание локальных параметров, которые будут проверяться
	UnitParameters* _parameters;

	// Описание глобальных параметров, которые будут проверяться 
	UnitParameters* _globalParameters;

	core::array<UnitEventBase*> _unitEvents;

	// Поведение, которому принадлежит данное условие
	UnitBehavior* _behavior;

	// Класс для вычисления параметров
	ParserExpression _parser;
};
