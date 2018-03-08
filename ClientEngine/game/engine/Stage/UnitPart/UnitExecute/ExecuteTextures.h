#pragma once
#include "ExecuteBase.h"
#include "../../../Core/Animators/IExtendAnimator.h"

class ExecuteTextures : public ExecuteBase
{
public:
	ExecuteTextures(SharedParams_t params);
	virtual ~ExecuteTextures(void);

	// Установить аниматор
	void SetAnimator( ISceneNodeAnimator* animator );

	// Выполнить действие
	virtual void Run(scene::ISceneNode* node, core::array<Event_t*>& events);

private:
	// Аниматор
	ISceneNodeAnimator* _animator;
	// Аниматор расширенный
	IExtendAnimator* _animatorExtend;
};
