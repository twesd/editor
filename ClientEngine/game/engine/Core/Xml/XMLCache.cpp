#include "XMLCache.h"
#include "../FileUtility.h"

// Иницилизация
XMLCache::XMLCache(SharedParams_t params) : Base(params),
	_items()
{
}

XMLCache::~XMLCache()
{	
	Clear();
}

// Добавить файл в кэш
XMLFileDocument* XMLCache::AddItem( stringc path )
{
	// Корректируем имя файла
	path = FileUtility::GetUpdatedFileName(path);

	XMLFileDocument* item = GetItem(path, false);
	if(item != NULL) return item;

	item = new XMLFileDocument(path, FileSystem);
	_items.push_back(item);
	return item;
}

// Получить файл из кэша
XMLFileDocument* XMLCache::GetItem( stringc path, bool addNewIfNotFound )
{
	// Корректируем имя файла
	path = FileUtility::GetUpdatedFileName(path);

	// TODO : доделать получение кэша, если относительный путь задан по иному
	for (u32 i = 0; i < _items.size() ; i++)
	{
		if(_items[i]->GetPath() == path)
		{			
			return _items[i];
		}
	}
	
	if (addNewIfNotFound)
	{
#ifdef DEBUG_MESSAGE_PRINT
		printf("WARNING : [XMLCache::GetItem] File not found in cache %s \n", path.c_str());
#endif
		return AddItem(path);
	}
	return NULL;
}

// Очистка кэша
void XMLCache::Clear()
{
	for (u32 i = 0; i < _items.size() ; i++)
	{
		delete _items[i];
	}
	_items.clear();
}
