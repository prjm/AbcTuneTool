namespace AbcTuneToolTests {

    /// <summary>
    ///     structural tests
    /// </summary>
    public class StructuralTests : CommonTest {

        [TestMethod]
        public void ParseSingleTune() {
            var t = ParseTuneBook("T:A§");
            Assert.AreEqual(0, t.FileHeader.Fields.Length);
            Assert.AreEqual(1, t.Tunes.Length);
            Assert.AreEqual(1, t.Tunes[0].Header.Fields.Length);

            t = ParseTuneBook("C:X§§T:A§");
            Assert.AreEqual(1, t.FileHeader.Fields.Length);
            Assert.AreEqual(1, t.Tunes.Length);
            Assert.AreEqual(1, t.Tunes[0].Header.Fields.Length);
        }

        [TestMethod]
        public void ParseMultipleTunes() {
            var t = ParseTuneBook("C:X§§T:A§§T:B");
            Assert.AreEqual(1, t.FileHeader.Fields.Length);
            Assert.AreEqual(2, t.Tunes.Length);
            Assert.AreEqual(1, t.Tunes[0].Header.Fields.Length);
        }

    }
}
