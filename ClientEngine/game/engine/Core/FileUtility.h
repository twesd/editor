#pragma once
#include "Base.h"

class FileUtility 
{
public:	
	// Получить имя файла без "..". TODO : сделать нормальный алгоритм
	static irr::core::stringc GetUpdatedFileName( stringc& filename );
};
