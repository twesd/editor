using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PublishData.Actions
{
    interface IAction
    {
        void Execute(ProjectData projectData);
    }
}
