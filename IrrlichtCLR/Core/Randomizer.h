#pragma once

#include "Base.h"

class Randomizer
{
public:

	//! resets the randomizer
	static void Reset();

	//! generates a pseudo random number
	static s32 Rand();

	static s32 Rand(int limit);

private:

	static s32 _seed;

};
