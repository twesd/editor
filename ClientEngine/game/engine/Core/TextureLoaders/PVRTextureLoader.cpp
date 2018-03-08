#include "PVRTextureLoader.h"
#include "CPVRImage.h"


typedef struct _PVRTexHeader
{
	u32 headerLength;
	u32 height;
	u32 width;
	u32 numMipmaps;
	u32 flags;
	u32 dataLength;
	u32 bpp;
	u32 bitmaskRed;
	u32 bitmaskGreen;
	u32 bitmaskBlue;
	u32 bitmaskAlpha;
	u32 pvrTag;
	u32 numSurfs;
} PVRTexHeader;

static char gPVRTexIdentifier[] = "PVR!";
#define PVR_TEXTURE_FLAG_TYPE_MASK	0xff
enum
{
	kPVRTextureFlagTypePVRTC_2 = 24,
	kPVRTextureFlagTypePVRTC_4
};


PVRTextureLoader::PVRTextureLoader()
{
}

PVRTextureLoader::~PVRTextureLoader(void)
{
}


u32 PVRTextureLoader::CFSwapInt32(u32 arg) const {
#if CF_USE_OSBYTEORDER_H
    return OSSwapInt32(arg);
#else
    u32 result;
    result = ((arg & 0xFF) << 24) | ((arg & 0xFF00) << 8) | ((arg >> 8) & 0xFF00) | ((arg >> 24) & 0xFF);
    return result;
#endif
}

//Converts a 32-bit integer from little-endian format to the hostís native byte order.
u32 PVRTextureLoader::CFSwapInt32LittleToHost(u32 arg) const {
#ifdef WINDOWS_COMPILE
	return arg; 
#else

#if CF_USE_OSBYTEORDER_H
    return OSSwapLittleToHostInt32(arg);
#elif __LITTLE_ENDIAN__
    return arg;
#else
    return CFSwapInt32(arg);
#endif

#endif
}

//! returns true if the file maybe is able to be loaded by this class
//! based on the file extension (e.g. ".tga")
bool PVRTextureLoader::isALoadableFileExtension(const c8* fileName) const
{
	return strstr(fileName, ".pvrtc")!=0;
}

//! returns true if the file maybe is able to be loaded by this class
bool PVRTextureLoader::isALoadableFileFormat(io::IReadFile* file) const
{
	if (!file)
		return false;

	unsigned char buffer[sizeof(PVRTexHeader)];
	// Read the first few bytes of the PNG file
	if (file->read(buffer, sizeof(PVRTexHeader)) != 8)
		return false;

	PVRTexHeader *header = NULL;
	u32 pvrTag;
	
	header = (PVRTexHeader *)buffer;
	
	pvrTag = CFSwapInt32LittleToHost(header->pvrTag);

	if (gPVRTexIdentifier[0] != ((pvrTag >>  0) & 0xff) ||
		gPVRTexIdentifier[1] != ((pvrTag >>  8) & 0xff) ||
		gPVRTexIdentifier[2] != ((pvrTag >> 16) & 0xff) ||
		gPVRTexIdentifier[3] != ((pvrTag >> 24) & 0xff))
	{
		return false;
	}
	return true;
}


// load in the image data
video::IImage* PVRTextureLoader::loadImage(io::IReadFile* file) const
{
#ifndef IPHONE_COMPILE
	return 0;
#endif

	if (!file)
		return 0;

	video::IImage* image = 0;


	///////////////////////////// unpackPVRData /////////////////////////////
	u32 _width, _height;
	ECOLOR_FORMAT _internalFormat;
	bool _hasAlpha;
	bool success = false;
	PVRTexHeader *header = NULL;
	u32 flags, pvrTag;
	u32 dataLength = 0, dataOffset = 0, dataSize = 0;
	u32 blockSize = 0, widthBlocks = 0, heightBlocks = 0;
	u32 width = 0, height = 0, bpp = 4;
	unsigned char *bytes = NULL;
	u32 formatFlags;
	unsigned char* buffer = NULL;	

	_width = _height = 0;
	//_internalFormat = GL_COMPRESSED_RGBA_PVRTC_4BPPV1_IMG;
//_internalFormat = ECF_PVRTC_16;
	_hasAlpha = false;


	buffer = new unsigned char[file->getSize()];
	file->read(buffer, file->getSize());

	header = (PVRTexHeader *)buffer;
	
	printf("is PVR ? \n");
	pvrTag = CFSwapInt32LittleToHost(header->pvrTag);

	if (gPVRTexIdentifier[0] != ((pvrTag >>  0) & 0xff) ||
		gPVRTexIdentifier[1] != ((pvrTag >>  8) & 0xff) ||
		gPVRTexIdentifier[2] != ((pvrTag >> 16) & 0xff) ||
		gPVRTexIdentifier[3] != ((pvrTag >> 24) & 0xff))
	{
		printf("No\n");

		return false;
	}
	printf("Yes \n");

	
	flags = CFSwapInt32LittleToHost(header->flags);
	formatFlags = flags & PVR_TEXTURE_FLAG_TYPE_MASK;
	
	unsigned char *imageData = NULL;
	u32 imageDataLength = 0;

	if (formatFlags == kPVRTextureFlagTypePVRTC_4 || formatFlags == kPVRTextureFlagTypePVRTC_2)
	{
//		[_imageData removeAllObjects];
		/*
		if (formatFlags == kPVRTextureFlagTypePVRTC_4)
			_internalFormat = GL_COMPRESSED_RGBA_PVRTC_4BPPV1_IMG;
		else if (formatFlags == kPVRTextureFlagTypePVRTC_2)
			_internalFormat = GL_COMPRESSED_RGBA_PVRTC_2BPPV1_IMG;
	*/
		_width = width = CFSwapInt32LittleToHost(header->width);
		_height = height = CFSwapInt32LittleToHost(header->height);
		
		if (CFSwapInt32LittleToHost(header->bitmaskAlpha)){
			_hasAlpha = true;
			if (formatFlags == kPVRTextureFlagTypePVRTC_4)
				_internalFormat = ECF_PVRTC_RGBA_4;
			else if (formatFlags == kPVRTextureFlagTypePVRTC_2)
				_internalFormat = ECF_PVRTC_RGBA_2;
		} else {
			_hasAlpha = false;
			if (formatFlags == kPVRTextureFlagTypePVRTC_4)
				_internalFormat = ECF_PVRTC_RGB_4;
			else if (formatFlags == kPVRTextureFlagTypePVRTC_2)
				_internalFormat = ECF_PVRTC_RGB_2;
		}
		
		dataLength = CFSwapInt32LittleToHost(header->dataLength);
		
		bytes = ((unsigned char *)buffer) + sizeof(PVRTexHeader);
		
		//TODO : unsupport more that one image
		// Calculate the data size for each texture level and respect the minimum number of blocks
		while (dataOffset < dataLength)
		{
			if (formatFlags == kPVRTextureFlagTypePVRTC_4)
			{
				blockSize = 4 * 4; // Pixel by pixel block size for 4bpp
				widthBlocks = width / 4;
				heightBlocks = height / 4;
				bpp = 4;
			}
			else
			{
				blockSize = 8 * 4; // Pixel by pixel block size for 2bpp
				widthBlocks = width / 8;
				heightBlocks = height / 4;
				bpp = 2;
			}
			
			// Clamp to minimum number of blocks
			if (widthBlocks < 2)
				widthBlocks = 2;
			if (heightBlocks < 2)
				heightBlocks = 2;

			dataSize = widthBlocks * heightBlocks * ((blockSize  * bpp) / 8);
			
//			[_imageData addObject:[NSData dataWithBytes:bytes+dataOffset length:dataSize]];
			imageData = bytes+dataOffset;
			imageDataLength = dataSize;

			dataOffset += dataSize;
			
			width = (width >> 1);
			height = (height >> 1);
		}
				  
		success = true;
	}

	if(!success) return NULL;

	// Create the image structure
	image = new CPVRImage(_internalFormat, imageDataLength, imageData, core::dimension2d<s32>(_width, _height), false);
	if (!image)
	{
		printf("LOAD PVRTC: Internal create image struct failure [%s]\n", file->getFileName());
		delete buffer;
		return 0;
	}
	delete buffer;

	return image;
}