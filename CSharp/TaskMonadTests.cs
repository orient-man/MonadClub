using System.Threading.Tasks;
using FluentAssertions;
using Xunit;

namespace Monads
{
    public class TaskMonadTests
    {
        [Fact]
        public void composition_with_linq_monad()
        {
            var r =
                from a in Compute5()
                from b in Compute7()
                from c in Add(a, b)
                select c * 2;

            r.Result.Should().Be(24);
        }

        [Fact]
        public void composition_with_callbacks_simplified_version()
        {
            var r =
                Compute5()
                    .ContinueWith(
                        ma => Compute7()
                            .ContinueWith(
                                mb => Add(ma.Result, mb.Result)
                                    .ContinueWith(mc => 2 * mc.Result))
                            .Unwrap())
                    .Unwrap();

            r.Result.Should().Be(24);
        }

        [Fact]
        public async void composition_with_async_await()
        {
            var a = await Compute5();
            var b = await Compute7();
            var c = await Add(a, b);
            var r = c * 2;
            r.Should().Be(24);
        }

        private static Task<int> Compute5()
        {
            return 5.ToTask();
        }

        private static Task<int> Compute7()
        {
            return 7.ToTask();
        }

        private static Task<int> Add(int x, int y)
        {
            return (x + y).ToTask();
        }
    }
}