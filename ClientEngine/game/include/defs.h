#pragma once


#if defined(_DEBUG)
#if defined(_IRR_WINDOWS_API_) && defined(_MSC_VER) && !defined (_WIN32_WCE)
#if defined(_WIN64) // using portable common solution for x64 configuration
#include <crtdbg.h>
#define _DEBUG_BREAK_IF( _CONDITION_ ) if (_CONDITION_) {_CrtDbgBreak();}
#else
#define _DEBUG_BREAK_IF( _CONDITION_ ) if (_CONDITION_) {_asm int 3}
#endif
#else
#include "assert.h"
#define _DEBUG_BREAK_IF( _CONDITION_ ) assert( !(_CONDITION_) );
#endif
#else
#define _DEBUG_BREAK_IF( _CONDITION_ )
#endif


#define MAX_USER_ACTION_COUNT			(2)

#define FAIL			(-1)
#define SUCCESS			(0)
#define FAIL_VALUE		(-1000)