using System;
using System.Collections.Generic;

namespace TsSoft.Commons.Collections
{
    public interface IIterator<out T>
    {
        bool HasNext { get; }

        T Next();
    }

    public class Iterator<T> : IIterator<T>
    {
        private IEnumerator<T> enumerator;

        public Iterator(IEnumerable<T> enumerable)
            : this(enumerable != null ? enumerable.GetEnumerator() : null)
        {
            
        }

        public Iterator(IEnumerator<T> enumerator)
        {
            this.enumerator = enumerator;
            MoveNext();
        }

        public bool HasNext { get; private set; }

        public T Next()
        {
            if (!HasNext)
            {
                throw new InvalidOperationException();
            }
            var previous = enumerator.Current;
            MoveNext();
            return previous;
        }

        private void MoveNext()
        {
            HasNext = enumerator != null && enumerator.MoveNext();
        }
    }
}