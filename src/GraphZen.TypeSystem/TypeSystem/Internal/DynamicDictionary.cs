// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
using System.Linq;
using System.Text;
using GraphZen.Infrastructure;

namespace GraphZen.TypeSystem.Internal
{
    /// <summary>
    ///     A dictionary that supports dynamic access.
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay, nq}")]
    public class DynamicDictionary : DynamicObject, IEquatable<DynamicDictionary>, IEnumerable<string>,
        IDictionary<string, object>
    {
        [NotNull] private readonly IDictionary<string, dynamic> _dictionary =
            new Dictionary<string, dynamic>(StringComparer.OrdinalIgnoreCase);

        private string DebuggerDisplay
        {
            get
            {
                var builder = new StringBuilder();
                var maxItems = Math.Min(_dictionary.Count, 5);

                builder.Append("{");

                for (var i = 0; i < maxItems; i++)
                {
                    var item = _dictionary.ElementAt(i);

                    builder.AppendFormat(" {0} = {1}{2}", item.Key, item.Value, i < maxItems - 1 ? "," : string.Empty);
                }

                if (maxItems < _dictionary.Count)
                {
                    builder.Append("...");
                }

                builder.Append(" }");

                return builder.ToString();
            }
        }


        public dynamic this[string name]
        {
            get
            {
                name = GetNeutralKey(name);

                if (!_dictionary.TryGetValue(name, out var member))
                {
                    member = null;
                }

                return member;
            }
            set
            {
                name = GetNeutralKey(name);
                _dictionary[name] = value;
            }
        }

        /// <summary>
        ///     Gets an <see cref="T:System.Collections.Generic.ICollection`1" /> containing the keys of the
        ///     <see cref="DynamicDictionary" />.
        /// </summary>
        /// <returns>
        ///     An <see cref="T:System.Collections.Generic.ICollection`1" /> containing the keys of the
        ///     <see cref="DynamicDictionary" />.
        /// </returns>
        public ICollection<string> Keys => _dictionary.Keys;

        /// <summary>
        ///     Gets the number of elements contained in the <see cref="DynamicDictionary" />.
        /// </summary>
        /// <returns>The number of elements contained in the <see cref="DynamicDictionary" />.</returns>
        public int Count => _dictionary.Count;

        /// <summary>
        ///     Gets a value indicating whether the <see cref="DynamicDictionary" /> is read-only.
        /// </summary>
        /// <returns>Always returns <see langword="false" />.</returns>
        public bool IsReadOnly => false;

        /// <summary>
        ///     Gets an <see cref="T:System.Collections.Generic.ICollection`1" /> containing the values in the
        ///     <see cref="DynamicDictionary" />.
        /// </summary>
        /// <returns>
        ///     An <see cref="T:System.Collections.Generic.ICollection`1" /> containing the values in the
        ///     <see cref="DynamicDictionary" />.
        /// </returns>
        public ICollection<dynamic> Values => _dictionary.Values;

        /// <summary>
        ///     Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        ///     A <see cref="T:System.Collections.Generic.IEnumerator`1" /> that can be used to iterate through the
        ///     collection.
        /// </returns>
        IEnumerator<KeyValuePair<string, dynamic>> IEnumerable<KeyValuePair<string, object>>.GetEnumerator() =>
            _dictionary.GetEnumerator();

        /// <summary>
        ///     Adds an element with the provided key and value to the <see cref="DynamicDictionary" />.
        /// </summary>
        /// <param name="key">The object to use as the key of the element to add.</param>
        /// <param name="value">The object to use as the value of the element to add.</param>
        public void Add(string key, dynamic value)
        {
            this[key] = value;
        }

        /// <summary>
        ///     Adds an item to the <see cref="DynamicDictionary" />.
        /// </summary>
        /// <param name="item">The object to add to the <see cref="DynamicDictionary" />.</param>
        public void Add(KeyValuePair<string, dynamic> item)
        {
            this[item.Key] = item.Value;
        }

        /// <summary>
        ///     Determines whether the <see cref="DynamicDictionary" /> contains an element with the specified key.
        /// </summary>
        /// <returns>
        ///     <see langword="true" /> if the <see cref="DynamicDictionary" /> contains an element with the key; otherwise,
        ///     <see langword="false" />.
        /// </returns>
        /// <param name="key">The key to locate in the <see cref="DynamicDictionary" />.</param>
        public bool ContainsKey(string key)
        {
            key = GetNeutralKey(key);
            return _dictionary.ContainsKey(key);
        }

        /// <summary>
        ///     Gets the value associated with the specified key.
        /// </summary>
        /// <returns>
        ///     <see langword="true" /> if the <see cref="DynamicDictionary" /> contains an element with the specified key;
        ///     otherwise, <see langword="false" />.
        /// </returns>
        /// <param name="key">The key whose value to get.</param>
        /// <param name="value">
        ///     When this method returns, the value associated with the specified key, if the key is found;
        ///     otherwise, the default value for the type of the <paramref name="value" /> parameter. This parameter is passed
        ///     uninitialized.
        /// </param>
        public bool TryGetValue(string key, out dynamic value)
        {
            key = GetNeutralKey(key);
            return _dictionary.TryGetValue(key, out value);
        }

        /// <summary>
        ///     Removes all items from the <see cref="DynamicDictionary" />.
        /// </summary>
        public void Clear()
        {
            _dictionary.Clear();
        }

        /// <summary>
        ///     Determines whether the <see cref="DynamicDictionary" /> contains a specific value.
        /// </summary>
        /// <returns>
        ///     <see langword="true" /> if <paramref name="item" /> is found in the <see cref="DynamicDictionary" />; otherwise,
        ///     <see langword="false" />.
        /// </returns>
        /// <param name="item">The object to locate in the <see cref="DynamicDictionary" />.</param>
        public bool Contains(KeyValuePair<string, dynamic> item)
        {
            var dynamicValueKeyValuePair =
                GetDynamicKeyValuePair(item);

            return _dictionary.Contains(dynamicValueKeyValuePair);
        }

        /// <summary>
        ///     Copies the elements of the <see cref="DynamicDictionary" /> to an <see cref="T:System.Array" />, starting at a
        ///     particular <see cref="T:System.Array" /> index.
        /// </summary>
        /// <param name="array">
        ///     The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied
        ///     from the <see cref="DynamicDictionary" />. The <see cref="T:System.Array" /> must have zero-based indexing.
        /// </param>
        /// <param name="arrayIndex">The zero-based index in <paramref name="array" /> at which copying begins.</param>
        public void CopyTo(KeyValuePair<string, dynamic>[] array, int arrayIndex)
        {
            _dictionary.CopyTo(array, arrayIndex);
        }

        /// <summary>
        ///     Removes the element with the specified key from the <see cref="DynamicDictionary" />.
        /// </summary>
        /// <returns><see langword="true" /> if the element is successfully removed; otherwise, <see langword="false" />.</returns>
        /// <param name="key">The key of the element to remove.</param>
        public bool Remove(string key)
        {
            key = GetNeutralKey(key);
            return _dictionary.Remove(key);
        }

        /// <summary>
        ///     Removes the first occurrence of a specific object from the <see cref="DynamicDictionary" />.
        /// </summary>
        /// <returns>
        ///     <see langword="true" /> if <paramref name="item" /> was successfully removed from the
        ///     <see cref="DynamicDictionary" />; otherwise, <see langword="false" />.
        /// </returns>
        /// <param name="item">The object to remove from the <see cref="DynamicDictionary" />.</param>
        public bool Remove(KeyValuePair<string, dynamic> item)
        {
            var dynamicValueKeyValuePair =
                GetDynamicKeyValuePair(item);

            return _dictionary.Remove(dynamicValueKeyValuePair);
        }

        /// <summary>
        ///     Returns the enumeration of all dynamic member names.
        /// </summary>
        /// <returns>A <see cref="IEnumerable{T}" /> that contains dynamic member names.</returns>
        public IEnumerator<string> GetEnumerator() => _dictionary.Keys.GetEnumerator();

        /// <summary>
        ///     Returns the enumeration of all dynamic member names.
        /// </summary>
        /// <returns>A <see cref="IEnumerator" /> that contains dynamic member names.</returns>
        IEnumerator IEnumerable.GetEnumerator() => _dictionary.Keys.GetEnumerator();

        /// <summary>
        ///     Indicates whether the current <see cref="DynamicDictionary" /> is equal to another object of the same type.
        /// </summary>
        /// <returns>
        ///     <see langword="true" /> if the current instance is equal to the <paramref name="other" /> parameter;
        ///     otherwise, <see langword="false" />.
        /// </returns>
        /// <param name="other">An <see cref="DynamicDictionary" /> instance to compare with this instance.</param>
        public bool Equals(DynamicDictionary other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }

            return ReferenceEquals(this, other) || Equals(other._dictionary, _dictionary);
        }


        /// <summary>
        ///     Provides the implementation for operations that set member values. Classes derived from the
        ///     <see cref="T:System.Dynamic.DynamicObject" /> class can override this method to specify dynamic behavior for
        ///     operations such as setting a value for a property.
        /// </summary>
        /// <returns>
        ///     true if the operation is successful; otherwise, false. If this method returns false, the run-time binder of
        ///     the language determines the behavior. (In most cases, a language-specific run-time exception is thrown.)
        /// </returns>
        /// <param name="binder">
        ///     Provides information about the object that called the dynamic operation. The binder.Name property
        ///     provides the name of the member to which the value is being assigned. For example, for the statement
        ///     sampleObject.SampleProperty = "Test", where sampleObject is an instance of the class derived from the
        ///     <see cref="T:System.Dynamic.DynamicObject" /> class, binder.Name returns "SampleProperty". The binder.IgnoreCase
        ///     property specifies whether the member name is case-sensitive.
        /// </param>
        /// <param name="value">
        ///     The value to set to the member. For example, for sampleObject.SampleProperty = "Test", where
        ///     sampleObject is an instance of the class derived from the <see cref="T:System.Dynamic.DynamicObject" /> class, the
        ///     <paramref name="value" /> is "Test".
        /// </param>
        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            this[binder.Name] = value;
            return true;
        }

        /// <summary>
        ///     Provides the implementation for operations that get member values. Classes derived from the
        ///     <see cref="T:System.Dynamic.DynamicObject" /> class can override this method to specify dynamic behavior for
        ///     operations such as getting a value for a property.
        /// </summary>
        /// <returns>
        ///     true if the operation is successful; otherwise, false. If this method returns false, the run-time binder of
        ///     the language determines the behavior. (In most cases, a run-time exception is thrown.)
        /// </returns>
        /// <param name="binder">
        ///     Provides information about the object that called the dynamic operation. The binder.Name property
        ///     provides the name of the member on which the dynamic operation is performed. For example, for the
        ///     Console.WriteLine(sampleObject.SampleProperty) statement, where sampleObject is an instance of the class derived
        ///     from the <see cref="T:System.Dynamic.DynamicObject" /> class, binder.Name returns "SampleProperty". The
        ///     binder.IgnoreCase property specifies whether the member name is case-sensitive.
        /// </param>
        /// <param name="result">
        ///     The result of the get operation. For example, if the method is called for a property, you can
        ///     assign the property value to <paramref name="result" />.
        /// </param>
        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            if (!_dictionary.TryGetValue(binder.Name, out result))
            {
                result = null;
            }

            return true;
        }

        /// <summary>
        ///     Returns the enumeration of all dynamic member names.
        /// </summary>
        /// <returns>A <see cref="IEnumerable{T}" /> that contains dynamic member names.</returns>
        public override IEnumerable<string> GetDynamicMemberNames() => _dictionary.Keys;

        /// <summary>
        ///     Determines whether the specified <see cref="System.Object" /> is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object" /> to compare with this instance.</param>
        /// <returns>
        ///     <see langword="true" /> if the specified <see cref="System.Object" /> is equal to this instance; otherwise,
        ///     <see langword="false" />.
        /// </returns>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            return obj.GetType() == typeof(DynamicDictionary) && Equals((DynamicDictionary) obj);
        }

        /// <summary>
        ///     Returns a hash code for this <see cref="DynamicDictionary" />.
        /// </summary>
        /// <returns>
        ///     A hash code for this <see cref="DynamicDictionary" />, suitable for use in hashing algorithms and data
        ///     structures like a hash table.
        /// </returns>
        public override int GetHashCode() => _dictionary != null ? _dictionary.GetHashCode() : 0;

        private KeyValuePair<string, dynamic> GetDynamicKeyValuePair(KeyValuePair<string, dynamic> item)
        {
            var dynamicValueKeyValuePair =
                new KeyValuePair<string, dynamic>(item.Key, item.Value);
            return dynamicValueKeyValuePair;
        }

        private static string GetNeutralKey(string key) => key.Replace("-", string.Empty);

        /// <summary>
        ///     Gets a typed Dictionary of <see cref="T:Dictionary{String, Object}" /> from <see cref="DynamicDictionary" />
        /// </summary>
        /// <returns>
        ///     Gets a typed Dictionary of <see cref="T:Dictionary{String, Object}" /> from <see cref="DynamicDictionary" />
        /// </returns>
        public Dictionary<string, object> ToDictionary()
        {
            var data = new Dictionary<string, object>();

            foreach (var item in _dictionary)
            {
                var newKey = item.Key;
                var newValue = item.Value as object;

                data.Add(newKey, newValue);
            }

            return data;
        }
    }
}