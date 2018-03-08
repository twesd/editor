#pragma once
#include "../../../Core/Base.h"

class UnitInstanceBase;

class UnitMappingBase : public Base
{
public:
	UnitMappingBase(SharedParams_t params);
	virtual ~UnitMappingBase(void);	
	
	// Выполнить обработку
	virtual void Update( ) = 0;

	// Установить экземпляр юнита, которому принадлежит действие
	virtual void SetUnitInstance(UnitInstanceBase* unitInstance);

protected:
	// Экземпляр юнита
	UnitInstanceBase* UnitInstance;
};
