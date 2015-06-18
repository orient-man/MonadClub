using System;
using System.Threading.Tasks;

namespace Monads
{
    public static class TaskExtensions
    {
        public static Task<T> ToTask<T>(this T value) // aka "unit" or "return"
        {
            return Task<T>.Factory.StartNew(() => value);
        }

        public static Task<B> Bind<A, B>(this Task<A> ma, Func<A, Task<B>> func)
        {
            return ma.ContinueWith(prev => func(prev.Result)).Unwrap();
        }

        public static Task<C> SelectMany<A, B, C>(
            this Task<A> ma,
            Func<A, Task<B>> func,
            Func<A, B, C> select)
        {
            return ma.Bind(a => func(a).Bind(b => select(a, b).ToTask()));
        }
    }
}