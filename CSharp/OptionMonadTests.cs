using FluentAssertions;
using Xunit;

namespace Monads
{
    public class OptionMonadTests
    {
        [Theory]
        [InlineData("10", "5", true, 4)]
        [InlineData("bla bla", "5", false, 0)]
        [InlineData("10", "foo", false, 0)]
        [InlineData("0", "5", true, 0)]
        [InlineData("10", "0", false, 0)]
        public void safe_division(
            string astring,
            string bstring,
            bool hasValue,
            int expectedValue = 0)
        {
            // r = (a / b) * 2
            var r = Option.None<int>();
            var ma = TryParse(astring);
            int a;
            if (ma.MatchSome(out a))
            {
                var mb = TryParse(bstring);
                int b;
                if (mb.MatchSome(out b))
                {
                    var mc = SafeDiv(a, b);
                    int c;
                    if (mc.MatchSome(out c))
                    {
                        r = Option.Some(c * 2);
                    }
                }
            }

            int value;
            r.MatchSome(out value).Should().Be(hasValue);
            value.Should().Be(expectedValue);
        }

        private static Option<int> TryParse(string value)
        {
            int result;
            return int.TryParse(value, out result)
                ? Option.Some(result)
                : Option.None<int>();
        }

        private static Option<int> SafeDiv(int a, int b)
        {
            return b != 0 ? Option.Some(a / b) : Option.None<int>();
        }
    }
}
