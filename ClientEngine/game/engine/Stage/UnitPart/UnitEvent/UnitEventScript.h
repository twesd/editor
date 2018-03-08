#pragma once
#include "UnitEventBase.h"
#include "../../../Core/Parsers/ParserAS.h"

class UnitEventScript : public UnitEventBase
{
public:
	UnitEventScript(SharedParams_t params);
	virtual ~UnitEventScript(void);

	// Выполняется ли условие
	virtual bool IsApprove(core::array<Event_t*>& events);

	void SetParser(ParserAS* parser);

private:
	ParserAS* _parser;
};

