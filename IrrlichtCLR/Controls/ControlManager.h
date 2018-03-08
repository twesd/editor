﻿#pragma once
#include "IControl.h"
#include "ControlButton.h"
#include "ControlPackage.h"
#include "../Core/Parsers/ParserAS.h"
#include "../Core/Xml/XMLCache.h"
#include "../Core/Parsers/ScriptCache.h"

class ControlManager : public Base
{
public:
	ControlManager(SharedParams_t params);
	virtual ~ControlManager(void);

	// Загрузка пакетов контролов
	void SetPackages(core::array<ControlPackage> packages);
	
	// Обновление данных
	void Update(reciverInfo_t *reciverInfo);

	// Отрисовка элементов
	void Draw();

	// Получить контрол по имени в активном пакете
	IControl* GetControlByName( stringc name );
	
	// Получить контрол по индентификатору в активном пакете
	IControl* GetControlById( stringc id );

	// Установить пакет
	void SetControlPackage( stringc name );

	// Получить имя текущего проекта
	stringc GetCurrentPackageName();

	// Добавить контрол в текущий пакет
	void AddControl(IControl* control);

	// Пометить контрол для удаления из текущего пакета
	void RemoveControl( stringc& name );

	void Clear();
	
	// Установить объект кэширования xml файлов
	void SetXmlCache(XMLCache* xmlCache);

	// Установить объект кэширования скритовых файлов
	void SetScriptCache( ScriptCache* scriptCache );

	// Получить объект кэширования xml файлов
	XMLCache* GetXmlCache();

	// Получить объект кэширования скритовых файлов
	ScriptCache* GetScriptCache();
private:
	// Установить пакет
	void SetControlPackageByIndex( u32 index );

	// Удаляем помеченные контролы
	void ExecuteRemovingControls();

	// Вызывает событие отображения нового пакета
	void RaiseEventPackageShow();

	XMLCache* _xmlCache;

	ScriptCache* _scriptCache;

	// Пакеты контролов
	core::array<ControlPackage> _packages;

	// Активный пакет контролов
	int _activatePackageNumber;

	// Необходимо загрузить новый пакет
	int _nexActivatePackage;

	// Контролы помеченые для удаления
	core::array<IControl*> _removeControls;

	ParserAS* _parser; 
};
