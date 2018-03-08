using System;
using System.Collections;
using System.Collections.Generic;

namespace Common
{
	/// <summary>
	/// A class that manages an array of IMemento objects, implementing
	/// undo/redo capabilities for the IMemento originator class.
	/// </summary>
	public class UndoBuffer
	{
        List<object> _buffer;

		int _idx;

        object _firstMemState;

		/// <summary>
		/// Returns true if there are items in the undo buffer.
		/// </summary>
		public bool CanUndo
		{
			get {return _idx > 0;}
		}

		/// <summary>
		/// Returns true if the current position in the undo buffer will
		/// allow for redo's.
		/// </summary>
		public bool CanRedo
		{
			// idx+1 because the topmost buffer item is the topmost state
			get {return _buffer.Count > _idx+1;}
		}

		/// <summary>
		/// Returns true if at the top of the undo buffer.
		/// </summary>
		public bool AtTop
		{
			get {return _idx == _buffer.Count;}
		}

		/// <summary>
		/// Количество объектов в буффере
		/// </summary>
		public int Count
		{
			get {return _buffer.Count;}
		}

        public int Index
        {
            get { return _idx; }
        }

		/// <summary>
		/// Constructor.
		/// </summary>
		public UndoBuffer()
		{
            _buffer = new List<object>();
			_idx=0;
		}

		/// <summary>
		/// Save the current state at the index position.  Anything past the index position is lost.  
        /// This means that the "redo" action is no longer possible.
		/// Scenario--The user does 10 things.  The user undo's 5 of them, then does something new.  
        /// He can only undo now, he cannot "redo".  If he does one 
		/// undo, then he can do one "redo".
		/// </summary>
		/// <param name="mem">The memento holding the current state.</param>
        public void Do(object mem)
		{   
			if (_buffer.Count > _idx)
			{
				_buffer.RemoveRange(_idx, _buffer.Count-_idx);
			}
            if (_idx == 0 && _firstMemState != null)
            {
                _buffer.Add(_firstMemState);
                ++_idx;
                _firstMemState = null;
            }
            _firstMemState = null;

			_buffer.Add(mem);
			++_idx;            

            //if (idx > 50)
            //{
            //    buffer.RemoveRange(0, 10);
            //    idx -= 10;
            //}
		}

		/// <summary>
		/// Returns the current memento.
		/// </summary>
        public object Undo()
		{
            if (!CanUndo) return null;
			--_idx;
            object state = _buffer[_idx];
            if (_idx == 0)
                _firstMemState = state;
            return state;
		}

		/// <summary>
		/// Returns the next memento.
		/// </summary>
        public object Redo()
		{
            if (!CanRedo) return null;
			++_idx;
            object state = (object)_buffer[_idx];
            _firstMemState = null;
            return state;
		}

		/// <summary>
		/// Removes all state information.
		/// </summary>
		public void Flush()
		{
			_buffer.Clear();
			_idx=0;
		}
	}
}
