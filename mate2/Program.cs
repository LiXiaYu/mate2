using System;
using Antlr4.Runtime;

namespace mate2
{
    class Program
    {
        static void Main(string[] args)
        {
            string input = @"mate 猫猫{string laji=""ddddhe和谐dee\n\n"";};";
            string output = "";
            var stream = new AntlrInputStream(input);
            var lexer = new MateGrammarLexer(stream);
            var tokens = new CommonTokenStream(lexer);
            var parser = new MateGrammarParser(tokens);
            var tree = parser.program();

            for (var index_token = 0; index_token < tokens.Size; index_token++)
            {

                var token = tokens.Get(index_token);
                Console.WriteLine("tokens: " + token.Text);
            }


            var visitor = new MateGrammarVisitor();
            var result = visitor.Visit(tree);

            output = Convert.ToString(result);

            Console.WriteLine(tree.ToStringTree(parser));
            Console.WriteLine(result);
            //Console.ReadKey();
        }
    }
}
