#pragma once
#include "IControl.h"
#include "IControlComponent.h"
#include "ControlPackage.h"
#include "ControlManager.h"
#include "ControlComponentScroll.h"
#include "../ModuleManager.h"
#include "../Sounds/SoundManager.h"
#include "../Core/Xml/XMLFileDocument.h"
#include "../Core/Parsers/ScriptCache.h"

class LoaderControlManager : public Base
{
public:
	// Загрузка из файла .controls
	static ControlPackage LoadControlPackage(
		SharedParams_t sharedParams, 
		stringc path, 
		ControlManager* manager, 
		ModuleManager* moduleManager, 
		SoundManager* soundManager,
		ScriptCache* scriptCache,
		float controlsScale,
		position2di controlsOffset);

	// Загрузка из <ControlPackages> в .stage
	static core::array<ControlPackage> LoadControlPackagesFromXmlTag(
		SharedParams_t sharedParams, 
		rapidxml::xml_node<>* node,
		stringc root, 
		ControlManager* manager, 
		ModuleManager* moduleManager, 
		SoundManager* soundManager,
		ScriptCache* scriptCache,
		float controlsScale,
		position2di controlsOffset);

private:
	// Загрузка свойств базового контрола из XML
	static bool LoadBaseParam(rapidxml::xml_node<>* node, SharedParams_t sharedParams, stringc root, IControl* control);

	// Загрузка контрола из XML (<Tag xsi:type="ControlButton">)
	static IControl* LoadControlButton(
		rapidxml::xml_node<>* node, SharedParams_t sharedParams, stringc root, SoundManager* soundManager, 
		float controlsScale, position2di controlsOffset);

	// Загрузка контрола из XML (<Tag xsi:type="ControlCircle">)
	static IControl* LoadControlCircle(rapidxml::xml_node<>* node, SharedParams_t sharedParams, stringc root, 
		float controlsScale, position2di controlsOffset);

	// Загрузка контрола из XML (<Tag xsi:type="ControlImage">)
	static IControl* LoadControlImage( rapidxml::xml_node<>* node, SharedParams_t sharedParams, stringc root, 
		float controlsScale, position2di controlsOffset );

	// Загрузка контрола из XML (<Tag xsi:type="ControlTapScene">)
	static IControl* LoadTapScene(rapidxml::xml_node<>* node, SharedParams_t sharedParams, stringc root, 
		float controlsScale, position2di controlsOffset);

	// Загрузка контрола из XML (<Tag xsi:type="ControlBehavior">)
	static IControl* LoadBehavior(rapidxml::xml_node<>* node, SharedParams_t sharedParams, stringc root,
		ControlManager* manager, ModuleManager* moduleManager, ScriptCache* scriptCache );

	// Загрузка контрола из XML (<Tag xsi:type="ControlText">)
	static IControl* LoadControlText( rapidxml::xml_node<>* node, SharedParams_t sharedParams, stringc root, 
		ControlManager* manager, float controlsScale, position2di controlsOffset );

	// Загрузка контрола из XML (<Tag xsi:type="ControlPanel">)
	static IControl* LoadControlPanel( rapidxml::xml_node<>* node, SharedParams_t sharedParams, stringc root, 
		float controlsScale, position2di controlsOffset );

	// Загрузка контрола из XML (<Tag xsi:type="ControlComponentScroll">)
	static ControlComponentScroll* LoadControlComponentScroll( rapidxml::xml_node<>* node, SharedParams_t sharedParams,
		stringc root, float controlsScale, position2di controlsOffset );

};
