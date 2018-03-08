#pragma once
#include "ExecuteBase.h"
#include "../../../Core/Animators/IExtendAnimator.h"
#include "../../../Core/Animators/ColorAnimator.h"

class ExecuteColor : public ExecuteBase
{
public:
	ExecuteColor(
		SharedParams_t params, 
		const core::array<video::SColor> &colors, 
		core::array<u32> timesForWay, 
		bool loop);
	virtual ~ExecuteColor(void);

	// Выполнить действие
	virtual void Run(scene::ISceneNode* node, core::array<Event_t*>& events);

private:

	// Точки изменения
	core::array<video::SColor> _colors;
	
	// Точки изменений
	core::array<u32> _timesForWay;

	// Повторять изменения
	bool _loop;

	// Аниматор 
	ColorAnimator* _colorAnimator;
};
