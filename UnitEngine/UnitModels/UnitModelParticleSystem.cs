using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Drawing.Design;
using CommonUI.UITypeEditors;

namespace UnitEngine
{
    [Serializable]
    public class UnitModelParticleSystem : UnitModelBase
    {
        public UnitModelParticleSystem()
        {
           
        }

        public override void ToAbsolutePaths(string root)
        {
        }

        public override void ToRelativePaths(string root)
        {
        }

        public override string ToString()
        {
            return "ParticleSystem";
        }
    }
}
