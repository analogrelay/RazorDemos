using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc.Razor;
using System.Web.Razor.Parser;
using System.Web.Razor.Parser.SyntaxTree;
using System.Web.Razor.Tokenizer.Symbols;

namespace RazorDemoSpike.Infrastructure
{
    public class MyCSharpRazorCodeParser : MvcCSharpRazorCodeParser
    {
        public MyCSharpRazorCodeParser()
            : base()
        {
            MapDirectives(ContentTypeDirective, "contentType");
        }

        private void ContentTypeDirective()
        {
            // Given the following example text, "($)" marks where we are when the parser calls this method
            // @contentType($) Text.Html
            
            // CurrentSymbol is at "contentType"
            AssertDirective("contentType"); // Helper that asserts that we're at a symbol matching the specified directive
            AcceptAndMoveNext(); // Add that symbol to the current span
            
            // Now, we accept all the intermediate whitespace
            AcceptWhile(Language.IsWhiteSpace);

            // This first part is a "Meta Code" span, it should be coloured Yellow, like the "@inherits " etc.
            Output(SpanKind.MetaCode, AcceptedCharacters.None);

            // This second part should be generated using our custom ContentTypeCodeGenerator:
            Span.CodeGenerator = new ContentTypeCodeGenerator();

            // Now we should be at another identifier, like "Text" in "Text.Html"
            if (!At(CSharpSymbolType.Identifier))
            {
                // Error!
                Context.OnError(CurrentLocation, "contextType should be followed by an identifier, like 'Text'");

                // Just read to the end of the line and abort
                AcceptUntil(CSharpSymbolType.NewLine);
                Optional(CSharpSymbolType.NewLine);
                Output(SpanKind.Code, AcceptedCharacters.Any);
                return;
            }

            AcceptAndMoveNext();

            // We should be at a dot
            if (!Optional(CSharpSymbolType.Dot))
            {
                // Error!
                Context.OnError(CurrentLocation, "contextType should be followed by two dot-separated identifiers!");

                // Just read to the end of the line and abort
                AcceptUntil(CSharpSymbolType.NewLine);
                Optional(CSharpSymbolType.NewLine);
                Output(SpanKind.Code, AcceptedCharacters.Any);
                return;
            }

            // Now we should be at the final identifier, like "Html" in "Text.Html"
            if (!At(CSharpSymbolType.Identifier))
            {
                // Error!
                Context.OnError(CurrentLocation, "contextType should be followed by two dot-separated identifiers!");

                // Just read to the end of the line and abort
                AcceptUntil(CSharpSymbolType.NewLine);
                Optional(CSharpSymbolType.NewLine);
                Output(SpanKind.Code, AcceptedCharacters.Any);
                return;
            }

            AcceptAndMoveNext();

            // Accept any whitespace and the newline
            AcceptWhile(Language.IsWhiteSpace);
            if (!EndOfFile && !At(CSharpSymbolType.NewLine))
            {
                // Error!
                Context.OnError(CurrentLocation, "contextType should end with a newline!");

                // Just read to the end of the line and abort
                AcceptUntil(CSharpSymbolType.NewLine);
                Optional(CSharpSymbolType.NewLine);
                Output(SpanKind.Code, AcceptedCharacters.Any);
                return;
            }

            // Finally, output the completed span
            Output(SpanKind.Code, AcceptedCharacters.Any);
        }
    }
}
