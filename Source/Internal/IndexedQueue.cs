using System;

namespace UnityConsole.Internal
{
    // A First-In-First-Out, fixed-capacity queue with lookup functionality by means of an indexer.
    // Enqueue, Dequeue and lookup are all O(1) operations thanks to the circular array implementation. This is useful for input history navigation.
    internal class IndexedQueue<T>
    {
        private T[] array;
        private int capacity;
        private int front;

        // The number of elements currently in the queue.
        public int Count { get; private set; }

        // Whether the queue is currently empty.
        public bool IsEmpty { get { return Count == 0; } }

        // Whether the queue is currently at maximum capacity.
        public bool IsFull { get { return Count == capacity; } }

        // Retrieves the element at the given index.
        // Throws a System.IndexOutOfRangeException when the queue is empty or if the given index is greater or equal than Count
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

        // Constructs a new queue with the given (fixed) capacity.
        // Throws a System.InvalidOperationException when a capacity of 0 is given
        public IndexedQueue(int capacity)
        {
            this.capacity = capacity;
            array = new T[capacity];
            if (capacity == 0)
                throw new InvalidOperationException("Cannot create an IndexedQueue with a capacity of 0.");
        }

        // Adds an element to the end of the queue.
        // If Count already equals the capacity, then the first element is replaced with the given one
        public void Enqueue(T element)
        {
            this[Count] = element;
            if (IsFull)
                front = (front + 1) % capacity;
            else
                Count++;
        }

        // Removes and returns the element at the beginning of the queue.
        // Throws a System.InvalidOperationException when the queue is already empty.
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

        // Returns the element at the beginning of the queue without removing it.
        // Throws a System.InvalidOperationException when the queue is already empty.
        public T Peek()
        {
            if (IsEmpty)
                throw new InvalidOperationException("Cannot peek an empty IndexedQueue.");

            return this[0];
        }

        // Removes all elements from the queue.
        public void Clear()
        {
            for (int i = 0; i < capacity; i++)
                array[i] = default(T);

            front = 0;
            Count = 0;
        }
    }
}