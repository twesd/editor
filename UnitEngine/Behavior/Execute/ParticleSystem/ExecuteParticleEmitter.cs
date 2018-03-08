using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace UnitEngine.Behavior
{
    /// <summary>
    /// Создаёт частицы в системе частиц
    /// </summary>
    [Serializable]
    public class ExecuteParticleEmitter : ExecuteBase
    {
        public ParticleEmitterBase Emitter { get; set; }

        public ExecuteParticleEmitter()
        {
            Emitter = new ParticleEmitterBox();
        }

        public override string ToString()
        {
            return string.Format("ParticleEmitter");
        }
    }
}
