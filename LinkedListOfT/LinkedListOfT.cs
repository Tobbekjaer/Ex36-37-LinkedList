using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkedListOfT
{
    public class LinkedList<T> : IEnumerable
    {
        private class Node
        {
            public T Data { get; set; }
            public Node Next { get; set; }

            public override string ToString()
            {
                return $"{Data}";
            }
        }

        private Node head;
        private int count;
        public int Count
        {
            get { return count; }
        }
        private T first;
        public T First
        {
            get
            {
                if (head != null) {
                    first = ItemAt(0);
                }
                else {
                    first = default(T);
                }
                return first;
            }
        }
        private T last;

        public T Last
        {
            get
            {
                if (head != null) {
                    last = ItemAt(count - 1);
                }
                else {
                    last = default(T);
                }
                return last;
            }
        }


        public void InsertAt(int index, T o)
        {
            // If list is empty insert node at beginning 
            if (head == null) {
                head = new Node();
                head.Data = o;
                count++;
                return;
            }

            try {
                Node current = head;

                // Insert element at index 0
                if (index == 0) {
                    Node newHead = new Node();
                    newHead.Data = o;
                    newHead.Next = head;
                    head = newHead;
                    count++;
                }
                // Insert element at the end of the list
                else if (index == count) {
                    // Get to the end of the linked list before you create and insert a new node
                    while (current.Next != null) {
                        current = current.Next;
                    }
                    // Create a new node at the end of the list 
                    current.Next = new Node();
                    current.Next.Data = o;
                    count++;
                }
                // Insert element in the middle of the list 
                else {
                    Node newNode = new Node();
                    newNode.Data = o;
                    // Use current (head) as a temporary node and traverse to the node previous to the index  

                    for (int i = 0; i < index - 1; i++) {
                        while (current.Next != null) {
                            current = current.Next;
                        }
                    }
                    // If the previous node is not null, make newNode next as current next and current next as newNode
                    if (current != null) {
                        newNode.Next = current.Next;
                        current.Next = newNode;
                    }
                    count++;
                }

            }
            catch (Exception e) {
                throw new IndexOutOfRangeException(e.Message);
            }
        }

        public void DeleteAt(int index)
        {
            Node current = head;
            // If head is null return
            if (head == null) { return; }

            // If we want to delete the head, just set the head to it's next value
            if (index == 0) {
                head = head.Next;
                count--;
                return;
            }
            else {
                // Use current (head) as a temporary node an traverse to the node previous to the index  
                for (int i = 0; i < index - 1; i++) {
                    while (current.Next != null) {
                        current = current.Next;
                        break;
                    }
                }
                // Make currents next value the value of the next next element of the list
                current.Next = current.Next.Next;
                count--;
            }
        }

        public T ItemAt(int index)
        {
            T oItem = default(T);
            Node current = head;

            if (index == 0) {
                oItem = current.Data;
            }
            else if (index != 0) {
                // Use current (head) as a temporary node an traverse to the node previous to the index  
                for (int i = 0; i < index; i++) {
                    while (current.Next != null) {
                        current = current.Next;
                        break;
                    }
                }
            }
            oItem = current.Data;

            return oItem;
        }

        // Inserts an element at the beginning of the list 
        public void Insert(T o)
        {
            InsertAt(0, o);
        }

        // Inserts an element at the end of the list 
        public void Append(T o)
        {
            InsertAt(count, o);
        }

        public override string ToString()
        {

            Node current = head;
            string allElements = null;
            if (Count == 1) {
                allElements += current.ToString();
            }
            else {
                while (current != null) {
                    allElements += current.ToString();
                    if (current.Next != null) {
                        allElements += "\n";
                    }
                    current = current.Next;
                }
            }
            return allElements;
        }

        public IEnumerator GetEnumerator()
        {
            return new LinkedListEnumerator(head);
        }

        private class LinkedListEnumerator : IEnumerator
        {
            private Node _head;

            private Node _position; 
            
            public LinkedListEnumerator(Node head)
            {
                _head = head;
            }
            public T Current
            {
                get
                {
                    try {
                        return _head.Data; 
                    }
                    catch (IndexOutOfRangeException) {
                        throw new InvalidOperationException();
                    }
                }
            }
            object IEnumerator.Current
            {
                get { return Current; }
            }

            public bool MoveNext()
            {
                _head = _head.Next;
                return (_head.Next != null);
            }
            public void Reset()
            {
                _position.Next = _head; 
            }
        }

    }
}
