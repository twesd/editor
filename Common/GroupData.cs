using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Common
{
    /// <summary>
    /// Доп. информация о группе
    /// </summary>
    [Serializable]
    public class GroupData : ISupportId
    {
        /// <summary>
        /// Индентификатор группы
        /// </summary>        
        [ReadOnlyAttribute(true)]
        public string Id { get; set; }

        public GroupData()
        {
            Id = Guid.NewGuid().ToString("B").ToUpper();
        }
    }
}
