#include "PolylineSceneNode.h"
#include "Core/NodeWorker.h"

PolylineSceneNode::PolylineSceneNode(SharedParams_t params) : 
	Base(params), 
	ISceneNode(params.SceneManager->getRootSceneNode(), params.SceneManager),
	_points(),
	_box(),
	_mesh(),
	_color(255, 255, 0, 0),
	_vertexColor(255, 0, 255, 0)
{
	IsVisible = true;
	_vertexVisible = true;
	_width = 0.5f;

	const u16 u[36] = {   0,2,1,   0,3,2,   1,5,4,   1,2,5,   4,6,7,   4,5,6, 
		7,3,0,   7,6,3,   9,5,2,   9,8,5,   0,11,10,   0,10,7};

	SMeshBuffer* buf = new SMeshBuffer();
	_mesh.addMeshBuffer(buf);
	buf->Indices.set_used(36);
	for (u32 i=0; i<36; ++i)
		buf->Indices[i] = u[i];	
	Resize(buf, _width);
	buf->drop();

	NodeWorker::ApplyMaterialSetting(&_material);
}

PolylineSceneNode::~PolylineSceneNode(void)
{

}


video::SMaterial& PolylineSceneNode::getMaterial(u32 i)
{
	return _material;
}


//! returns amount of materials used by this scene node.
u32 PolylineSceneNode::getMaterialCount() const
{
	return 1;
}

//! pre render event
void PolylineSceneNode::OnRegisterSceneNode()
{
	if (IsVisible)
		ISceneNode::SceneManager->registerNodeForRendering(this);

	ISceneNode::OnRegisterSceneNode();
}

//! render
void PolylineSceneNode::render()
{
	if(!IsVisible || _points.size() < 1) return;

	Driver->setMaterial(_material);

	core::matrix4 mat;

	for (u32 i = 1; i < _points.size() ; i++)
	{
		vector3df vec = _points[i] - _points[i-1];
		vector3df horzAngle = vec.getHorizontalAngle();

		mat.setTranslation((_points[i] + _points[i-1]) / 2);
		mat.setRotationDegrees(horzAngle);

		core::matrix4 smat;
		smat.setScale(vector3df(1, 1, vec.getLength() * (1 / _width)));
		mat *= smat;
					
		Driver->setMaterial(_material);			

		// Рисуем линии
		//
		Driver->setTransform(video::ETS_WORLD, mat);
		SetVertexColors(&_mesh, _color);
		for ( u32 a = 0; a != _mesh.getMeshBufferCount(); ++a )
		{
			Driver->drawMeshBuffer(_mesh.getMeshBuffer(a));
		}

		if(_vertexVisible)
		{
			// Рисуем базовые точки
			//
			mat.makeIdentity();
			vector3df t1 = _points[i];
			mat.setTranslation(_points[i]);
			mat.setScale(_width*6);
			Driver->setTransform(video::ETS_WORLD, mat);
			SetVertexColors(&_mesh, _vertexColor);
			for ( u32 a = 0; a != _mesh.getMeshBufferCount(); ++a )
			{			
				Driver->drawMeshBuffer(_mesh.getMeshBuffer(a));
			}
		}
	}
}

//! returns the axis aligned bounding box of this node
const core::aabbox3d<f32>& PolylineSceneNode::getBoundingBox() const
{
	return _box;
}

void PolylineSceneNode::Clear()
{
	_points.clear();
}

void PolylineSceneNode::AddPoint( vector3df point )
{
	_points.push_back(point);
}


//! Sets the colors of all vertices to one color
void PolylineSceneNode::SetVertexColors(IMesh* mesh, video::SColor color)
{
	if (!mesh)
		return;

	const u32 bcount = mesh->getMeshBufferCount();
	for (u32 b=0; b<bcount; ++b)
	{
		IMeshBuffer* buffer = mesh->getMeshBuffer(b);
		void* v = buffer->getVertices();
		const u32 vtxcnt = buffer->getVertexCount();
		u32 i;

		switch(buffer->getVertexType())
		{
		case video::EVT_STANDARD:
			{
				for ( i=0; i<vtxcnt; ++i)
					((video::S3DVertex*)v)[i].Color = color;
			}
			break;
		case video::EVT_2TCOORDS:
			{
				for ( i=0; i<vtxcnt; ++i)
					((video::S3DVertex2TCoords*)v)[i].Color = color;
			}
			break;
		case video::EVT_TANGENTS:
			{
				for ( i=0; i<vtxcnt; ++i)
					((video::S3DVertexTangents*)v)[i].Color = color;
			}
			break;
		}
	}
}

void PolylineSceneNode::Resize( SMeshBuffer* buf, f32 size )
{
	// we are creating the cube mesh here. 

	video::SColor clr(255,255,0,0);

	buf->Vertices.reallocate(12);
	// Start setting vertices from index 0 to deal with this method being called multiple times.
	buf->Vertices.set_used(0);
	buf->Vertices.push_back(video::S3DVertex(0,0,0, -1,-1,-1, clr, 0, 1));
	buf->Vertices.push_back(video::S3DVertex(1,0,0,  1,-1,-1, clr, 1, 1));
	buf->Vertices.push_back(video::S3DVertex(1,1,0,  1, 1,-1, clr, 1, 0));
	buf->Vertices.push_back(video::S3DVertex(0,1,0, -1, 1,-1, clr, 0, 0));
	buf->Vertices.push_back(video::S3DVertex(1,0,1,  1,-1, 1, clr, 0, 1));
	buf->Vertices.push_back(video::S3DVertex(1,1,1,  1, 1, 1, clr, 0, 0));
	buf->Vertices.push_back(video::S3DVertex(0,1,1, -1, 1, 1, clr, 1, 0));
	buf->Vertices.push_back(video::S3DVertex(0,0,1, -1,-1, 1, clr, 1, 1));
	buf->Vertices.push_back(video::S3DVertex(0,1,1, -1, 1, 1, clr, 0, 1));
	buf->Vertices.push_back(video::S3DVertex(0,1,0, -1, 1,-1, clr, 1, 1));
	buf->Vertices.push_back(video::S3DVertex(1,0,1,  1,-1, 1, clr, 1, 0));
	buf->Vertices.push_back(video::S3DVertex(1,0,0,  1,-1,-1, clr, 0, 0));

	buf->BoundingBox.reset(0,0,0); 

	for (u32 i=0; i<12; ++i)
	{
		buf->Vertices[i].Pos -= core::vector3df(0.5f, 0.5f, 0.5f);
		buf->Vertices[i].Pos *= size;
		buf->BoundingBox.addInternalPoint(buf->Vertices[i].Pos);
	}
}

// Установка ширины линии
void PolylineSceneNode::SetWidth( f32 width )
{
	_DEBUG_BREAK_IF(width <= 0)
	_width = width;
	Resize((SMeshBuffer*)_mesh.getMeshBuffer(0), _width);
}

void PolylineSceneNode::SetVertexVisible( bool visible )
{
	_vertexVisible = visible;
}

void PolylineSceneNode::SetColor( video::SColor& color )
{
	_color = color;
}

void PolylineSceneNode::SetVertexColor( video::SColor& color )
{
	_vertexColor = color;
}
