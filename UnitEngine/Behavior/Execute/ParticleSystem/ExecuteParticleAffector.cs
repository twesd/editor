using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace UnitEngine.Behavior
{
    /// <summary>
    /// Воздействует на частицы в системе частиц 
    /// </summary>
    [Serializable]
    public class ExecuteParticleAffector : ExecuteBase
    {
        [CategoryAttribute("Основные")]
        [DescriptionAttribute("Получить позицию из события TapScene")]
        public ParticleAffectorBase Affector { get; set; }

        public ExecuteParticleAffector()
        {
            Affector = new ParticleAffectorFadeOut();
        }

        public override string ToString()
        {
            return string.Format("ParticleAffector [{0}]", Affector);
        }
    }
}
