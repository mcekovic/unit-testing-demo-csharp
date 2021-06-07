using System;
using System.Collections;
using System.Collections.Generic;

namespace BetCalculator.Util
{
    public class CompositeEnumerator<T> : IEnumerator<T>
    {
        private readonly IEnumerable<IEnumerator<T>> _enumerators;
        private IEnumerator<IEnumerator<T>> _enumerator;
        private IEnumerator<T> _current;

        public CompositeEnumerator(IEnumerable<IEnumerator<T>> enumerators)
        {
            _enumerators = enumerators;
            Reset();
        }

        public void Reset()
        {
            _enumerator = _enumerators.GetEnumerator();
            if (_enumerator.MoveNext())
                _current = _enumerator.Current;
        }

        public bool MoveNext()
        {
            if (_current != null && _current.MoveNext())
                return true;
            if (_enumerator.MoveNext())
            {
                _current = _enumerator.Current;
                return _current.MoveNext();
            }
            return false;
        }

        public T Current => _current != null ? _current.Current : throw new InvalidOperationException("Enumerator is empty");

        object IEnumerator.Current => Current;

        public void Dispose() {}
    }
}