namespace Monads.SimpleParser
{
    // A Parser is a delegate which takes an input and returns a result.
    public delegate Result<TInput, TValue> Parser<TInput, TValue>(TInput input);
}