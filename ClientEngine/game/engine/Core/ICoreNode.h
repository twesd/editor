#pragma once

#include "Base.h"

class ICoreNode : public Base
{
public:
	ICoreNode(SharedParams_t params) : Base(params) {
	};

	virtual void Update() = 0;
	virtual void Draw() = 0;
	virtual ~ICoreNode(){
	}
};
