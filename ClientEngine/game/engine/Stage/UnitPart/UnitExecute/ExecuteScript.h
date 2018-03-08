#pragma once
#include "ExecuteBase.h"
#include "../../../Core/Parsers/ParserAS.h"

class ExecuteScript : public ExecuteBase
{
public:
	ExecuteScript(SharedParams_t params);
	virtual ~ExecuteScript(void);

	// Выполнить действие
	virtual void Run(scene::ISceneNode* node, core::array<Event_t*>& events);

	// Установить экземпляр юнита
	virtual void SetUnitInstance(UnitInstanceStandard* unitInstance);

	void SetParser(ParserAS* parser);

private:
	ParserAS* _parser;

	UnitInstanceStandard* _unitInstance;
};
