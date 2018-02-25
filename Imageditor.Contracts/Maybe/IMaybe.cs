namespace Imageditor.Contracts.Maybe
{
    public interface IMaybe<T>
    {
        T Value { get; }

        bool IsJust { get; }
    }
}
