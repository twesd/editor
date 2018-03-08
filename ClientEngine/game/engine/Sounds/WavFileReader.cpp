#include "WavFileReader.h"
#include <string.h>
#include <stdio.h>
#include <stdlib.h>
#include <math.h>

WavFileReader::WavFileReader()
{
	_buffer = NULL;
	_bufferLength = NULL;
}

WavFileReader::~WavFileReader()
{
	if(_buffer != NULL) delete _buffer;
}


int WavFileReader::GetNumChannels()
{
	return _numChannel;
}

int WavFileReader::GetBitsPerSample()
{
	return _bitsPerSample;
}

int WavFileReader::GetSampleRateHz()
{
	return _freq;
}

bool WavFileReader::Read(const char* fileName)
{
	int i;
	FILE *hFile;
	unsigned int res;	
	long int rMore;

	WavHeader* header = new WavHeader();
	ChunkHeader* chunkHeader = new ChunkHeader();

	if(header == NULL || chunkHeader == NULL)
		return false;

	hFile = fopen( fileName, "rb");
	if(hFile == NULL)
	{       
		return false;
	}

	// Чтение заголовка
	res = fread((void*) header, sizeof(WavHeader), (size_t)1, hFile);
	if(res != 1)
	{
		return false;
	}

	// Проверка формата
	//
	char outBuffer[5];
	for(i = 0; i < 4; i++)
	{
		outBuffer[i] = header->RIFFId[i];
	}
	outBuffer[4] = 0;
	if(strcmp(outBuffer, "RIFF") != 0)
	{
		return false;
	}

	for(i = 0; i < 4; i++)
	{
		outBuffer[i] = header->WaveId[i];
	}
	outBuffer[4] = 0;

	if(strcmp(outBuffer, "WAVE") != 0)
	{
		return false;
	}

	for(i = 0; i < 4; i++)
	{
		outBuffer[i] = header->FmtId[i];
	}
	outBuffer[4] = 0;

	if(strcmp(outBuffer, "fmt ") != 0)
	{
		return false;
	}

	if(header->FormatTag != 1)
	{
		return false;
	}

	if( (header->NumBitsPerSample != 16) && (header->NumBitsPerSample != 8))
	{
		return false;
	}

	rMore = header->PCMHeaderLength - (sizeof(WavHeader) - 20);
	if(fseek(hFile, rMore, SEEK_CUR) != 0)
	{
		return false;
	}

	int counter = 1;
	while(true)
	{		
		if(counter > 10) 
		{ 
			// Превышен лимит
			return false;
		}

		// Читаем блок данных
		res = fread((void*)chunkHeader, sizeof(ChunkHeader), (size_t)1, hFile);
		if(res != 1)
		{
			return false;
		}
		// Тип блока данных
		for(i =0; i < 4; i++)
		{
			outBuffer[i] = chunkHeader->DataId[i];
		}
		outBuffer[4] = 0;
		if(strcmp(outBuffer, "data") == 0) 
			break;

		counter++;
		res = fseek(hFile, chunkHeader->DataLength, SEEK_CUR);
		if(res != 0)
			return false;
	}

	_bufferLength = chunkHeader->DataLength;

	_buffer = new unsigned char[_bufferLength];
	if( _buffer == NULL)
		return false;

	// Чтение данных
	res = fread((void*)_buffer, _bufferLength, (size_t)1, hFile);
	if(res == 0)
		return false;

	// Сохраняем информация о файле
	_freq = (int) (header->SamplesPerSec);
	_bitsPerSample = header->NumBitsPerSample;
	_numChannel = header->NumChannels;

	if(header != NULL) delete header;
	if(chunkHeader != NULL) delete chunkHeader;
	fclose(hFile);

	return true;
}


bool WavFileReader::GetActualData( unsigned char** buffer, unsigned int* bufferLength )
{
	if(_buffer == NULL || _bufferLength == 0) 
		return false;
	*buffer = _buffer;
	*bufferLength = _bufferLength;
	return true;
}



