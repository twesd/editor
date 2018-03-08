#pragma once
#include "../Base.h"
#include "RapidXML/rapidxml.h"

class XMLHelper 
{
public:
	// Поиск по xpath. TODO : Медленный алгоритм.
	static core::array<rapidxml::xml_node<>*> Select(rapidxml::xml_node<>* node, stringc xpath);
	
	// Поиск всех узлов с заданным именем.
	static core::array<rapidxml::xml_node<>*> SelectAllByNodeName(rapidxml::xml_node<>* node, const char* nodeName);
	
	// Получить текст элемента как Bool
	static bool GetTextAsBool(rapidxml::xml_node<>* node);
	
	// Получить текст элемента как Float
	static f32 GetTextAsFloat(rapidxml::xml_node<>* node);
	
	// Получить текст элемента как UInt
	static u32 GetTextAsUInt(rapidxml::xml_node<>* node);
	
	// Получить текст элемента как Int
	static s32 GetTextAsInt(rapidxml::xml_node<>* node);

	// Получение позиции из тагов (x,y)
	static position2di GetPosition2di(rapidxml::xml_node<>* node);

	// Получение позиции из тагов (x,y,z)
	static vector3df GetVector3df(rapidxml::xml_node<>* node);

	// Прочитать объект типа Boundbox
	static aabbox3df GetBoundbox( rapidxml::xml_node<>* node );

	static video::SColor GetTextAsSColor( rapidxml::xml_node<>* node );

	static dimension2df GetTextAsDimension2d( rapidxml::xml_node<>* node );
};
