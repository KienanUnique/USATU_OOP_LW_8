namespace CustomDoublyLinkedListLibrary
{
    public class CustomDoublyLinkedList<T>
    {
        internal class DoublyLinkedElement
        {
            public T Object;
            public readonly bool IsBorder;
            public DoublyLinkedElement Next;
            public DoublyLinkedElement Previous;

            public DoublyLinkedElement()
            {
                IsBorder = true;
            }

            public DoublyLinkedElement(DoublyLinkedElement previous)
            {
                IsBorder = true;
                Previous = previous;
            }

            public DoublyLinkedElement(T objectSet, DoublyLinkedElement next, DoublyLinkedElement previous)
            {
                IsBorder = false;
                Object = objectSet;
                Next = next;
                Previous = previous;
            }
        }

        public int Count { get; private set; }

        public T First
        {
            get => _firstElement.Object;
            set => _firstElement.Object = value;
        }

        public T Last
        {
            get => _lastElement.Object;
            set => _lastElement.Object = value;
        }

        private DoublyLinkedElement _firstElement;
        private DoublyLinkedElement _lastElement;

        public CustomDoublyLinkedList()
        {
            Count = 0;
            _firstElement = new DoublyLinkedElement();
            _firstElement.Next = new DoublyLinkedElement(_firstElement);
            _lastElement = _firstElement.Next;
        }

        public PointerCustomDoublyLinkedList<T> GetPointerOnBeginning()
        {
            return new PointerCustomDoublyLinkedList<T>(_firstElement);
        }

        public PointerCustomDoublyLinkedList<T> GetPointerOnEnd()
        {
            return new PointerCustomDoublyLinkedList<T>(_lastElement);
        }

        public void Add(T objectToAdd)
        {
            if (Count == 0)
            {
                var addedElement = new DoublyLinkedElement(objectToAdd, _lastElement, _firstElement);
                _firstElement.Next = addedElement;
                _lastElement.Previous = addedElement;
                _firstElement = addedElement;
                _lastElement = addedElement;
            }
            else
            {
                _lastElement.Next = new DoublyLinkedElement(objectToAdd, _lastElement.Next, _lastElement);
                _lastElement = _lastElement.Next;
            }

            Count++;
        }

        public void RemovePointerElement(PointerCustomDoublyLinkedList<T> pointer)
        {
            switch (pointer.CurrentElement.Next.IsBorder)
            {
                case true when !pointer.CurrentElement.Previous.IsBorder:
                    _lastElement = pointer.CurrentElement.Previous;
                    _lastElement.Next = pointer.CurrentElement.Next;
                    pointer.CurrentElement.Next.Previous = _lastElement;
                    break;
                case true when pointer.CurrentElement.Previous.IsBorder:
                    _firstElement = pointer.CurrentElement.Previous;
                    _lastElement = pointer.CurrentElement.Next;
                    break;
                case false when pointer.CurrentElement.Previous.IsBorder:
                    _firstElement = pointer.CurrentElement.Next;
                    _firstElement.Previous = pointer.CurrentElement.Previous;
                    pointer.CurrentElement.Previous.Next = _firstElement;
                    break;
            }

            pointer.CurrentElement.Previous.Next = pointer.CurrentElement.Next;
            pointer.CurrentElement.Next.Previous = pointer.CurrentElement.Previous;
            Count--;
        }

        public void InsertListAfterPointer(CustomDoublyLinkedList<T> listToInsert,
            PointerCustomDoublyLinkedList<T> pointer)
        {
            if (Count == 0)
            {
                _firstElement = listToInsert._firstElement;
                _lastElement = listToInsert._lastElement;
                pointer.CurrentElement = _firstElement;
            }
            else
            {
                if (pointer.CurrentElement.Next.IsBorder)
                {
                    _lastElement = listToInsert._lastElement;
                }
                else
                {
                    pointer.CurrentElement.Next.Previous = listToInsert._lastElement;
                    listToInsert._lastElement.Next = pointer.CurrentElement.Next;
                }

                pointer.CurrentElement.Next = listToInsert._firstElement;
                listToInsert._firstElement.Previous = pointer.CurrentElement;
            }

            Count += listToInsert.Count;
        }

        public void InsertListBeforePointer(CustomDoublyLinkedList<T> listToInsert,
            PointerCustomDoublyLinkedList<T> pointer)
        {
            if (Count == 0)
            {
                _firstElement = listToInsert._firstElement;
                _lastElement = listToInsert._lastElement;
                pointer.CurrentElement = _firstElement;
            }
            else
            {
                if (pointer.CurrentElement.Previous.IsBorder)
                {
                    _firstElement = listToInsert._firstElement;
                }
                else
                {
                    pointer.CurrentElement.Previous.Next = listToInsert._firstElement;
                    listToInsert._firstElement.Previous = pointer.CurrentElement.Previous;
                }

                pointer.CurrentElement.Previous = listToInsert._lastElement;
                listToInsert._lastElement.Next = pointer.CurrentElement;
            }

            Count += listToInsert.Count;
        }
    }

    public class PointerCustomDoublyLinkedList<T>
    {
        public T Current
        {
            get => CurrentElement.Object;
            set => CurrentElement.Object = value;
        }

        internal CustomDoublyLinkedList<T>.DoublyLinkedElement CurrentElement;

        internal PointerCustomDoublyLinkedList(CustomDoublyLinkedList<T>.DoublyLinkedElement startElement) =>
            CurrentElement = startElement;


        public void MoveNext()
        {
            CurrentElement = CurrentElement.Next;
        }

        public void MovePrevious()
        {
            CurrentElement = CurrentElement.Previous;
        }

        public bool IsBorderReached()
        {
            return CurrentElement.IsBorder;
        }
    }
}