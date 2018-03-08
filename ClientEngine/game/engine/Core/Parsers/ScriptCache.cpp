#include "ScriptCache.h"
#include "../../ParserExtensions/ParserExtensionCommon.h"
#include "../../ParserExtensions/ParserExtensionUnits.h"
#include "../../ParserExtensions/ParserExtensionControls.h"
#include "../FileUtility.h"

// Иницилизация
ScriptCache::ScriptCache(SharedParams_t params) : Base(params),
	_items()
{
}

ScriptCache::~ScriptCache()
{	
	Clear();
}

// Добавить файл в кэш
ParserAS* ScriptCache::AddItem( stringc path )
{
	// Корректируем имя файла
	path = FileUtility::GetUpdatedFileName(path);

	ParserAS* parser = GetItemByFileName(path);
	if (parser != NULL) return parser;

	CacheItem item;
	item.Path = path;
	item.Parser = CreateParserFromFile(path);
	_items.push_back(item);
	return item.Parser;
}


ParserAS* ScriptCache::CreateParserFromFile(stringc scriptFile)
{
	io::IReadFile* f = FileSystem->createAndOpenFile(scriptFile.c_str());
	_DEBUG_BREAK_IF(f == 0)

	int len = f->getSize();


	// Read the entire file
	char* code = new char[len+1];
//	size_t c = fread(code, len, 1, f);
	s32 c = f->read(code, len);
	code[len] = 0;
	f->drop();
	if(c == 0)
	{
		_DEBUG_BREAK_IF(c == 0)
	}

		
	ParserAS* parser = new ParserAS();
	
	ParserExtensionCommon_RegisterFunctions(parser->GetEngine());
	ParserExtensionUnits_RegisterFunctions(parser->GetEngine());
	ParserExtensionControls_RegisterFunctions(parser->GetEngine());

	parser->SetCode(stringc(code));

	delete code;
	return parser;	
}

// Получить файл из кэша
ParserAS* ScriptCache::GetItem( stringc path, bool addNewIfNotExist )
{
	// Корректируем имя файла
	path = FileUtility::GetUpdatedFileName(path);

	// TODO : доделать получение кэша, если относительный путь задан по иному
	ParserAS* parser = GetItemByFileName(path);
	if (parser != NULL) 
	{
		return parser;
	}

	if(!addNewIfNotExist)
	{
		return NULL;
	}
	
#ifdef DEBUG_MESSAGE_PRINT
	printf("WARNING : [ScriptCache::GetItem] File not found in cache %s \n", path.c_str());
#endif
	return AddItem(path);
}

// Получить файл из кэша
ParserAS* ScriptCache::GetItem( stringc path )
{
	return GetItem(path, true);
}

// Очистка кэша
void ScriptCache::Clear()
{
	for (u32 i = 0; i < _items.size() ; i++)
	{
		delete _items[i].Parser;
	}
	_items.clear();
}

ParserAS* ScriptCache::GetItemByFileName( stringc path )
{
	for (u32 i = 0; i < _items.size() ; i++)
	{
		if(_items[i].Path == path)
		{
			return _items[i].Parser;
		}
	}
	return NULL;
}
