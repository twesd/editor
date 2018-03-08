using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommonUI;
using System.Windows.Forms;

namespace IrrlichtWrap
{
    /// <summary>
    /// Класс для работы с выборкой объектов
    /// </summary>
    public class IrrSelector
    {
        public delegate void SelectonChangedHandler(SceneNodeW sceneNode);

        /// <summary>
        /// Разрешить выделение объектов пользователем
        /// </summary>
        public bool EnableUserSelection
        {
            set
            {
                if (value)
                {
                    _irrDevice.PreviewPanel.MouseClick += PreviewPanelMouseDownClick;
                }
                else
                {
                    _irrDevice.PreviewPanel.MouseDown -= PreviewPanelMouseDownClick;
                }
            }
        }


        /// <summary>
        /// Количество выбранных объектов
        /// </summary>
        public int SelectionCount
        {
            get
            {
                return _deviceW.Selector.SelectionResult.Count;
            }
        }

        /// <summary>
        /// Выбранные объекты
        /// </summary>
        public List<SceneNodeW> SelectionResult
        {
            get
            {
                return _deviceW.Selector.SelectionResult;
            }
        }

        public event SelectonChangedHandler SelectionChanged;

        IrrDevice _irrDevice;

        IrrDeviceW _deviceW;

        /// <summary>
        /// Флаг отмены операции
        /// </summary>
        bool _aborted;


        /// <summary>
        /// Иницилизация
        /// </summary>
        /// <param name="irrDevice"></param>
        public IrrSelector(IrrDevice irrDevice)
        {
            if (irrDevice == null) throw new NullReferenceException("irrDevice");
            _irrDevice = irrDevice;
            _deviceW = irrDevice.DeviceW;

            Panel panel = _irrDevice.PreviewPanel;
            panel.PreviewKeyDown += PreviewKeyDown;
        }

        /// <summary>
        /// Выбрать один объект
        /// </summary>
        /// <returns></returns>
        public SceneNodeW SelectSingleNode(string message = null)
        {
            if (message == null) message = "Выбирете объект:";

            _aborted = false;
            Panel panel = _irrDevice.PreviewPanel;
            _deviceW.Selector.ClearSelection();
            using (ToolTipMouse mouseTooltip = new ToolTipMouse(message, panel))
            {
                while (true)
                {
                    Application.DoEvents();
                    if (_aborted) return null;
                    if (panel.IsDisposed) return null;
                    if (_deviceW.Selector.SelectionResult.Count > 0)
                    {
                        return _deviceW.Selector.SelectionResult[0];
                    }
                }
            }
        }

        /// <summary>
        /// Очистка выборки
        /// </summary>
        public void ClearSelection()
        {
            _irrDevice.DeviceW.Selector.ClearSelection();
            if (SelectionChanged != null)
            {
                SelectionChanged(null);
            }
        }

        /// <summary>
        /// Выборка модели
        /// </summary>
        /// <param name="node"></param>
        public void SetSelection(SceneNodeW node)
        {
            if (node == null)
            {
                return;
            }
            _irrDevice.DeviceW.Selector.SelectNode(node);
            if (SelectionChanged != null)
            {
                SelectionChanged(node);
            }
        }
        
        void PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                _aborted = true;
            }
        }

        void PreviewPanelMouseDownClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            var modifiers = System.Windows.Input.Keyboard.Modifiers;
            bool shiftPressed = ((modifiers & System.Windows.Input.ModifierKeys.Shift) != 0);
            bool nothingPressed = (modifiers == 0);

            // Если нажата левая клавиша мыши, то
            if (((e.Button & System.Windows.Forms.MouseButtons.Left) != 0) && nothingPressed)
            {
                _deviceW.Selector.ClearSelection();
                SceneNodeW node = _deviceW.Selector.SelectNode(e.X, e.Y);
                if (SelectionChanged != null)
                {
                    SelectionChanged(node);
                }
            }

        }

    }
}
