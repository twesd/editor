using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FastColoredTextBoxNS;
using System.Windows.Forms;

namespace CommonUI
{
    public class TextBoxAutoComplete
    {
        AutocompleteMenu _popupMenu;

        /// <summary>
        /// Включить автозаполнение для текста
        /// </summary>
        /// <param name="textBox"></param>
        /// <param name="autoCompleteWords">Дополнительные слова</param>
        public void Init(FastColoredTextBox textBox, List<string> autoCompleteWords)
        {
            _popupMenu = new AutocompleteMenu(textBox);
            _popupMenu.MinFragmentLength = 2;

            if (autoCompleteWords != null)                
                _popupMenu.Items.SetAutocompleteItems(autoCompleteWords);            
            _popupMenu.Items.MaximumSize = new System.Drawing.Size(200, 300);
            _popupMenu.Items.Width = 200;

            textBox.KeyDown += KeyDown;
        }

        private void KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == (Keys.K | Keys.Control))
            {
                //forced show (MinFragmentLength will be ignored)
                _popupMenu.Show(true);
                e.Handled = true;
            }
        }
    }
}
