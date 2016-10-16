using System;
using System.Collections;
using System.Collections.Generic;

namespace WindowsFormsApplication1
{
    class LinkedDictionary<TK, TV> : IDictionary<TK, TV>
    {
        public LinkedDictionary() : this(EqualityComparer<TK>.Default) { }
        public LinkedDictionary(bool moveOnReplace) : this(EqualityComparer<TK>.Default, moveOnReplace) { }
        public LinkedDictionary(int capacity) : this(capacity, EqualityComparer<TK>.Default) {}
        public LinkedDictionary(IEqualityComparer<TK> equalityComparer) : this(equalityComparer, true) { }
        public LinkedDictionary(IEqualityComparer<TK> equalityComparer, bool moveOnReplace)
        {
            MoveOnReplace = moveOnReplace;
            list = new LinkedList<KeyValuePair<TK, TV>>();
            dictionary = new Dictionary<TK, LinkedListNode<KeyValuePair<TK, TV>>>(equalityComparer);
        }

        public LinkedDictionary(int capacity, IEqualityComparer<TK> equalityComparer) : this(capacity, equalityComparer, true) { }

        public LinkedDictionary(int capacity, IEqualityComparer<TK> equalityComparer, bool moveOnReplace)
        {
            MoveOnReplace = moveOnReplace;
            list = new LinkedList<KeyValuePair<TK, TV>>();
            dictionary = new Dictionary<TK, LinkedListNode<KeyValuePair<TK, TV>>>(capacity, equalityComparer);
        }

        public LinkedDictionary(IDictionary<TK,TV> origin) : this(origin, EqualityComparer<TK>.Default, true) { }

        public LinkedDictionary(IDictionary<TK,TV> origin, IEqualityComparer<TK> equalityComparer, bool moveOnReplace):this(origin.Count,equalityComparer,moveOnReplace)
        { 
            foreach(var item in origin)
            {
                Add(item);
            }
        }

        public bool ContainsKey(TK key)
        {
            return dictionary.ContainsKey(key);
        }

        public bool ContainsValue(TV value)
        {
            foreach(var iter in list)
            {
                if (EqualityComparer<TV>.Default.Equals(iter.Value, value))
                    return true;
            }
            return false;
        }

        public void Add(TK key,TV value)
        {
            if (key == null)
            {
                throw new ArgumentNullException("Key must be not null");
            }
            if (ContainsKey(key))
            {
                throw new ArgumentException("Already contains key");
            }
            else
            {
                LinkedListNode<KeyValuePair<TK, TV>> node = new LinkedListNode<KeyValuePair<TK, TV>>(new KeyValuePair<TK, TV>(key, value));
                list.AddLast(node);
                dictionary.Add(key, node);
            }
        }

        public void Add(KeyValuePair<TK,TV> pair)
        {
            Add(pair.Key, pair.Value);
        }

        public bool Remove(TK key)
        {
            LinkedListNode<KeyValuePair<TK, TV>> node;
            if(dictionary.TryGetValue(key,out node))
            {
                list.Remove(node);
                return dictionary.Remove(key);
            }
            else
            {
                return false;
            }
        }

        public bool TryGetValue(TK key, out TV value)
        {
            if (ContainsKey(key))
            {
                value = this[key];
                return true;
            }
            else
            {
                value = (TV)getDefault();
                return false;
            }
        }

        public TV this[TK key]
        {
            get
            {
                return dictionary[key].Value.Value;
            }
            set
            {
                LinkedListNode<KeyValuePair<TK, TV>> node = dictionary[key];
                node.Value = new KeyValuePair<TK, TV>(key, value);
                if (MoveOnReplace)
                {
                    list.Remove(node);
                    list.AddLast(node);
                }
            }
        }


        public void Clear()
        {
            list.Clear();
            dictionary.Clear();
        }

        public bool Contains(KeyValuePair<TK, TV> item)
        {
            if (!ContainsKey(item.Key))
                return false;
            return EqualityComparer<KeyValuePair<TK, TV>>.Default.Equals(item, dictionary[item.Key].Value);
        }

        public void CopyTo(KeyValuePair<TK, TV>[] array, int arrayIndex)
        {
            list.CopyTo(array, arrayIndex);
        }

        public IEnumerator<KeyValuePair<TK, TV>> GetEnumerator()
        {
            return list.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return list.GetEnumerator();
        }

        public ICollection<TK> Keys
        {
            get
            {
               return new KeyCollection(this);
            }
        }

        public ICollection<TV> Values
        {
            get
            {
                return new ValueCollection(this);
            }
        }

        public int Count
        {
            get
            {
                return dictionary.Count;
            }
        }

        public bool IsReadOnly
        {
            get
            {
                return false;
            }
        }

        public bool Remove(KeyValuePair<TK, TV> item)
        {
            LinkedListNode<KeyValuePair<TK, TV>> node;
            if (!dictionary.TryGetValue(item.Key, out node))
            {
                return false;
            }
            list.Remove(node);
            return dictionary.Remove(item.Key);
        }

        public void Sort(IComparer<KeyValuePair<TK,TV>> comparer)
        {
            lock (this)
            {
                if (comparer == null)
                    throw new ArgumentNullException("Comparer is null");
                KeyValuePair<TK, TV>[] arr = new KeyValuePair<TK, TV>[Count];
                list.CopyTo(arr, 0);
                Array.Sort(arr, comparer);
                Clear();
                foreach (var pair in arr)
                {
                    Add(pair.Key, pair.Value);
                }
            }
        }


        private IEqualityComparer<TK> equalityComparer;
        private LinkedList<KeyValuePair<TK, TV>> list;
        private Dictionary<TK, LinkedListNode<KeyValuePair<TK, TV>>> dictionary;
        public bool MoveOnReplace { get; private set; }

        private object getDefault()
        {
            Type t = typeof(TV);
            if (t.IsValueType)
            {
                return Activator.CreateInstance(t);
            }
            else
            {
                return null;
            }
        }


        private abstract class InmodifableAdapter<T> : IReadOnlyCollection<T>, ICollection<T>
        {
            protected LinkedDictionary<TK, TV> Owner { get; private set; }
            public InmodifableAdapter(LinkedDictionary<TK, TV> owner)
            {
                Owner = owner;
            }
            public int Count
            {
                get
                {
                    return Owner.Count;
                }
            }

            public bool IsReadOnly
            {
                get
                {
                    return true;
                }
            }

            public void Add(T item)
            {
                throw new NotImplementedException();
            }

            public void Clear()
            {
                throw new NotImplementedException();
            }

            public void CopyTo(T[] array, int arrayIndex)
            {
                if (array == null)
                    throw new ArgumentNullException("Array is null");
                if (arrayIndex < 0)
                    throw new ArgumentOutOfRangeException("arrayIndex is below zero");
                if (array.Length < this.Count + arrayIndex)
                    throw new ArgumentException("Too short array");
                foreach (T item in this)
                {
                    array[arrayIndex++] = item;
                }
            }

            protected abstract T getItemFromKeyValuePair(KeyValuePair<TK, TV> pair);

            public IEnumerator<T> GetEnumerator()
            {
                foreach (var pair in Owner.list)
                {
                    yield return getItemFromKeyValuePair(pair);
                }
            }

            public bool Remove(T item)
            {
                throw new NotImplementedException();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                foreach (var pair in Owner.list)
                {
                    yield return getItemFromKeyValuePair(pair);
                }
            }

            public abstract bool Contains(T item);
        }
        private class ValueCollection : InmodifableAdapter<TV>, IReadOnlyCollection<TV>, ICollection<TV>
        {
            public ValueCollection(LinkedDictionary<TK, TV> owner) : base(owner) { }

            public override bool Contains(TV item)
            {
                return Owner.ContainsValue(item);
            }

            protected override TV getItemFromKeyValuePair(KeyValuePair<TK, TV> pair)
            {
                return pair.Value;
            }
        }

        private class KeyCollection : InmodifableAdapter<TK>, ICollection<TK>, IReadOnlyCollection<TK>
        {
            public KeyCollection(LinkedDictionary<TK, TV> owner) : base(owner) { }

            public override bool Contains(TK item)
            {
                return Owner.ContainsKey(item);
            }

            protected override TK getItemFromKeyValuePair(KeyValuePair<TK, TV> pair)
            {
                return pair.Key;
            }
        }
    }
}
