using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Serialization;
using System.IO;

namespace CommonUI
{
    public class FormWorker
    {
        public static DialogResult ShowDialog(string caption, Control control, Form parent = null)
        {
            FormDialog form = new FormDialog();
            form.Size = control.Size + (new System.Drawing.Size(40, 80));
            //form.PanelContent.Size = control.Size;
            control.Dock = DockStyle.Fill;
            form.PanelContent.Controls.Add(control);
            form.Text = caption;
            form.KeyPreview = true;
            form.StartPosition = FormStartPosition.CenterParent;
            control.Focus();
            return form.ShowDialog(parent);
        }

        /// <summary>
        /// Добавить контрол
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="child"></param>
        /// <param name="fullSize">Растянуть на весь родительский контрол</param>
        public static void AddControl(Control parent, Control child, bool fullSize = true)
        {
            if (fullSize)
            {
                parent.Controls.Clear();
                child.Size = parent.Size;// - new System.Drawing.Size(10, 10);
                child.Anchor = AnchorStyles.Bottom | AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Left;
            }
            parent.Controls.Add(child);
        }

        /// <summary>
        /// Очистить все контролы
        /// </summary>
        /// <param name="mainControl"></param>
        public static void SetEmptyValueForControl(Control mainControl)
        {
            foreach (Control control in mainControl.Controls)
            {
                if (control is TextBox)
                {
                    (control as TextBox).Text = string.Empty;
                }
                else if (control is FastColoredTextBoxNS.FastColoredTextBox)
                {
                    (control as FastColoredTextBoxNS.FastColoredTextBox).Text = string.Empty;
                }
                else if (control is NumericUpDown)
                {
                    (control as NumericUpDown).Value = 0;
                }
                else if (control is DataGridView)
                {
                    ClearDataGrid(control as DataGridView);
                }
                if (control.Controls.Count > 0)
                {
                    SetEmptyValueForControl(control);
                }
            }
        }

        /// <summary>
        /// Выбрать строку в DataGridView
        /// </summary>
        public static void SelectRow(DataGridViewRow row)
        {
            DataGridView dataGrig = row.DataGridView;
            foreach (DataGridViewRow cRow in dataGrig.Rows)
            {
                cRow.Selected = false;
            }
            row.Selected = true;
        }


        /// <summary>
        /// Получить родительскую форму компонента
        /// </summary>
        /// <param name="control"></param>
        /// <returns></returns>
        public static Form GetParentForm(Control control)
        {
            if (control is Form) return control as Form;
            Control parent = control.Parent;
            while (parent != null)
            {
                Form form = parent as Form;
                if (form != null) return form;
                parent = parent.Parent;
            }
            return null;
        }

        /// <summary>
        /// Очистить DataGridView
        /// </summary>
        public static void ClearDataGrid(DataGridView dataGrid)
        {
            dataGrid.Rows.Clear();
            for (int columnIndex = 0; columnIndex < dataGrid.ColumnCount; columnIndex++)
            {
                dataGrid.Rows[0].Cells[columnIndex].Value = null;
            }
        }


        /// <summary>
        /// Очистить DataGridView
        /// </summary>
        public static void SetNullImageDataGrid(DataGridView dataGrid)
        {
            if (dataGrid == null) return;
            for (int rowIndex = 0; rowIndex < dataGrid.RowCount; rowIndex++)
            {
                for (int columnIndex = 0; columnIndex < dataGrid.ColumnCount; columnIndex++)
                {
                    if (dataGrid.Rows[rowIndex].Cells[columnIndex].ValueType == typeof(System.Drawing.Image))
                    {
                        dataGrid.Rows[rowIndex].Cells[columnIndex].Value = null;
                    }
                }
            }
        }

        public static void ShowErrorBox(string message)
        {
            MessageBox.Show(message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static void ShowErrorBox(Exception ex)
        {
            ShowErrorBox(ex.Message);
        }


        /// <summary>
        /// Сохранить положение окна
        /// </summary>
        /// <param name="fileName"></param>
        public static void LoadState(string fileName, Control control)
        {
            WindowXmlState state;
            XmlSerializer xmlSerelialize = new XmlSerializer(typeof(WindowXmlState));
            using (StreamReader reader = new StreamReader(fileName, Encoding.UTF8))
            {
                state = xmlSerelialize.Deserialize(reader) as WindowXmlState;
            }
            if (control is Form)
            {
                (control as Form).StartPosition = FormStartPosition.Manual;
            }
            control.Location = new System.Drawing.Point(state.X, state.Y);
            control.Width = state.Width;
            control.Height = state.Height;
            if (control is Form && state.IsMaximazed)
            {
                (control as Form).WindowState = FormWindowState.Maximized;
            }
        }

        /// <summary>
        /// Загрузить положение окна
        /// </summary>
        /// <param name="fileName"></param>
        public static void SaveState(string fileName, Control control)
        {
            bool isMaximazed = false;
            if (control is Form)
            {
                var winState = (control as Form).WindowState;
                if (winState == FormWindowState.Minimized)
                {
                    return;
                }

                isMaximazed = (winState == FormWindowState.Maximized);
            }

            if (control.Width != 0 && control.Height != 0)
            {
                WindowXmlState state = new WindowXmlState()
                {
                    X = control.Location.X,
                    Y = control.Location.Y,
                    Height = control.Height,
                    Width = control.Width,
                    IsMaximazed = isMaximazed
                };
                XmlSerializer xmlSerelializeDsg = new XmlSerializer(typeof(WindowXmlState));
                using (StreamWriter writer = new StreamWriter(fileName, false, Encoding.UTF8))
                {
                    xmlSerelializeDsg.Serialize(writer, state);
                }
            }
        }

        /// <summary>
        /// Поднятие текущей строчки вверх
        /// </summary>
        /// <param name="dataGrid"></param>
        public static void DataGridViewRowUp(DataGridView dataGrid)
        {
            if (dataGrid.SelectedRows.Count == 0) return;
            if (dataGrid.Rows.Count < 2) return;
            var curRow = dataGrid.SelectedRows[0];
            if (curRow.Index == 0) return;
            int index = curRow.Index;
            dataGrid.Rows.RemoveAt(index);
            dataGrid.Rows.Insert(index - 1, curRow);
        }

        /// <summary>
        /// Опускание текущей строчки вниз
        /// </summary>
        /// <param name="dataGrid"></param>
        public static void DataGridViewRowDown(DataGridView dataGrid)
        {
            if (dataGrid.SelectedRows.Count == 0) return;
            if (dataGrid.Rows.Count < 2) return;
            var curRow = dataGrid.SelectedRows[0];
            if (curRow.Index == dataGrid.Rows.Count - 1) return;
            int index = curRow.Index;
            dataGrid.Rows.RemoveAt(index);
            dataGrid.Rows.Insert(index + 1);
        }
    }
}
