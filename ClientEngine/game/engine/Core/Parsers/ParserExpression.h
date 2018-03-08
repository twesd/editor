#pragma once
#include "../Base.h"

const int PARSER_NAME_LEN_MAX = 30;

class ParserExpression
{
public:
	ParserExpression();

	virtual ~ParserExpression();

	// Установить выражение
	void SetExpression(stringc expression);

	// Установить переменную
	void SetVariable(stringc name, stringc val);

	// Очитка переменных
	void ClearVariables();

	// Выполнить выражение
	stringc Eval();

	// Выполнить выражение и получить результат в виде float
	f32 EvalAsFloat();
	
	// Получить переменную
	Parameter* GetVariable(stringc& name);

private:
	enum TOKENTYPE {NOTHING = -1, DELIMETER, NUMBER, VARIABLE, FUNCTION, STRING, UNKNOWN};

	enum OPERATOR_ID 
	{
		AND, LOGICAL_AND, OR, BITSHIFTLEFT, BITSHIFTRIGHT,                 
		EQUAL, UNEQUAL, SMALLER, LARGER, SMALLEREQ, LARGEREQ, 
		PLUS, MINUS,                    
		MULTIPLY, DIVIDE, MODULUS, XOR, 
		POW                            
	}; 

	void GetToken();

	stringc ParseLevel1();

	stringc ParseLevel2();

	stringc ParseLevel3();

	stringc ParseLevel4();

	stringc ParseLevel5();

	stringc ParseLevel6();

	stringc ParseLevel7();

	stringc ParseLevel8();

	stringc ParseLevel9();

	stringc ParseNumber();

	int GetOperatorId(const char op_name[]);

	stringc EvalOperator(const int op_id, const stringc &val1Str, const stringc &val2Str);

	// Обработать функцию
	stringc ParseFunction(stringc& functionName);

	bool IsDigit(const char c);

	bool IsDigitDot(const char c);

	bool IsAlpha(const char c);

	bool IsNotDelimeter(const char c);

	bool IsDelimeter(const char c);

	bool IsWhiteSpace(const char c);

	bool IsMinus(const char c);

	c8* _curExpr;                      // points to a character in expr

	char _token[PARSER_NAME_LEN_MAX+1];   // holds the token
	TOKENTYPE _tokenType;         // type of the token

	// Выражение для параметра
	stringc _expression;

	// Переменные
	core::array<Parameter> _vars;

};
