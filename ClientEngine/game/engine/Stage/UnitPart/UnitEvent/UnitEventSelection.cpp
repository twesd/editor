#include "UnitEventSelection.h"
#include "../UnitBehavior.h"
#include "../../UnitInstance/UnitInstanceStandard.h"

UnitEventSelection::UnitEventSelection(SharedParams_t params) : UnitEventBase(params),
	Count(0), 
	_selectors(),
	_isInit(false)
{
}

UnitEventSelection::~UnitEventSelection(void)
{
	for (u32 i = 0; i < _selectors.size() ; i++)
	{
		_selectors[i]->drop();
	}
}

// Добавить выборку
// Захват ресурса grab
void UnitEventSelection::AddSelector( UnitSelectSceneNodeBase* selector )
{
	_selectors.push_back(selector);
	selector->grab();
}

// Выполняется ли условие
bool UnitEventSelection::IsApprove( core::array<Event_t*>& events )
{
	if(!_isInit)
	{
		_isInit = true;

		UnitInstanceStandard* instance = Behavior->GetUnitInstance();
		for (u32 i = 0; i < _selectors.size() ; i++)
		{
			_selectors[i]->SetUnitInstance(instance);
		}
	}

	int count = 0;
	for (u32 i = 0; i < _selectors.size() ; i++)
	{
		count += _selectors[i]->Select(events).size();
	}
	return (Count == count);
}

// Начало выполнения
void UnitEventSelection::Begin()
{
	
}


