namespace Monads.SimpleParser
{
    // The result of a parse consists of a value and the unconsumed input.
    public class Result<TInput, TValue>
    {
        public readonly TValue Value;
        public readonly TInput Rest;

        public Result(TValue value, TInput rest)
        {
            Value = value;
            Rest = rest;
        }
    }
}