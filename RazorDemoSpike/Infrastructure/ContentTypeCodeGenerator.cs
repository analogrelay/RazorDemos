using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Razor.Generator;
using System.Web.Razor.Parser.SyntaxTree;

namespace RazorDemoSpike.Infrastructure
{
    public class ContentTypeCodeGenerator : SpanCodeGenerator
    {
        private const string Prefix = "Response.ContentType = ContentTypes.";

        public override void GenerateCode(Span target, CodeGeneratorContext context)
        {
            // Build the string
            string code = Prefix + target.Content + ";";

            // Calculate the line pragma including information about where the user-specified code starts and ends (for editors)
            CodeLinePragma pragma = context.GenerateLinePragma(target, Prefix.Length, target.Content.Length);

            // Add the statement
            context.AddStatement(code, pragma);
        }
    }
}
