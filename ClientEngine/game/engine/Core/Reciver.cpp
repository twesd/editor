#include "Reciver.h"


Reciver::Reciver(f32 screenScale)
{
	XTouch				=	FAIL_VALUE;
	YTouch				=	FAIL_VALUE;
	StateTouch			=	FAIL_VALUE;
	XTouchSecond		=	FAIL_VALUE;
	YTouchSecond		=	FAIL_VALUE;
	StateTouchSecond	=	FAIL_VALUE;
	Xaccel = 0;
	Yaccel = 0;
	Zaccel = 0;
	ScreenScale = screenScale;
#ifndef IPHONE_COMPILE
	for (u32 i=0; i<KEY_KEY_CODES_COUNT; ++i)
		_keyIsDown[i] = false;
	_lastKey = FAIL_VALUE;
	MousePressed = false;
#endif	
}

bool Reciver::OnEvent(const SEvent& event)
{
#ifdef IPHONE_COMPILE
	if (event.EventType == irr::EET_TOUCH_INPUT_EVENT)
	{		
		//printf("event[%d][X:[%d],Y:[%d],Tap:[%d]]\n",event.TouchInput.Event,event.TouchInput.X,event.TouchInput.Y,event.TouchInput.TapCount);
		if(event.TouchInput.Event>4)
		{
			XTouchSecond = event.TouchInput.X * ScreenScale;
			YTouchSecond = event.TouchInput.Y * ScreenScale;
			StateTouchSecond = event.TouchInput.Event - 4;
		}else
		{
			XTouch = event.TouchInput.X * ScreenScale;
			YTouch = event.TouchInput.Y * ScreenScale;
			StateTouch = event.TouchInput.Event;
		}
	}
	else if (event.EventType == irr::EET_ACCELEROMETER_EVENT)
	{
		Xaccel = event.AccelerometerInput.X;
		Yaccel = event.AccelerometerInput.Y;
		Zaccel = event.AccelerometerInput.Z;
	}
#else
	if(event.EventType == irr::EET_MOUSE_INPUT_EVENT)
	{		
		if(event.MouseInput.Event == irr::EMIE_LMOUSE_LEFT_UP || event.MouseInput.Event == irr::EMIE_LMOUSE_PRESSED_DOWN)
		{
			StateTouch = event.MouseInput.Event;			
			XTouch = event.MouseInput.X;
			YTouch = event.MouseInput.Y;
			
			if(StateTouch == irr::EMIE_LMOUSE_LEFT_UP) 
			{
				//printf("[Reciver] Left mouse up \n");
				MousePressed = false;
			}
			if(StateTouch == irr::EMIE_LMOUSE_PRESSED_DOWN) 
			{
				//printf("[Reciver] Left mouse down \n");
				MousePressed = true;
			}
		}

		if(MousePressed && event.MouseInput.Event == irr::EMIE_MOUSE_MOVED)
		{
			StateTouch = event.MouseInput.Event;			
			XTouch = event.MouseInput.X;
			YTouch = event.MouseInput.Y;
		}
		//printf("x [%d] y [%d] \n",XTouch,XTouch);
	}
	// Remember whether each key is down or up
	if (event.EventType == irr::EET_KEY_INPUT_EVENT)
	{
		_keyIsDown[event.KeyInput.Key] = event.KeyInput.PressedDown;				
		if(event.KeyInput.PressedDown)
		{
			_lastKey = event.KeyInput.Key;
			//interpritate key action
			switch(event.KeyInput.Key)
			{
			case irr::KEY_KEY_D:
				XTouchSecond = 236;
				YTouchSecond = 490;
				break;
			case irr::KEY_KEY_A:
				XTouchSecond = 68;
				YTouchSecond = 487;
				break;
			case irr::KEY_KEY_S:
				XTouchSecond = 146;
				YTouchSecond = 565;
				break;
			case irr::KEY_KEY_W:
				XTouchSecond = 154;
				YTouchSecond = 404;
				break;
			case irr::KEY_KEY_M:
				XTouchSecond = 429;
				YTouchSecond = 274;
				break;
			case irr::KEY_KEY_Z:
				Yaccel = 0.5f;
				break;
			case irr::KEY_KEY_X:
				Yaccel = -0.5f;
				break;
			}
		}
		else
		{
			if(_lastKey == event.KeyInput.Key)
			{
				XTouchSecond = FAIL_VALUE;
				YTouchSecond = FAIL_VALUE;
				_lastKey = FAIL_VALUE;
				Xaccel = 0;
				Yaccel = 0;
				Zaccel = 0;
			}
		}
	}
#endif

	return false;
}
