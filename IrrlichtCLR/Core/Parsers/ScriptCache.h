#pragma once
#include "../Base.h"
#include "ParserAS.h"

// Класс для чтения данных из XML
class ScriptCache : public Base
{
public:	
	// Иницилизация
	ScriptCache(SharedParams_t params);
	~ScriptCache();

	// Добавить файл в кэш
	ParserAS* AddItem(stringc path);

	// Получить файл из кэша
	ParserAS* GetItem(stringc path);

	// Очистка кэша
	void Clear();
private:
	typedef struct
	{
		// Путь до файла
		stringc Path;
		// Объект скрипта
		ParserAS* Parser;
	}CacheItem;

	ParserAS* CreateParserFromFile(stringc scriptFile);

	ParserAS* GetItem( stringc path, bool addNewIfNotExist );

	ParserAS* GetItemByFileName( stringc path );

	// Закэшированные объекты
	core::array<CacheItem> _items;
};
