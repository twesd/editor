#include "Billboard.h"
#include "Core/NodeWorker.h"

Billboard::Billboard(SharedParams_t params) : 
	Base(params), ISceneNode(params.SceneManager->getRootSceneNode(), params.SceneManager) 
{
	IsVisible = true;
	_useUpVector = false;	
	_useViewVector = false;

	_indices[0] = 0;
	_indices[1] = 2;
	_indices[2] = 1;
	_indices[3] = 0;
	_indices[4] = 3;
	_indices[5] = 2;

	_indices[6] = 1;
	_indices[7] = 2;
	_indices[8] = 0;
	_indices[9] = 2;
	_indices[10] = 3;
	_indices[11] = 0;

	_color = 0xFFFFFFFF;

	_vertices[0].TCoords.set(1.0f, 1.0f);
	_vertices[0].Color = _color;

	_vertices[1].TCoords.set(1.0f, 0.0f);
	_vertices[1].Color = _color;

	_vertices[2].TCoords.set(0.0f, 0.0f);
	_vertices[2].Color = _color;

	_vertices[3].TCoords.set(0.0f, 1.0f);
	_vertices[3].Color = _color;

	for (s32 i=0; i<4; ++i)
		_vertices[i].Normal = vector3df(0,-1,0);

	NodeWorker::ApplyMaterialSetting(&_material);

	RelativeScale = vector3df(1,1,1);

	_upVector = vector3df(0, 1, 0);
}

Billboard::~Billboard(void)
{

}


void Billboard::SetDimension(dimension2df dimension)
{
	_dimension = dimension;
}


video::SMaterial& Billboard::getMaterial(u32 i)
{
	return _material;
}


//! returns amount of materials used by this scene node.
u32 Billboard::getMaterialCount() const
{
	return 1;
}

//! pre render event
void Billboard::OnRegisterSceneNode()
{
	if (IsVisible)
		ISceneNode::SceneManager->registerNodeForRendering(this);

	ISceneNode::OnRegisterSceneNode();
}

//! render
void Billboard::render()
{
	if(!IsVisible) return;

	ICameraSceneNode* camera = Base::SceneManager->getActiveCamera();

	if (!camera) return;

	// make billboard look to camera

	core::vector3df pos = getAbsolutePosition();

	core::vector3df campos = camera->getAbsolutePosition();
	core::vector3df target = camera->getTarget();
	core::vector3df up = (_useUpVector) ? _upVector : camera->getUpVector();
	core::vector3df view;
	if(_useViewVector) 
	{
		view = _viewVector;
	}
	else
	{
		view = target - campos;
	}
	view.normalize();

	core::vector3df horizontal = up.crossProduct(view);
	if ( horizontal.getLength() == 0 )
	{
		horizontal.set(up.Y,up.X,up.Z);
	}
	horizontal.normalize();
	horizontal *= 0.5f * _dimension.Width * RelativeScale.X;

	core::vector3df vertical = horizontal.crossProduct(view);
	vertical.normalize();
	vertical *= 0.5f * _dimension.Height * RelativeScale.Y;

	//view *= -1.0f;
	//for (s32 i=0; i<4; ++i)
	//	_vertices[i].Normal = view;

	_vertices[0].Pos = horizontal + vertical;
	_vertices[1].Pos = horizontal - vertical;
	_vertices[2].Pos = -horizontal - vertical;
	_vertices[3].Pos = -horizontal + vertical;
	
	if(_useViewVector) 
	{
		core::matrix4 mat;
		mat.setRotationDegrees(RelativeRotation);
		mat.transformVect(_vertices[0].Pos);
		mat.transformVect(_vertices[1].Pos);
		mat.transformVect(_vertices[2].Pos);
		mat.transformVect(_vertices[3].Pos);
	}

	_vertices[0].Pos += pos;
	_vertices[1].Pos += pos;
	_vertices[2].Pos += pos;
	_vertices[3].Pos += pos;

	Driver->setTransform(video::ETS_WORLD, core::IdentityMatrix);
	Driver->setMaterial(_material);
	Driver->drawIndexedTriangleList(_vertices, 4, _indices, 4);
}

//! returns the axis aligned bounding box of this node
const core::aabbox3d<f32>& Billboard::getBoundingBox() const
{
	return _boundBox;
}

void Billboard::SetUpVector( vector3df up )
{
	_upVector = up;
}

void Billboard::SetUseUpVector( bool useIt )
{
	_useUpVector = useIt;
}

void Billboard::SetUseViewVector( bool useIt )
{
	_useViewVector = useIt;
}

void Billboard::SetViewVector( vector3df vec )
{
	_viewVector = vec;
}

video::SColor Billboard::GetColor() const
{
	return _color;
}

void Billboard::SetColor( const video::SColor& color )
{
	_color = color;
	for (s32 i=0; i<4; ++i)
		_vertices[i].Color = _color;
}







