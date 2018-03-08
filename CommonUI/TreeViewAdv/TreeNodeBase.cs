using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using Aga.Controls.Tree;
using System.Windows.Forms;
using System.Drawing;
using TransactionCore;
using System.Runtime.Serialization;

namespace CommonUI
{
    [Serializable]
    public class TreeNodeBase
    {
        #region NodeCollection

        [Serializable]
        private class NodeCollection : Collection<TreeNodeBase>
        {
            [NonSerialized]
            private TransactionManager _transManager;

            private TreeNodeBase _owner;

            [IgnoreDataMember]
            public TransactionManager TransactionManager
            {
                get
                {
                    return _transManager;
                }
                set
                {
                    _transManager = value;
                    foreach (TreeNodeBase node in this)
                    {
                        node.TransactionManager = value;
                    }
                }
            }

            public NodeCollection(TreeNodeBase owner, TransactionManager transManager = null)
            {
                _owner = owner;
                _transManager = transManager;
            }

            protected override void ClearItems()
            {
                if (this.Count == 0)
                {
                    return;
                }

                Transaction trans = null;
                if (_transManager != null && _transManager.EnableTansactions)
                {
                    trans = _transManager.StartTransaction();
                    trans.AddObject(this);
                }

                while (this.Count != 0)
                    this.RemoveAt(this.Count - 1);

                if (trans != null)
                {
                    trans.Commit();
                    trans.Dispose();
                }
            }

            protected override void InsertItem(int index, TreeNodeBase item)
            {
                if (item == null)
                    throw new ArgumentNullException("item");

                if (item.Parent != _owner)
                {
                    Transaction trans = null;
                    if (_transManager != null && _transManager.EnableTansactions)
                    {
                        trans = _transManager.StartTransaction();
                        trans.AddObject(this);
                    }

                    if (item.Parent != null)
                        item.Parent.Nodes.Remove(item);
                    item._parent = _owner;
                    item._index = index;
                    for (int i = index; i < Count; i++)
                        this[i]._index++;
                    
                    item.TransactionManager = _transManager;
                    
                    base.InsertItem(index, item);

                    TreeModel model = _owner.FindModel();
                    if (model != null)
                        model.OnNodeInserted(_owner, index, item);

                    if (trans != null)
                    {
                        trans.Commit();
                        trans.Dispose();
                    }
                }
            }

            protected override void RemoveItem(int index)
            {
                Transaction trans = null;
                if (_transManager != null && _transManager.EnableTansactions)
                {
                    trans = _transManager.StartTransaction();
                    trans.AddObject(this);
                }

                TreeNodeBase item = this[index];
                item._parent = null;
                item._index = -1;
                for (int i = index + 1; i < Count; i++)
                    this[i]._index--;
                base.RemoveItem(index);

                TreeModel model = _owner.FindModel();
                if (model != null)
                    model.OnNodeRemoved(_owner, index, item);

                if (trans != null)
                {
                    trans.Commit();
                    trans.Dispose();
                }
            }

            protected override void SetItem(int index, TreeNodeBase item)
            {
                if (item == null)
                    throw new ArgumentNullException("item");

                RemoveAt(index);
                InsertItem(index, item);
            }
        }

        #endregion

        #region Properties

        [NonSerialized]
        private TreeModel _model;

        [IgnoreDataMember]
        public TreeModel Model
        {
            get { return _model; }
            set { _model = value; }
        }

        private NodeCollection _nodes;
        public Collection<TreeNodeBase> Nodes
        {
            get { return _nodes; }
        }

        private TreeNodeBase _parent;
        public TreeNodeBase Parent
        {
            get { return _parent; }
            set
            {
                if (value != _parent)
                {
                    if (_parent != null)
                        _parent.Nodes.Remove(this);

                    if (value != null)
                        value.Nodes.Add(this);
                }
            }
        }

        private int _index = -1;
        public int Index
        {
            get
            {
                return _index;
            }
        }

        public TreeNodeBase PreviousNode
        {
            get
            {
                int index = Index;
                if (index > 0)
                    return _parent.Nodes[index - 1];
                else
                    return null;
            }
        }

        public TreeNodeBase NextNode
        {
            get
            {
                int index = Index;
                if (index >= 0 && index < _parent.Nodes.Count - 1)
                    return _parent.Nodes[index + 1];
                else
                    return null;
            }
        }

        private string _text;
        public virtual string Text
        {
            get { return _text; }
            set
            {
                if (_text != value)
                {
                    _text = value;
                    NotifyModel();
                }
            }
        }

        private CheckState _checkState;
        public virtual CheckState CheckState
        {
            get { return _checkState; }
            set
            {
                if (_checkState != value)
                {
                    _checkState = value;
                    NotifyModel();
                }
            }
        }

        private object _tag;
        public object Tag
        {
            get { return _tag; }
            set { _tag = value; }
        }

        public bool IsChecked
        {
            get
            {
                return CheckState != CheckState.Unchecked;
            }
            set
            {
                if (value)
                    CheckState = CheckState.Checked;
                else
                    CheckState = CheckState.Unchecked;
            }
        }

        public virtual bool IsLeaf
        {
            get
            {
                return false;
            }
        }

        [NonSerialized]
        TransactionManager _transManager;

        [IgnoreDataMember]
        public TransactionManager TransactionManager
        {
            get
            {
                return _transManager;
            }
            set
            {
                _transManager = value;
                _nodes.TransactionManager = value;
            }
        }

        #endregion

        public TreeNodeBase(TransactionManager transManager = null)
            : this(string.Empty, null, transManager)
        {
        }


        public TreeNodeBase(string text, object tag, TransactionManager transManager)
        {
            _text = text;
            _tag = tag;
            _transManager = transManager;            
            _nodes = new NodeCollection(this, transManager);
        }

        /// <summary>
        /// Для Serializable
        /// </summary>
        private TreeNodeBase()
        {
        }

        public TreeNodeBase Clone()
        {
            var res = this.MemberwiseClone() as TreeNodeBase;
            res.Parent = null;
            return res;
        }

        public override string ToString()
        {
            return Text;
        }

        private TreeModel FindModel()
        {
            TreeNodeBase node = this;
            while (node != null)
            {
                if (node.Model != null)
                    return node.Model;
                if (node == node.Parent)
                {
                    return null;
                }
                node = node.Parent;
            }
            return null;
        }

        protected void NotifyModel()
        {
            TreeModel model = FindModel();
            if (model != null && Parent != null)
            {
                TreePath path = model.GetPath(Parent);
                if (path != null)
                {
                    TreeModelEventArgs args = new TreeModelEventArgs(path, new int[] { Index }, new object[] { this });
                    model.OnNodesChanged(args);
                }
            }
        }
    }
}
