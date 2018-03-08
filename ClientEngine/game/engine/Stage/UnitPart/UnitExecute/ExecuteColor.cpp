#include "ExecuteColor.h"
#include "../../UnitInstance/UnitInstanceStandard.h"


ExecuteColor::ExecuteColor(
	SharedParams_t params,
	const core::array<video::SColor> &colors, 
	core::array<u32> timesForWay, 
	bool loop) : ExecuteBase(params), 
		_colors(), _timesForWay()
{
	_colors = colors;
	_timesForWay = timesForWay;
	_loop = loop;
	_colorAnimator = new ColorAnimator(params);
}

ExecuteColor::~ExecuteColor()
{
	if(_colorAnimator != NULL)
		_colorAnimator->drop();
}


// Выполнить действие
void ExecuteColor::Run( scene::ISceneNode* node, core::array<Event_t*>& events )
{
	ExecuteBase::Run(node, events);

	_colorAnimator->ResetState();		
	_colorAnimator->Init(_colors, _timesForWay, _loop);
	node->addAnimator(_colorAnimator);
}