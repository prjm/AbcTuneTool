using AbcTuneTool.FileIo;

namespace AbcTuneToolTests {
    public class MnemoTest {

        [TestMethod]
        public void TestSimpleMnemo() {
            Assert.AreEqual(Mnemonics.Decode('A', 'E'), "Æ");
            Assert.AreEqual(Mnemonics.Encode("Æ"), ('A', 'E'));

            Assert.AreEqual(Mnemonics.Decode('?', '?'), string.Empty);
            Assert.AreEqual(Mnemonics.Encode(string.Empty), ('\0', '\0'));
        }

    }
}
