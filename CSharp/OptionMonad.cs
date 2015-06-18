using System;

namespace Monads
{
    public static class OptionMonad
    {
        public static Option<B> Bind<A, B>(
            this Option<A> ma,
            Func<A, Option<B>> f)
        {
            A value;
            return ma.MatchSome(out value) ? f(value) : Option.None<B>();
        }

        public static Option<B> Map<A, B>(this Option<A> opt, Func<A, B> f)
        {
            A value;
            return opt.MatchSome(out value)
                ? Option.Some(f(value))
                : Option.None<B>();
        }

        public static Option<C> SelectMany<A, B, C>(
            this Option<A> ma,
            Func<A, Option<B>> f,
            Func<A, B, C> select)
        {
            //return ma.Bind(a => f(a).Map(b => select(a, b)));
            return ma.Bind(a => f(a).Bind(b => Option.Some(select(a, b))));
        }
    }
}