namespace AbcTuneToolTests {

    public class TuneBodyTests : CommonTest {

        [TestMethod]
        public void TestBasicNotes() {
            var s = "cdefgab";
            Assert.AreEqualSeq(StringToNotes(s), ParseTuneBody(s).Items);

            s = "c\'";
            Assert.AreEqualSeq(StringToNotes(s, 2), ParseTuneBody(s).Items);
        }

    }
}
