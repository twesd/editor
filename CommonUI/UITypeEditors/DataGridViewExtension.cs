using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace CommonUI
{
    public class DataGridViewExtension
    {
        public delegate void PasteItemHandler(object oItem);

        /// <summary>
        /// Событие вставик объекта
        /// </summary>
        public event PasteItemHandler PasteItem;

        /// <summary>
        /// Изменение позиции перетаскиванием
        /// </summary>
        public bool EnableDragDropReorder { get; set; }

        Rectangle _dragBoxFromMouseDown;

        int _rowIndexFromMouseDown;

        DataGridView _dataGridView;

        public DataGridViewExtension(DataGridView dataGridView)
        {
            _dataGridView = dataGridView;
            _dataGridView.MouseDown += DataGridView_MouseDown;
            _dataGridView.MouseMove += DataGridView_MouseMove;
            _dataGridView.DragOver += DataGridView_DragOver;
            _dataGridView.DragDrop += DataGridView_DragDrop;
            _dataGridView.AllowDrop = true;

            _dataGridView.KeyDown += KeyDown;
        }

        void KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.C && e.Control)
            {
                if (_dataGridView.SelectedRows.Count == 0)
                {
                    return;
                }
                var item = _dataGridView.SelectedRows[0].Tag;
                if (item == null)
                {
                    return;
                }
                CopyPasteContainer container = new CopyPasteContainer(item);
                Clipboard.SetData(typeof(CopyPasteContainer).FullName, container);
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.V && e.Control)
            {
                CopyPasteContainer container = Clipboard.GetData(typeof(CopyPasteContainer).FullName) as CopyPasteContainer;
                if (container == null)
                {
                    return;
                }

                if (PasteItem != null)
                {
                    PasteItem(container.Item);
                }
                e.Handled = true;
            }
        }


        private void DataGridView_MouseMove(object sender, MouseEventArgs e)
        {
            if (EnableDragDropReorder)
            {
                if (((e.Button & MouseButtons.Left) != 0) &&
                    !_dragBoxFromMouseDown.IsEmpty &&
                    !_dragBoxFromMouseDown.Contains(e.Location))
                {
                    // Proceed with the drag and drop, passing in the list item.                    
                    DragDropEffects dropEffect = _dataGridView.DoDragDrop(
                        _dataGridView.Rows[_rowIndexFromMouseDown],
                        DragDropEffects.Move);
                }
            }
        }

        private void DataGridView_MouseDown(object sender, MouseEventArgs e)
        {
            if (EnableDragDropReorder)
            {
                if ((e.Button & MouseButtons.Left) != 0)
                {
                    // Get the index of the item the mouse is below.
                    _rowIndexFromMouseDown = _dataGridView.HitTest(e.X, e.Y).RowIndex;
                    if (_rowIndexFromMouseDown != -1)
                    {
                        // Remember the point where the mouse down occurred. 
                        // The DragSize indicates the size that the mouse can move 
                        // before a drag event should be started.                
                        Size dragSize = SystemInformation.DragSize;

                        // Create a rectangle using the DragSize, with the mouse position being
                        // at the center of the rectangle.
                        _dragBoxFromMouseDown = new Rectangle(new Point(e.X - (dragSize.Width / 2),
                                                                       e.Y - (dragSize.Height / 2)),
                                                                dragSize);

                       
                    }
                    else
                        // Reset the rectangle if the mouse is not over an item in the ListBox.
                        _dragBoxFromMouseDown = Rectangle.Empty;
                }
            }
        }

        private void DataGridView_DragOver(object sender, DragEventArgs e)
        {
            if (EnableDragDropReorder)
            {
                e.Effect = DragDropEffects.Move;
            }
        }

        private void DataGridView_DragDrop(object sender, DragEventArgs e)
        {
            if (EnableDragDropReorder)
            {
                // The mouse locations are relative to the screen, so they must be 
                // converted to client coordinates.
                Point clientPoint = _dataGridView.PointToClient(new Point(e.X, e.Y));

                // Get the row index of the item the mouse is below. 
                int rowIndexOfItemUnderMouseToDrop =
                    _dataGridView.HitTest(clientPoint.X, clientPoint.Y).RowIndex;

                if (rowIndexOfItemUnderMouseToDrop == - 1)
                    return;

                if (_dataGridView.Rows[rowIndexOfItemUnderMouseToDrop].Tag == null) 
                    return;

                // If the drag operation was a move then remove and insert the row.
                if (e.Effect == DragDropEffects.Move)
                {
                    DataGridViewRow rowToMove = e.Data.GetData(
                            typeof(DataGridViewRow)) as DataGridViewRow;                    
                    _dataGridView.Rows.RemoveAt(_rowIndexFromMouseDown);                    
                    _dataGridView.Rows.Insert(rowIndexOfItemUnderMouseToDrop, rowToMove);
                }
            }
        }

    }
}
