#pragma once
#include "../UnitPart/DataGetters/DataGetterBase.h"
#include "../UnitPart/UnitBehavior.h"
#include "Core/CompareType.h"
#include "Core/Xml/XMLFileDocument.h"

// Загрузка выборки
class LoaderDataGetters
{
public:
	// Загрузка получателся данных <DataGetter xsi:type="....">
	static DataGetterBase* LoadDataGetterBase(SharedParams_t params, rapidxml::xml_node<>* sourceNode, stringc& root, UnitBehavior* behavior, UnitAction* action);

	// Загрузка <DataGetter xsi:type="DataGetterTapControl">
	static DataGetterBase* LoadDataGetterTapControl( SharedParams_t params, rapidxml::xml_node<>* sourceNode, stringc& root, UnitBehavior* behavior, UnitAction* action );

	// Загрузка <DataGetter xsi:type="DataGetterTapControl">
	static DataGetterBase* LoadDataGetterId( SharedParams_t params, rapidxml::xml_node<>* sourceNode, stringc& root, UnitBehavior* behavior, UnitAction* action );

	static DataGetterBase* LoadDataGetterDistance( SharedParams_t params, rapidxml::xml_node<>* sourceNode, stringc& root, UnitBehavior* behavior, UnitAction* action );
};
