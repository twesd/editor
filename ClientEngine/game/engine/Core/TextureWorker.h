#pragma once

#include "Base.h"

class TextureWorker
{
public:

	static video::ITexture* GetTexture(video::IVideoDriver* driver, stringc path,bool use32bit = false);

	static void DrawTexture(video::IVideoDriver* driver, video::ITexture *texture,int x, int y, int alpha = 255, int width = 0, int height = 0);
};
