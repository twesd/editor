#ifndef __3D_H_RECIVER_INCLUDED__
#define __3D_H_RECIVER_INCLUDED__

#include "Base.h"

/*
*  @brief To get all the events sent by User.
*/
class Reciver : public IEventReceiver
{
public:
	Reciver(f32 screenScale);
	virtual bool OnEvent(const SEvent& event);
	// This is used to check whether a key is being held down
#ifndef IPHONE_COMPILE
	virtual bool IsKeyDown(EKEY_CODE keyCode) const	
	{
		return _keyIsDown[keyCode];
	}
#endif
	int XTouch;
	int YTouch;
	int StateTouch;
	int XTouchSecond;
	int YTouchSecond;
	int StateTouchSecond;
	f32 Xaccel;
	f32 Yaccel;
	f32 Zaccel;
	f32 ScreenScale;
#ifndef IPHONE_COMPILE
	bool MousePressed;
#endif
private:
#ifndef IPHONE_COMPILE
	// We use this array to store the current state of each key
	bool _keyIsDown[KEY_KEY_CODES_COUNT];
	int	 _lastKey;	
#endif
};

#endif