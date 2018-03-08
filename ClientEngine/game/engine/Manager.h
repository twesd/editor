#pragma once

#include "Core/Event.h"
#include "Core/Reciver.h"
#include "Core/Parsers/ParserAS.h"
#include "Stage/StageManager.h"
#include "ManagerEvents.h"
#include "ModuleManager.h"
#include "TestFlight/TestFlightManager.h"
#include "InAppPurchases/InAppPurchases.h"
#include "Ad/AdManager.h"
#ifdef DEBUG_VISUAL_DATA
#include "Stage/Debug/DebugSettings.h"
#include "Stage/Debug/DebugReciver.h"
#endif

class Manager: public Base
{
public:
	Manager(
		SharedParams_t params, 
		bool useAd, 
		float controlsScale, 
		float reciverScale, 
		position2di controlsOffset);
	~Manager();

	static SharedParams_t CreateSharedParams(
		IrrlichtDevice *device,
		UserSettings *setting,
		core::dimension2d<s32> windowSize);

	void Update();
	
	void Init();
private:
	typedef struct  
	{
		// Путь до файла стадии
		stringc Path;

		// Скрипт при завершении стадии
		stringc ScriptOnComplete;

		// Является ли стадия начальной
		bool IsStartStage;
	}StageItem;
	

	void HandleEvent();
	void GetReciverInfo();
	void ClearReciverInfo();
	void Clear();

	// Загрузка основного файла
	void LoadMain( stringc path );

	// Загрузка стадии
	void LoadStage(stringc stageName);

	//Отвечает за события от клавиатуры, мыши и прикосновений
	Reciver*				_reciver;
	reciverInfo_t			_reciverInfo;
#ifdef DEBUG_VISUAL_DATA
	DebugReciver* _dbgReciver;
#endif

	// Коэфициент масштабирования контролов
	float _controlsScale;

	// Глобальное смещение контролов
	position2di _controlsOffset;

	// Время предыдущего тика
	u32 _lastTime;

	// Объект для работы с скриптами
	ParserAS _parser;

	// Объект для работы с стадиями
	StageManager* _stageManager;

	// Объект для работы с доп. модулями
	ModuleManager* _moduleManager;

	InAppPurchases* _inAppManager;

	AdManager* _adManager;

	// Объект для работы со звуком
	SoundManager* _soundManager;

	core::array<StageItem> _stageItems;

	// Индекс текущей стадии
	s32 _currentStageIndex;	
};
