#pragma once
#include "../../../Core/Base.h"
//#include "../../UnitInstance/UnitInstanceStandard.h"


class UnitInstanceBase;

class UnitSelectSceneNodeBase : public Base
{
public:
	UnitSelectSceneNodeBase(SharedParams_t params);
	virtual ~UnitSelectSceneNodeBase(void);	
	
	// Выполнить выборку
	virtual core::array<scene::ISceneNode*> Select( core::array<Event_t*>& events) = 0;

	// Установить экземпляр юнита, которому принадлежит действие
	virtual void SetUnitInstance(UnitInstanceBase* unitInstance);

	// Фильтр модели
	int FilterNodeId;

	// Выбирать дочерниче элементы
	bool SelectChilds;
protected:
	// Экземпляр юнита
	UnitInstanceBase* UnitInstance;
};
