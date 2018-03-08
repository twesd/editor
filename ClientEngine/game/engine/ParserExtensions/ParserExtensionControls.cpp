#include "ParserExtensionControls.h"
#include "../Controls/ControlManager.h"
#include "../Controls/ControlButton.h"
#include "../Controls/ControlImage.h"
#include "../Controls/ControlText.h"
#include "../Controls/ControlCircle.h"

ParserUserData* ParserExtensionControls_GetUserData()
{
	asIScriptContext* ctx = asGetActiveContext();
	asIScriptEngine* engine = ctx->GetEngine();
	ParserUserData* parserUserData = (ParserUserData*)engine->GetUserData();
	_DEBUG_BREAK_IF(parserUserData == NULL)
	_DEBUG_BREAK_IF(parserUserData->ControlManagerData == NULL)
	_DEBUG_BREAK_IF(parserUserData->ControlOwnerData == NULL)
	return parserUserData;
}

// Получить параметр контрола по имени
std::string ParserExtensionControls_GetControlParameter(std::string& name)
{
	ParserUserData* extension = (ParserUserData*)ParserExtensionControls_GetUserData();
	IControl* ownerControl = (IControl*)extension->ControlOwnerData;
	stringc namec = stringc(name.c_str());

	for (u32 i = 0; i < ownerControl->Parameters.size() ; i++)
	{
		if (ownerControl->Parameters[i].Name == namec)
		{			
			return std::string(ownerControl->Parameters[i].Value.c_str());
		}
	}
	return std::string();
}

// Установить параметр для контрола void SetControlParameter(attrName,attrVal)
void ParserExtensionControls_SetControlParameter( std::string& name, std::string& val )
{
	ParserUserData* extension = (ParserUserData*)ParserExtensionControls_GetUserData();
	IControl* ownerControl = (IControl*)extension->ControlOwnerData;
	stringc attrName = stringc(name.c_str());
	stringc attrVal = stringc(val.c_str());

	for (u32 i = 0; i < ownerControl->Parameters.size() ; i++)
	{
		if (ownerControl->Parameters[i].Name == attrName)
		{
			ownerControl->Parameters[i].Value = attrVal;			
			return;
		}
	}
	Parameter param;
	param.Name = attrName;
	param.Value = attrVal;
	ownerControl->Parameters.push_back(param);
}

// Установить видимость контрола SetControlVisible(name,visible)
void ParserExtensionControls_SetControlVisible( std::string& name, int visible )
{
	ParserUserData* extension = (ParserUserData*)ParserExtensionControls_GetUserData();
	ControlManager* controlManager = (ControlManager*)extension->ControlManagerData;
	stringc controlName(name.c_str());
	IControl* control = controlManager->GetControlByName(controlName);
	_DEBUG_BREAK_IF(control == NULL)
	control->IsVisible = (visible != 0);
}

// Получить позицию контрола GetControlPosition(name)
vector3df ParserExtensionControls_GetControlPosition( std::string& name )
{
	ParserUserData* extension = (ParserUserData*)ParserExtensionControls_GetUserData();
	ControlManager* controlManager = (ControlManager*)extension->ControlManagerData;
	stringc controlName(name.c_str());
	IControl* control = controlManager->GetControlByName(controlName);
	_DEBUG_BREAK_IF(control == NULL)
	position2di pos = control->GetPosition();
	return vector3df((f32)pos.X, (f32)pos.Y, 0);
}

// Установить позицию контрола void SetControlPosition(name,position)
void ParserExtensionControls_SetControlPosition( std::string& name, vector3df& pos )
{
	ParserUserData* extension = (ParserUserData*)ParserExtensionControls_GetUserData();
	ControlManager* controlManager = (ControlManager*)extension->ControlManagerData;
	stringc controlName(name.c_str());
	IControl* control = controlManager->GetControlByName(controlName);
		
	position2di pos2di;
	pos2di.X = (int)pos.X;
	pos2di.Y = (int)pos.Y;
	control->SetPosition(pos2di);
}

// Получить видимость контрола
int ParserExtensionControls_GetControlVisible( std::string& name )
{
	ParserUserData* extension = (ParserUserData*)ParserExtensionControls_GetUserData();
	ControlManager* controlManager = (ControlManager*)extension->ControlManagerData;
	stringc controlName(name.c_str());
	IControl* control = controlManager->GetControlByName(controlName);
	_DEBUG_BREAK_IF(control == NULL)
	return (int)control->IsVisible;
}

// Событие нажатия
int ParserExtensionControls_ControlClickedUp( std::string& name )
{
	ParserUserData* extension = (ParserUserData*)ParserExtensionControls_GetUserData();
	ControlManager* controlManager = (ControlManager*)extension->ControlManagerData;
	stringc controlName(name.c_str());
	IControl* control = controlManager->GetControlByName(controlName);
	if(control->GetControlType() == CONTROL_TYPE_BUTTON)
	{
		ControlButton* cntrl = (ControlButton*)control;
		if(cntrl->GetButtonState() == BUTTON_STATE_UP)
		{	
			return 1;
		}
	}
	return 0;
}

// Активировать пакет SetControlPackage(name)
void ParserExtensionControls_SetControlPackage(std::string& name)
{
	ParserUserData* extension = (ParserUserData*)ParserExtensionControls_GetUserData();
	ControlManager* controlManager = (ControlManager*)extension->ControlManagerData;
	stringc controlName(name.c_str());
	controlManager->SetControlPackage(controlName);
}

// Удалить контрол по имени RemoveControl(name)
void ParserExtensionControls_RemoveControl(std::string& name)
{
	ParserUserData* extension = (ParserUserData*)ParserExtensionControls_GetUserData();
	ControlManager* controlManager = (ControlManager*)extension->ControlManagerData;

	stringc controlName(name.c_str());
	controlManager->RemoveControl(controlName);
}

// Установить область обрезки SetControlClip(name,x1,y1,x2,y2)
void ParserExtensionControls_SetControlClip(std::string& name, int x1, int y1, int x2, int y2)
{
	ParserUserData* extension = (ParserUserData*)ParserExtensionControls_GetUserData();
	ControlManager* controlManager = (ControlManager*)extension->ControlManagerData;

	stringc controlName(name.c_str());
	IControl* control = controlManager->GetControlByName(controlName);
	_DEBUG_BREAK_IF(control == NULL)
	if(control->GetControlType() == CONTROL_TYPE_BUTTON)
	{
		ControlButton* cntrl = (ControlButton*)control;
		recti clip(x1, y1, x2, y2);
		cntrl->SetClip(clip);
	}
	else if(control->GetControlType() == CONTROL_TYPE_IMAGE)
	{
		ControlImage* cntrl = (ControlImage*)control;
		recti clip(x1, y1, x2, y2);
		cntrl->SetClip(clip);
	}
}

// Установить область обрезки SetControlText(name, text)
void ParserExtensionControls_SetControlText(std::string& name, std::string& text)
{
	ParserUserData* extension = (ParserUserData*)ParserExtensionControls_GetUserData();
	ControlManager* controlManager = (ControlManager*)extension->ControlManagerData;

	stringc textc = stringc(text.c_str());
	stringc controlName(name.c_str());
	IControl* control = controlManager->GetControlByName(controlName);
	_DEBUG_BREAK_IF(control == NULL)
	if(control->GetControlType() == CONTROL_TYPE_TEXT)
	{
		ControlText* cntrlText = (ControlText*)control;
		cntrlText->Text = textc;
	}
}

float ParserExtensionControls_GetControlAngle(std::string& name)
{
	ParserUserData* extension = (ParserUserData*)ParserExtensionControls_GetUserData();
	ControlManager* controlManager = (ControlManager*)extension->ControlManagerData;

	stringc controlName(name.c_str());
	IControl* control = controlManager->GetControlByName(controlName);
	_DEBUG_BREAK_IF(control == NULL)
	if(control->GetControlType() == CONTROL_TYPE_CIRCLE)
	{
		ControlCircle* cntrl = (ControlCircle*)control;
		return cntrl->GetCenterAngle();
	}
	else{
		_DEBUG_BREAK_IF(true)
	}
	return -1;
}

int ParserExtensionControls_GetControlState(std::string& name)
{
	ParserUserData* extension = (ParserUserData*)ParserExtensionControls_GetUserData();
	ControlManager* controlManager = (ControlManager*)extension->ControlManagerData;

	stringc controlName(name.c_str());
	IControl* control = controlManager->GetControlByName(controlName);
	_DEBUG_BREAK_IF(control == NULL)
		if(control->GetControlType() == CONTROL_TYPE_CIRCLE)
		{
			ControlCircle* cntrl = (ControlCircle*)control;
			return cntrl->GetState();
		}
		else{
			_DEBUG_BREAK_IF(true)
		}
	return -1;
}


// Регистрация функций
void ParserExtensionControls_RegisterFunctions(asIScriptEngine *engine )
{
	int r;

	r = engine->RegisterGlobalFunction("int ControlClickedUp(string &in)", asFUNCTION(ParserExtensionControls_ControlClickedUp), asCALL_CDECL); _DEBUG_BREAK_IF( r < 0 );
	r = engine->RegisterGlobalFunction("int GetControlVisible(string &in)", asFUNCTION(ParserExtensionControls_GetControlVisible), asCALL_CDECL); _DEBUG_BREAK_IF( r < 0 );
	r = engine->RegisterGlobalFunction("vector3df GetControlPosition(string &in)", asFUNCTION(ParserExtensionControls_GetControlPosition), asCALL_CDECL); _DEBUG_BREAK_IF( r < 0 );
	r = engine->RegisterGlobalFunction("string GetControlParameter(string &in)", asFUNCTION(ParserExtensionControls_GetControlParameter), asCALL_CDECL); _DEBUG_BREAK_IF( r < 0 );
	r = engine->RegisterGlobalFunction("float GetControlAngle(string &in)", asFUNCTION(ParserExtensionControls_GetControlAngle), asCALL_CDECL); _DEBUG_BREAK_IF( r < 0 );
	r = engine->RegisterGlobalFunction("int GetControlState(string &in)", asFUNCTION(ParserExtensionControls_GetControlState), asCALL_CDECL); _DEBUG_BREAK_IF( r < 0 );

	r = engine->RegisterGlobalFunction("void SetControlPosition(string &in, vector3df &in)", asFUNCTION(ParserExtensionControls_SetControlPosition), asCALL_CDECL); _DEBUG_BREAK_IF( r < 0 );
	r = engine->RegisterGlobalFunction("void SetControlVisible(string &in, int)", asFUNCTION(ParserExtensionControls_SetControlVisible), asCALL_CDECL); _DEBUG_BREAK_IF( r < 0 );
	r = engine->RegisterGlobalFunction("void SetControlParameter(string &in, string& in)", asFUNCTION(ParserExtensionControls_SetControlParameter), asCALL_CDECL); _DEBUG_BREAK_IF( r < 0 );
	r = engine->RegisterGlobalFunction("void SetControlPackage(string &in)", asFUNCTION(ParserExtensionControls_SetControlPackage), asCALL_CDECL); _DEBUG_BREAK_IF( r < 0 );
	r = engine->RegisterGlobalFunction("void SetControlClip(string &in, int, int, int, int)", asFUNCTION(ParserExtensionControls_SetControlClip), asCALL_CDECL); _DEBUG_BREAK_IF( r < 0 );
	r = engine->RegisterGlobalFunction("void SetControlText(string &in, string&in)", asFUNCTION(ParserExtensionControls_SetControlText), asCALL_CDECL); _DEBUG_BREAK_IF( r < 0 );

	r = engine->RegisterGlobalFunction("void RemoveControl(string &in)", asFUNCTION(ParserExtensionControls_RemoveControl), asCALL_CDECL); _DEBUG_BREAK_IF( r < 0 );

}

