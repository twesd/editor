using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace StageEngine
{
    /// <summary>
    /// Класс для проверки данных в стадии
    /// </summary>
    public class StageValidator
    {
        /// <summary>
        /// Проверить данные
        /// </summary>
        public List<Common.Message> Validate(ContainerStage container)
        {
            var messages = new List<Common.Message>();
            return messages;
        }

        /// <summary>
        /// Исправить ошибки
        /// </summary>
        public List<Common.Message> FixErrors(ContainerStage container)
        {
            var messages = new List<Common.Message>();
            UpdatePaths(container.CacheModelPaths, messages);
            UpdatePaths(container.CacheTexturePaths, messages);
            UpdatePaths(container.CacheXmlFiles, messages);
            UpdatePaths(container.CacheScripts, messages);
            UpdatePaths(container.UnitBehaviorPaths, messages);
            return messages;
        }

        /// <summary>
        /// Удалить не существующие пути, а также дублирующиеся пути
        /// </summary>
        /// <param name="paths"></param>
        /// <param name="messages"></param>
        private void UpdatePaths(List<string> paths, List<Common.Message> messages)
        {
            if (paths == null || paths.Count == 0)
                return;
            List<string> destPaths = new List<string>();
            foreach (string path in paths)
            {
                if (destPaths.Contains(path))
                {
                    messages.Add(new Common.Message(
                        string.Format("Удалён повторяющийся путь \"{0}\"", path),
                        Common.MessageType.Info));
                }
                else if (!File.Exists(path))
                {
                    messages.Add(new Common.Message(
                        string.Format("Удалён несуществующий путь \"{0}\"", path),
                        Common.MessageType.Info));
                }
                else
                {
                    destPaths.Add(path);
                }                    
            }

            // Обновляем список
            paths.Clear();
            paths.AddRange(destPaths);
        }
    }
}
