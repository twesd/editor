using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using UnitEngine;
using CommonUI;

namespace UnitEngineUI.Controls
{
    public partial class ControlAnimation : UserControl
    {
        public delegate void OnChangeEventHandler(object data);

        public OnChangeEventHandler Changed;

        /// <summary>
        /// Обрабатываемый объект
        /// </summary>
        UnitAction _editItem;

        List<UnitAnimation> _animations;

        public UnitAction EditItem
        {
            get
            {
                return _editItem;
            }
            set
            {
                SetEditItem(value); 
            }
        }


        public ControlAnimation(List<UnitAnimation> animations)
        {
            InitializeComponent();

            _animations = animations;

            _animationsBox.Items.Clear();
            _animationsBox.Items.Add("Отсутствует");
            foreach (UnitAnimation anim in animations)
            {
                _animationsBox.Items.Add(anim);
            }
            _animationsBox.SelectedIndex = 0;
        }


        private void SetEditItem(UnitAction editItem)
        {
            _editItem = editItem;
            UnitAnimation anim = _animations.Find(x => x.Id == _editItem.AnimationId);
            if (anim != null)
            {
                _animationsBox.SelectedItem = anim;
            }
            else
            {
                _animationsBox.SelectedIndex = 0;
            }

        }

        private void SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_editItem == null)
            {
                return;
            }
            UnitAnimation anim = _animationsBox.SelectedItem as UnitAnimation;
            if (anim != null)
            {
                _editItem.AnimationId = anim.Id;
            }
            else
            {
                _editItem.AnimationId = string.Empty;
            }
            if (Changed != null) Changed(_editItem);
        }

    }
}
