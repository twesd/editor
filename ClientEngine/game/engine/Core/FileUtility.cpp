#include "FileUtility.h"

// Получить имя файла без "..". TODO : сделать нормальный алгоритм
irr::core::stringc FileUtility::GetUpdatedFileName( stringc& filename )
{
	_DEBUG_BREAK_IF(filename.size() == 0)
	stringc destFileName;

	int skipCounter = 0;
	for (s32 i = filename.size() - 1; i >= 0; i--)
	{
		char c = filename[i];
		if(skipCounter > 0 && c != '/')
			continue;
		if(skipCounter > 0)
		{
			// пропускаем '/'
			skipCounter--;
			if(i > 2)
			{
				if(filename[i - 1] == '.' && filename[i - 2] == '.')
					skipCounter += 2;
			}
			continue;
		}

		if(c == '/')
		{
			if( i >= 2 && filename[i - 1] == '.' && filename[i - 2] == '.')
			{
				skipCounter += 2;
			}
		}
		stringc cStr;
		cStr += c;
		destFileName = cStr + destFileName;
	}

	if(destFileName[0] == '/')
		destFileName = destFileName.subString(1, destFileName.size());

	if(filename[0] == '.')
	{
		stringc startPart;
		for (u32 i = 0; i < filename.size(); i++)
		{
			char c = filename[i];
			if(c != '.' && c != '/') break;
			startPart += c;
		}
		destFileName = startPart + destFileName;
	}

	return destFileName;
}