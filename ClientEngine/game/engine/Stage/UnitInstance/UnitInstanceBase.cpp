#include "UnitInstanceBase.h"
#include "Core/NodeWorker.h"

UnitInstanceBase::UnitInstanceBase(
	SharedParams_t params, 
	UnitGenInstanceBase* genInstance, 
	UnitInstanceBase* parent, 
	UnitInstanceBase* creator) : Base(params),
		Parent(parent), Childs(),
		Name(), DataItems(),
		CreatorInst(creator)
{
	SceneNode = NULL;
	Name = genInstance->UnitName;
	_isErased = false;
}

UnitInstanceBase::~UnitInstanceBase(void)
{
	for (u32 i = 0; i < Childs.size(); i++)
	{
		Childs[i]->drop();
	}
	if(SceneNode != NULL)
	{
		SceneNode->removeAll();
		SceneNode->removeAnimators();
		SceneNode->remove();
	}
}

// Пометить экземпляр юнита для удаления
void UnitInstanceBase::Erase()
{
	_isErased = true;
}

// Помечен ли экземпляр для удаления
bool UnitInstanceBase::GetIsErased()
{
	return _isErased;
}

void* UnitInstanceBase::GetData( stringc& name )
{
	DataItem* item = GetDataItem(name);
	return (item != NULL) ? item->Data : NULL;
}

// Получить данные
UnitInstanceBase::DataItem* UnitInstanceBase::GetDataItem( stringc& name )
{
	for (u32 i = 0; i < DataItems.size() ; i++)
	{
		if (DataItems[i].Name == name)
		{
			return &DataItems[i];
		}
	}
	return NULL;
}

void UnitInstanceBase::SetData( stringc& name, void* data )
{
	DataItem* item = GetDataItem(name);
	if (item != NULL)
	{
		item->Data = data;
		return;
	}

	DataItem newItem;
	newItem.Name = name;
	newItem.Data = data;
	DataItems.push_back(newItem);
}

// Получить данные как SceneNode
ISceneNode* UnitInstanceBase::GetDataAsSceneNode( stringc& name )
{
	void* data = GetData(name);
	if(data == NULL) return NULL;

	
	if(!NodeWorker::GetIsNodeExist((ISceneNode*)data, SceneManager))
		return NULL;
	return (ISceneNode*)data;
}

UnitInstanceBase* UnitInstanceBase::GetParent()
{
	return Parent;
}


// Получить юнит, который является создателем текущего юнита 
UnitInstanceBase* UnitInstanceBase::GetCreator()
{
	return CreatorInst;
}

// Получить дочерние узлы
core::array<UnitInstanceBase*> UnitInstanceBase::GetChilds()
{
	return Childs;
}

// Удалить потомка
void UnitInstanceBase::RemoveChild( UnitInstanceBase* instance )
{
	for (u32 idx = 0; idx < Childs.size() ; idx++)
	{
		if(Childs[idx] == instance)
		{
			instance->drop();
			Childs.erase(idx);
			return;
		}
	}
}

