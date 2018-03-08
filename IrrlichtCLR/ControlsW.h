#pragma once
#include "Vertex3dW.h"
#include "SceneNodeW.h"
#include "BaseW.h"
#include "Controls/ControlManager.h"

using namespace System::Collections::Generic;

public ref class ControlsW : BaseW
{
public:
	ControlsW(SharedParams_t* sharedParams);

	void Draw();

	void Remove(String^ name);
	void Clear();

	void AddButton(String^ name,String^ texture, Vertex3dW^ positionW);

	void AddText(String^ text,String^ font, Vertex3dW^ positionW, int kerningWidth, int kerningHeight);

	// Добавить прямоугольник
	void AddRect(Vertex3dW^ positionW, int width, int height, u32 color, bool outline);

	// Добавить линию
	void AddLine(Vertex3dW^ startPnt, Vertex3dW^ endPnt, int width, u32 color);
private:
	ControlManager* _controlManager;
};

