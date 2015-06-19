using System;
using System.Linq;

namespace Monads.SimpleParser
{
    // Term and its derived classes define the AST for terms in the MiniML language.
    public abstract class Term
    {
    }

    public class LambdaTerm : Term
    {
        public readonly string Ident;
        public readonly Term Term;

        public LambdaTerm(string i, Term t)
        {
            Ident = i;
            Term = t;
        }
    }

    public class LetTerm : Term
    {
        public readonly string Ident;
        public readonly Term Rhs;
        public Term Body;

        public LetTerm(string i, Term r, Term b)
        {
            Ident = i;
            Rhs = r;
            Body = b;
        }
    }

    public class AppTerm : Term
    {
        public readonly Term Func;
        public readonly Term[] Args;

        public AppTerm(Term func, Term[] args)
        {
            Func = func;
            Args = args;
        }
    }

    public class VarTerm : Term
    {
        public readonly string Ident;

        public VarTerm(string ident)
        {
            Ident = ident;
        }
    }

    // Provides a set of parsers for the MiniML Language defined above.  
    public abstract class MiniMLParsers<TInput> : CharParsers<TInput>
    {
        public MiniMLParsers()
        {
            Whitespace = Rep(Char(' ').OR(Char('\t').OR(Char('\n')).OR(Char('\r'))));
            WsChr = chr => Whitespace.AND(Char(chr));
            Id = from w in Whitespace
                from c in Char(char.IsLetter)
                from cs in Rep(Char(char.IsLetterOrDigit))
                select cs.Aggregate(c.ToString(), (acc, ch) => acc + ch);
            Ident = from s in Id where s != "let" && s != "in" select s;
            LetId = from s in Id where s == "let" select s;
            InId = from s in Id where s == "in" select s;
            Term1 = (from x in Ident
                select (Term)new VarTerm(x))
                .OR(
                    (from u1 in WsChr('(')
                        from t in Term
                        from u2 in WsChr(')')
                        select t));
            Term = (from u1 in WsChr('\\')
                from x in Ident
                from u2 in WsChr('.')
                from t in Term
                select (Term)new LambdaTerm(x, t))
                .OR(
                    (from letid in LetId
                        from x in Ident
                        from u1 in WsChr('=')
                        from t in Term
                        from inid in InId
                        from c in Term
                        select (Term)new LetTerm(x, t, c)))
                .OR(
                    (from t in Term1
                        from ts in Rep(Term1)
                        select (Term)new AppTerm(t, ts)));
            All = from t in Term from u in WsChr(';') select t;
        }

        public Parser<TInput, char[]> Whitespace;
        public Func<char, Parser<TInput, char>> WsChr;
        public Parser<TInput, string> Id;
        public Parser<TInput, string> Ident;
        public Parser<TInput, string> LetId;
        public Parser<TInput, string> InId;
        public Parser<TInput, Term> Term;
        public Parser<TInput, Term> Term1;
        public Parser<TInput, Term> All;
    }
}