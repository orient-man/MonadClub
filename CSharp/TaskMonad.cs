using System;
using System.Threading.Tasks;

namespace Monads
{
    public static class TaskMonad
    {
        public static Task<T> ToTask<T>(this T value) // aka "unit" or "return"
        {
            return Task<T>.Factory.StartNew(() => value);
        }

        public static Task<B> Bind<A, B>(this Task<A> ma, Func<A, Task<B>> func)
        {
            throw new NotImplementedException();
        }

        public static Task<C> SelectMany<A, B, C>(
            this Task<A> ma,
            Func<A, Task<B>> f,
            Func<A, B, C> select)
        {
            throw new NotImplementedException();
        }
    }
}