using System;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Utils
{
    [Serializable]
    public class SerializableDictionary<TKey, TValue>
    {
        [SerializeField] private List<SerializableKeyValuePair<TKey, TValue>> pairs = new();

        private Dictionary<TKey, TValue> _dictionary;

        public Dictionary<TKey, TValue> ToDictionary()
        {
            if (_dictionary == null)
            {
                _dictionary = new Dictionary<TKey, TValue>();
                foreach (var pair in pairs)
                {
                    if (!_dictionary.ContainsKey(pair.Key))
                    {
                        _dictionary.Add(pair.Key, pair.Value);
                    }
                }
            }
            return _dictionary;
        }

        public void FromDictionary(Dictionary<TKey, TValue> dict)
        {
            pairs.Clear();
            foreach (var kvp in dict)
            {
                pairs.Add(new SerializableKeyValuePair<TKey, TValue>(kvp.Key, kvp.Value));
            }

            _dictionary = dict;
        }

        public TValue this[TKey key]
        {
            get
            {
                ToDictionary();
                return _dictionary[key];
            }
            set
            {
                ToDictionary();
                _dictionary[key] = value;

                // Update the pairs list so it serializes correctly.
                // This is important if you modify the dictionary from code.
                int index = pairs.FindIndex(p => p.Key.Equals(key));
                if (index >= 0)
                {
                    pairs[index] = new SerializableKeyValuePair<TKey, TValue>(key, value);
                }
                else
                {
                    pairs.Add(new SerializableKeyValuePair<TKey, TValue>(key, value));
                }
            }
        }

        public bool ContainsKey(TKey key)
        {
            ToDictionary();
            return _dictionary.ContainsKey(key);
        }
    }

    [Serializable]
    public struct SerializableKeyValuePair<TKey, TValue>
    {
        [SerializeField] private TKey key;
        [SerializeField] private TValue value;

        public SerializableKeyValuePair(TKey key, TValue value)
        {
            this.key = key;
            this.value = value;
        }

        public readonly TKey Key => key;
        public readonly TValue Value => value;
    }
}