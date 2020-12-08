using System;
using System.Collections;
using System.Collections.Generic;

namespace finalpetrolstation_assignment
{
    public class VehicleQueue<T> : LinkedList<T>//array vehicles created in queue
    {
        private readonly int _maxSize;//size of array
        public VehicleQueue(int maxSize)//
        {
            _maxSize = maxSize;
        }

        public void Push(T item)
        {
            this.AddFirst(item);

            if (this.Count > _maxSize)
                this.RemoveLast();
        }

        public T Pop()
        {
            var item = this.First.Value;
            this.RemoveFirst();
            return item;
        }
    }
}
