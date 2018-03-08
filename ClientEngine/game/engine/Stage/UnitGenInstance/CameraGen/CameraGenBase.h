#pragma once
#include "../../../Core/Base.h"

class CameraGenBase 
{
public:
	CameraGenBase();
	virtual ~CameraGenBase(void);
		
	// Типы камер
	enum GenCameraType
	{
		Static = 0,
		FollowToNode
	};

	// Получить тип камеры
	virtual GenCameraType GetType() = 0;

};
