#pragma once

#include "Vertex3dW.h"
#include "SceneNodeW.h"
#include "BillboardW.h"
#include "SelectorW.h"
#include "CameraW.h"
#include "ControlsW.h"
#include "BoundboxW.h"

public ref class IrrDeviceW
{
public:
	IrrDeviceW(int windowId);
	virtual ~IrrDeviceW(void);
	void Close();

	void DrawAll();

	// Добавить модель
	SceneNodeW^ AddSceneNode(String^ path);

	// Добавить BoundBox
	SceneNodeW^ AddCube(BoundboxW^ boundbox);

	// Добавить сферу
	SceneNodeW^ AddSphere(float radius, int polyCount);

	// Добавить Billboard
	SceneNodeW^ AddBillboard(float width, float height);

	// Обновить размер экрана
	void ResizeScreen(int width, int height);

	// Удалить модель
	void DeleteSceneNode(SceneNodeW^ node);

	// Удалить все модели
	void DeleteSceneNodes();

	// Преобразовать координаты из экранных
	Vertex3dW^ IrrDeviceW::ScreenCoordToPosition3d(int x, int y, f32 unitY);
	// Преобразовать координаты в экранные координаты
	Vertex3dW^ IrrDeviceW::Position3dToSceenCoord(Vertex3dW^ pos);

	property SelectorW^ Selector
	{
		SelectorW^ get()
		{
			return _selector;
		}
	}
	property CameraW^ Camera
	{
		CameraW^ get()
		{
			return _cameraW;
		}
	}

	property ControlsW^ Controls
	{
		ControlsW^ get()
		{
			return _controlsW;
		}
	}
private:
	IrrlichtDevice*			_device;
	video::IVideoDriver*	_driver;
	scene::ISceneManager*	_sceneManager;	
	ITimer*					mTimer;

	ICameraSceneNode*		mCamera;
	ISceneCollisionManager* mCollisionManager;

	SharedParams_t* SharedParams;
	SelectorW^ _selector;
	CameraW^ _cameraW;
	ControlsW^ _controlsW;
};
