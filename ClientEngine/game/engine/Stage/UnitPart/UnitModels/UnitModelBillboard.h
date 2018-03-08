#pragma once
#include "UnitModelBase.h"

class UnitModelBillboard : public UnitModelBase
{
public:
	UnitModelBillboard(SharedParams_t params);

	UNIT_MODEL_TYPE GetModelType()
	{
		return UNIT_MODEL_TYPE_BILLBOARD;
	}

	ISceneNode* LoadSceneNode();

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
