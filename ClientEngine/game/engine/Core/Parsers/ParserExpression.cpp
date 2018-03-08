#include "ParserExpression.h"
#include <stdio.h>
#include <ctype.h>

ParserExpression::ParserExpression() :
	_expression(), _vars()
{
	_curExpr = NULL;

	_token[0] = '\0';
	_tokenType = NOTHING;

	// Устанавливаем параметры по умолчанию
	SetVariable("true", "true");
	SetVariable("false", "false");
}

ParserExpression::~ParserExpression()
{
	
}

// Установить переменную
void ParserExpression::SetVariable( stringc name, stringc val )
{
	Parameter* param = GetVariable(name);
	if (param != NULL)
	{
		param->Value = val;
		return;
	}

	Parameter newParam;
	newParam.Name = name;
	newParam.Value = val;
	_vars.push_back(newParam);
}

// Очитка переменных
void ParserExpression::ClearVariables()
{
	_vars.clear();
}

// Установить выражение
void ParserExpression::SetExpression( stringc expression )
{
	_DEBUG_BREAK_IF(expression == "")
	_expression = expression;
}

// Выполнить выражение
stringc ParserExpression::Eval()
{
	stringc resVal;
	stringc expr;

	expr = _expression;

	// initialize all variables
	_curExpr = (c8*)expr.c_str(); // let e point to the start of the expression

	GetToken();
	if (_tokenType == DELIMETER && *_token == '\0')
	{
		_DEBUG_BREAK_IF(true)
	}

	resVal = ParseLevel2();

	// check for garbage at the end of the expression
	// an expression ends with a character '\0' and token_type = delimeter
	if (_tokenType != DELIMETER || *_token != '\0')
	{
		_DEBUG_BREAK_IF(true)
	}

	return resVal;
}

// Выполнить выражение
irr::f32 ParserExpression::EvalAsFloat()
{
	return core::fast_atof(Eval().c_str());
}

// Получить переменную
Parameter* ParserExpression::GetVariable( stringc& name )
{
	for (u32 i = 0; i < _vars.size() ; i++)
	{
		if (name == _vars[i].Name)
		{
			return &_vars[i];
		}
	}
	return NULL;
}


/*
 * checks if the given char c is a minus
 */
bool ParserExpression::IsMinus(const char c)
{
    if (c == 0) return 0;
    return c == '-';
}

/*
 * checks if the given char c is whitespace
 * whitespace when space chr(32) or tab chr(9)
 */
bool ParserExpression::IsWhiteSpace(const char c)
{
    if (c == 0) return 0;
    return c == 32 || c == 9;  // space or tab
}

/*
 * checks if the given char c is a delimeter
 * minus is checked apart, can be unary minus
 */
bool ParserExpression::IsDelimeter(const char c)
{
    if (c == 0) return 0;
    return strchr("&|<>=+/*%^!,", c) != 0;
}

/*
 * checks if the given char c is NO delimeter
 */
bool ParserExpression::IsNotDelimeter(const char c)
{
    if (c == 0) return 0;
    return strchr("&|<>=+-/*%^!,()", c) != 0;
}

/*
 * checks if the given char c is a letter or undersquare
 */
bool ParserExpression::IsAlpha(const char c)
{
    if (c == 0) return false;
    return strchr("ABCDEFGHIJKLMNOPQRSTUVWXYZ_", toupper(c)) != 0;
}

/*
 * checks if the given char c is a digit or dot
 */
bool ParserExpression::IsDigitDot(const char c)
{
    if (c == 0) return 0;
    return strchr("0123456789.", c) != 0;
}

/*
 * checks if the given char c is a digit
 */
bool ParserExpression::IsDigit(const char c)
{
    if (c == 0) return 0;
    return strchr("0123456789", c) != 0;
}

void ParserExpression::GetToken()
{
    _tokenType = NOTHING;
	// Указатель на символ в токене
    char* t;
    t = _token;
	// Зануляем токен
    *t = '\0';

    // Пропускаем пробелы, переходы на новую строчку
    while (*_curExpr == ' ' || *_curExpr == '\t' || 
		*_curExpr == '\r'|| *_curExpr == '\n')
    {
        _curExpr++;
    }

    // check for end of expression
    if (*_curExpr == '\0')
    {
        // token is still empty
        _tokenType = DELIMETER;
        return;
    }

    // check for minus
    if (*_curExpr == '-')
    {
        _tokenType = DELIMETER;
        *t = *_curExpr;
        _curExpr++;
        t++;
        *t = '\0';  // add a null character at the end of token
        return;
    }

    // check for parentheses
    if (*_curExpr == '(' || *_curExpr == ')')
    {
        _tokenType = DELIMETER;
        *t = *_curExpr;
        _curExpr++;
        t++;
        *t = '\0';
        return;
    }

    // check for operators (delimeters)
    if (IsDelimeter(*_curExpr))
    {
        _tokenType = DELIMETER;
        while (IsDelimeter(*_curExpr))
        {
            *t = *_curExpr;
            _curExpr++;
            t++;
        }
        *t = '\0';  // add a null character at the end of token
        return;
    }

    // check for a value
    if (IsDigitDot(*_curExpr))
    {
        _tokenType = NUMBER;
        while (IsDigitDot(*_curExpr))
        {
            *t = *_curExpr;
            _curExpr++;
            t++;
        }

        // check for scientific notation like "2.3e-4" or "1.23e50"
        if (toupper(*_curExpr) == 'E')
        {
            *t = *_curExpr;
            _curExpr++;
            t++;

            if (*_curExpr == '+' || *_curExpr == '-')
            {
                *t = *_curExpr;
                _curExpr++;
                t++;
            }

            while (IsDigit(*_curExpr))
            {
                *t = *_curExpr;
                _curExpr++;
                t++;
            }
        }

        *t = '\0';
        return;
    }

    // check for variables or functions
    if (IsAlpha(*_curExpr))
    {
        while (IsAlpha(*_curExpr) || IsDigit(*_curExpr))
        //while (isNotDelimeter(*e))
        {
            *t = *_curExpr;
            _curExpr++;
            t++;
        }
        *t = '\0';  // Завершаем строку

        // check if this is a variable or a function.
        // a function has a parentesis '(' open after the name
        char* e2 = NULL;
        e2 = _curExpr;

		// Пропускаем пробелы

		while (*e2 == ' ' || *e2 == '\t')
		{
            e2++;
        }

        if (*e2 == '(')
        {
            _tokenType = FUNCTION;
        }
        else
        {
            _tokenType = VARIABLE;
        }
        return;
    }

	if (*_curExpr == '\"')
	{
		_tokenType = STRING;
		_curExpr++;
		while (*_curExpr != '\0' && *_curExpr != '\"')
		{
			*t = *_curExpr;
			_curExpr++;
			t++;
		}
		
		_curExpr++;
		*t = '\0';  // Завершаем строку
		return;
	}

    // something unknown is found, wrong characters -> a syntax error
    _tokenType = UNKNOWN;
    while (*_curExpr != '\0')
    {
        *t = *_curExpr;
        _curExpr++;
        t++;
    }
    *t = '\0';
    
	_DEBUG_BREAK_IF(true)

    return;
}


/*
 * assignment of variable or function
 */
stringc ParserExpression::ParseLevel1()
{
	if (_tokenType == VARIABLE)
	{
		char* savePos = _curExpr;
		TOKENTYPE tokenTypeNow = _tokenType;
		stringc tokenNow(_token);

		GetToken();
		if (strcmp(_token, "=") == 0)
		{			
			GetToken();
			stringc var = ParseLevel2();
			SetVariable(tokenNow, var);
			return stringc(1);
		}
		else
		{
			// Возвращаемся к предыдущему токену
			_curExpr = savePos;
			_tokenType = tokenTypeNow;			
			strcpy(_token, tokenNow.c_str());
		}
	}
    return ParseLevel2();
}


/*
 * conditional operators and bitshift
 */
stringc ParserExpression::ParseLevel2()
{
    int op_id;
    stringc res;
    res = ParseLevel3();

    op_id = GetOperatorId(_token);
    while (op_id == LOGICAL_AND || op_id == AND || op_id == OR || op_id == BITSHIFTLEFT || op_id == BITSHIFTRIGHT)
    {
        GetToken();
        res = EvalOperator(op_id, res, ParseLevel3());
		op_id = GetOperatorId(_token);
    }

    return res;
}

/*
 * conditional operators
 */
stringc ParserExpression::ParseLevel3()
{
    int op_id;
    stringc res;
    res = ParseLevel4();

    op_id = GetOperatorId(_token);
    while (op_id == EQUAL || op_id == UNEQUAL || op_id == SMALLER || op_id == LARGER || op_id == SMALLEREQ || op_id == LARGEREQ)
    {
        GetToken();
        res = EvalOperator(op_id, res, ParseLevel4());
		op_id = GetOperatorId(_token);
    }

    return res;
}

/*
 * add or subtract
 */
stringc ParserExpression::ParseLevel4()
{
    int op_id;
    stringc res;
    res = ParseLevel5();

    op_id = GetOperatorId(_token);
    while (op_id == PLUS || op_id == MINUS)
    {
        GetToken();
        res = EvalOperator(op_id, res, ParseLevel5());
		op_id = GetOperatorId(_token);
    }

    return res;
}


// Умножение, модули и XOR
stringc ParserExpression::ParseLevel5()
{
    int op_id;
    stringc res;
    res = ParseLevel6();

    op_id = GetOperatorId(_token);
    while (op_id == MULTIPLY || op_id == DIVIDE || op_id == MODULUS || op_id == XOR)
    {
        GetToken();
        res = EvalOperator(op_id, res, ParseLevel6());
		op_id = GetOperatorId(_token);
    }

    return res;
}


/*
 * power
 */
stringc ParserExpression::ParseLevel6()
{
    int op_id;
    stringc res;
    res = ParseLevel7();

    op_id = GetOperatorId(_token);
    while (op_id == POW)
    {
        GetToken();
        res = EvalOperator(op_id, res, ParseLevel7());
		op_id = GetOperatorId(_token);
    }

    return res;
}


/*
 * Unary minus
 */
stringc ParserExpression::ParseLevel7()
{
    stringc res;

    int op_id = GetOperatorId(_token);
    if (op_id == MINUS)
    {
        GetToken();
        res = ParseLevel8();
		f32 resF32 = core::fast_atof(res.c_str());
		resF32 = -resF32;
        res = stringc(resF32);
    }
    else
    {
        res = ParseLevel8();
    }

    return res;
}


// Функции
stringc ParserExpression::ParseLevel8()
{
    if (_tokenType == FUNCTION)
    {
		stringc tokenC = stringc(_token);
        return ParseFunction(tokenC);
    }
    else
    {
		// check if it is a parenthesized expression
		if (_tokenType == DELIMETER)
		{
			if (_token[0] == '(' && _token[1] == '\0')
			{
				GetToken();
				stringc res = ParseLevel2();
				if (_tokenType != DELIMETER || _token[0] != ')' || _token[1] || '\0')
				{
					_DEBUG_BREAK_IF(true)
				}
				GetToken();
				return res;
			}
		}

		// Возвращаем число
		return ParseNumber();
    }
}

// Обработать функцию
stringc ParserExpression::ParseFunction(stringc& functionName)
{
	GetToken();

	// check if it is a parenthesized expression
	if (_tokenType == DELIMETER)
	{
		if (_token[0] == '(' && _token[1] == '\0')
		{
			GetToken();

			stringc val1 = ParseLevel2();
			stringc val2;
			int numParams = 1;
			if(_tokenType == DELIMETER && _token[0] == ',')
			{
				GetToken();
				val2 = ParseLevel2();
				numParams++;
			}

			_DEBUG_BREAK_IF(_tokenType != DELIMETER || _token[0] != ')' || _token[1] || '\0')
			
			//bool execute = false;
			stringc res;
			/*for (u32 i = 0; i < _funcExtensions.size() ; i++)
			{
				if(numParams == 1)
					execute = _funcExtensions[i]->EvalFunction(functionName, val1, res);
				else if(numParams == 2)
					execute = _funcExtensions[i]->EvalFunction(functionName, val1, val2, res);
				if (execute) break;
			}				*/
			//_DEBUG_BREAK_IF(!execute)
			
			GetToken();
			return res;
		}
	}

	// unknown function
	_DEBUG_BREAK_IF(true)
	return stringc();
}

stringc ParserExpression::ParseNumber()
{
	stringc res;

	if(_tokenType == NUMBER)
	{
		res = stringc(_token);
		GetToken();
	}
	else if(_tokenType == VARIABLE)
	{
		stringc tokenC = stringc(_token);
		Parameter* variable = GetVariable(tokenC);
		_DEBUG_BREAK_IF(variable == NULL)
		res = variable->Value;
		GetToken();
	}
	else if(_tokenType == STRING)
	{		
		res = stringc(_token);
		GetToken();
	}
	else
	{
		_DEBUG_BREAK_IF(true)
	}
    return res;
}


/*
 * returns the id of the given operator
 * treturns -1 if the operator is not recognized
 */
int ParserExpression::GetOperatorId(const char op_name[])
{
    // level 2
    if (!strcmp(op_name, "&")) return AND;
	if (!strcmp(op_name, "&&")) return LOGICAL_AND;
    if (!strcmp(op_name, "|")) return OR;
    if (!strcmp(op_name, "<<")) return BITSHIFTLEFT;
    if (!strcmp(op_name, ">>")) return BITSHIFTRIGHT;

    // level 3
    if (!strcmp(op_name, "==")) return EQUAL;
    if (!strcmp(op_name, "!=")) return UNEQUAL;
    if (!strcmp(op_name, "<")) return SMALLER;
    if (!strcmp(op_name, ">")) return LARGER;
    if (!strcmp(op_name, "<=")) return SMALLEREQ;
    if (!strcmp(op_name, ">=")) return LARGEREQ;

    // level 4
    if (!strcmp(op_name, "+")) return PLUS;
    if (!strcmp(op_name, "-")) return MINUS;

    // level 5
    if (!strcmp(op_name, "*")) return MULTIPLY;
    if (!strcmp(op_name, "/")) return DIVIDE;
    if (!strcmp(op_name, "%")) return MODULUS;
    if (!strcmp(op_name, "||")) return XOR;

    // level 6
    if (!strcmp(op_name, "^")) return POW;
	
    return -1;
}

// Выполнить оператор
stringc ParserExpression::EvalOperator(const int op_id, const stringc &val1Str, const stringc &val2Str)
{
	float val1f32 = core::fast_atof(val1Str.c_str());
	float val2f32 = core::fast_atof(val2Str.c_str());
	float res = 0;
    switch (op_id)
    {        
        case AND:   
			res = (float)(static_cast<int>(val1f32) & static_cast<int>(val2f32));
			break;
		case LOGICAL_AND:   
			res = (float)(static_cast<int>(val1f32) && static_cast<int>(val2f32));
			break;
        case OR:       
			res = (float)(static_cast<int>(val1f32) | static_cast<int>(val2f32));
			break;
        case BITSHIFTLEFT: 
			res = (float)(static_cast<int>(val1f32) << static_cast<int>(val2f32));
			break;
        case BITSHIFTRIGHT:
			res = (float)(static_cast<int>(val1f32) >> static_cast<int>(val2f32));
			break;
        
        case EQUAL:     
			res = val1Str == val2Str;
			break;
        case UNEQUAL:   
			res = val1Str != val2Str;
			break;
        case SMALLER:   
			res = val1f32 < val2f32;
			break;
        case LARGER:    
			res = val1f32 > val2f32;
			break;
        case SMALLEREQ: 
			res = val1f32 <= val2f32;
			break;
        case LARGEREQ: 
			res = val1f32 >= val2f32;
			break;
        
        case PLUS:      
			res = val1f32 + val2f32;
			break;
        case MINUS:     
			res = val1f32 - val2f32;
			break;
       
        case MULTIPLY:  
			res = val1f32 * val2f32;
			break;
        case DIVIDE:    
			res = val1f32 / val2f32;
			break;
        case MODULUS:   
			res = (float)(static_cast<int>(val1f32) % static_cast<int>(val2f32)); 
			break;
        case XOR:       
			res = (float)(static_cast<int>(val1f32) ^ static_cast<int>(val2f32));
			break;
       
        case POW: 
			res = pow(val1f32, val2f32);
			break;
		default:
			_DEBUG_BREAK_IF(true)
			break;
    }
    if(res == 0) return stringc("0");// Чтобы вместо 0.0000 вернуть 0
    return stringc(res);
}


