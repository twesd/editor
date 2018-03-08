#pragma once
#include "../../Core/Base.h"
#include "../../Core/scene/SceneText.h"

#ifdef DEBUG_VISUAL_DATA

class UnitInstanceStandard;

class DebugNode : public Base, public ISceneNode
{
public:
	DebugNode(SharedParams_t params, ISceneNode* parent);
	virtual ~DebugNode(void);

	// Установить юнит
	void SetInstance(UnitInstanceStandard* instance);
	
    // Получить юнит
	UnitInstanceStandard* GetInstance();

	//---- OVERVRIDE ----

	//! pre render event
	virtual void OnRegisterSceneNode();
	//! render
	virtual void render();
	//! returns the axis aligned bounding box of this node
	virtual const core::aabbox3d<f32>& getBoundingBox() const;
	video::SMaterial& getMaterial(u32 i);
	u32 getMaterialCount() const;

private:
	UnitInstanceStandard* _instance;

	video::SMaterial _material;

	core::aabbox3d<f32> _boundBox;

	SceneText* _textNode;

	// Предыдущие дейсвие
	stringc _prevAction;

	// Предыдущие дейсвие
	stringc _prevShowAction;
};

#endif
