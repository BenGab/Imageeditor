using System;

namespace Imageditor.Contracts.Maybe
{
    public class Just<T> : IMaybe<T>
    {
        private readonly T _value;
        public bool IsJust => true;

        public Just(T value)
        {
            if(value == null)
            {
                throw new ArgumentNullException();
            }

            _value = value;
        }

        public T Value => _value;      
    }
}
