using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;

namespace UnitEngine
{
    [Serializable]
    public class UnitModelVolumeLight : UnitModelBase
    {
        public UInt32 SubdivU { get; set; }

        public UInt32 SubdivV { get; set; }

        public SColor Foot { get; set; }

        public SColor Tail { get; set; }

        public UnitModelVolumeLight()
        {
            SubdivU = 32;
            SubdivV = 32;
            Foot = new SColor(255, 255, 255, 255);
            Tail = new SColor(255, 255, 255, 255);
        }
        public override string ToString()
        {
            return "VolumeLight";
        }
    }
}
