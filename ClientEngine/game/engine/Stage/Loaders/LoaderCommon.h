#pragma once
#include "../UnitPart/UnitBehavior.h"
#include "../UnitPart/UnitExecute/ExecuteBase.h"
#include "../UnitPart/UnitSelectSceneNodes/UnitSelectSceneNodeBase.h"
#include "Core/CompareType.h"
#include "Core/OperatorType.h"

// Загрузка выборки
class LoaderCommon
{
public:
	// Преобразовывает stringc в CompareType
	static CompareTypeEnum GetCompareType( stringc compareTypeStr );

	// Преобразовывает stringc в OperatorType
	static OperatorTypeEnum GetOperatorType( stringc str );

	// Получить тип материи
	static video::E_MATERIAL_TYPE GetMaterialType( stringc matType );
};
