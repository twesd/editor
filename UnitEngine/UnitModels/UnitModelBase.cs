using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace UnitEngine
{
    [Serializable]
    [XmlInclude(typeof(UnitModelAnim))]
    [XmlInclude(typeof(UnitModelEmpty))]    
    [XmlInclude(typeof(UnitModelBillboard))]
    [XmlInclude(typeof(UnitModelSphere))]
    [XmlInclude(typeof(UnitModelParticleSystem))]
    [XmlInclude(typeof(UnitModelVolumeLight))]       
    public abstract class UnitModelBase
    {
        public virtual void ToAbsolutePaths(string root)
        {
        }

        public virtual void ToRelativePaths(string root) 
        {
        }
    }
}
