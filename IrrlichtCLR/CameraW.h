#pragma once

#include "Vertex3dW.h"
#include "SceneNodeW.h"
#include "BaseW.h"

using namespace System::Collections::Generic;


public ref class CameraW : BaseW
{
public:
	CameraW(SharedParams_t* sharedParams, ICameraSceneNode* camera);

	// ������������ � ������
	void ZoomToNode(SceneNodeW^ node);
	// ������������ �� ���� ��������
	void FullZoom();

	// ��������� ������
	void RotateCamera(Vertex3dW^ vertex);
	// �������� / ���������� ������
	void WheelCamera(float distance);
	// ����������� ������
	void MoveCamera(Vertex3dW^ vertex);

	property Vertex3dW^ Position
	{
		Vertex3dW^ get(){ return GetCameraPosition(); }
		void set(Vertex3dW^ value){ return SetCameraPosition(value); }
	}

	property Vertex3dW^ Target
	{
		Vertex3dW^ get(){ return GetCameraTarget(); }
		void set(Vertex3dW^ value){ return SetCameraTarget(value); }
	}
private:
	// ���������� ������� ������
	void SetCameraPosition(Vertex3dW^ vertex);
	// �������� ������� ������
	Vertex3dW^ GetCameraPosition();
	// ���������� ���� ������
	void SetCameraTarget(Vertex3dW^ vertex);
	// �������� ���� ������
	Vertex3dW^ GetCameraTarget();


	ICameraSceneNode*		_camera;
};

