#pragma once
#include "UnitInstanceBase.h"
#include "../UnitGenInstance/UnitGenInstanceCamera.h"
#include "CameraBehavior/CameraBehaviorBase.h"

class UnitManager;

class UnitInstanceCamera : public UnitInstanceBase
{
public:
	UnitInstanceCamera(
		SharedParams_t params, 
		UnitGenInstanceCamera* genInstance,
		UnitInstanceBase* parent, 
		UnitManager* unitManager,
		UnitInstanceBase* creator);
	virtual ~UnitInstanceCamera(void);
	// Обновить данные
	void Update();
	// Обработка событий
	void HandleEvent(core::array<Event_t*>& events);

	// Активировать камеру
	void Activate();

private:
	ICameraSceneNode* _camera;
	UnitManager* _unitManager;
	CameraBehaviorBase* _cameraBehavior;
};
