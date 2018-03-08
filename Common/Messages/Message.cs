using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common
{
    /// <summary>
    /// Сообщение
    /// </summary>
    public class Message
    {
        /// <summary>
        /// Текст сообщения
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Тип сообщения
        /// </summary>
        public MessageType MessageType { get; set; }

        /// <summary>
        /// Индентификатор объекта, которому относится ошибка
        /// </summary>
        public int EnityId { get; set; }

        public Message()
        {
        }

        public Message(string text, MessageType type = MessageType.Error, int entId = 0)
        {
            Text = text;
            MessageType = type;
            EnityId = entId;
        }
    }
}
