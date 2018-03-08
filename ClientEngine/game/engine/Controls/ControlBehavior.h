﻿#pragma once
#include "IControl.h"
#include "ControlManager.h"
#include "../ModuleManager.h"
#include "../Core/Parsers/ParserAS.h"

class ControlBehavior : public IControl
{
public:
	ControlBehavior(SharedParams_t params, ControlManager* manager);
	virtual ~ControlBehavior(void);
	virtual CONTROL_TYPE GetControlType()
	{
		return CONTROL_TYPE_BEHAVIOR;
	};

	///////////////// IControl ////////////////////

	void Update(reciverInfo_t *reciverInfo);

	void Draw();

	// Получить границы контрола
	recti GetBounds();

	// Установить позицию контрола - ничего не делаем
	void SetPosition(position2di newPosition);

	// Получить позицию контрола
	// Всегда возвращает position2di(0, 0)
	position2di GetPosition();

	/////////////////////////////////////

	// Установить скрипт для поведения
	void SetParser( ParserAS* parser );

	// Установить модуль для поведения (скрипт не выполняется)
	void SetModule(stringc name, ModuleManager* moduleManager);
	
private:
	ParserAS* _parser;

	ModuleManager* _moduleManager;

	stringc _moduleName;
};