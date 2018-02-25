using Imageditor.Contracts.Maybe;

namespace Imageditor.Contracts
{
    public static class MaybeExtensions
    {
        public static IMaybe<T> ToMaybe<T>(this T value)
        {
            return new Just<T>(value);
        }
    }
}
