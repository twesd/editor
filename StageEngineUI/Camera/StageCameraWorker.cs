using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Serializable;
using CommonUI;
using StageEngine;

namespace StageEngineUI.Camera
{
    class StageCameraWorker
    {
        public static UnitInstanceCamera SelectCamera(ContainerTreeView fullTreeView)
        {
            ContainerTreeView contTreeView = new ContainerTreeView();
            foreach (SerializableTreeNode node in fullTreeView.Nodes)
            {
                if (node.Tag is UnitInstanceCamera)
                {
                    contTreeView.Nodes.Add(new SerializableTreeNode()
                    {
                        Text = node.Text,
                        Tag = node.Tag
                    });
                }
            }
            FormSelectObject selectForm = new FormSelectObject(
                "Выберите камеру", contTreeView);
            if (selectForm.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                return null;
           return selectForm.Result as UnitInstanceCamera;
        }
    }
}
