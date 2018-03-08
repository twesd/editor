using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace IrrlichtWrap
{
    public class IrrDevice : IDisposable
    {
        public IrrDeviceW DeviceW;

        public Panel PreviewPanel
        {
            get
            {
                return _panel;
            }
        }

        /// <summary>
        /// Класс для работы с выборкой
        /// </summary>
        public IrrSelector Selector;

        /// <summary>
        /// Класс для работы с камерой
        /// </summary>
        public IrrCamera Camera;

        /// <summary>
        /// Класс для редоктирования моделей
        /// </summary>
        public IrrEditor Editor;

        object _oLock = new object();

        /// <summary>
        /// Заокончена ли 
        /// </summary>
        bool _drawTaskDone = true;

        /// <summary>
        /// Поток отрисовки
        /// </summary>
        System.Windows.Forms.Timer _timer = new System.Windows.Forms.Timer();

        /// <summary>
        /// Панель на которой рисуется графика
        /// </summary>
        Panel _panel;

        public IrrDevice(Panel panel)
        {
            _panel = panel;

            DeviceW = new IrrDeviceW(_panel.Handle.ToInt32());

            Selector = new IrrSelector(this);
            Selector.EnableUserSelection = true;
            Selector.SelectionChanged += SelectionChanged;

            Camera = new IrrCamera(this);

            Editor = new IrrEditor(this);

            // Иницилизация таймера
            //
            _timer.Enabled = false;
            _timer.Interval = 20;
            _timer.Tick += new EventHandler(DrawTask);

            _panel.Resize += new EventHandler(Panel_Resize);

            StartDrawing();
        }

        public void Dispose()
        {
            lock (_oLock)
            {
                if (DeviceW != null)
                {
                    StopDrawing();
                    DeviceW.Close();
                    DeviceW = null;
                }
            }
        }

        /// <summary>
        /// Получить объект блокировки
        /// </summary>
        /// <returns></returns>
        public object GetLock()
        {
            return _oLock;
        }


        void Panel_Resize(object sender, EventArgs e)
        {
            DeviceW.ResizeScreen(_panel.Width, _panel.Height);
        }

        void SelectionChanged(SceneNodeW sceneNode)
        {
            if (sceneNode != null)
            {
                BoundboxW bbox = sceneNode.GetBoundBox();
                Common.Geometry.Vertex vertMax = new Common.Geometry.Vertex(
                    bbox.MaxPoint.X, bbox.MaxPoint.Y, bbox.MaxPoint.Z);
                Common.Geometry.Vertex vertMin = new Common.Geometry.Vertex(
                    bbox.MinPoint.X, bbox.MinPoint.Y, bbox.MinPoint.Z);
                float dist = vertMax.GetDistanceFrom(vertMin);
                Camera.Speed = dist / 50;
                if (Camera.Speed < 1)
                {
                    Camera.Speed = 1;
                }
            }
            else
            {
                Camera.Speed = 1;
            }
        }

        /// <summary>
        /// Задача прорисовки
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void DrawTask(object sender, EventArgs e)
        {
            if (!_drawTaskDone)
            {
                return;
            }
            _drawTaskDone = false;
            try
            {
                bool isLock = Monitor.TryEnter(_oLock, 10);
                if (!isLock)
                {
                    return;
                }

                if (DeviceW != null && !_panel.IsDisposed)
                {
                    DeviceW.DrawAll();
                }

                Monitor.Exit(_oLock);
            }
            finally
            {
                _drawTaskDone = true;
            }
        }

        /// <summary>
        /// Начать отрисовку
        /// </summary>
        public void StartDrawing()
        {
            lock (_oLock)
            {
                _timer.Start();
            }
        }

        /// <summary>
        /// Завершить отрисовку
        /// </summary>
        public void StopDrawing()
        {
            int counter = 0;
            _timer.Stop();
            while (!_drawTaskDone)
            {
                Thread.Sleep(50);
                Application.DoEvents();
                counter++;
                // TODO : fix me
                if (counter > 10)
                {
                    break;
                }
            }
        }

    }
}
