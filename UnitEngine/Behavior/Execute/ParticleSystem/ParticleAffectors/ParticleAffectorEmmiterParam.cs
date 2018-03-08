using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UnitEngine.Behavior
{
    [Serializable]
    public class ParticleAffectorEmmiterParam : ParticleAffectorBase
    {
        /// <summary>
        /// Изменение параметров MinParticlesPerSecond и MaxParticlesPerSecond
        /// </summary>
        public int ParticlesPerSecond { get; set; }

        /// <summary>
        /// Время за которое должно измениться на велечину ParticlesPerSecond
        /// </summary>
        public UInt32 TimePerSecond { get; set; }

        public ParticleAffectorEmmiterParam()
        {
            ParticlesPerSecond = 0;
            TimePerSecond = 0;
        }
    }
}
