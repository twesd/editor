using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using IrrlichtWrap;
using IrrTools;
using CommonUI;

namespace IrrTools
{
    /// <summary>
    /// Палитра инструментов для окна просмотра
    /// </summary>
    public partial class ControlPreviewTools : UserControl
    {
        public delegate void NodeChangedEventHandler(SceneNodeW node);

        public NodeChangedEventHandler NodeChanged;

        IrrDevice _irrDevice;

        IrrDeviceW _deviceW;

        /// <summary>
        /// Панель для отображения свойств модели
        /// </summary>
        Panel _panelProperties;

        InstrumentDynamicMove _instrDynamicMove;
        
        /// <summary>
        /// Иницилизация инструментов
        /// </summary>
        /// <param name="irrDevice"></param>
        /// <param name="panelProperties">Панель для отображения свойств модели</param>
        public ControlPreviewTools(IrrDevice irrDevice, Panel panelProperties = null)
        {
            InitializeComponent();
            if (irrDevice == null) throw new NullReferenceException("irrDevice");
            _irrDevice = irrDevice;
            _deviceW = irrDevice.DeviceW;
            Panel panel = irrDevice.PreviewPanel;
            panel.PreviewKeyDown += PanelPreviewKeyDown;
            _panelProperties = panelProperties;

            _instrDynamicMove = new InstrumentDynamicMove(_irrDevice);
            _instrDynamicMove.ChangeComplete += NodeChangedDetectObj;
        }

        /// <summary>
        /// Извлечь меню инструментов
        /// </summary>
        /// <returns></returns>
        public ToolStrip ExtractToolStrip()
        {
            this.Controls.Remove(_toolStripInstruments);
            return _toolStripInstruments;
        }

        /// <summary>
        /// Обработчик клавиш
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void PanelPreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            var modifiers = System.Windows.Input.Keyboard.Modifiers;
            bool cntrlPressed = ((modifiers & System.Windows.Input.ModifierKeys.Control) != 0);

            if (cntrlPressed)
            {
                if (e.KeyCode == Keys.E) NodeEdit_Click(sender, null);
                if (e.KeyCode == Keys.M) Move_Click(sender, null);
            }
            else
            {
                if (e.KeyCode == Keys.F) ZoomToNode_Click(sender, null);
                if (e.KeyCode == Keys.W) Move_Click(sender, null);
            }
        }

        private void ZoomAll_Click(object sender, EventArgs e)
        {
            _deviceW.Camera.FullZoom();
        }

        private void ButtonClearSelection_Click(object sender, EventArgs e)
        {
            _deviceW.Selector.ClearSelection();
        }

        /// <summary>
        /// Редактирование объекта
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NodeEdit_Click(object sender, EventArgs e)
        {
            SceneNodeW node;
            if (_deviceW.Selector.SelectionResult.Count == 1)
            {
                node = _deviceW.Selector.SelectionResult[0];
            }
            else
            {
                node = _irrDevice.Selector.SelectSingleNode();
            }
            if (node == null) return;

        }

        /// <summary>
        /// Обработчик изменения модели
        /// </summary>
        /// <param name="node"></param>
        void NodeChangedDetect(SceneNodeW node)
        {
            // Передаём событие выше
            if (NodeChanged != null)
            {
                NodeChanged(node);
            }
        }

        /// <summary>
        /// Обработчик изменения модели
        /// </summary>
        /// <param name="node"></param>
        void NodeChangedDetectObj(object node)
        {
            // Передаём событие выше
            if (NodeChanged != null)
            {
                NodeChanged(node as SceneNodeW);
            }
        }

        /// <summary>
        /// Переместить выделенный объект
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Move_Click(object sender, EventArgs e)
        {             
            _instrDynamicMove.Activate();
        }

        /// <summary>
        /// Получить выбранную модель
        /// </summary>
        /// <returns></returns>
        SceneNodeW GetSelectedNode()
        {
            if (_deviceW.Selector.SelectionResult.Count == 1)
            {
                return _deviceW.Selector.SelectionResult[0];
            }
            return null;
        }

        /// <summary>
        /// Приблизиться к объекту
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ZoomToNode_Click(object sender, EventArgs e)
        {
            if (_deviceW.Selector.SelectionResult.Count == 0)
            {
                MessageBox.Show("Ничего не выбрано");
                return;
            }
            if (_deviceW.Selector.SelectionResult.Count > 1)
            {
                MessageBox.Show("Выбрано более двух объектов");
                return;
            }
            _deviceW.Camera.ZoomToNode(_deviceW.Selector.SelectionResult[0]);

        }

    }
}
