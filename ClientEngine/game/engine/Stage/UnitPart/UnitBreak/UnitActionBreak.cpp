#include "UnitActionBreak.h"

UnitActionBreak::UnitActionBreak(SharedParams_t params) : Base(params),
	 AnimationEnd(true), 
	 StartClauseNotApproved(false), 
	 StartClauseApproved(false),
	 IsExecuteOnly(false),	 
	 AnimatorEnd(UnitActionBreak::AnimatorNone),
	 ScriptFileName()
{
	
}

UnitActionBreak::~UnitActionBreak(void)
{
	
}

