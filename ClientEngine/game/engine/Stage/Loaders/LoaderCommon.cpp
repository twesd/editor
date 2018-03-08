#include "LoaderCommon.h"
#include "../../Controls/ControlEvent.h"
#include "../../Core/Xml/XMLHelper.h"
#include "../../Core/Animators/TextureAnimator.h"

// Преобразовывает stringc в CompareType
CompareTypeEnum LoaderCommon::GetCompareType( stringc compareTypeStr )
{
	if (compareTypeStr == "More")
	{
		return COMPARE_TYPE_MORE;
	}
	else if (compareTypeStr == "Less")
	{
		return COMPARE_TYPE_LESS;
	}
	else
	{
		return COMPARE_TYPE_EQUAL;
	}
}

OperatorTypeEnum LoaderCommon::GetOperatorType( stringc str )
{
	if (str == "AND")
	{
		return OPERATOR_TYPE_AND;
	}
	else
	{
		return OPERATOR_TYPE_OR;
	}
}

// Получить тип материи
video::E_MATERIAL_TYPE LoaderCommon::GetMaterialType( stringc matType )
{
	if (matType == "Solid")
	{
		return video::EMT_SOLID;
	}
	else if (matType == "Transparent")
	{
		return video::EMT_TRANSPARENT_ALPHA_CHANNEL;
	}
	else if (matType == "VertexAlpha")
	{
		return video::EMT_TRANSPARENT_VERTEX_ALPHA;
	}
	else if (matType == "AddColor")
	{
		return video::EMT_TRANSPARENT_ADD_COLOR;
	}
	_DEBUG_BREAK_IF(true)
	return video::EMT_SOLID;
}


