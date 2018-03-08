#include "CPVRImage.h"



//! Constructor from raw data
CPVRImage::CPVRImage(ECOLOR_FORMAT format, u32 size, void* data, core::dimension2d<s32> dimension,
			bool ownForeignMemory, bool deleteForeignMemory)
: Data(0), Size(size), Format(format), DeleteMemory(deleteForeignMemory),
Dimension(dimension)
{
	if (ownForeignMemory)
	{
		Data = (u8*)0xbadf00d;
		initData(size);
		Data = (u8*)data;
	}
	else
	{		
		Data = 0;
		initData(size);
		memcpy(Data, data, size);
	}
}


//! assumes format and size has been set and creates the rest
void CPVRImage::initData(u32 size)
{
	if (!Data)
	{
		Size = size;
		DeleteMemory=true;
		Data = new u8[size];
	}	
}


//! destructor
CPVRImage::~CPVRImage()
{
	printf("\n\nDeleted \n");
	if ( DeleteMemory )
		delete [] Data;
}


//! Returns width and height of image data.
const core::dimension2d<s32>& CPVRImage::getDimension() const
{
	return Dimension;
}


//! Returns bits per pixel.
u32 CPVRImage::getBitsPerPixel() const
{
//	return getBitsPerPixelFromFormat(Format);
	return 0;
}


//! Returns bytes per pixel
u32 CPVRImage::getBytesPerPixel() const
{
	//return getBitsPerPixelFromFormat(Format)/8;
	return 0;
}


//! Returns image data size in bytes
u32 CPVRImage::getImageDataSizeInBytes() const
{
	return Size;
}


//! Returns image data size in pixels
u32 CPVRImage::getImageDataSizeInPixels() const
{
	//not implement
	return 0;
}


//! returns mask for red value of a pixel
u32 CPVRImage::getRedMask() const
{
	//not implement
	return 0x0;
}


//! returns mask for green value of a pixel
u32 CPVRImage::getGreenMask() const
{
	//not implement
	return 0x0;
}


//! returns mask for blue value of a pixel
u32 CPVRImage::getBlueMask() const
{
	return 0x0;
}


//! returns mask for alpha value of a pixel
u32 CPVRImage::getAlphaMask() const
{
	//not implement
	return 0x0;
}


//! sets a pixel
void CPVRImage::setPixel(u32 x, u32 y, const SColor &color)
{
	//not implement
}


//! returns a pixel
SColor CPVRImage::getPixel(u32 x, u32 y) const
{
	//not implement
	return SColor(0);
}


//! returns the color format
ECOLOR_FORMAT CPVRImage::getColorFormat() const
{
	return Format;
}


//! copies this surface into another at given position
void CPVRImage::copyTo(IImage* target, const core::position2d<s32>& pos)
{
	//not implement
}


//! copies this surface partially into another at given position
void CPVRImage::copyTo(IImage* target, const core::position2d<s32>& pos, const core::rect<s32>& sourceRect, const core::rect<s32>* clipRect)
{
	//not implement
}


//! copies this surface into another, using the alpha mask, a cliprect and a color to add with
void CPVRImage::copyToWithAlpha(IImage* target, const core::position2d<s32>& pos, const core::rect<s32>& sourceRect, const SColor &color, const core::rect<s32>* clipRect)
{
//not implement
}


//! copies this surface into another, scaling it to the target image size
// note: this is very very slow.
void CPVRImage::copyToScaling(void* target, s32 width, s32 height, ECOLOR_FORMAT format, u32 pitch)
{
	//not implement
}


//! copies this surface into another, scaling it to the target image size
// note: this is very very slow.
void CPVRImage::copyToScaling(IImage* target)
{
	//not implement
}


//! copies this surface into another, scaling it to fit it.
void CPVRImage::copyToScalingBoxFilter(IImage* target, s32 bias, bool blend)
{
	//not implement
}


//! fills the surface with given color
void CPVRImage::fill(const SColor &color)
{
	//not implement
}


// Methods for Software drivers, non-virtual and not necessary to copy into other image classes
//! draws a rectangle
void CPVRImage::drawRectangle(const core::rect<s32>& rect, const SColor &color)
{
	//not impliment
}


//! draws a line from to with color
void CPVRImage::drawLine(const core::position2d<s32>& from, const core::position2d<s32>& to, const SColor &color)
{
	////not impliment
}


