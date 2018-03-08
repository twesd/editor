#pragma once
#include "Core/Xml/XMLFileDocument.h"
#include "Core/CompareType.h"
#include "../UnitPart/UnitBehavior.h"
#include "../UnitPart/UnitExecute/ExecuteBase.h"
#include "../UnitPart/UnitSelectSceneNodes/UnitSelectSceneNodeBase.h"


// Загрузка выборки
class LoaderUnitSelectSceneNode
{
public:
	static UnitSelectSceneNodeBase* LoadUnitSelectSceneNodeBase(SharedParams_t params, rapidxml::xml_node<>* sourceNode, stringc& root, UnitBehavior* behavior, UnitAction* action );
private:
	// Загрузить <UnitSelectSceneNodeBase xsi:type="UnitSelectSceneNodeDistance">
	static UnitSelectSceneNodeBase* LoadUnitSelectSceneNodeDistance( SharedParams_t params, rapidxml::xml_node<>* sourceNode, stringc& root, UnitBehavior* behavior, UnitAction* action );

	// Загрузить <UnitSelectSceneNodeBase xsi:type="UnitSelectSceneNodeTapControl">
	static UnitSelectSceneNodeBase* LoadUnitSelectSceneNodeTapControl( SharedParams_t params, rapidxml::xml_node<>* sourceNode, stringc& root, UnitBehavior* behavior, UnitAction* action );

	// Загрузить <UnitSelectSceneNodeBase xsi:type="UnitSelectSceneNodeData">
	static UnitSelectSceneNodeBase* LoadUnitSelectSceneNodeData( SharedParams_t params, rapidxml::xml_node<>* sourceNode, stringc& root, UnitBehavior* behavior, UnitAction* action );

	// Загрузить <UnitSelectSceneNodeBase xsi:type="UnitSelectSceneNodeId">
	static UnitSelectSceneNodeBase* LoadUnitSelectSceneNodeId( SharedParams_t params, rapidxml::xml_node<>* sourceNode, stringc& root, UnitBehavior* behavior, UnitAction* action );
};
