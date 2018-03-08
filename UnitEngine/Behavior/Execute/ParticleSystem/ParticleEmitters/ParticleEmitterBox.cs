using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.Geometry;
using Common;
using System.ComponentModel;

namespace UnitEngine.Behavior
{
    [Serializable]    
    public class ParticleEmitterBox : ParticleEmitterBase
    {
        [CategoryAttribute("Основные")]
        [DescriptionAttribute("The box for the emitter.")]
        public Boundbox Box { get; set; }

        [CategoryAttribute("Основные")]
        [DescriptionAttribute(" Direction and speed of particle emission.")]
        public Vertex Direction { get; set; }

        [CategoryAttribute("Количество")]
        [DescriptionAttribute("Minimal amount of particles emitted per second.")]
        public UInt32 MinParticlesPerSecond { get; set; }

        [CategoryAttribute("Количество")]
        [DescriptionAttribute("Maximal amount of particles emitted per second")]
        public UInt32 MaxParticlesPerSecond { get; set; }

        [CategoryAttribute("Цвет")]
        [DescriptionAttribute("Minimal initial start color of a particle. ")]
        public SColor MinStartColor { get; set; }

        [CategoryAttribute("Цвет")]
        [DescriptionAttribute("Minimal initial start color of a particle.  ")]
        public SColor MaxStartColor { get; set; }

        [CategoryAttribute("Время")]
        [DescriptionAttribute("Minimal lifetime of a particle, in milliseconds. ")]
        public UInt32 LifeTimeMin { get; set; }

        [CategoryAttribute("Время")]
        [DescriptionAttribute("Maximal lifetime of a particle, in milliseconds.")]
        public UInt32 LifeTimeMax { get; set; }

        [CategoryAttribute("Время")]
        [DescriptionAttribute("Maximal angle in degrees, the emitting direction of the particle will differ from the original direction.")]
        public int MaxAngleDegrees { get; set; }

        [CategoryAttribute("Размер")]
        [DescriptionAttribute("Minimal initial start size of a particle")]
        public Dimension MinStartSize { get; set; }

        [CategoryAttribute("Размер")]
        [DescriptionAttribute("Maximal initial start size of a particle")]
        public Dimension MaxStartSize { get; set; }

        [CategoryAttribute("Основные")]
        [DescriptionAttribute("Учитывать поворот родителя")]
        public bool UseParentRotation { get; set; }

        public ParticleEmitterBox()
        {
            Box = new Boundbox(new Vertex(0, 0, 0), new Vertex(1, 1, 1));
            Direction = new Vertex(0.0f, 0.5f, 0.0f);
            MinParticlesPerSecond = 30;
            MaxParticlesPerSecond = 50;
            MinStartColor = new SColor(255, 255, 255, 255);
            MaxStartColor = new SColor(255, 255, 255, 255);
            LifeTimeMin = 800;
            LifeTimeMax = 2000;
            MaxAngleDegrees = 0;
            MinStartSize = new Dimension(5,5);
            MaxStartSize = new Dimension(5,5);
            UseParentRotation = false;
        }
    }
}
