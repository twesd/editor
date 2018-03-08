using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace StUpdater
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void origFile_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.OpenFileDialog dialog = new System.Windows.Forms.OpenFileDialog();
            dialog.Multiselect = false;
            dialog.Filter = "stage (*.stage)|*.stage";
            if (dialog.ShowDialog() != DialogResult.OK)
                return;
            _stFileName.Text = dialog.FileName;
        }

        private void btnMSelect_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.OpenFileDialog dialog = new System.Windows.Forms.OpenFileDialog();
            dialog.Multiselect = true;
            dialog.Filter = "stage (*.stage)|*.stage";
            if (dialog.ShowDialog() != DialogResult.OK)
                return;

            _dataGridView.Rows.Clear();

            foreach (string filename in dialog.FileNames)
            {
                _dataGridView.Rows.Add(new object[] { filename });
            }

        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            List<string> filenames = new List<string>();
            foreach (DataGridViewRow row in _dataGridView.Rows)
            {
                if (row.Cells[0].Value == null)
                    continue;
                string filename = row.Cells[0].Value.ToString();
                if (filename == _stFileName.Text)
                    continue;
                filenames.Add(filename);
            }

            XmlDocument origDoc = new XmlDocument();
            origDoc.Load(_stFileName.Text);

            XmlNode CacheModelPaths = origDoc.SelectSingleNode("ContainerStage/CacheModelPaths");
            XmlNode CacheTexturePaths = origDoc.SelectSingleNode("ContainerStage/CacheTexturePaths");
            XmlNode CacheXmlFiles = origDoc.SelectSingleNode("ContainerStage/CacheXmlFiles");
            XmlNode CacheScripts = origDoc.SelectSingleNode("ContainerStage/CacheScripts");

            foreach (string filename in filenames)
            {
                XmlDocument outDoc = new XmlDocument();
                outDoc.Load(filename);

                XmlNode outCacheModelPaths = outDoc.SelectSingleNode("ContainerStage/CacheModelPaths");
                CopyChilds(outCacheModelPaths, CacheModelPaths);

                XmlNode outCacheTexturePaths = outDoc.SelectSingleNode("ContainerStage/CacheTexturePaths");
                CopyChilds(outCacheTexturePaths, CacheTexturePaths);

                XmlNode outCacheXmlFiles = outDoc.SelectSingleNode("ContainerStage/CacheXmlFiles");
                CopyChilds(outCacheXmlFiles, CacheXmlFiles);

                XmlNode outCacheScripts = outDoc.SelectSingleNode("ContainerStage/CacheScripts");
                CopyChilds(outCacheScripts, CacheScripts);

                outDoc.Save(filename);
            }

            MessageBox.Show(this, "Done");
        }

        private void CopyChilds(XmlNode dest, XmlNode source)
        {
            if (dest == null)
                return;
            dest.RemoveAll();
            foreach (XmlNode node in source.ChildNodes)
            {
                XmlNode copyNode = dest.OwnerDocument.ImportNode(node, true);
                dest.AppendChild(copyNode);
            }
        }
    }
}
