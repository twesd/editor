#pragma once
#include "UnitAction.h"
#include "UnitExprParameter.h"
#include "UnitActionUpdater.h"
#include "UnitModels/UnitModelBase.h"
#include "../../Core/Parsers/ParserAS.h"
#include "../../ModuleManager.h"

class UnitInstanceStandard;

class UnitBehavior : public Base, scene::IAnimationEndCallBack
{
public:
	UnitBehavior(SharedParams_t params);
	virtual ~UnitBehavior(void);

	// Добавить описания
	void SetActions(core::array<UnitAction*> actions);

	// Добавить доступные анимации
	void SetAnimations(core::array<UnitAnimation*> animations);
	
	// Установить экземпляр юнита которому принадлежит поведения
	void SetUnitInstance(UnitInstanceStandard* unitInstance);
	
	// Обработка событий
	void HandleEvent( core::array<Event_t*>& events );

	// Назначить модель
	void SetSceneNode( ISceneNode* node );

	// Добавить условие для внешнего параметра
	void AddExprParameterExt(UnitExprParameter* exprParam);

	// Добавить начальный параметр
	void AddStartParameter(stringc name, stringc val);

	// Задать скрипт
	void SetParser(ParserAS* parser);

	// Получить параметры
	UnitParameters* GetParameters();

	// Получаить анимацию по индентификатору
	UnitAnimation* GetAnimationById(stringc& animId);

	// Получить текущее действие
	UnitAction* GetCurrentAction();

	// Получить модель
	ISceneNode* GetSceneNode( );

	// Получить экземпляр юнита
	UnitInstanceStandard* GetUnitInstance();

	// Завершение анимации
	void OnAnimationEnd(scene::IAnimatedMeshSceneNode* node);

	// Добавить претендента на след. действие
	void AddCandidateNextAction(stringc actionName);

	// Применить действие
	bool ApplyAction(stringc actionName);

	// Изменить параметр
	bool SetParameter(stringc name, stringc val);

	// Получить параметр
	const Parameter* GetParameter(stringc name);

	// Отменить текущие действие
	bool CurrentActionNeedBreak( core::array<Event_t*>& events );

	// Завершина ли анимация
	bool GetAnimationEnd();

	// Пометить, что начилась новая анимация
	void NotifyAnimationStart();
	
	void SetModuleManager( ModuleManager* moduleManager );

	// Завершение загрузки
	void LoadComplete();

	// Путь до файла поведений
	stringc OwnerPath;

	// Путь до модели
	UnitModelBase* UnitModel;

	// Индентификатор типа модели
	int NodeId;

	// Имя модуля для выполнения
	stringc ModuleName;
private:

	// Получить действие по имени
	UnitAction* GetActionByName(stringc &actionName);


	// Класс для работы с действиями
	UnitActionUpdater* _actionUpdater;

	// Действия объекта
	core::array<UnitAction*> _actions;

	// Доступные анимации
	core::array<UnitAnimation*> _animations;

	// Объект сцены
	scene::ISceneNode* _node;
	
	// Текущие действие
	UnitAction* _currentAction;

	// Экземпляр юнита которому принадлежит поведение
	UnitInstanceStandard* _unitInstance;

	// Закончена ли анимация
	bool _animationEnd;

	// Кандидаты на след. действие
	core::array<UnitAction*> _candidateActions;
	
	// Локальные параметры
	UnitParameters* _parameters;

	// Условия установки параметров
	core::array<UnitExprParameter*> _exprParametersExt;

	// Скрипт, выполняющийся каждый update
	stringc _script;

	// Объект для выполнения скрипта
	ParserAS* _parser;

	ModuleManager* _moduleManager;

};
