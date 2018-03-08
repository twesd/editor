#pragma once
#include "../../Core/Xml/XMLFileDocument.h"

#include "../UnitGenInstance/UnitGenInstanceBillboard.h"
#include "../UnitGenInstance/UnitGenInstanceStandard.h"
#include "../UnitGenInstance/UnitGenInstanceEnv.h"
#include "../UnitGenInstance/UnitGenInstanceCamera.h"
#include "../UnitGenInstance/UnitGenInstanceEmpty.h"
#include "../UnitGenInstance/UnitCreationBase.h"

class LoaderUnitGenInstance
{
public:
	// Загрузка описания юнита <TreeView>
	static core::array<UnitGenInstanceBase*> LoadUnitGensFromXmlTag(
		SharedParams_t params, rapidxml::xml_node<>* sourceNode, stringc& root);
private:
	// Загрузка из <Tag xsi:type="UnitInstanceStandard">
	static UnitGenInstanceStandard* LoadInstanceStandard(SharedParams_t params, rapidxml::xml_node<>* sourceNode, stringc& root);
	// Загрузка из <Tag xsi:type="UnitInstanceEnv">
	static UnitGenInstanceEnv* LoadInstanceEnv(SharedParams_t params, rapidxml::xml_node<>* sourceNode, stringc& root);
	// Загрузка из <Tag xsi:type="UnitInstanceArea">
	static UnitGenInstanceBillboard* LoadInstanceBillboard( SharedParams_t params, rapidxml::xml_node<>* sourceNode, stringc root );
	// Загрузка из <Tag xsi:type="UnitInstanceCamera">
	static UnitGenInstanceCamera* LoadInstanceCamera( SharedParams_t params, rapidxml::xml_node<>* sourceNode, stringc root );

	// Загрузка из <Creations>
	static core::array<UnitCreationBase*> LoadCreations(SharedParams_t params, rapidxml::xml_node<>* sourceNode, stringc& root, UnitGenInstanceBase* genInstance);
	// Загрузка из <UnitCreationBase xsi:type="UnitCreationTimer">
	static UnitCreationBase* LoadCreationTimer(SharedParams_t params, rapidxml::xml_node<>* sourceNode);
	static UnitCreationBase* LoadCreationDistance( SharedParams_t params, rapidxml::xml_node<>* sourceNode );
	static UnitCreationBase* LoadCreationBBox( SharedParams_t params, rapidxml::xml_node<>* sourceNode );
	static UnitGenInstanceEmpty* LoadInstanceEmpty( SharedParams_t params, rapidxml::xml_node<>* sourceNode, stringc root );
	static UnitCreationBase* LoadCreationGlobalParameters( SharedParams_t params, rapidxml::xml_node<>* chNode );
};
