using System;
using System.Collections.Generic;
using PipServices3.Expressions.Tokenizers;
using Xunit;

namespace PipServices3.Expressions.Calculator.Tokenizers
{
    /// <summary>
    /// Contains unit tests for Tokenizer package classes.
    /// </summary>
    public class ExpressionTokenizerTest
    {

        [Fact]
        public void TestQuoteToken()
        {
            string tokenString = "A'xyz'\"abc\ndeg\" 'jkl\"def'\"ab\"\"de\"'df''er'";
            Token[] expectedTokens = new Token[] {
                new Token(TokenType.Word, "A"), new Token(TokenType.Quoted, "xyz"),
                new Token(TokenType.Word, "abc\ndeg"), new Token(TokenType.Whitespace, " "),
                new Token(TokenType.Quoted, "jkl\"def"), new Token(TokenType.Word, "ab\"de"),
                new Token(TokenType.Quoted, "df'er")
            };

            ITokenizer tokenizer = new ExpressionTokenizer();
            tokenizer.SkipEof = true;
            tokenizer.DecodeStrings = true;
            IList<Token> tokenList = tokenizer.TokenizeBuffer(tokenString);

            TokenizerFixture.AssertAreEqualsTokenLists(expectedTokens, tokenList);
        }

        [Fact]
        public void TestWordToken()
        {
            string tokenString = "A'xyz'Ebf_2\n2_2";
            Token[] expectedTokens = new Token[] {
                new Token(TokenType.Word, "A"), new Token(TokenType.Quoted, "xyz"),
                new Token(TokenType.Word, "Ebf_2"), new Token(TokenType.Whitespace, "\n"),
                new Token(TokenType.Integer, "2"), new Token(TokenType.Word, "_2")};

            ITokenizer tokenizer = new ExpressionTokenizer();
            tokenizer.SkipEof = true;
            tokenizer.DecodeStrings = true;
            IList<Token> tokenList = tokenizer.TokenizeBuffer(tokenString);

            TokenizerFixture.AssertAreEqualsTokenLists(expectedTokens, tokenList);
        }

        [Fact]
        public void TestNumberToken()
        {
            string tokenString = "123-321 .543-.76-. 123.456 123e45 543.11E+43 1e 3E-";
            Token[] expectedTokens = new Token[] {
                new Token(TokenType.Integer, "123"), new Token(TokenType.Symbol, "-"),
                new Token(TokenType.Integer, "321"), new Token(TokenType.Whitespace, " "),
                new Token(TokenType.Float, ".543"), new Token(TokenType.Symbol, "-"),
                new Token(TokenType.Float, ".76"), new Token(TokenType.Symbol, "-"),
                new Token(TokenType.Symbol, "."), new Token(TokenType.Whitespace, " "),
                new Token(TokenType.Float, "123.456"), new Token(TokenType.Whitespace, " "),
                new Token(TokenType.Float, "123e45"), new Token(TokenType.Whitespace, " "),
                new Token(TokenType.Float, "543.11E+43"), new Token(TokenType.Whitespace, " "),
                new Token(TokenType.Integer, "1"), new Token(TokenType.Word, "e"),
                new Token(TokenType.Whitespace, " "), new Token(TokenType.Integer, "3"),
                new Token(TokenType.Word, "E"), new Token(TokenType.Symbol, "-")
            };

            ITokenizer tokenizer = new ExpressionTokenizer();
            tokenizer.SkipEof = true;
            tokenizer.DecodeStrings = true;
            IList<Token> tokenList = tokenizer.TokenizeBuffer(tokenString);

            TokenizerFixture.AssertAreEqualsTokenLists(expectedTokens, tokenList);
        }

        [Fact]
        public void TestExpressionToken()
        {
            var tokenString = "A + b / (3 - Max(-123, 1)*2)";

            var tokenizer = new ExpressionTokenizer();
            var tokenList = tokenizer.TokenizeBuffer(tokenString);

            Assert.Equal(25, tokenList.Count);
        }


    }
}
