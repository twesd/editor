#include "ParserAS.h"
#include "scriptstdstring.h"
#include "scriptarray.h"
#include "scriptvector3df.h"
#include "ParserUserData.h"

void ParserAS_MessageCallback(const asSMessageInfo *msg, void *param)
{
	const char *type = "ERR ";
	if( msg->type == asMSGTYPE_WARNING ) 
		type = "WARN";
	else if( msg->type == asMSGTYPE_INFORMATION ) 
		type = "INFO";

	printf("%s (%d, %d) : %s : %s/n", msg->section, msg->row, msg->col, type, msg->message);

	_DEBUG_BREAK_IF(msg->type == asMSGTYPE_ERROR);
}

ParserAS::ParserAS() :
	_expression()
{
	_funcId = 0;
	_funcIdStr = 0;
	_funcIdBool = 0;
	_funcIdCustom = 0;

	_ctx = NULL;
	_engine = NULL;
	_preparedCustom = false;

	// Create the script engine
	_engine = asCreateScriptEngine(ANGELSCRIPT_VERSION);
	_DEBUG_BREAK_IF(_engine == NULL)

	// The script compiler will write any compiler messages to the callback.
	_engine->SetMessageCallback(asFUNCTION(ParserAS_MessageCallback), 0, asCALL_CDECL);

	// Register the script string type
	// Look at the implementation for this function for more information  
	// on how to register a custom string type, and other object types.
	RegisterStdString(_engine);

	RegisterScriptArray(_engine, false);

	RegisterVector3df(_engine);
		
	ParserUserData* userData = new ParserUserData();
	userData->ControlManagerData = NULL;
	userData->ControlOwnerData = NULL;
	userData->UnitInstanceData = NULL;
	userData->BaseData = NULL;
	_engine->SetUserData(userData);
}

ParserAS::~ParserAS()
{
	ParserUserData* userData = (ParserUserData*)_engine->GetUserData();
	delete userData;

	if(_ctx != NULL)
		_ctx->Release();
	_engine->Release();
}


void ParserAS::SetArgString( asUINT index, std::string* val )
{
	_ctx->SetArgObject(index, (void*)val);
}

void ParserAS::SetArgFloat( asUINT index, float val )
{
	_ctx->SetArgFloat(index, val);
}

// Выполнить выражение
irr::core::stringc ParserAS::EvalAsString()
{
	_DEBUG_BREAK_IF(_funcIdStr < 0)
	int r = _ctx->Prepare(_funcIdStr);
	_DEBUG_BREAK_IF(r < 0)
	r = _ctx->Execute();
	_DEBUG_BREAK_IF(r != asEXECUTION_FINISHED)
	void* ret = _ctx->GetReturnObject();
	_DEBUG_BREAK_IF(ret == NULL)
	std::string* str = (std::string*)ret;
	ClearUserData();
	return stringc(str->c_str());
}

// Выполнить выражение и вернуть результат как float
bool ParserAS::EvalAsBool()
{
	_DEBUG_BREAK_IF(_funcIdBool < 0)
		
	int r = _ctx->Prepare(_funcIdBool);
	_DEBUG_BREAK_IF(r < 0)
	r = _ctx->Execute();
	_DEBUG_BREAK_IF(r != asEXECUTION_FINISHED)
	ClearUserData();
	return (_ctx->GetReturnWord() != 0);
}

void ParserAS::ExecuteCustom()
{
	int r;
	if(!_preparedCustom)
	{
		// После Prepare пропадают входные аргументы функции, поэтому проверяем ...
		r = _ctx->Prepare(_funcIdCustom);
		_DEBUG_BREAK_IF(r < 0)
	}
	r = _ctx->Execute();
	_DEBUG_BREAK_IF(r != asEXECUTION_FINISHED)
	_preparedCustom = false;

	ClearUserData();
}

void ParserAS::Execute()
{
	// Prepare the script context with the function we wish to execute. Prepare()
	// must be called on the context before each new script function that will be
	// executed. Note, that if you intend to execute the same function several 
	// times, it might be a good idea to store the function id returned by 
	// GetFunctionIDByDecl(), so that this relatively slow call can be skipped.
	int r = _ctx->Prepare(_funcId);
	_DEBUG_BREAK_IF(r < 0)
	r = _ctx->Execute();
	_DEBUG_BREAK_IF(r != asEXECUTION_FINISHED)

	ClearUserData(); 	
}

void ParserAS::ClearUserData()
{
	ParserUserData* userData = GetUserData();
	userData->ControlManagerData = NULL;
	userData->ControlOwnerData = NULL;
	userData->UnitInstanceData = NULL;
	userData->BaseData = NULL;
}

// Установить выражение
void ParserAS::SetCode( stringc expression )
{
	_DEBUG_BREAK_IF(expression.trim() == "")
	_expression = expression;

	if(_ctx != NULL)
	{
		_ctx->Release();
		_ctx = NULL;
	}

	// Add the script sections that will be compiled into executable code.
	// If we want to combine more than one file into the same script, then 
	// we can call AddScriptSection() several times for the same module and
	// the script engine will treat them all as if they were one. The script
	// section name, will allow us to localize any errors in the script code.

	asIScriptModule *mod = _engine->GetModule(0, asGM_CREATE_IF_NOT_EXISTS);	
	int r = mod->AddScriptSection("script", _expression.c_str(), _expression.size());
	_DEBUG_BREAK_IF(r < 0)
	r = mod->Build();
	_DEBUG_BREAK_IF(r < 0)

	// Create a context that will execute the script.
	_ctx = _engine->CreateContext();
	_DEBUG_BREAK_IF(_ctx == NULL)

	// Find the function id for the function we want to execute.
	_funcId = _engine->GetModule(0)->GetFunctionIdByDecl("void main()");
	_funcIdStr = _engine->GetModule(0)->GetFunctionIdByDecl("string main()");
	_funcIdBool = _engine->GetModule(0)->GetFunctionIdByDecl("bool main()");
}


// Установить выполняемую функции
void ParserAS::SetFuncDecl( stringc decl )
{
	_funcIdCustom = _engine->GetModule(0)->GetFunctionIdByDecl(decl.c_str());
	_DEBUG_BREAK_IF(_funcIdCustom < 0)
	// После Prepare можно устанавливать входные аргументы функции
	int r = _ctx->Prepare(_funcIdCustom);
	_DEBUG_BREAK_IF(r < 0)
	_preparedCustom = true;
}

// Получить результат как string
stringc ParserAS::GetReturnAsString()
{
	void* ret = _ctx->GetReturnObject();
	_DEBUG_BREAK_IF(ret == NULL)
	std::string* str = (std::string*)ret;
	return stringc(str->c_str());
}

// Получить результат как float
float ParserAS::GetReturnAsFloat()
{
	return _ctx->GetReturnFloat();
}

ParserUserData* ParserAS::GetUserData()
{
	return (ParserUserData*)_engine->GetUserData();
}

asIScriptEngine* ParserAS::GetEngine()
{
	return _engine;
}
