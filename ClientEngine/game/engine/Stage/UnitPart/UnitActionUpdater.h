#pragma once
#include "UnitAction.h"

class UnitInstanceStandard;

class UnitActionUpdater : public Base
{
public:
	UnitActionUpdater(SharedParams_t params);
	virtual ~UnitActionUpdater(void);
	
	void SetBehavior( UnitBehavior* behavior );

	bool ApplyAction(
		scene::ISceneNode* node, 
		UnitAction* newAction, 
		UnitAction** ppCurrentAction, 
		core::array<Event_t*>& events );

	// Необходимо ли отменить текущие действие
	bool CurrentActionNeedBreak(UnitAction* currentAction, core::array<Event_t*>& events );

private:
	UnitBehavior* _behavior;

};
