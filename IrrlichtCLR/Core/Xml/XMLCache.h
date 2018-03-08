#pragma once
#include "../Base.h"
#include "XMLFileDocument.h"

// Класс для чтения данных из XML
class XMLCache : public Base
{
public:	
	// Иницилизация
	XMLCache(SharedParams_t params);
	~XMLCache();

	// Добавить файл в кэш
	XMLFileDocument* AddItem(stringc path);

	// Получить данные из кэша
	XMLFileDocument* GetItem(stringc path, bool addNewIfNotFound);

	// Очистка кэша
	void Clear();

private:
	// Кэшированные объекты
	core::array<XMLFileDocument*> _items;
};
