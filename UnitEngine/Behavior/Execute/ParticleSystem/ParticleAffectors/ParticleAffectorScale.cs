using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using Common.Geometry;

namespace UnitEngine.Behavior
{
    [Serializable]
    public class ParticleAffectorScale : ParticleAffectorBase
    {
        public List<ParticleScaleItem> Items { get; set; }

        public bool Loop { get; set; }

        public ParticleAffectorScale()
        {
            Items = new List<ParticleScaleItem>();
        }
    }
}
