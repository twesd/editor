#pragma once
#include "../Base.h"

class PVRTextureLoader : public video::IImageLoader
{
public:
	PVRTextureLoader();
	virtual ~PVRTextureLoader(void);

	//! Check if the file might be loaded by this class
	/** Check is based on the file extension (e.g. ".tga")
	\param fileName Name of file to check.
	\return True if file seems to be loadable. */
	virtual bool isALoadableFileExtension(const c8* fileName) const;

	//! Check if the file might be loaded by this class
	/** Check might look into the file.
	\param file File handle to check.
	\return True if file seems to be loadable. */
	virtual bool isALoadableFileFormat(io::IReadFile* file) const;

	//! Creates a surface from the file
	/** \param file File handle to check.
	\return Pointer to newly created image, or 0 upon error. */
	virtual video::IImage* loadImage(io::IReadFile* file) const;

private:
	u32 CFSwapInt32(u32 arg) const;
	//Converts a 32-bit integer from little-endian format to the host’s native byte order.
	u32 CFSwapInt32LittleToHost(u32 arg) const;
	
	//GLuint _name;
	
	//GLenum _internalFormat;
	//BOOL _hasAlpha;

};
