#pragma once
#include "../Base.h"
#include "XMLHelper.h"
#include "RapidXML/rapidxml.h"

// Класс для данных XML
class XMLFileDocument 
{
public:	
	// Иницилизация
	XMLFileDocument(stringc path, io::IFileSystem* fileSystem);
	~XMLFileDocument();
		
	rapidxml::xml_document<>* GetDocument();

	stringc GetPath();

private:
	// Документ
	rapidxml::xml_document<>* _document;

	// Данные файла
	char* _data;

	stringc _path;
};
