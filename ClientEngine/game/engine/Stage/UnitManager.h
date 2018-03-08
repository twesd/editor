#pragma once
#include "Core/Xml/XMLCache.h"
#include "Core/Parsers/ScriptCache.h"
#include "Core/TimerBase.h"
#include "unitGenInstance/UnitGenInstanceBase.h"
#include "unitInstance/UnitInstanceBase.h"
#include "../ModuleManager.h"


class UnitManager : public Base
{
public:
	UnitManager(SharedParams_t params);
	virtual ~UnitManager(void);
	
	// Загрузка описания юнита <TreeView>
	void LoadUnitGensFromXmlTag(rapidxml::xml_node<>* sourceNode, stringc root);

	// Обновить данные
	void Update();
	
	// Создать новый юнит
	void CreateNewUnit(UnitGenInstanceBase* genInstance, UnitInstanceBase* creator );

	// Получить юнит по модели
	UnitInstanceBase* GetInstanceBySceneNode(ISceneNode* node);

	// Получить юниты по имени
	UnitInstanceBase* GetInstanceByName(const stringc& name);

	core::array<UnitInstanceBase*> GetInstances();
	
	// Существует ли заданный юнит
	bool GetIsUnitInstanceExist( UnitInstanceBase* instance );

	// Установить объект кэширования xml файлов
	void SetXmlCache(XMLCache* xmlCache);

	// Установить объект кэширования скритовых файлов
	void SetScriptCache( ScriptCache* scriptCache );

	// Установить начальное имя камеры
	void SetStartCameraName(stringc cameraName);
	
	// Получить объект кэширования xml файлов
	XMLCache* GetXmlCache();

	// Получить объект кэширования скритовых файлов
	ScriptCache* GetScriptCache();

	// Установить объект доп. модулей
	void SetModuleManager(ModuleManager* moduleManager);

	// Установить менеджер звуков
	void SetSoundManager(SoundManager* soundManager);

	// Получить объект доп. модулей
	ModuleManager* GetModuleManager();

	TimerBase* GetTimerByName(const stringc& name, bool createNewIfNotExist);

private:
	// Получить юнит по модели в дочерних элементах
	UnitInstanceBase* GetChildInstanceBySceneNode(UnitInstanceBase* instance, ISceneNode* node );

	// Получить юнит по имени в дочерних элементах
	UnitInstanceBase* GetChildInstanceByName(UnitInstanceBase* instance, const stringc& name );

	// Обновление таймеров
	void UpdateTimers();

	// Обработка создания новых юнитов
	void UpdateGenInstances();

	// Обработка событий
	void HandleEvent();

	// Обновить юниты
	void UpdateInstances();

	// Удалить помеченные для удаления юниты
	void DeleteErasedChilds(UnitInstanceBase* instance);

	// Описания для создания экземпляра юнита
	core::array<UnitGenInstanceBase*> _genInstances;

	// Описания юнитов
	core::array<UnitInstanceBase*> _instances;

	// Таймеры
	core::array<TimerBase*> _timers;

	XMLCache* _xmlCache;

	ScriptCache* _scriptCache;

	stringc _startCameraName;

	ModuleManager* _moduleManager;

	SoundManager* _soundManager;
};
