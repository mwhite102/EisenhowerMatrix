using System;
using System.Collections.Generic;

namespace EisenhowerMatrix.WPF
{
    /// <summary>
    /// A custom generic Stack implementation with a MaxNumber of Items based on a LinkedList/>
    /// </summary>
    class CustomStack<T>
    {
        private LinkedList<T> _LinkedList = new LinkedList<T>();

        private int _MaxItems;

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="maxItems">The maximum number of items the stack can hold</param>
        public CustomStack(int maxItems)
        {
            _MaxItems = maxItems;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the number of items in the stack
        /// </summary>
        public int Count
        {
            get
            {
                return _LinkedList.Count;
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Gets the first item on the stack without removing it from the stack
        /// </summary>
        /// <returns>The first item on the stack</returns>
        public T Peek()
        {
            if (_LinkedList.Count == 0)
            {
                throw new InvalidOperationException("There are no items on the stack");
            }
            return _LinkedList.First.Value;
        }

        /// <summary>
        /// Gets the first item on the stack and removes it from the stack
        /// </summary>
        /// <returns>The first item on the stack</returns>
        public T Pop()
        {
            T value = this.Peek();
            _LinkedList.RemoveFirst();
            return value;
        }

        /// <summary>
        /// Pushes an item onto the stack
        /// </summary>
        /// <param name="item">The item to be pushed onto the stack</param>
        public void Push(T item)
        {
            if (_LinkedList.Count == _MaxItems)
            {
                _LinkedList.RemoveLast();
            }
            _LinkedList.AddFirst(item);
        }

        #endregion

    }
}
