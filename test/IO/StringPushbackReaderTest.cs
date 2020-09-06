using System.IO;
using Xunit;

namespace PipServices3.Expressions.IO
{
    public class StringPushbackReaderTest
    {
        private string _content;
        private StringPushbackReader _reader;
        private PushbackReaderFixture _fixture;

        public StringPushbackReaderTest()
        {
            _content = "Test String";
            _reader = new StringPushbackReader(_content);
            _fixture = new PushbackReaderFixture(_reader, _content);
        }

        [Fact]
        public void TestOperations()
        {
            _fixture.TestOperations();
        }

        [Fact]
        public void TestPushback()
        {
            _fixture.TestPushback();
        }

    }
}
