using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.Reflection;
using System.IO;
using FinaleSignalR_Client.Template_Method;
using FinaleSignalR_Client.Composite;

namespace FinaleSignalR_Client.Iterator
{
    public interface IIterator<T>
    {
        T First();
        T Next();
        bool IsDone();
        T CurrentItem();
    }

    public class ListIterator<T> : IIterator<T>
    {
        private List<T> _collection;
        private int _currentIndex = 0;

        public ListIterator(List<T> collection)
        {
            _collection = collection;
        }

        public T First()
        {
            _currentIndex = 0;
            return _collection.FirstOrDefault();
        }

        public T Next()
        {
            _currentIndex++;
            if (!IsDone())
                return _collection[_currentIndex];
            else
                return default(T);
        }

        public bool IsDone()
        {
            return _currentIndex >= _collection.Count;
        }

        public T CurrentItem()
        {
            if (_currentIndex < _collection.Count)
                return _collection[_currentIndex];
            else
                return default(T);
        }
    }


    public class CompositeIterator : IIterator<Node>
    {
        private HashSet<Node> _visited = new HashSet<Node>();
        private Queue<Node> _queue = new Queue<Node>();

        public CompositeIterator(Node root)
        {
            _queue.Enqueue(root);
            _visited.Add(root);
        }

        public Node First()
        {
            return _queue.Count > 0 ? _queue.Peek() : null;
        }

        public Node Next()
        {
            if (_queue.Count == 0) return null;

            var current = _queue.Dequeue();
            if (current is CompositeButton compositeButton)
            {
                foreach (var child in compositeButton.Nodes)
                {
                    if (_visited.Add(child)) // Only enqueue if it hasn't been visited
                    {
                        _queue.Enqueue(child);
                    }
                }
                foreach (var neighbour in compositeButton.Neighbours)
                {
                    if (_visited.Add(neighbour)) // Only enqueue if it hasn't been visited
                    {
                        _queue.Enqueue(neighbour);
                    }
                }
            }
            return current;
        }

        public bool IsDone()
        {
            return _queue.Count == 0;
        }

        public Node CurrentItem()
        {
            return _queue.Peek();
        }
    }


}
