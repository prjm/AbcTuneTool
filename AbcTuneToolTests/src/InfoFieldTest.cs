using AbcTuneTool.Model;

namespace AbcTuneToolTests {

    public class InfoFieldTest : CommonTest {

        [TestMethod]
        public void Test_A_Area() {
            var field = ParseInfoField("A:Main area");
            Assert.AreEqual(field.Kind, InformationFieldKind.Area);
            Assert.AreEqual(field.Kind.InFileHeader(), true);
            Assert.AreEqual(field.Kind.InTuneHeader(), true);
            Assert.AreEqual(field.Kind.InTuneBody(), false);
            Assert.AreEqual(field.Kind.InInline(), false);
            Assert.AreEqual(field.Kind.GetContentType(), InformationFieldContent.StringContent);
        }

        [TestMethod]
        public void Test_B_Book() {
            var field = ParseInfoField("B:The book");
            Assert.AreEqual(field.Kind, InformationFieldKind.Book);
            Assert.AreEqual(field.Kind.InFileHeader(), true);
            Assert.AreEqual(field.Kind.InTuneHeader(), true);
            Assert.AreEqual(field.Kind.InTuneBody(), false);
            Assert.AreEqual(field.Kind.InInline(), false);
            Assert.AreEqual(field.Kind.GetContentType(), InformationFieldContent.StringContent);
        }

        [TestMethod]
        public void Test_C_Composer() {
            var field = ParseInfoField("C:The composer");
            Assert.AreEqual(field.Kind, InformationFieldKind.Composer);
            Assert.AreEqual(field.Kind.InFileHeader(), true);
            Assert.AreEqual(field.Kind.InTuneHeader(), true);
            Assert.AreEqual(field.Kind.InTuneBody(), false);
            Assert.AreEqual(field.Kind.InInline(), false);
            Assert.AreEqual(field.Kind.GetContentType(), InformationFieldContent.StringContent);
        }

        [TestMethod]
        public void Test_D_Discography() {
            var field = ParseInfoField("D:The must have life album");
            Assert.AreEqual(field.Kind, InformationFieldKind.Discography);
            Assert.AreEqual(field.Kind.InFileHeader(), true);
            Assert.AreEqual(field.Kind.InTuneHeader(), true);
            Assert.AreEqual(field.Kind.InTuneBody(), false);
            Assert.AreEqual(field.Kind.InInline(), false);
            Assert.AreEqual(field.Kind.GetContentType(), InformationFieldContent.StringContent);
        }

        [TestMethod]
        public void Test_F_FileUrl() {
            var field = ParseInfoField("F:http://www.tunes.abc");
            Assert.AreEqual(field.Kind, InformationFieldKind.FileUrl);
            Assert.AreEqual(field.Kind.InFileHeader(), true);
            Assert.AreEqual(field.Kind.InTuneHeader(), true);
            Assert.AreEqual(field.Kind.InTuneBody(), false);
            Assert.AreEqual(field.Kind.InInline(), false);
            Assert.AreEqual(field.Kind.GetContentType(), InformationFieldContent.StringContent);
        }

        [TestMethod]
        public void Test_G_Group() {
            var field = ParseInfoField("G:the others");
            Assert.AreEqual(field.Kind, InformationFieldKind.Group);
            Assert.AreEqual(field.Kind.InFileHeader(), true);
            Assert.AreEqual(field.Kind.InTuneHeader(), true);
            Assert.AreEqual(field.Kind.InTuneBody(), false);
            Assert.AreEqual(field.Kind.InInline(), false);
            Assert.AreEqual(field.Kind.GetContentType(), InformationFieldContent.StringContent);
        }

        [TestMethod]
        public void Test_H_History() {
            var field = ParseInfoField("H: Some history");
            Assert.AreEqual(field.Kind, InformationFieldKind.History);
            Assert.AreEqual(field.Kind.InFileHeader(), true);
            Assert.AreEqual(field.Kind.InTuneHeader(), true);
            Assert.AreEqual(field.Kind.InTuneBody(), false);
            Assert.AreEqual(field.Kind.InInline(), false);
            Assert.AreEqual(field.Kind.GetContentType(), InformationFieldContent.StringContent);
        }

        [TestMethod]
        public void Test_I_Instruction() {
            var field = ParseInstructionField("I:abc-charset utf-8");
            Assert.AreEqual(field.Kind, InformationFieldKind.Instruction);
            Assert.AreEqual(field.Kind.InFileHeader(), true);
            Assert.AreEqual(field.Kind.InTuneHeader(), true);
            Assert.AreEqual(field.Kind.InTuneBody(), true);
            Assert.AreEqual(field.Kind.InInline(), true);
            Assert.AreEqual(field.Kind.GetContentType(), InformationFieldContent.Instruction);
            Assert.AreEqual("utf-8", field.InstrValue);

            field = ParseInstructionField("I:abc-version 2.0");
            Assert.AreEqual(InformationFieldKind.Instruction, field.Kind);
            Assert.AreEqual(InstructionKind.Version, field.InstrKind);
            Assert.AreEqual("2.0", field.InstrValue);

            field = ParseInstructionField("I:abc-include demo.abh");
            Assert.AreEqual(InformationFieldKind.Instruction, field.Kind);
            Assert.AreEqual(InstructionKind.Include, field.InstrKind);
            Assert.AreEqual("demo.abh", field.InstrValue);

            field = ParseInstructionField("I:abc-creator abctunetool-1.0");
            Assert.AreEqual(InformationFieldKind.Instruction, field.Kind);
            Assert.AreEqual(InstructionKind.Creator, field.InstrKind);
            Assert.AreEqual("abctunetool-1.0", field.InstrValue);

            field = ParseInstructionField("I:decoration +");
            Assert.AreEqual(InformationFieldKind.Instruction, field.Kind);
            Assert.AreEqual(InstructionKind.Decoration, field.InstrKind);
            Assert.AreEqual("+", field.InstrValue);

            field = ParseInstructionField("I:linebreak <EOL>");
            Assert.AreEqual(InformationFieldKind.Instruction, field.Kind);
            Assert.AreEqual(InstructionKind.Linebreak, field.InstrKind);
            Assert.AreEqual("<EOL>", field.InstrValue);

        }

    }
}
