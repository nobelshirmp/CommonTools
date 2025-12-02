
using System;

namespace CommonTools
{

    public class RecatValue<T> : IDisposable
    {
        private T _value;

        public RecatValue(T value = default)
        {
            _value = value;
        }

        public T Value
        {
            get => _value;
            set
            {
                _value = value;
                OnChanged?.Invoke(_value);
            }
        }

        private event Action<T> OnChanged;

        public event Action<T> OnChangedValue
        {
            add
            {
                OnChanged += value;
                value?.Invoke(_value);
            }
            remove
            {
                OnChanged -= value;
            }
        }

        public void Dispose()
        {
            _value = default;
            OnChanged = null;

            GC.SuppressFinalize(this);
        }
    }
}