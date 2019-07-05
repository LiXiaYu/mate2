using System;
using System.Collections.Generic;
using System.Text;

using Antlr4.Runtime;
using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;

namespace mate2
{
    public class MateGrammarVisitor : MateGrammarBaseVisitor<object>
    {
        Dictionary<string, object> memory = new Dictionary<string, object>();

        public string last_texts = "";

        public override object VisitProgram([NotNull] MateGrammarParser.ProgramContext context)
        {
            return base.VisitProgram(context);
        }
        public override object VisitExpressionMateDefine([NotNull] MateGrammarParser.ExpressionMateDefineContext context)
        {
            var text = Convert.ToString(Visit(context.mate_define()));
            return text;
        }
        public override object VisitExpressionVarDefine([NotNull] MateGrammarParser.ExpressionVarDefineContext context)
        {
            var text = (Visit(context.var_define()));
            return text;
        }
        //public override object VisitExpressionNone([NotNull] MateGrammarParser.ExpressionNoneContext context)
        //{
        //    var text = (Visit(context.none_expression()));
        //    return text;
        //}
        //public override object VisitExpressionSchars([NotNull] MateGrammarParser.ExpressionScharsContext context)
        //{
        //    var text = Convert.ToString(context.Schar());
        //    return text;
        //}
        public override object VisitBlock([NotNull] MateGrammarParser.BlockContext context)
        {
            var strExp = "";
            var text=CannotVisit((context.children as List<Antlr4.Runtime.Tree.IParseTree>)/*.GetRange(1, context.children.Count - 2)*/);

            return text;
        }

        public override object VisitMateDefineWithBlock([NotNull] MateGrammarParser.MateDefineWithBlockContext context)
        {
            var text= "class" + " " + Convert.ToString(context.T标识符()) +" \n"+ Convert.ToString(Visit(context.block())) + ";";

            return text;
        }
        public override object VisitMateDefineWithoutBlock([NotNull] MateGrammarParser.MateDefineWithoutBlockContext context)
        {
            var text = Convert.ToString(context.MATE()) + " " + Convert.ToString(context.T标识符()) + ";";

            return text;
        }

        public override object VisitString([NotNull] MateGrammarParser.StringContext context)
        {
            var text = Convert.ToString(context.Stringliteral());
            return text;
        }
        public override object VisitVarDefineWithVarval([NotNull] MateGrammarParser.VarDefineWithVarvalContext context)
        {
            var text = Convert.ToString(context.T类型名()+" "+context.T标识符()+" "+ Visit(context.varval())+";");
            return text;
        }
        public override object VisitVarval([NotNull] MateGrammarParser.VarvalContext context)
        {
            var text = Convert.ToString(Visit(context.@string()));
            return text;
        }

        /// <summary>
        /// 对没有被语法包括的内容进行处理
        /// </summary>
        /// <param name="contextChildren"></param>
        /// <returns></returns>
        public object CannotVisit(List<Antlr4.Runtime.Tree.IParseTree> contextChildren)
        {
            var strExp = "";

            foreach(var context in contextChildren)
            {
                var vc = Visit(context);
                if(vc!=null)
                {
                    return vc;
                }
                else
                {
                    if(context.ChildCount==0)
                    {
                        strExp += context + " ";
                    }
                    else
                    {
                        var cclist = new List<Antlr4.Runtime.Tree.IParseTree>();
                        for (var index_cc = 0; index_cc < context.ChildCount; index_cc++)
                        {
                            var sExpC = context.GetChild(index_cc);
                            cclist.Add(sExpC);
                        }

                        strExp += CannotVisit(cclist)+"";
                    }

                }
            }


            return strExp;
        }


        //public override object Visit([NotNull] IParseTree tree)
        //{
        //    var strExp = "";

        //    var vc = base.Visit(tree);
        //    if (vc != null)
        //    {
        //        strExp+=VisitChildren(tree)
        //    }
        //    else
        //    {
        //        if (tree.ChildCount == 0)
        //        {
        //            strExp += tree + " ";
        //        }
        //        else
        //        {
        //            var cclist = new List<Antlr4.Runtime.Tree.IParseTree>();
        //            for (var index_cc = 0; index_cc < tree.ChildCount; index_cc++)
        //            {
        //                var sExpC = tree.GetChild(index_cc);
        //                strExp += Visit(sExpC) + "";
        //            }
                    
        //        }
        //    }



        //    return strExp;
        //}
    }
}
