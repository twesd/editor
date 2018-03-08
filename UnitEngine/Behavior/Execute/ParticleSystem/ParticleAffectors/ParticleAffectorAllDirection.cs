using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.Geometry;

namespace UnitEngine.Behavior
{
    [Serializable]
    public class ParticleAffectorAllDirection : ParticleAffectorBase
    {
        public Vertex Position { get; set; }

        public float Speed { get; set; }

        public ParticleAffectorAllDirection()
        {
            Position = new Vertex();
            Speed = 1;
        }
    }
}
