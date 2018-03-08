
#include "Event.h"

Event::Event()
{	
}

Event::~Event(){
}

int Event::GetEvent(int handlerId,int *eventId,EventParameters_t **params)
{
	u32 i;
	for(i=0;i<_events.size();i++)
	{
		if(!_events[i].IsUsed) continue;
		if(_events[i].HandlerId == handlerId)
		{
			*eventId = _events[i].EventId;
			*params = &_events[i].Params;
			_events[i].IsUsed = false;
			return SUCCESS;
		}
	}
	return FAIL;
}

int Event::GetEvent(int handlerId,Event_t **eventInst)
{
	u32 i;
	for(i=0;i<_events.size();i++)
	{
		if(!_events[i].IsUsed) continue;
		if(_events[i].HandlerId == handlerId)
		{
			*eventInst = &_events[i];
			_events[i].IsUsed = false;
			return SUCCESS;
		}
	}
	return FAIL;
}

void Event::RemoveNotUsedEvent()
{
	bool done = false;
	while(!done)
	{
		done = true;
		for(u32 i=0;i<_events.size();i++)
		{
			if(!_events[i].IsUsed)
			{
				_events.erase(i);
				done = false;
				break;
			}
		}
	}	
}

void Event::PostEvent(int handlerId,int eventId,EventParameters_t params)
{
	Event_t eventDesc;
	eventDesc.EventId = eventId;
	eventDesc.HandlerId = handlerId;
	eventDesc.Params = params;
	eventDesc.IsUsed = true;
	_events.push_back(eventDesc);
}

void Event::PostEvent(int handlerId,int eventId)
{
	EventParameters_t params;
	params.CommonParam = NULL;
	params.StrVar = "";
	PostEvent(handlerId,eventId,params);
}

void Event::Clear()
{
	_events.clear();
}
