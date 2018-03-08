#include "ColorAnimator.h"
#include "../Scene/Billboard.h"

ColorAnimator::ColorAnimator(SharedParams_t params) : Base(params)
{
	_timeForWay = 0;
	_lastTime = 0;
	_currentNumber = 0;
	_completeAnimate = true;
	_loop = false;
	_manipulator = SceneManager->getMeshManipulator();	
}

ColorAnimator::~ColorAnimator(void)
{
}

void ColorAnimator::Init(
	const core::array<video::SColor> &colors, 
	core::array<u32> timesForWay, 
	bool loop)
{
	if(colors.size() == 0 || timesForWay.size() != colors.size())
	{
		_IRR_DEBUG_BREAK_IF(true)
		_completeAnimate = true;
		return;
	}

	_colors = colors;
	_lastTime = GetNowTime();
	_timesForWay = timesForWay;		
	_completeAnimate = false;
	_currentNumber = 0;
	_loop = loop;
	CalulateValues(0);
}


void ColorAnimator::SetUserParams(void* params)
{
	
}

bool ColorAnimator::AnimationEnd()
{
	return _completeAnimate;
}

void ColorAnimator::CalulateValues(u32 number)
{
	if(_colors.size() == 0) return;
	_curTargetColor = _colors[number];	
	_timeForWay = _timesForWay[number];
	_startTime = _lastTime = GetNowTime();
	_endTime = _lastTime + _timeForWay;		
}

ISceneNodeAnimator* ColorAnimator::createClone(ISceneNode* node, ISceneManager* newManager){
	//TODO : create valid clone
	//ColorAnimator* newAnimator = new ColorAnimator(SharedParams);	
	//return newAnimator;	
	return NULL;
}

void ColorAnimator::animateNode(ISceneNode* node, u32 nowTime)
{
	if(_completeAnimate) return;
	
	
	//if(node->getType() != ESNT_ANIMATED_MESH)
	//	return;
	//IAnimatedMeshSceneNode* animSceneNode = static_cast<IAnimatedMeshSceneNode*>(node);
	//scene::IMesh* mesh = animSceneNode->getMesh();

	Billboard* billboard = dynamic_cast<Billboard*>(node);
	if(billboard == NULL)
	{
		return;
	}

	video::SColor color = billboard->GetColor();
	if( nowTime >= _endTime )
	{
		billboard->SetColor(_curTargetColor);

		_currentNumber++;
		if(_currentNumber >= _colors.size())
		{
			if(!_loop)
			{
				_completeAnimate = true;
			} 
			else 
			{
				_currentNumber = 0;
				_lastTime = _endTime;
				CalulateValues(_currentNumber);
				animateNode(node, nowTime);
			}			
		} 
		else 
		{
			_lastTime = _endTime;
			CalulateValues(_currentNumber);
			animateNode(node, nowTime);
		}
		return;
	}

	f32 timeForWay = (f32)(_endTime - nowTime);
	f32 tDiff = (f32)(nowTime - _lastTime);
	f32 tCoeff = (_timeForWay != 0) ? (tDiff / timeForWay) : 0;	
	if(tCoeff == 0)
		return;

	_lastTime = nowTime;

	video::SColor dColor = _curTargetColor.getInterpolated(color, tCoeff);
	//printf("%d\n", dColor.getAlpha());
	billboard->SetColor(dColor);
}

// Сброс в начальные установки
void ColorAnimator::ResetState()
{
	_lastTime = GetNowTime();
	_completeAnimate = false;
	_currentNumber = 0;
	CalulateValues(0);
}

