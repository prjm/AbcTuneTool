using AbcTuneTool.FileIo;
using AbcTuneTool.Model;
using System.IO;

namespace AbcTuneToolTests
{
    public class TokenizerTest
    {


        protected void RunTokenizerTest(string toTokenize, params (AbcCharacterKind kind, char value)[] tokens)
        {
            using var reader = new StringReader(toTokenize);
            using var tokenizer = new AbcTokenizer(reader);

            var counter = 0;
            while (tokenizer.HasToken)
            {
                var currentToken = tokenizer.CurrentToken;
                var expectedToken = tokens[counter];
                counter++;
                Assert.AreEqual(expectedToken.kind, currentToken.Kind);
                Assert.AreEqual(expectedToken.value, currentToken.Value);
            }
        }


    }
}
