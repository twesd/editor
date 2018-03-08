using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace PublishData
{
    [Serializable]
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class FolderSettings
    {
        [Category("Основные"), Description("Удалить временные файлы (.bk, .bpng и т.д.)")]
        public bool DeleteBackFiles { get; set; }

        [Category("Основные"), Description("Повернуть изображения на 90 градусов")]
        public bool RotateImages { get; set; }

        public FolderSettings()
        {
            DeleteBackFiles = true;
            RotateImages = false;
        }
    }
}
