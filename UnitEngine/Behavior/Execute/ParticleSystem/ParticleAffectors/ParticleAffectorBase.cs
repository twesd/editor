using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace UnitEngine.Behavior
{
    [Serializable]
    [XmlInclude(typeof(ParticleAffectorFadeOut))]
    [XmlInclude(typeof(ParticleAffectorAllDirection))]
    [XmlInclude(typeof(ParticleAffectorEmmiterParam))]
    [XmlInclude(typeof(ParticleAffectorRotation))]
    [XmlInclude(typeof(ParticleAffectorScale))]  
    public abstract class ParticleAffectorBase
    {
    }
}
