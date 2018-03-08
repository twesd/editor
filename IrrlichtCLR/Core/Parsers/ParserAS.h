#pragma once
#include "../Base.h"
#include <angelscript.h>
#include "ParserUserData.h"

class ParserAS
{
public:
	ParserAS();
	~ParserAS();

	// Установить входной аргумент функции
	void SetArgString( asUINT index, std::string* val );

	// Установить входной аргумент функции
	void SetArgFloat( asUINT index, float val );

	// Установить выражение
	void SetCode(stringc expression);

	// Установить выполняемую функции
	void SetFuncDecl(stringc decl);

	ParserUserData* GetUserData();

	// Выполнить выражение
	stringc EvalAsString();

	// Выполнить выражение и вернуть результат как bool
	bool EvalAsBool();

	void ExecuteCustom();

	void Execute();

	// Получить результат как string
	stringc GetReturnAsString();

	// Получить результат как float
	float GetReturnAsFloat();

	asIScriptEngine* GetEngine();

private:
	void ClearUserData();

	asIScriptEngine *_engine;

	asIScriptContext *_ctx;

	int _funcId;

	int _funcIdStr;

	int _funcIdBool;

	int _funcIdCustom;

	stringc _expression;

	bool _preparedCustom;
};
