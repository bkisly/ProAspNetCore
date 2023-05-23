using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleApp.Tests
{
    public class Comparer
    {
        public static Comparer<U?> Get<U>(Func<U?, U?, bool> predicate)
        {
            return new Comparer<U?>(predicate);
        }
    }

    public class Comparer<T> : IEqualityComparer<T>
    {
        private readonly Func<T?, T?, bool> _predicate;

        public Comparer(Func<T?, T?, bool> predicate)
        {
            _predicate = predicate;
        }

        public bool Equals(T? x, T? y)
        {
            return _predicate(x, y);
        }

        public int GetHashCode(T obj)
        {
            return obj?.GetHashCode() ?? 0;
        }
    }
}
