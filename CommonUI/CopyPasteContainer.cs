using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommonUI
{
    [Serializable]
    public class CopyPasteContainer
    {
        public object Item { get; set; }

        public CopyPasteContainer(object oItem)
        {
            if (oItem == null)
            {
                throw new ArgumentNullException();
            }
            if (!oItem.GetType().IsSerializable)
            {
                throw new Exception("Тип должен быть сереализуем " + oItem.GetType());
            }

            Item = oItem;
        }

        private CopyPasteContainer()
        {
        }
    }
}
