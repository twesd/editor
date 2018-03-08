#pragma once

#include "../../Core/Base.h"

#ifdef DEBUG_VISUAL_DATA

#include "DebugSettings.h"

// Отлавливает события для отладки
class DebugReciver : public IEventReceiver
{
public:
	DebugReciver(IEventReceiver* baseReciver, ISceneManager* sceneManager, Base* baseItem);
	virtual bool OnEvent(const SEvent& event);

private:
	// Показать все глобальные параметры
	void ShowAllGlobalParameters();
	// Показать данные камеры
	void PrintCameraData();
	// Показать границы объектов
	void ShowSceneNodeBoundBox(bool show);
	// Показать объекты прозрачными
	void ShowSceneNodeTransparent( bool show );
	// Повернуть камеру
	void RotateCamera();

	IEventReceiver* _baseReciver;
	ISceneManager* _sceneManager;

	Base* _baseItem;

	ICameraSceneNode* _cameraFPS;
	ICameraSceneNode* _cameraGame;

	bool _boundsShowing;

	bool _transparentShowing;
};

#endif