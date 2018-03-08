using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Serialization;
using System.IO;

namespace CommonUI
{
    public class FormKeysWorker
    {
        /// <summary>
        /// Функция обработки на комбинацию клавиш
        /// </summary>
        public delegate void FormKeyEventHandler();

        /// <summary>
        /// Функция обработки сохранения данных
        /// </summary>
        /// <returns></returns>
        public delegate bool FormSaveEventHandler();

        /// <summary>
        /// Форма
        /// </summary>
        Control _control;

        /// <summary>
        /// Событие по нажатию Enter
        /// </summary>
        FormKeyEventHandler _applyKeyDown;

        /// <summary>
        /// Событие по нажатию Cntrl + O
        /// </summary>
        FormKeyEventHandler _openKeyDown;

        /// <summary>
        /// Событие по нажатию Cntrl + N
        /// </summary>
        FormKeyEventHandler _newKeyDown;

        /// <summary>
        /// Событие по нажатию Cntrl + S
        /// </summary>
        FormSaveEventHandler _saveKeyDown;

        public FormKeysWorker(Control form)
        {
            _control = form;            
        }

        /// <summary>
        /// Обработка открытия / создания / сохранения файла
        /// </summary>
        public void KeyFileEvents(FormKeyEventHandler newFileFunc, FormKeyEventHandler openFileFunc, FormSaveEventHandler saveFileFunc)
        {
            _control.KeyDown += OpenKeyDown;
            foreach (Control control in _control.Controls)
            {
                control.KeyDown += OpenKeyDown;
            }
            _newKeyDown = newFileFunc;
            _openKeyDown = openFileFunc;
            _saveKeyDown = saveFileFunc;
        }

        void OpenKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.O && e.Control && _openKeyDown != null)
                _openKeyDown();
            if (e.KeyCode == Keys.N && e.Control && _newKeyDown != null)
                _newKeyDown();
            if (e.KeyCode == Keys.S && e.Control && _saveKeyDown != null)
                _saveKeyDown();
            if (e.KeyCode == Keys.W && e.Control)
            {
                var form = FormWorker.GetParentForm(_control);
                form.Close();
            }
        }

        /// <summary>
        /// Обработка Esc / Enter
        /// </summary>
        /// <param name="form"></param>
        /// <param name="apply"></param>
        public void EscEnterEvent(FormKeyEventHandler applyFunc)
        {            
            _control.KeyDown += EscEnterKeyDown;
            foreach (Control control in _control.Controls)
            {
                control.KeyDown += EscEnterKeyDown; 
            }
            _applyKeyDown = applyFunc;
        }

        void EscEnterKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                var form = FormWorker.GetParentForm(_control);
                form.Close();
            }
            if (e.KeyCode == Keys.Enter)
            {
                if (_applyKeyDown != null)
                    _applyKeyDown();
            }
        }

    }
}
