#pragma once
#include "UnitGenInstanceBase.h"

class UnitGenInstanceBillboard : public UnitGenInstanceBase
{
public:
	UnitGenInstanceBillboard(SharedParams_t params);
	virtual ~UnitGenInstanceBillboard(void);

	// Получить тип
	virtual GenInstanceType GetType()
	{
		return UnitGenInstanceBase::Billboard;
	}

	// Необходимо ли удалить описание. Случай когда юниты больше создаватья не будут
	virtual bool CanDispose();
	
	s32 NodeId;

	// Ширина объекта
	f32 Width;

	// Высота объекта
	f32 Height;

	// Путь до текстуры
	stringc TexturePath;

	// Использовать 32 бита
	bool Use32Bit;

	// Тип материи
	video::E_MATERIAL_TYPE MaterialType;

	bool UseUpVector;

	vector3df UpVector;

	bool UseViewVector;

	vector3df ViewVector;

};
