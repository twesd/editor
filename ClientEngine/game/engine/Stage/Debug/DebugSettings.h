#pragma once
#include "../../Core/Base.h"

#ifdef DEBUG_VISUAL_DATA

class DebugSettings
{
public:
	static DebugSettings* GetInstance()
	{
		static DebugSettings instance;
		return &instance;
	}

	// Загрузка описания отладки из файла
	static void LoadFromFile(SharedParams_t params, stringc path);
		
	// Включено ли отладка
	bool Enabled;

	// Путь до шрифта
	stringc FontPath;

	// Показывать ли динамически параметры
	bool DynamicShowParameters;

	// Отображать текущее действие
	bool DynamicShowAction;

	// Режим получения информации
	bool DynamicDebugInfoMode;

	// Фильтр для режима получения информации
	int DebugInfoFilterId;

	// Фильтр моделий
	core::array<int> FilterIds;

	// Содержит ли фильтр моделий данный индентификатор
	bool ContainsNodeId(int id);

private:
	DebugSettings();

};

#endif