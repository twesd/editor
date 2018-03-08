using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace StageEngine
{
    /// <summary>
    /// Описание создания юнита
    /// </summary>
    [Serializable]
    [XmlInclude(typeof(UnitCreationTimer))]
    [XmlInclude(typeof(UnitCreationDistance))]
    [XmlInclude(typeof(UnitCreationBBox))]
    [XmlInclude(typeof(UnitCreationGlobalParameters))]    
    public class UnitCreationBase
    {

    }
}
