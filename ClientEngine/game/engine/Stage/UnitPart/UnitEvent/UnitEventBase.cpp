#include "UnitEventBase.h"

UnitEventBase::UnitEventBase(SharedParams_t params) : Base(params),
	Behavior(NULL)
{
}

UnitEventBase::~UnitEventBase(void)
{
}

// Начало выполнения
void UnitEventBase::Begin()
{

}

// Установить поведение, которому принадлежит условия
// Установка ссылки на объект
void UnitEventBase::SetBehavior( UnitBehavior* behavior )
{
	_DEBUG_BREAK_IF(behavior == NULL)
	Behavior = behavior;
}

