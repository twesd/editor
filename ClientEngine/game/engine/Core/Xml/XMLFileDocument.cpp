#include "XMLFileDocument.h"
#include "Core/FileUtility.h"

using namespace rapidxml;

// Иницилизация
XMLFileDocument::XMLFileDocument( stringc path, io::IFileSystem* fileSystem)
{
	_document = NULL;
	_data = NULL;
	_path = FileUtility::GetUpdatedFileName(path);
	io::IReadFile* rFile = fileSystem->createAndOpenFile(_path.c_str());
	_DEBUG_BREAK_IF(rFile == NULL)
	u32 fSize = (u32)rFile->getSize();
	_data = new char[fSize + 1];
	s32 nBytes = rFile->read(_data, fSize);
	if(nBytes != fSize)
	{
	  _DEBUG_BREAK_IF(nBytes != fSize)
	}
	_data[fSize] = 0;	
	_document = new xml_document<>();
	_document->parse<0>(_data);
	rFile->drop();
}

XMLFileDocument::~XMLFileDocument()
{	
	if(_document != NULL)
		delete _document;
	if(_data != NULL)
		delete _data;
}

xml_document<>* XMLFileDocument::GetDocument()
{
	return _document;
}

stringc XMLFileDocument::GetPath()
{
	return _path;
}
