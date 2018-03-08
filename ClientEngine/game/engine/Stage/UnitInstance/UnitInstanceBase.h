#pragma once
#include "../../Core/Base.h"
#include "../../Sounds/SoundManager.h"
#include "../UnitGenInstance/UnitGenInstanceBase.h"

class UnitInstanceBase : public Base
{
public:
	UnitInstanceBase(
		SharedParams_t params, 
		UnitGenInstanceBase* genInstance, 
		UnitInstanceBase* parent, 
		UnitInstanceBase* creator);
	virtual ~UnitInstanceBase(void);
	
	// Обновление данных
	virtual void Update() = 0;
	
	// Обработка событий
	virtual void HandleEvent(core::array<Event_t*>& events) = 0;

	// Пометить экземпляр юнита для удаления
	void Erase();

	// Помечен ли экземпляр для удаления
	bool GetIsErased();

	// Установить данные
	void SetData(stringc& name, void* data);

	// Получить данные
	void* GetData(stringc& name);

	// Получить родителя
	UnitInstanceBase* GetParent();

	// Получить юнит, который является создателем текущего юнита 
	UnitInstanceBase* GetCreator();

	// Получить данные как SceneNode
	ISceneNode* GetDataAsSceneNode(stringc& name);

	// Получить дочерние узлы
	core::array<UnitInstanceBase*> GetChilds();

	// Удалить потомка
	void RemoveChild(UnitInstanceBase* child);

	// Объект сцены
	ISceneNode* SceneNode;

	// Имя юнита
	stringc Name;

protected:
	typedef struct
	{
		stringc Name;
		void* Data;
	}DataItem;

	// Получить данные
	DataItem* GetDataItem(stringc& name);

	// Родительский экземпляр
	UnitInstanceBase* Parent;

	// Дочерние узлы
	core::array<UnitInstanceBase*> Childs;

	// Данные
	core::array<DataItem> DataItems;

	// Юнит, который является создателем текущего юнита
	UnitInstanceBase* CreatorInst;

private:
	// Помечен ли экземпляр для удаления
	bool _isErased;
};
