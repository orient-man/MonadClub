using System.Linq;
using FluentAssertions;
using Sprache;
using Xunit;

namespace Monads
{
    public class ParserMonad
    {
        [Fact]
        public void parses_identifiers()
        {
            var identifier =
                from leading in Parse.WhiteSpace.Many()
                from first in Parse.Letter.Once()
                from rest in Parse.LetterOrDigit.Many()
                from trailing in Parse.WhiteSpace.Many()
                select new string(first.Concat(rest).ToArray());

            Assert.Throws<ParseException>(() => identifier.Parse(" "));
            Assert.Throws<ParseException>(() => identifier.Parse("123"));
            identifier.Parse("abc").Should().Be("abc");
            identifier.Parse("abc123").Should().Be("abc123");
            identifier.Parse(" abc123  ").Should().Be("abc123");
        }
    }
}