using System;
using System.Collections.Generic;
using System.Text;

namespace PipServices3.Expressions.Mustache.Parsers
{
    public enum MustacheTokenType
    {
        Unknown = 0,
        Value,
        Variable,
        EscapedVariable,
        Section,
        InvertedSection,
        SectionEnd,
        Partial,
        Comment
    }
}
