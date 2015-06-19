using Xunit;

namespace Monads.SimpleParser
{
    public class MiniMLParserTests
    {
        [Fact]
        public void parses_ml()
        {
            var parser = new MiniMLParserFromString();
            var result =
                parser.All(@"let true = \x.\y.x in 
                         let false = \x.\y.y in 
                         let if = \b.\l.\r.(b l) r in
                         if true then false else true;");
        }

        private class MiniMLParserFromString : MiniMLParsers<string>
        {
            public override Parser<string, char> AnyChar
            {
                get
                {
                    {
                        return input => input.Length > 0
                            ? new Result<string, char>(input[0], input.Substring(1))
                            : null;
                    }
                }
            }
        }
    }
}