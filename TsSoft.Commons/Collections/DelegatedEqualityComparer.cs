using System;
using System.Collections.Generic;

namespace TsSoft.Commons.Collections
{
    /// <summary>
    /// Поддержка операций сравнения объектов в отношении равенства на основе лямбда-выражений
    /// <example> 
    /// <code>
    /// new EqualityComparer<Tuple<DateTime, int>>(x => x.Item1.GetHashCode()); 
    /// </code>
    /// </example>    
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public class DelegatedEqualityComparer<T> : IEqualityComparer<T>
    {
        public Func<T, T, bool> EqualsFunc { get { return equalsFunc; } }
        public Func<T, int> GetHashCodeFunc { get { return getHashCodeFunc; } }
        private Func<T, T, bool> equalsFunc;
        private Func<T, int> getHashCodeFunc;

        public DelegatedEqualityComparer(Func<T, int> getHashCodeFunc)
            : this((x, y) => getHashCodeFunc(x) == getHashCodeFunc(y), getHashCodeFunc) { }

        public DelegatedEqualityComparer(Func<T, T, bool> equalsFunc)
            : this(equalsFunc, x => x.GetHashCode()) { }

        public DelegatedEqualityComparer(Func<T, T, bool> equalsFunc, Func<T, int> getHashCodeFunc)
        {
            this.equalsFunc = equalsFunc;
            this.getHashCodeFunc = getHashCodeFunc;
        }

        public bool Equals(T x, T y)
        {
            return equalsFunc(x, y);
        }

        public int GetHashCode(T obj)
        {
            return getHashCodeFunc(obj);
        }
    }
}
