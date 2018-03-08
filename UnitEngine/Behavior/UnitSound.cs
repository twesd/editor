using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UnitEngine
{
    [Serializable]
    public class UnitSound
    {
        /// <summary>
        /// Наименование
        /// </summary>
        public string Name;

        /// <summary>
        /// Путь до CAF файла (OpenAL)
        /// </summary>
        public string FilenameCaf;

        /// <summary>
        /// Путь до Wav файла (Windows - PCM)
        /// </summary>
        public string FilenameWav;

        /// <summary>
        /// Иницилизация
        /// </summary>
        /// <param name="name">Наименование</param>
        /// <param name="filenameWav">Путь до Wav файла (Windows - PCM)</param>
        /// <param name="filenameCaf">Путь до CAF файла (OpenAL)</param>
        public UnitSound(string name, string filenameWav, string filenameCaf) 
        {
            Name = name;
            FilenameWav = filenameWav;
            FilenameCaf = filenameCaf;
        }

        /// <summary>
        /// Иницилизация (Для Serializable)
        /// </summary>
        public UnitSound() { }
    }
}
