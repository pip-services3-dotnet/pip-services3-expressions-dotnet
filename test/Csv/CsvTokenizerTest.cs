using System;
using System.Collections.Generic;
using PipServices3.Expressions.Tokenizers;
using Xunit;

namespace PipServices3.Expressions.Csv
{
    /// <summary>
    /// Contains unit tests for Tokenizer package classes.
    /// </summary>
    public class CsvTokenizerTest
    {
        [Fact]
        public void TestTokenizerWithDefaultParameters()
        {
            string tokenString = "\n\r\"John \"\"Da Man\"\"\",Repici,120 Jefferson St.,Riverside, NJ,08075\r\n"
                + "Stephen,Tyler,\"7452 Terrace \"\"At the Plaza\"\" road\",SomeTown,SD, 91234\r"
                + ",Blankman,,SomeTown, SD, 00298\n";
            Token[] expectedTokens = new Token[] {
                new Token(TokenType.Eol, "\n\r"),
                new Token(TokenType.Quoted, "\"John \"\"Da Man\"\"\""), new Token(TokenType.Symbol, ","),
                new Token(TokenType.Word, "Repici"), new Token(TokenType.Symbol, ","),
                new Token(TokenType.Word, "120 Jefferson St."), new Token(TokenType.Symbol, ","),
                new Token(TokenType.Word, "Riverside"), new Token(TokenType.Symbol, ","),
                new Token(TokenType.Word, " NJ"), new Token(TokenType.Symbol, ","),
                new Token(TokenType.Word, "08075"), new Token(TokenType.Eol, "\r\n"),
                new Token(TokenType.Word, "Stephen"), new Token(TokenType.Symbol, ","),
                new Token(TokenType.Word, "Tyler"), new Token(TokenType.Symbol, ","),
                new Token(TokenType.Quoted, "\"7452 Terrace \"\"At the Plaza\"\" road\""), new Token(TokenType.Symbol, ","),
                new Token(TokenType.Word, "SomeTown"), new Token(TokenType.Symbol, ","),
                new Token(TokenType.Word, "SD"), new Token(TokenType.Symbol, ","),
                new Token(TokenType.Word, " 91234"), new Token(TokenType.Eol, "\r"),
                new Token(TokenType.Symbol, ","),
                new Token(TokenType.Word, "Blankman"), new Token(TokenType.Symbol, ","),
                new Token(TokenType.Symbol, ","),
                new Token(TokenType.Word, "SomeTown"), new Token(TokenType.Symbol, ","),
                new Token(TokenType.Word, " SD"), new Token(TokenType.Symbol, ","),
                new Token(TokenType.Word, " 00298"), new Token(TokenType.Eol, "\n")};

            ITokenizer tokenizer = new CsvTokenizer();
            tokenizer.SkipEof = true;
            IList<Token> tokenList = tokenizer.TokenizeBuffer(tokenString);

            TokenizerFixture.AssertAreEqualsTokenLists(expectedTokens, tokenList);
        }

        [Fact]
        public void TestTokenizerWithOverridenParameters()
        {
            string tokenString = "\n\r\'John, \'\'Da Man\'\'\'\tRepici\t120 Jefferson St.\tRiverside\t NJ\t08075\r\n"
                + "Stephen\t\"Tyler\"\t\'7452 \t\nTerrace \'\'At the Plaza\'\' road\'\tSomeTown\tSD\t 91234\r"
                + "\tBlankman\t\tSomeTown \'xxx\t\'\t SD\t 00298\n";
            Token[] expectedTokens = new Token[] {
                new Token(TokenType.Eol, "\n\r"),
                new Token(TokenType.Quoted, "\'John, \'\'Da Man\'\'\'"), new Token(TokenType.Symbol, "\t"),
                new Token(TokenType.Word, "Repici"), new Token(TokenType.Symbol, "\t"),
                new Token(TokenType.Word, "120 Jefferson St."), new Token(TokenType.Symbol, "\t"),
                new Token(TokenType.Word, "Riverside"), new Token(TokenType.Symbol, "\t"),
                new Token(TokenType.Word, " NJ"), new Token(TokenType.Symbol, "\t"),
                new Token(TokenType.Word, "08075"), new Token(TokenType.Eol, "\r\n"),
                new Token(TokenType.Word, "Stephen"), new Token(TokenType.Symbol, "\t"),
                new Token(TokenType.Quoted, "\"Tyler\""), new Token(TokenType.Symbol, "\t"),
                new Token(TokenType.Quoted, "\'7452 \t\nTerrace \'\'At the Plaza\'\' road\'"), new Token(TokenType.Symbol, "\t"),
                new Token(TokenType.Word, "SomeTown"), new Token(TokenType.Symbol, "\t"),
                new Token(TokenType.Word, "SD"), new Token(TokenType.Symbol, "\t"),
                new Token(TokenType.Word, " 91234"), new Token(TokenType.Eol, "\r"),
                new Token(TokenType.Symbol, "\t"),
                new Token(TokenType.Word, "Blankman"), new Token(TokenType.Symbol, "\t"),
                new Token(TokenType.Symbol, "\t"),
                new Token(TokenType.Word, "SomeTown "), new Token(TokenType.Quoted, "\'xxx\t\'"), new Token(TokenType.Symbol, "\t"),
                new Token(TokenType.Word, " SD"), new Token(TokenType.Symbol, "\t"),
                new Token(TokenType.Word, " 00298"), new Token(TokenType.Eol, "\n")};

            //CsvTokenizer tokenizer = new CsvTokenizer();
            //tokenizer.FieldSeparators = new char[] { '\t' };
            //tokenizer.QuoteSymbols = new char[] { '\'', '\"' };
            //tokenizer.EndOfLine = "\n";
            //tokenizer.SkipEof = true;
            //IList<Token> tokenList = tokenizer.TokenizeBuffer(tokenString);

            //TokenizerFixture.AssertAreEqualsTokenLists(expectedTokens, tokenList);
        }

    }
}
