using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;

namespace UnitEngine.Behavior
{
    [Serializable]
    public class ParticleAffectorFadeOut : ParticleAffectorBase
    {
        public List<ParticleFadeOutItem> Items { get; set; }

        public bool Loop { get; set; }

        public ParticleAffectorFadeOut()
        {
            Items = new List<ParticleFadeOutItem>();
            Loop = false;
        }
    }
}
