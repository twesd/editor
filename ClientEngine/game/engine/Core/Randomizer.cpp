#include "Randomizer.h"

// our Randomizer is not really os specific, so we
// code one for all, which should work on every platform the same,
// which is desireable.

s32 Randomizer::_seed = 0x0f0f0f0f;

//! generates a pseudo random number
s32 Randomizer::Rand()
{
	const s32 m = 2147483399;	// a non-Mersenne prime
	const s32 a = 40692;		// another spectral success story
	const s32 q = m/a;
	const s32 r = m%a;		// again less than q

	_seed = a * (_seed%q) - r* (_seed/q);
	if (_seed<0) _seed += m;

	return _seed;
}

//! resets the randomizer
void Randomizer::Reset()
{
	_seed = 0x0f0f0f0f;
}

s32 Randomizer::Rand( int limit )
{
	int	  retValue;
	const s32 m = 2147483399;	// a non-Mersenne prime
	const s32 a = 40692;		// another spectral success story
	const s32 q = m/a;
	const s32 r = m%a;		// again less than q

	_seed = a * (_seed%q) - r* (_seed/q);
	if (_seed<=0) _seed += m;

	//2147483647 - max Int
	retValue =	(_seed/(2147483647/(limit+1)));

	return	retValue;
}
