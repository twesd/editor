#pragma once
#include "UnitInstanceBase.h"
#include "Core/TimerBase.h"
#include "../UnitGenInstance/UnitGenInstanceStandard.h"
#include "../UnitPart/UnitMapping/UnitMappingBase.h"

class UnitManager;

class UnitInstanceStandard : public UnitInstanceBase
{
public:
	UnitInstanceStandard(
		SharedParams_t params, 
		UnitGenInstanceStandard *genInstance, 
		UnitInstanceBase* parent, 
		UnitManager* unitManager,
		SoundManager* soundManager,
		UnitInstanceBase* creator);
	virtual ~UnitInstanceStandard(void);
	
	// Установить модель
	void SetSceneNode(ISceneNode* node);

	// Обновить данные
	virtual void Update();
	
	// Обработка событий
	virtual void HandleEvent(core::array<Event_t*>& events);
	
	// Создать дочерний юнит
	void CreateChild(UnitGenInstanceStandard genInstance, bool allowSeveralInstances);

	// Получить дочерний узел
	UnitInstanceStandard* GetChildByPath(stringc behaviorPath);

	// Удалить дочерний узел
	void DeleteChild(stringc behaviorPath);

	// Создать внешний юнит
	void CreateExternalUnit(UnitGenInstanceStandard genInstance, bool allowSeveralInstances);

	// Получить путь до файла поведения
	stringc GetBehaviorPath();

	// Получить описание поведения
	UnitBehavior* GetBehavior();

	// Получить менджер юнитов
	UnitManager* GetUnitManager();

	// Добавить связь
	// Данный метод захватывает объект grab()
	void AddMapping(UnitMappingBase* mapping);

	TimerBase* GetTimerByName( const stringc& name, bool createNewIfNotExist);

private:
	// Обновление локальных таймеров
	void UpdateTimers();

	// Поведение юнита
	UnitBehavior* _behavior;

	// Менеджер юнитов
	UnitManager* _unitManager;
	
	// Менеджер звуков
	SoundManager* _soundManager;

	// Связи
	core::array<UnitMappingBase*> _mappings;
		
	// Начальный скрипт
	ParserAS* _startScript;

	// Флаг первой обработки событий
	bool _isFirstHandle;

	// Таймеры
	core::array<TimerBase*> _timers;
};
