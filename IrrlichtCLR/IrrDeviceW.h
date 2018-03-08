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

	// �������� ������
	SceneNodeW^ AddSceneNode(String^ path);

	// �������� BoundBox
	SceneNodeW^ AddCube(BoundboxW^ boundbox);

	// �������� �����
	SceneNodeW^ AddSphere(float radius, int polyCount);

	// �������� Billboard
	SceneNodeW^ AddBillboard(float width, float height);

	// �������� ������ ������
	void ResizeScreen(int width, int height);

	// ������� ������
	void DeleteSceneNode(SceneNodeW^ node);

	// ������� ��� ������
	void DeleteSceneNodes();

	// ������������� ���������� �� ��������
	Vertex3dW^ IrrDeviceW::ScreenCoordToPosition3d(int x, int y, f32 unitY);
	// ������������� ���������� � �������� ����������
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
