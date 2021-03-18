using System;
using System.Collections.Generic;
using System.Text;

using PipServices3.Expressions.Tokenizers;
using Xunit;

namespace PipServices3.Expressions.Mustache.Tokenizers
{
    public class MustacheTokenizerTest
    {
        [Fact]
        public void TestTemplate1()
        {
            string tokenString = "Hello, {{ Name }}!";
            Token[] expectedTokens =
            {
                new Token(TokenType.Special, "Hello, "),
                new Token(TokenType.Symbol, "{{"),
                new Token(TokenType.Whitespace, " "),
                new Token(TokenType.Word, "Name"),
                new Token(TokenType.Whitespace, " "),
                new Token(TokenType.Symbol, "}}"),
                new Token(TokenType.Special, "!"),
            };

            var tokenizer = new MustacheTokenizer();
            tokenizer.SkipEof = true;
            var tokenList = tokenizer.TokenizeBuffer(tokenString);

            TokenizerFixture.AssertAreEqualsTokenLists(expectedTokens, tokenList);
        }

        [Fact]
        public void TestTemplate2()
        {
            string tokenString = "Hello, {{{ Name }}}!";
            Token[] expectedTokens =
            {
                new Token(TokenType.Special, "Hello, "),
                new Token(TokenType.Symbol, "{{{"),
                new Token(TokenType.Whitespace, " "),
                new Token(TokenType.Word, "Name"),
                new Token(TokenType.Whitespace, " "),
                new Token(TokenType.Symbol, "}}}"),
                new Token(TokenType.Special, "!"),
            };

            var tokenizer = new MustacheTokenizer();
            tokenizer.SkipEof = true;
            var tokenList = tokenizer.TokenizeBuffer(tokenString);

            TokenizerFixture.AssertAreEqualsTokenLists(expectedTokens, tokenList);
        }

        [Fact]
        public void TestTemplate3()
        {
            string tokenString = "{{ Name }}}";
            Token[] expectedTokens =
            {
                new Token(TokenType.Symbol, "{{"),
                new Token(TokenType.Whitespace, " "),
                new Token(TokenType.Word, "Name"),
                new Token(TokenType.Whitespace, " "),
                new Token(TokenType.Symbol, "}}}")
            };

            var tokenizer = new MustacheTokenizer();
            tokenizer.SkipEof = true;
            var tokenList = tokenizer.TokenizeBuffer(tokenString);

            TokenizerFixture.AssertAreEqualsTokenLists(expectedTokens, tokenList);
        }

        [Fact]
        public void TestTemplate4()
        {
            string tokenString = "Hello, World!";
            Token[] expectedTokens =
            {
                 new Token(TokenType.Special, "Hello, World!")
            };

            var tokenizer = new MustacheTokenizer();
            tokenizer.SkipEof = true;
            var tokenList = tokenizer.TokenizeBuffer(tokenString);

            TokenizerFixture.AssertAreEqualsTokenLists(expectedTokens, tokenList);
        }
    }
}
