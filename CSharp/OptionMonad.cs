using System;

namespace Monads
{
    public static class OptionMonad
    {
        public static Option<B> Bind<A, B>(
            this Option<A> ma,
            Func<A, Option<B>> f)
        {
            throw new NotImplementedException();
        }

        // Select
        public static Option<B> Map<A, B>(this Option<A> opt, Func<A, B> f)
        {
            throw new NotImplementedException();
        }

















        public static Option<C> SelectMany<A, B, C>(
            this Option<A> ma,
            Func<A, Option<B>> f,
            Func<A, B, C> select)
        {
            throw new NotImplementedException();
        }
    }
}