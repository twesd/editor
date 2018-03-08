#pragma once
#include "../../../Core/Base.h"

class CameraBehaviorBase : public Base 
{
public:
	CameraBehaviorBase(SharedParams_t params, ICameraSceneNode* camera);
	virtual ~CameraBehaviorBase(void);
		
	// Типы камер
	enum CameraBehaviorType
	{
		Static = 0,
		FollowToNode
	};

	// Получить тип камеры
	virtual CameraBehaviorType GetType() = 0;

	virtual void Update() = 0;

protected:
	ICameraSceneNode* Camera;
};
