using System;
using System.Collections.Generic;
using System.IO;
using PipServices3.Expressions.IO;
using Xunit;

namespace PipServices3.Expressions.Tokenizers.Generic
{
    /// <summary>
    /// Contains unit tests for Tokenizer package classes.
    /// </summary>
    public class GenericTokenizerTest
    {
        [Fact]
        public void TestPushbackReader()
        {
            const string TestString = "This is a test string.";
            StringReader stringReader = new StringReader(TestString);
            TextPushbackReader reader = new TextPushbackReader(stringReader);

            Assert.Equal('T', reader.Read());
            Assert.Equal('h', reader.Read());
            Assert.Equal('i', reader.Read());
            Assert.Equal('s', reader.Read());

            Assert.Equal(' ', reader.Peek());
            Assert.Equal(' ', reader.Peek());
            Assert.Equal(' ', reader.Read());
            Assert.Equal('i', reader.Peek());

            reader.Pushback('#');
            Assert.Equal('#', reader.Peek());
            Assert.Equal('#', reader.Read());

            reader.Pushback('$');
            reader.Pushback('%');
            Assert.Equal('%', reader.Read());
            Assert.Equal('$', reader.Read());

            reader.Pushback('$');
            reader.PushbackString("%@");
            Assert.Equal('%', reader.Read());
            Assert.Equal('@', reader.Read());
            Assert.Equal('$', reader.Read());

            reader.PushbackString("ABC");
            Assert.Equal('A', reader.Peek());
            Assert.Equal('A', reader.Read());
            Assert.Equal('B', reader.Read());
            Assert.Equal('C', reader.Read());
            Assert.Equal('i', reader.Read());
        }

        [Fact]
        public void TestExpression()
        {
            string tokenString = "A+B/123 - \t 'xyz'\n <>-10.11# This is a comment";
            Token[] expectedTokens = new Token[] {
                new Token(TokenType.Word, "A"), new Token(TokenType.Symbol, "+"),
                new Token(TokenType.Word, "B"), new Token(TokenType.Symbol, "/"),
                new Token(TokenType.Integer, "123"), new Token(TokenType.Whitespace, " "),
                new Token(TokenType.Symbol, "-"), new Token(TokenType.Whitespace, " \t "),
                new Token(TokenType.Quoted, "'xyz'"), new Token(TokenType.Whitespace, "\n "),
                new Token(TokenType.Symbol, "<>"), new Token(TokenType.Float, "-10.11"),
                new Token(TokenType.Comment, "# This is a comment"), new Token(TokenType.Eof, null)};

            ITokenizer tokenizer = new GenericTokenizer();
            IList<Token> tokenList = tokenizer.TokenizeBuffer(tokenString);

            TokenizerFixture.AssertAreEqualsTokenLists(expectedTokens, tokenList);
        }

        [Fact]
        public void TestQuoteToken()
        {
            string tokenString = "A'xyz'\"abc\ndeg\" 'jkl\"def'";
            Token[] expectedTokens = new Token[] {
                new Token(TokenType.Word, "A"), new Token(TokenType.Quoted, "xyz"),
                new Token(TokenType.Quoted, "abc\ndeg"), new Token(TokenType.Whitespace, " "),
                new Token(TokenType.Quoted, "jkl\"def")};

            ITokenizer tokenizer = new GenericTokenizer();
            tokenizer.SkipEof = true;
            tokenizer.DecodeStrings = true;
            IList<Token> tokenList = tokenizer.TokenizeBuffer(tokenString);

            TokenizerFixture.AssertAreEqualsTokenLists(expectedTokens, tokenList);
        }

        [Fact]
        public void TestWordToken()
        {
            string tokenString = "A'xyz'Ebf_2\n2x_2";
            Token[] expectedTokens = new Token[] {
                new Token(TokenType.Word, "A"), new Token(TokenType.Quoted, "xyz"),
                new Token(TokenType.Word, "Ebf_2"), new Token(TokenType.Whitespace, "\n"),
                new Token(TokenType.Integer, "2"), new Token(TokenType.Word, "x_2")};

            ITokenizer tokenizer = new GenericTokenizer();
            tokenizer.SkipEof = true;
            tokenizer.DecodeStrings = true;
            IList<Token> tokenList = tokenizer.TokenizeBuffer(tokenString);

            TokenizerFixture.AssertAreEqualsTokenLists(expectedTokens, tokenList);
        }

        [Fact]
        public void TestNumberToken()
        {
            string tokenString = "123-321 .543-.76-. -123.456";
            Token[] expectedTokens = new Token[] {
                new Token(TokenType.Integer, "123"), new Token(TokenType.Integer, "-321"),
                new Token(TokenType.Whitespace, " "), new Token(TokenType.Float, ".543"),
                new Token(TokenType.Float, "-.76"), new Token(TokenType.Symbol, "-"),
                new Token(TokenType.Symbol, "."), new Token(TokenType.Whitespace, " "),
                new Token(TokenType.Float, "-123.456")};

            ITokenizer tokenizer = new GenericTokenizer();
            tokenizer.SkipEof = true;
            tokenizer.DecodeStrings = true;
            IList<Token> tokenList = tokenizer.TokenizeBuffer(tokenString);

            TokenizerFixture.AssertAreEqualsTokenLists(expectedTokens, tokenList);
        }

        [Fact]
        public void TestWrongToken()
        {
            var tokenString = "1>2";
            var expectedTokens = new Token[] {
                new Token(TokenType.Integer, "1"),
                new Token(TokenType.Symbol, ">"),
                new Token(TokenType.Integer, "2"),

            };

            var tokenizer = new GenericTokenizer();
            tokenizer.SkipEof = true;
            var tokenList = tokenizer.TokenizeBuffer(tokenString);

            TokenizerFixture.AssertAreEqualsTokenLists(expectedTokens, tokenList);
        }
    }
}
