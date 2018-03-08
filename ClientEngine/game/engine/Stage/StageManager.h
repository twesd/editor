#pragma once
#include "../Core/Base.h"
#include "../Controls/ControlManager.h"
#include "../Core/Xml/XMLCache.h"
#include "../Core/Parsers/ScriptCache.h"
#include "UnitManager.h"
#include "StageManagerEvents.h"
#include "../ModuleManager.h"
#include "../Sounds/SoundManager.h"
#include "../InAppPurchases/InAppPurchases.h"


class StageManager : public Base
{
public:
	StageManager(
		SharedParams_t params, 
		ModuleManager* moduleManager,
		SoundManager* soundManager,
		InAppPurchases* inAppManager,
		float controlsScale,
		position2di controlsOffset);

	virtual ~StageManager(void);

	// Загрузка стадии из файла
	void LoadStage(stringc path);

	// Обновление
	void Update(reciverInfo_t *reciverInfo);

private:
	// Обработка событий
	void HandleEvent();
	// Иницилизация стадии
	void InitStage();

	ControlManager* _controlManager;
	
	UnitManager* _unitManager;
	
	ModuleManager* _moduleManager;

	SoundManager* _soundManager;

	InAppPurchases* _inAppManager;

	// Кэш xml файлов
	XMLCache* _xmlCache;

	// Кэш скриптов
	ScriptCache* _scriptCache;

	/// <summary>
	/// Включить / отключить обработку UnitManager
	/// </summary>
	bool _enableUnitManager;

	/// <summary>
	/// Включить / отключить обработку SceneManager->DrawAll()
	/// </summary>
	bool _enableDrawAll;

	// Это первый вызов обновления
	bool _isFirst;

	// Коэф. масштабирования контролов
	float _controlsScale;

	// Глобальное смещение контролов
	position2di _controlsOffset;
};
