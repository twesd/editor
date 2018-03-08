using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common
{
    /// <summary>
    /// Операция удаления во внешней ссылки
    /// </summary>
    [Serializable]
    public class XRefOperationDelete : XRefOperation
    {
        /// <summary>
        /// Индентификатор удалённого объекта
        /// </summary>
        public string Id { get; set; }

        public XRefOperationDelete(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentException("id");
            }
            Id = id;
        }

        /// <summary>
        /// Для Serializable
        /// </summary>
        private XRefOperationDelete() { }
    }
}
