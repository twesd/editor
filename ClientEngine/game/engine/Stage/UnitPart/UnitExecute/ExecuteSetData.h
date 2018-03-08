#pragma once
#include "ExecuteBase.h"
#include "../DataGetters/DataGetterBase.h"

class ExecuteSetData : public ExecuteBase
{
public:
	ExecuteSetData(SharedParams_t params);
	virtual ~ExecuteSetData(void);

	// Добавление параметра
	void AddDataItem(stringc name, DataGetterBase* dataGetter);

	// Выполнить действие
	virtual void Run(scene::ISceneNode* node, core::array<Event_t*>& events);

private:
	typedef struct
	{
		// Наименование параметра данных
		stringc Name;
		// Объект для получения данных
		DataGetterBase* DataGetter;
	}DataItem;

	core::array<DataItem> _dataItems;
};
