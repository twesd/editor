using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using Common.Geometry;

namespace UnitEngine.Behavior
{
    [Serializable]
    public class ParticleAffectorRotation : ParticleAffectorBase
    {
        public Vertex Position { get; set; }

        public Vertex Speed { get; set; }

        public ParticleAffectorRotation()
        {
            Position = new Vertex();
            Speed = new Vertex(1, 1, 1);
        }
    }
}
