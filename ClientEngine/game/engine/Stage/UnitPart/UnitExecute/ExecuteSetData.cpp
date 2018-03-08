#include "ExecuteSetData.h"
#include "../../UnitInstance/UnitInstanceStandard.h"

ExecuteSetData::ExecuteSetData(SharedParams_t params) : ExecuteBase(params),
	_dataItems()
{

}

ExecuteSetData::~ExecuteSetData()
{
	for (u32 i = 0; i < _dataItems.size() ; i++)
	{
		_dataItems[i].DataGetter->drop();
	}
}

// Выполнить действие
void ExecuteSetData::Run( scene::ISceneNode* node, core::array<Event_t*>& events )
{
	ExecuteBase::Run(node, events);

	for (u32 i = 0; i < _dataItems.size() ; i++)
	{
		void* data = _dataItems[i].DataGetter->GetDataItem(events);
		UnitInstance->SetData(_dataItems[i].Name, data);
	}	
}

// Добавление параметра
void ExecuteSetData::AddDataItem( stringc name, DataGetterBase* dataGetter )
{
	DataItem item;
	item.Name = name;
	item.DataGetter = dataGetter;
	_dataItems.push_back(item);
}
