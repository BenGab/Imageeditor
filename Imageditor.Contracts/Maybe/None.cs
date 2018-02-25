using System;

namespace Imageditor.Contracts.Maybe
{
    public class None<T> : IMaybe<T>
    {
        public bool IsJust => false;

        public T Value
        {
            get
            {
                throw new NullReferenceException();
            }
        }
    }
}
