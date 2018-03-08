#include "ExecuteTextures.h"
#include "../../UnitInstance/UnitInstanceStandard.h"

ExecuteTextures::ExecuteTextures(SharedParams_t params) : ExecuteBase(params),
	_animator(NULL), _animatorExtend(NULL)
{
}

ExecuteTextures::~ExecuteTextures()
{
	if(_animator != NULL) 
		_animator->drop();
}

// Установить аниматор
void ExecuteTextures::SetAnimator( ISceneNodeAnimator* animator )
{
	_DEBUG_BREAK_IF(_animator != NULL)
	_animator = animator;
	_animatorExtend = dynamic_cast<IExtendAnimator*>(_animator);
}

// Выполнить действие
void ExecuteTextures::Run( scene::ISceneNode* node, core::array<Event_t*>& events )
{
	ExecuteBase::Run(node, events);

	if (_animatorExtend != NULL)
	{
		_animatorExtend->ResetState();
	}
	node->addAnimator(_animator);
}
