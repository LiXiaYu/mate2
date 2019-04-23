using System;
using Antlr4.Runtime;

namespace mate2
{
    class Program
    {
        static void Main(string[] args)
        {
            string input = @"mate a {};";
            string output = "";
            var stream = new AntlrInputStream(input);
            var lexer = new MateGrammarLexer(stream);
            var tokens = new CommonTokenStream(lexer);
            var parser = new MateGrammarParser(tokens);
            var tree = parser.program();

            var visitor = new MateGrammarVisitor();
            var result = visitor.Visit(tree);

            output = Convert.ToString(result);

            Console.WriteLine(tree.ToStringTree(parser));
            Console.WriteLine(result);
            //Console.ReadKey();
        }
    }
}
