using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace UnitEngine.Behavior
{
    /// <summary>
    /// Действия для поведения
    /// </summary>
    [Serializable]
    [XmlInclude(typeof(ExecuteCreateUnit))]
    [XmlInclude(typeof(ExecuteDeleteUnit))]
    [XmlInclude(typeof(ExecuteDeleteUnitsAll))]
    [XmlInclude(typeof(ExecuteParameter))]
    [XmlInclude(typeof(ExecuteTransform))]
    [XmlInclude(typeof(ExecuteTextures))]
    [XmlInclude(typeof(ExecuteMaterial))]
    [XmlInclude(typeof(ExecuteMoveToPoint))]
    [XmlInclude(typeof(ExecuteExtAction))]
    [XmlInclude(typeof(ExecuteDeleteSelf))]
    [XmlInclude(typeof(ExecuteAddNextAction))]
    [XmlInclude(typeof(ExecuteGroup))]
    [XmlInclude(typeof(ExecuteExtParameter))]
    [XmlInclude(typeof(ExecuteMappingTransform))]
    [XmlInclude(typeof(ExecuteChangeSceneNodeId))]
    [XmlInclude(typeof(ExecuteMoveToSceneNode))]
    [XmlInclude(typeof(ExecuteSetData))]
    [XmlInclude(typeof(ExecuteScript))]
    [XmlInclude(typeof(ExecuteRotation))]
    [XmlInclude(typeof(ExecuteSound))]
    [XmlInclude(typeof(ExecuteParticleEmitter))]
    [XmlInclude(typeof(ExecuteParticleAffector))]
    [XmlInclude(typeof(ExecuteColor))]
    [XmlInclude(typeof(ExecuteTimer))]    
    public class ExecuteBase
    {
        /// <summary>
        /// Условие выполнения
        /// </summary>
        public UnitClause Clause = new UnitClause();

        public ExecuteBase() { }

        public virtual void ToRelativePaths(string root) { }

        public virtual void ToAbsolutePaths(string root) { }

    }
}
