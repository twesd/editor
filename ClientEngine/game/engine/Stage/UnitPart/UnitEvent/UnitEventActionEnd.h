#pragma once
#include "UnitEventBase.h"

class UnitActionUpdater;

class UnitEventActionEnd : public UnitEventBase
{
public:
	UnitEventActionEnd(SharedParams_t params);
	virtual ~UnitEventActionEnd(void);

	// Выполняется ли условие
	virtual bool IsApprove(core::array<Event_t*>& events);

	virtual void SetBehavior(UnitBehavior* behavior);

private:
	UnitActionUpdater* _actionUpdater;
};

