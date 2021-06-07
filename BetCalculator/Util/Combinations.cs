using System;
using System.Collections;
using System.Collections.Generic;

namespace BetCalculator.Util
{
    public class Combinations<T> : IEnumerator<IList<T>>
    {
        private readonly IList<T> _items;
        private readonly int _length;
        private int _variableCount;
        private int[] _indexes;
        private bool _hasNext;

        public Combinations(IList<T> items, int length)
        {
            if (length < 0)
                throw new ArgumentException("length must be positive");
            if (items.Count < length)
                throw new ArgumentException("items count must be greater or equal to length");
            _items = items;
            _length = length;
            Reset();
        }

        public void Reset()
        {
            _variableCount = _items.Count - _length;
            _indexes = new int[_length];
            _hasNext = _items.Count > 0;
            for (var i = 0; i < _length; i++)
                _indexes[i] = i;
        }

        public bool MoveNext() => _hasNext;

        public IList<T> Current
        {
            get
            {
                var next = new List<T>(_length);
                for (var i = 0; i < _length; i++)
                    next.Add(_items[_indexes[i]]);
                if (_hasNext)
                    MoveIndex();
                return next;
            }
        }

        object IEnumerator.Current => Current;

        public void Dispose() {}

        private void MoveIndex()
        {
            var i = NextDiffIndex();
            if (i >= 0)
            {
                _indexes[i]++;
                for (var j = i + 1; j < _length; j++)
                    _indexes[j] = _indexes[j - 1] + 1;
            }
            else
                _hasNext = false;
        }

        private int NextDiffIndex()
        {
            for (var i = _length - 1; i >= 0; i--)
            {
                if (_indexes[i] < _variableCount + i)
                    return i;
            }

            return -1;
        }
    }
}