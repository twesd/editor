using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ControlEngine;
using System.Xml.Serialization;
using System.IO;
using Serializable;

namespace Convertor
{
    public partial class FormMain : Form
    {
        enum MethodType
        {
            s960x640,
            s1136x640
        }

        public FormMain()
        {
            InitializeComponent();
        }

        private void btnMSelect_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.OpenFileDialog dialog = new System.Windows.Forms.OpenFileDialog();
            dialog.Multiselect = true;
            dialog.Filter = "controls (*.controls)|*.controls";
            if (dialog.ShowDialog() != DialogResult.OK)
                return;

            _dataGridView.Rows.Clear();

            foreach (string filename in dialog.FileNames)
            {
                _dataGridView.Rows.Add(new object[] { filename });
            }
        }

        private void btn960x640_Click(object sender, EventArgs e)
        {
            Convert(MethodType.s960x640);
        }


        private void btn1136x640_Click(object sender, EventArgs e)
        {
            Convert(MethodType.s1136x640);
        }

        /// <summary>
        /// Преобразование 1x(480x320) в заданное разрешение
        /// </summary>
        /// <param name="type"></param>
        private void Convert(MethodType type)
        {
            List<string> filenames = new List<string>();
            foreach (DataGridViewRow row in _dataGridView.Rows)
            {
                if (row.Cells[0].Value == null)
                    continue;
                string filename = row.Cells[0].Value.ToString();
                filenames.Add(filename);
            }

            foreach (string filename in filenames)
            {
                ControlPackage controlPackage = null;
                // Десерелизуем данные
                //
                XmlSerializer xmlSerelialize = new XmlSerializer(typeof(ControlPackage), GetExtraTypes());
                using (StreamReader reader = new StreamReader(filename, Encoding.UTF8))
                {
                    controlPackage = xmlSerelialize.Deserialize(reader) as ControlPackage;
                    controlPackage = controlPackage.GetWithAbsolutePaths(filename);
                }

                if (type == MethodType.s960x640)
                {
                    Execute_960x640(controlPackage.TreeView.Nodes);
                }
                else
                {
                    Execute_1136x640(controlPackage.TreeView.Nodes);
                }

                // Серелизуем данные для редактора
                //
                ControlPackage containerRelative = controlPackage.GetWithRelativePaths(filename);
                XmlSerializer xmlSerelializeDsg = new XmlSerializer(typeof(ControlPackage), GetExtraTypes());
                using (StreamWriter writer = new StreamWriter(filename, false, Encoding.UTF8))
                {
                    xmlSerelializeDsg.Serialize(writer, containerRelative);
                }
            }

            MessageBox.Show("Выполнено");
        }

        /// <summary>
        /// Преобразование 1x(480x320) в 2x(960x640) 
        /// </summary>
        /// <param name="nodes"></param>
        private void Execute_960x640(List<SerializableTreeNode> nodes)
        {
            if (nodes == null)
            {
                return;
            }

            foreach (SerializableTreeNode node in nodes)
            {
                if (node.Tag is ControlImage)
                {
                    ControlImage controlImg = node.Tag as ControlImage;
                    controlImg.Position *= 2;
                }
                else if (node.Tag is ControlButton)
                {
                    ControlButton controlBtn = node.Tag as ControlButton;
                    controlBtn.Position *= 2;
                }
                else if (node.Tag is ControlText)
                {
                    ControlText control = node.Tag as ControlText;
                    control.Position *= 2;
                    control.FontPath += "2x";
                }
                else if (node.Tag is ControlCircle)
                {
                    ControlCircle controlCircle = node.Tag as ControlCircle;
                    controlCircle.Position *= 2;
                }
                else if (node.Tag is ControlTapScene)
                {
                    ControlTapScene controlTap = node.Tag as ControlTapScene;
                    controlTap.MinPoint *= 2;
                    controlTap.MaxPoint *= 2;
                }
                else if (node.Tag is ControlPanel)
                {
                    ControlPanel control = node.Tag as ControlPanel;
                    control.Position *= 2;
                }
                else if (node.Tag is ControlBehavior ||
                    node.Tag == null)
                {
                    // nothing
                }
                else
                {
                    throw new NotSupportedException();
                }

                Execute_960x640(node.Nodes);
            }
        }

        /// <summary>
        /// Преобразование 1x(480x320) в 586h(1136x640) 
        /// </summary>
        /// <param name="nodes"></param>
        private void Execute_1136x640(List<SerializableTreeNode> nodes)
        {
            if (nodes == null)
            {
                return;
            }

            foreach (SerializableTreeNode node in nodes)
            {
                if (node.Tag is ControlImage)
                {
                    ControlImage control = node.Tag as ControlImage;
                    control.Position *= 2;
                    control.Position.X += 88;
                }
                else if (node.Tag is ControlButton)
                {
                    ControlButton control = node.Tag as ControlButton;
                    control.Position *= 2;
                    control.Position.X += 88;
                }
                else if (node.Tag is ControlText)
                {
                    ControlText control = node.Tag as ControlText;
                    control.Position *= 2;
                    control.FontPath += "2x";
                    control.Position.X += 88;
                }
                else if (node.Tag is ControlCircle)
                {
                    ControlCircle controlCircle = node.Tag as ControlCircle;
                    controlCircle.Position *= 2;
                    controlCircle.Position.X += 88;
                }
                else if (node.Tag is ControlTapScene)
                {
                    ControlTapScene control = node.Tag as ControlTapScene;
                    control.MinPoint *= 2;
                    control.MaxPoint *= 2;

                    control.MinPoint.X += 88;
                    control.MaxPoint.X += 88;
                }
                else if (node.Tag is ControlPanel)
                {
                    ControlPanel control = node.Tag as ControlPanel;
                    control.Position *= 2;
                    control.Position.X += 88;
                }
                else if (node.Tag is ControlBehavior ||
                    node.Tag == null)
                {
                    // nothing
                }
                else
                {
                    throw new NotSupportedException();
                }

                Execute_1136x640(node.Nodes);
            }
        }

        private static Type[] GetExtraTypes()
        {
            List<Type> extraTypes = new List<Type>();
            extraTypes.Add(typeof(ControlBase));
            return extraTypes.ToArray();
        }

    }
}
