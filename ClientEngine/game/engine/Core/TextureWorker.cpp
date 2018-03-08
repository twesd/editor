#include "TextureWorker.h"
#include "Core/FileUtility.h"

video::ITexture* TextureWorker::GetTexture(video::IVideoDriver* driver,stringc modelPath,bool use32bit)
{
	video::ITexture* texture;

	if(use32bit)
	{
		//for this texture use good quality
		driver->setTextureCreationFlag(video::ETCF_ALWAYS_32_BIT, true);
	}
	else 
	{		
		driver->setTextureCreationFlag(video::ETCF_ALWAYS_16_BIT, true);
	}
	
	modelPath = FileUtility::GetUpdatedFileName(modelPath);

#ifdef DEBUG_MESSAGE_PRINT
	if(driver->findTexture( modelPath.c_str ()) == NULL)
	{
		printf("[WARNING] Texture not in cache : %s \n", modelPath.c_str());
	}
#endif

	texture = driver->getTexture( modelPath.c_str () );
	if(!texture)
		printf("[ERROR]<Base::GetTexture[%s]>(nul pointer)\n",modelPath.c_str());

	//restore quality
	driver->setTextureCreationFlag(video::ETCF_ALWAYS_16_BIT, true);

	return texture;
}


//2D Images
void TextureWorker::DrawTexture(video::IVideoDriver* driver, video::ITexture *texture,int x, int y, int alpha, int width, int height)
{
	if(texture == NULL)
	{
		return;
	}
	if(height == 0 || width == 0)
	{
		width = texture->getOriginalSize().Width;
		height = texture->getOriginalSize().Height;
	}

	if(texture != NULL)
	{
		driver->draw2DImage(texture, core::position2di(x,y), 
			core::rect<s32>(0,0,width,height), 0, video::SColor(alpha,255,255,255), true);	
	}
}
