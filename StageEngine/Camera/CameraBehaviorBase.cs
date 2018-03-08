using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Serialization;
using UnitEngine;
using System.ComponentModel;
using Common;
using Serializable;
using UnitEngine.Behavior;

namespace StageEngine
{
    /// <summary>
    /// Поведение камеры = базовый класс
    /// </summary>
    [Serializable]
    [XmlInclude(typeof(CameraBehaviorStatic))]
    [XmlInclude(typeof(CameraBehaviorFollowToNode))]
    public class CameraBehaviorBase
    {
        
    }
}
