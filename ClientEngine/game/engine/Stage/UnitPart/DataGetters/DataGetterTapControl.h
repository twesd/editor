#pragma once
#include "DataGetterBase.h"

class UnitInstanceStandard;

class DataGetterTapControl : public DataGetterBase
{
public:
	DataGetterTapControl(SharedParams_t params);
	virtual ~DataGetterTapControl(void);	
	
	// Получить данные
	virtual void* GetDataItem(core::array<Event_t*>& events);

	// Наименование контрола
	stringc TapSceneName;

	// Установить тип данных
	void SetGetterType(stringc getterType);

private:
	// Тип данных
	enum GetterTypeEnum
	{
		// Модель
		SceneNode,
		// Позиция клика
		Position
	};
	
	GetterTypeEnum _getterType;

};
