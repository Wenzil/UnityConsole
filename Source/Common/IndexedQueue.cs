using System;

namespace UnityConsole
{
    /// <summary>
    /// A First-In-First-Out, fixed-capacity queue with lookup functionality by means of an indexer.
    /// </summary>
    /// <remarks>
    /// Enqueue, Dequeue and lookup are all O(1) operations thanks to the circular array implementation. This is useful for input history navigation.
    /// </remarks>
    public class IndexedQueue<T>
    {
        private T[] array;
        private int capacity;
        private int front;

        /// <summary>
        /// The number of elements currently in the queue.
        /// </summary>
        public int Count { get; private set; }

        /// <summary>
        /// Whether the queue is currently empty.
        /// </summary>
        public bool IsEmpty { get { return Count == 0; } }

        /// <summary>
        /// Whether the queue is currently at maximum capacity.
        /// </summary>
        public bool IsFull { get { return Count == capacity; } }

        /// <summary>
        /// Retrieves the element at the given index.
        /// </summary>
        /// <exception cref="System.IndexOutOfRangeException">
        /// Thrown when the queue is empty or if the given index is greater or equal than Count
        /// </exception>
        public T this[int i]
        {
            get
            {
                if (IsEmpty)
                    throw new IndexOutOfRangeException("Cannot retrieve an element from an empty IndexedQueue");
                else if (i >= Count)
                    throw new IndexOutOfRangeException("Index " + i + " is out of range in IndexedQueue");
                return array[(front + i) % capacity];
            }
            private set
            {
                array[(front + i) % capacity] = value;
            }
        }

        /// <summary>
        /// Constructs a new queue with the given (fixed) capacity.
        /// </summary>
        /// <exception cref="System.InvalidOperationException">
        /// Thrown when a capacity of 0 is given
        /// </exception>
        public IndexedQueue(int capacity)
        {
            this.capacity = capacity;
            array = new T[capacity];
            if (capacity == 0)
                throw new InvalidOperationException("Cannot create an IndexedQueue with a capacity of 0.");
        }

        /// <summary>
        /// Adds an element to the end of the queue.
        /// </summary>
        /// <remarks>
        /// If Count already equals the capacity, then the first element is replaced with the given one
        /// </remarks>
        public void Enqueue(T element)
        {
            this[Count] = element;
            if (IsFull)
                front = (front + 1) % capacity;
            else
                Count++;
        }

        /// <summary>
        /// Removes and returns the element at the beginning of the queue.
        /// </summary>
        /// <exception cref="System.InvalidOperationException">
        /// Thrown when the queue is already empty.
        /// </exception>
        public T Dequeue()
        {
            if (IsEmpty)
                throw new InvalidOperationException("Cannot dequeue an empty IndexedQueue.");
        
            T element = this[0];
            this[0] = default(T);
            front = (front + 1) % capacity;
            Count--;
            return element;
        }

        /// <summary>
        /// Returns the element at the beginning of the queue without removing it.
        /// </summary>
        /// <exception cref="System.InvalidOperationException">
        /// Thrown when the queue is already empty.
        /// </exception>
        public T Peek()
        {
            if (IsEmpty)
                throw new InvalidOperationException("Cannot peek an empty IndexedQueue.");

            return this[0];
        }

        /// <summary>
        /// Removes all elements from the queue.
        /// </summary>
        public void Clear()
        {
            for (int i = 0; i < capacity; i++)
                array[i] = default(T);

            front = 0;
            Count = 0;
        }
    }
}