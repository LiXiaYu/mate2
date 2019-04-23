using System;
using System.Collections.Generic;
using System.Text;

using Antlr4.Runtime;
using Antlr4.Runtime.Misc;


namespace mate2
{
    public class MateGrammarVisitor : MateGrammarBaseVisitor<object>
    {
        Dictionary<string, object> memory = new Dictionary<string, object>();

        public string last_texts = "";

        //public override object VisitExpression([NotNull] MateGrammarParser.ExpressionContext context)
        //{
        //    return base.VisitExpression(context);
        //}

        public override object VisitBlock([NotNull] MateGrammarParser.BlockContext context)
        {
            var text = "{\n" + Convert.ToString(Visit(context.expression())) + " \n}";
            return text;
        }

        public override object VisitMateDefineWithBlock([NotNull] MateGrammarParser.MateDefineWithBlockContext context)
        {
            var text= "class" + " " + Convert.ToString(context.ID()) +" \n"+ Convert.ToString(Visit(context.block())) + ";";

            return text;
        }
        public override object VisitMateDefineWithoutBlock([NotNull] MateGrammarParser.MateDefineWithoutBlockContext context)
        {
            var text = Convert.ToString(context.MATE()) + " " + Convert.ToString(context.ID()) + ";";

            return text;
        }
    }
}
