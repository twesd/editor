#include "UnitInstanceCamera.h"
#include "CameraBehavior/CameraBehaviorFollowToNode.h"
#include "CameraBehavior/CameraBehaviorStatic.h"
#include "../UnitManager.h"

UnitInstanceCamera::UnitInstanceCamera(
	SharedParams_t params, 
	UnitGenInstanceCamera* genInstance, 
	UnitInstanceBase* parent,
	UnitManager* unitManager,
	UnitInstanceBase* creator) : 
	UnitInstanceBase(params, genInstance, parent, creator)
{
	_unitManager = unitManager;

	// После создания новой камеры, автоматически меняется активная камера
	ICameraSceneNode* prevCamera = SceneManager->getActiveCamera();

	_camera = SceneManager->addCameraSceneNode();	
	_camera->setPosition(genInstance->StartPosition);
	_camera->setTarget(genInstance->StartTarget);
	_camera->setFarValue(genInstance->FarValue);

	irr::core::vector3df upv = irr::core::vector3df (0.0f, 1.0f, 0.0f);
	_camera->setUpVector(upv);

	if (prevCamera)
		SceneManager->setActiveCamera(prevCamera);

	// Создаём поведение
	//
	_DEBUG_BREAK_IF(genInstance->CameraGenBaseData == NULL)
	if (genInstance->CameraGenBaseData->GetType() == CameraGenBase::Static)
	{
		_cameraBehavior = new CameraBehaviorStatic(SharedParams, _camera);
	}
	else if (genInstance->CameraGenBaseData->GetType() == CameraGenBase::FollowToNode)
	{
		CameraGenFollowToNode* genBehavior = (CameraGenFollowToNode*)genInstance->CameraGenBaseData;
		_cameraBehavior = new CameraBehaviorFollowToNode(
			SharedParams,
			_camera, 
			_unitManager,
			genBehavior->UnitInstanceName,
			genBehavior->RotateWithUnit,
			genInstance->StartPosition,
			genInstance->StartTarget,
			genBehavior->MapSize2d,
			genBehavior->MapSize3d,
			genBehavior->ObstacleFilterId);
	}
	else
	{
		_DEBUG_BREAK_IF(true)
	}
}

UnitInstanceCamera::~UnitInstanceCamera(void)
{
	if (_cameraBehavior) delete _cameraBehavior;
	_cameraBehavior = NULL;
	_camera->remove();
}

// Обновить данные
void UnitInstanceCamera::Update()
{
	
}

// Обработка событий
void UnitInstanceCamera::HandleEvent(core::array<Event_t*>& events)
{
	if (SceneManager->getActiveCamera() != _camera) 
		return;

	_cameraBehavior->Update();
}

// Активировать камеру
void UnitInstanceCamera::Activate()
{
	SceneManager->setActiveCamera(_camera);
}
