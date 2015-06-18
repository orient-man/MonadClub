namespace Monads
{
    public enum OptionType
    {
        Some,
        None
    };

    public abstract class Option<T>
    {
        public OptionType Tag { get; private set; }

        protected Option(OptionType tag)
        {
            this.Tag = tag;
        }

        public bool MatchSome(out T value)
        {
            value = Tag == OptionType.Some ? ((Some<T>)this).Value : default(T);
            return Tag == OptionType.Some;
        }
    }

    public class None<T> : Option<T>
    {
        public None() : base(OptionType.None)
        {
        }
    }

    public class Some<T> : Option<T>
    {
        public Some(T value) : base(OptionType.Some)
        {
            Value = value;
        }

        public T Value { get; private set; }
    }

    public static class Option
    {
        public static Option<T> None<T>()
        {
            return new None<T>();
        }

        public static Option<T> Some<T>(T value)
        {
            return new Some<T>(value);
        }
    }
}