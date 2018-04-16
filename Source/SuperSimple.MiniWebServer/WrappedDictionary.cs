namespace SuperSimple.MiniWebServer
{
    using System.Collections;
    using System.Collections.Generic;

    public class WrappedDictionary<TKey, TValue> : IDictionary<TKey, TValue>
    {
        private IDictionary<TKey, TValue> wrappedDictionary { get; set; }

        public ICollection<TKey> Keys
        { get { return wrappedDictionary.Keys; } }
        public ICollection<TValue> Values
        { get { return wrappedDictionary.Values; } }
        public int Count
        { get { return wrappedDictionary.Count; } }
        public bool IsReadOnly
        { get { return wrappedDictionary.IsReadOnly; } }
        public TValue this[TKey key]
        {
            get { return wrappedDictionary[key]; }
            set { wrappedDictionary[key] = value; }
        }

        public WrappedDictionary(IDictionary<TKey, TValue> wrappedDictionary)
        {
            this.wrappedDictionary = wrappedDictionary;
        }

        public bool ContainsKey(TKey key)
        {
            return wrappedDictionary.ContainsKey(key);
        }

        public void Add(TKey key, TValue value)
        {
            wrappedDictionary.Add(key, value);
        }

        public bool Remove(TKey key)
        {
            return wrappedDictionary.Remove(key);
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            return wrappedDictionary.TryGetValue(key, out value);
        }

        public void Add(KeyValuePair<TKey, TValue> item)
        {
            wrappedDictionary.Add(item);
        }

        public void Clear()
        {
            wrappedDictionary.Clear();
        }

        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            return wrappedDictionary.Contains(item);
        }

        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            wrappedDictionary.CopyTo(array, arrayIndex);
        }

        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            return wrappedDictionary.Remove(item);
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return wrappedDictionary.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)wrappedDictionary).GetEnumerator();
        }
    }
}
