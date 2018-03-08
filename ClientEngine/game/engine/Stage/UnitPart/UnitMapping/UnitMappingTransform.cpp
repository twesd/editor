#include "UnitMappingTransform.h"
#include "../../UnitInstance/UnitInstanceStandard.h"
#include <stdio.h>
#include <ctype.h>


UnitMappingTransform::UnitMappingTransform(SharedParams_t params) : UnitMappingBase(params),
	ScaleX(), ScaleY(), ScaleZ(),
	PositionX(), PositionY(), PositionZ(),
	RotationX(), RotationY(), RotationZ(),
	_parserExpr(), _externalParameters(NULL)
{
}

UnitMappingTransform::~UnitMappingTransform()
{
}

// Р’С‹РїРѕР»РЅРёС‚СЊ РѕР±СЂР°Р±РѕС‚РєСѓ
void UnitMappingTransform::Update( )
{
	_DEBUG_BREAK_IF(UnitInstance == NULL)

	UnitInstanceStandard* instStandard = dynamic_cast<UnitInstanceStandard*>(UnitInstance);
	if (instStandard != NULL)
	{	
		// РџРѕР»СѓС‡Р°РµРј СЃСѓС‰РµСЃС‚РІСѓСЋС‰РёРµ РїР°СЂР°РјРµС‚СЂС‹ СЋРЅРёС‚Р°
		UnitParameters* parameters;
		if (_externalParameters != NULL)
		{
			parameters = _externalParameters;
		}else
		{
			parameters = instStandard->GetBehavior()->GetParameters();
		}
		
		vector3df scale = instStandard->SceneNode->getScale();
		if(instStandard->SceneNode->getType() == ESNT_BILLBOARD)
		{
			IBillboardSceneNode* billboard = (IBillboardSceneNode*)(instStandard->SceneNode);
			scale.X = billboard->getSize().Width;
			scale.Y = billboard->getSize().Height;
			scale.Z = 0;
		}

		if (ScaleX != "")
		{
			_parserExpr.SetVariable("ThisParam", stringc(scale.X));
			SetVariablesFromExpr(parameters, ScaleX);			
			float val = _parserExpr.EvalAsFloat();
			scale.X = (val > 0) ? val : 0.000001f;
		}
		if (ScaleY != "")
		{
			_parserExpr.SetVariable("ThisParam", stringc(scale.Y));
			SetVariablesFromExpr(parameters, ScaleY);
			f32 val = _parserExpr.EvalAsFloat();
			scale.Y = (val > 0) ? val : 0.000001f;
		}
		if (ScaleZ != "")
		{
			_parserExpr.SetVariable("ThisParam", stringc(scale.Z));
			SetVariablesFromExpr(parameters, ScaleZ);
			f32 val = _parserExpr.EvalAsFloat();
			scale.Z = (val > 0) ? val : 0.000001f;
		}
		if(instStandard->SceneNode->getType() == ESNT_BILLBOARD)
		{
			IBillboardSceneNode* billboard = (IBillboardSceneNode*)(instStandard->SceneNode);
			billboard->setSize(dimension2df(scale.X, scale.Y));
		}
		else
		{
			instStandard->SceneNode->setScale(scale);
		}		
		
		vector3df position = instStandard->SceneNode->getPosition();
		if (PositionX != "")
		{
			_parserExpr.SetVariable("ThisParam", stringc(position.X));
			SetVariablesFromExpr(parameters, ScaleX);			
			float val = _parserExpr.EvalAsFloat();
			position.X = (val > 0) ? val : 0.000001f;
		}
		if (PositionY != "")
		{
			_parserExpr.SetVariable("ThisParam", stringc(position.Y));
			SetVariablesFromExpr(parameters, ScaleY);
			f32 val = _parserExpr.EvalAsFloat();
			position.Y = (val > 0) ? val : 0.000001f;
		}
		if (PositionZ != "")
		{
			_parserExpr.SetVariable("ThisParam", stringc(position.Z));
			SetVariablesFromExpr(parameters, ScaleZ);
			f32 val = _parserExpr.EvalAsFloat();
			position.Z = (val > 0) ? val : 0.000001f;
		}
		instStandard->SceneNode->setPosition(position);

		vector3df rotation = instStandard->SceneNode->getRotation();
		if (RotationX != "")
		{
			_parserExpr.SetVariable("ThisParam", stringc(rotation.X));
			SetVariablesFromExpr(parameters, ScaleX);			
			float val = _parserExpr.EvalAsFloat();
			rotation.X = (val > 0) ? val : 0.000001f;
		}
		if (RotationY != "")
		{
			_parserExpr.SetVariable("ThisParam", stringc(rotation.Y));
			SetVariablesFromExpr(parameters, ScaleY);
			f32 val = _parserExpr.EvalAsFloat();
			rotation.Y = (val > 0) ? val : 0.000001f;
		}
		if (RotationZ != "")
		{
			_parserExpr.SetVariable("ThisParam", stringc(rotation.Z));
			SetVariablesFromExpr(parameters, ScaleZ);
			f32 val = _parserExpr.EvalAsFloat();
			rotation.Z = (val > 0) ? val : 0.000001f;
		}
		instStandard->SceneNode->setRotation(rotation);
	}
}

// Р—Р°РґР°С‚СЊ РЅРµРѕР±С…РѕРґРёРјС‹Рµ РїР°СЂР°РјРµС‚СЂС‹
void UnitMappingTransform::SetVariablesFromExpr(UnitParameters* parameters, stringc& expr )
{	
	// TODO : СЃРґРµР»Р°С‚СЊ Р±РѕР»РµРµ Р±С‹СЃС‚СЂС‹Р№ Р°Р»РіРѕСЂРёС‚Рј
	// Р�Р·РІР»РµРєР°РµРј Рё СѓСЃС‚Р°РЅР°РІР»РёРІР°РµРј РІСЃРµ РїРµСЂРµРјРµРЅРЅС‹Рµ РёР· РІС‹СЂР°Р¶РµРЅРёСЏ
	//
	c8* e = (c8*)expr.c_str();
	while(*e != '\0')
	{
		c8 c = *e;
		if(strchr("[", toupper(c)) != 0)
		{
			e++;
			c = *e;
			stringc varName;
			while((c != '\0') && (strchr("ABCDEFGHIJKLMNOPQRSTUVWXYZ_0123456789", toupper(c)) != 0))
			{
				varName += c;
				e++;
				c = *e;
			}
			
			_DEBUG_BREAK_IF(varName == "")

			Parameter* param = parameters->FindParameter(varName);
			if (param != NULL)
			{
				_parserExpr.SetVariable(varName, param->Value);
			}
			else
			{
				//_parser.SetVariable(varName, "0");
				_DEBUG_BREAK_IF(true)
			}
		}
		if(c == '\0') break;
		e++;
	}
	expr.replace('[',' ');
	expr.replace(']',' ');

	_parserExpr.SetExpression(expr);
}

// РЈСЃС‚Р°РЅРѕРІРёС‚СЊ РІРЅРµС€РЅРёРµ РїР°СЂР°РјРµС‚СЂС‹
void UnitMappingTransform::SetExternalParameters( UnitParameters* parameters )
{
	_externalParameters = parameters;
}
