using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.ComponentModel;

namespace UnitEngine.Behavior
{
    [Serializable]
    [TypeConverter(typeof(ExpandableObjectConverter))]
    [XmlInclude(typeof(ParticleEmitterBox))]
    public abstract class ParticleEmitterBase
    {
    }
}
