using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace PipServices3.Expressions.Mustache.Parsers
{
    public class MustacheParserTest
    {
        [Fact]
        public void TestLexicalAnalysis()
        {
            MustacheParser parser = new MustacheParser();
            parser.Template = "Hello, {{{NAME}}}{{ #if ESCLAMATION }}!{{/if}}{{{^ESCLAMATION}}}.{{{/ESCLAMATION}}}";

            List<MustacheToken> expectedTokens = new List<MustacheToken>
            {
                new MustacheToken(MustacheTokenType.Value, "Hello, "),
                new MustacheToken(MustacheTokenType.EscapedVariable, "NAME"),
                new MustacheToken(MustacheTokenType.Section, "ESCLAMATION"),
                new MustacheToken(MustacheTokenType.Value, "!"),
                new MustacheToken(MustacheTokenType.SectionEnd, null),
                new MustacheToken(MustacheTokenType.InvertedSection, "ESCLAMATION"),
                new MustacheToken(MustacheTokenType.Value, "."),
                new MustacheToken(MustacheTokenType.SectionEnd, "ESCLAMATION"),
            };

            var tokens = parser.InitialTokens;
            Assert.Equal(expectedTokens.Count, tokens.Count);

            for (int i = 0; i < tokens.Count; i++)
            {
                Assert.Equal(expectedTokens[i].Type, tokens[i].Type);
                Assert.Equal(expectedTokens[i].Value, tokens[i].Value);
            }
        }

        [Fact]
        public void TestSyntaxAnalysis()
        {
            MustacheParser parser = new MustacheParser();
            parser.Template = "Hello, {{{NAME}}}{{ #if ESCLAMATION }}!{{/if}}{{{^ESCLAMATION}}}.{{{/ESCLAMATION}}}";

            List<MustacheToken> expectedTokens = new List<MustacheToken>
            {
                new MustacheToken(MustacheTokenType.Value, "Hello, "),
                new MustacheToken(MustacheTokenType.EscapedVariable, "NAME"),
                new MustacheToken(MustacheTokenType.Section, "ESCLAMATION"),
                new MustacheToken(MustacheTokenType.InvertedSection, "ESCLAMATION"),
            };

            var tokens = parser.ResultTokens;
            Assert.Equal(expectedTokens.Count, tokens.Count);

            for (int i = 0; i < tokens.Count; i++)
            {
                Assert.Equal(expectedTokens[i].Type, tokens[i].Type);
                Assert.Equal(expectedTokens[i].Value, tokens[i].Value);
            }
        }

        [Fact]
        public void TestVariableNames()
        {
            MustacheParser parser = new MustacheParser();
            parser.Template = "Hello, {{{NAME}}}{{ #if ESCLAMATION }}!{{/if}}{{{^ESCLAMATION}}}.{{{/ESCLAMATION}}}";

            Assert.Equal(2, parser.VariableNames.Count);
            Assert.Equal("NAME", parser.VariableNames[0]);
            Assert.Equal("ESCLAMATION", parser.VariableNames[1]);
        }
    }
}
