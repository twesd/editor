#pragma once
#include <cmath>
#include <iostream>
#include <iomanip>


typedef struct
{
    char RIFFId[4];
    long int Length;
    char WaveId[4];
    char FmtId[4];
    long int PCMHeaderLength;
    short int FormatTag;
    short int NumChannels;
    long int SamplesPerSec;
    long int AvgBytesPerSec;
    short int NumBlockAlingn;
    short int NumBitsPerSample;
} WavHeader;

typedef struct
{
    char DataId[4];
    long DataLength;
} ChunkHeader;


class WavFileReader
{
    public:
        WavFileReader ();

		~WavFileReader ();

		bool Read(const char* fileName);

		bool GetActualData(unsigned char** buffer, unsigned int* bufferLength);

        int GetNumChannels();

        int GetBitsPerSample();

        int GetSampleRateHz();

private:
        int _freq;
        int _bitsPerSample;
        int _numChannel;

		unsigned char* _buffer;
		unsigned int _bufferLength;
};
