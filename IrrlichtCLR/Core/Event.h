#pragma once

#include "global.h"

class Event
{
public:
	Event();
	~Event();
	enum{
		ID_UNKNOWN = 0x787,
		ID_MANAGER,
		ID_STAGE_MANAGER,
		ID_UNIT_MANAGER,
		ID_CONTROL_MANAGER
	};

	int GetEvent(int handlerId,int *eventId,EventParameters_t **params);
	int GetEvent(int handlerId, Event_t **eventInst);
	void PostEvent(int handlerId,int eventId,EventParameters_t params);
	void PostEvent(int handlerId,int eventId);
	void RemoveNotUsedEvent();
	void Clear();
private:
	core::array<Event_t> _events;
};
