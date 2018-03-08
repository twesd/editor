#pragma once
#include "IControl.h"

class ControlTapScene : public IControl
{
public:
	ControlTapScene(SharedParams_t params);
	virtual ~ControlTapScene(void);
	virtual CONTROL_TYPE GetControlType()
	{
		return CONTROL_TYPE_TAPSCENE;
	};
	void Init(position2di minPoint, position2di maxPoint, bool pickUpNode, int filterNodeId);	
	
	///////////////// IControl ////////////////////
	
	void Update(reciverInfo_t *reciverInfo);

	void Draw();

	// Получить границы контрола
	recti GetBounds();

	// Установить позицию контрола
	void SetPosition(position2di newPosition);

	// Получить позицию контрола
	position2di GetPosition();

	/////////////////////////////////////
	
	// Установить не обрабатываемые области
	void SetNotProcessingSpace(core::array<recti> &rects);

	// Получить последние событие
	ControlTapSceneDesc_t GetTapSceneDesc();

private:
	// Структура для события
	ControlTapSceneDesc_t _controlTapSceneDesc;

	// Объект для получения модели
	ISceneCollisionManager* _collisionManager;

	// Выбирать модели в игровом пространстве
	bool _pickUpNode;

	// Фильтр моделий (-1 выбирать любую)
	int _filterNodeId;

	// Границы обработки
	core::recti _bounds;

	// Не обрабатываемые области
	core::array<recti> _notProcessingRects;
};
