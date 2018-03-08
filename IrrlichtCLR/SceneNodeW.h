#pragma once

#include "BaseW.h"
#include "core/animators/LineAnimator.h"

using namespace System::Collections::Generic;

public ref class SceneNodeW
{
public:
	SceneNodeW(ISceneNode* node, SharedParams_t* shareParams);	

	// Индентификатор модели
	property int Id
	{
		int get()
		{
			return (int)ScNode;
		}
	}

	// Индентификатор модели
	property int FilterId
	{
		int get()
		{
			return ScNode->getID();
		}
		void set(int val)
		{ 
			ScNode->setID(val);
		}
	}

	// Видимость модели
	property bool Visible
	{
		bool get()
		{
			return ScNode->isVisible();
		}
		void set(bool visible)
		{ 
			ScNode->setVisible(visible);
		}
	}

	property bool IsExist
	{
		bool get()
		{
			return GetIsExist();
		}
	}

	// Получить модель
	ISceneNode* GetNode();

	// Установить свойства по умолчанию
	void SetDefaultProperty();

	// Установить скорость 
	void SetAnimationSpeed(float speed);

	// Установить кадры
	void SetFrameLoop(int startFrame, int endFrame);

	// Установить повтор
	void SetLoopMode(bool loop);

	// Установить полупрозрачность
	void EnableHalfTransparent(bool enable);
	
	// Получить позицию
	Vertex3dW^ GetPosition();

	// Установить позицию
	void SetPosition(Vertex3dW^ position);

	// Получить уголы поворота
	Vertex3dW^ GetRotation();

	// Установить уголы поворота
	void SetRotation(Vertex3dW^ rotation);

	// Получить масштаб
	Vertex3dW^ GetScale();

	// Установить масштаб
	void SetScale(Vertex3dW^ scale);

	// Установить тип материи
	void SetMaterialType(int matType);

	// Установить текстуру
	void SetTexture(int index, String^ path);

	// Получить границы
	BoundboxW^ GetBoundBox();

	// Подсветить объект
	void Highlight();

	// Снять подсветку
	void UnHighlight();

	// Добавить трансформацию
	void AddTransform(int type, List<Vertex3dW^>^ pointsW, List<u32>^ times);

	// Удалить все трансформации
	void ClearTransforms();
protected:
	ISceneNode* ScNode;

private:
	bool GetIsExist();

	bool GetIsNodeExistInChilds( ISceneNode* &rootNode, ISceneNode* searchNode );

	core::array<vector3df> GetTransformPoints(core::array<vector3df> points, vector3df rotation);

	IAnimatedMeshSceneNode* _animNode;
	SharedParams_t* SharedParams;
};
