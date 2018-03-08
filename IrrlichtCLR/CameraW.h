#pragma once

#include "Vertex3dW.h"
#include "SceneNodeW.h"
#include "BaseW.h"

using namespace System::Collections::Generic;


public ref class CameraW : BaseW
{
public:
	CameraW(SharedParams_t* sharedParams, ICameraSceneNode* camera);

	// Приблизиться к модели
	void ZoomToNode(SceneNodeW^ node);
	// Приблизиться ко всем объектам
	void FullZoom();

	// Повернуть камеру
	void RotateCamera(Vertex3dW^ vertex);
	// Отдалить / приблизить камеру
	void WheelCamera(float distance);
	// Переместить камеру
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
	// Установить позицию камеры
	void SetCameraPosition(Vertex3dW^ vertex);
	// Получить позицию камеры
	Vertex3dW^ GetCameraPosition();
	// Установить цель камеры
	void SetCameraTarget(Vertex3dW^ vertex);
	// Получить цель камеры
	Vertex3dW^ GetCameraTarget();


	ICameraSceneNode*		_camera;
};

