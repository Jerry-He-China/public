using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;

namespace UnityExtendDemo
{
    public sealed class PooledLifetimeManager : LifetimeManager, IDisposable
    {
        private const int DefaultPoolSize = 10;
        private readonly List<Object> _pool;
        private int _index = -1;

        public PooledLifetimeManager(int poolSize)
        {
            _pool = new List<object>(poolSize);
        }

        public PooledLifetimeManager():this(DefaultPoolSize){ }

        public override object GetValue()
        {
            if (_pool.Count < _pool.Capacity)
            {
                return null;
            }
            else
            {
                if (++_index == _pool.Capacity)
                {
                    _index = 0;
                }

                return _pool[_index];
            }
        }

        public override void SetValue(object newValue)
        {
            _pool.Add(newValue);
        }

        public override void RemoveValue()
        {
            Dispose();
        }

        public void Dispose()
        {
            foreach (var disposable in _pool.OfType<IDisposable>())
            {
                disposable.Dispose();
            }
            _pool.Clear();
            _index = -1;
        }
    }
}
