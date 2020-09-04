using AbcTuneTool.Common;
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

            field = ParseInstructionField("I:reverse");
            Assert.AreEqual(InformationFieldKind.Instruction, field.Kind);
            Assert.AreEqual(InstructionKind.Otherwise, field.InstrKind);
            Assert.AreEqual("reverse", field.InstrValue);

        }

        [TestMethod]
        public void Test_K_Major_Key() {
            var field = ParseKeyField("K:G");
            static Note n(char c, char a) => new Note(c, a.AsAccidental());
            Note p(string k) => ParseKeyField("K:" + k).KeyValue;

            Assert.AreEqual(field.Kind, InformationFieldKind.Key);
            Assert.AreEqual(false, field.Kind.InFileHeader());
            Assert.AreEqual(true, field.Kind.InTuneHeader());
            Assert.AreEqual(true, field.Kind.InTuneBody());
            Assert.AreEqual(true, field.Kind.InInline());
            Assert.AreEqual(InformationFieldContent.Key, field.Kind.GetContentType());

            Assert.AreEqual(n('C', '#'), p("C#"));
            Assert.AreEqual(n('F', '#'), p("F#"));
            Assert.AreEqual(n('B', '\0'), p("B"));
            Assert.AreEqual(n('E', '\0'), p("E"));
            Assert.AreEqual(n('A', '\0'), p("A"));
            Assert.AreEqual(n('D', '\0'), p("D"));
            Assert.AreEqual(n('G', '\0'), p("G"));
            Assert.AreEqual(n('C', '\0'), p("C"));
            Assert.AreEqual(n('F', '\0'), p("F"));
            Assert.AreEqual(n('B', 'b'), p("Bb"));
            Assert.AreEqual(n('E', 'b'), p("Eb"));
            Assert.AreEqual(n('A', 'b'), p("Ab"));
            Assert.AreEqual(n('D', 'b'), p("Db"));
            Assert.AreEqual(n('G', 'b'), p("Gb"));
            Assert.AreEqual(n('C', 'b'), p("Cb"));

            Assert.AreEqual(n('C', '#'), p("C# major"));
            Assert.AreEqual(n('F', '#'), p("F# major"));
            Assert.AreEqual(n('B', '\0'), p("B maj"));
            Assert.AreEqual(n('E', '\0'), p("E maj"));
            Assert.AreEqual(n('A', '\0'), p("A maj"));
            Assert.AreEqual(n('D', '\0'), p("D maj"));
            Assert.AreEqual(n('G', '\0'), p("G maj"));
            Assert.AreEqual(n('C', '\0'), p("C maj"));
            Assert.AreEqual(n('F', '\0'), p("F maj"));
            Assert.AreEqual(n('B', 'b'), p("Bb Maj"));
            Assert.AreEqual(n('E', 'b'), p("Eb Maj"));
            Assert.AreEqual(n('A', 'b'), p("Ab Maj"));
            Assert.AreEqual(n('D', 'b'), p("Db Maj"));
            Assert.AreEqual(n('G', 'b'), p("Gb Maj"));
            Assert.AreEqual(n('C', 'b'), p("Cb Maj"));
        }

        [TestMethod]
        public void Test_K_Minor_Key() {
            static Note n(char c, char a) => new Note(c, a.AsAccidental());
            Note p(string k) => ParseKeyField("K:" + k).KeyValue;

            Assert.AreEqual(n('A', '#'), p("A# minor"));
            Assert.AreEqual(n('D', '#'), p("D# minor"));
            Assert.AreEqual(n('G', '#'), p("G# minor"));
            Assert.AreEqual(n('C', '#'), p("C# minor"));
            Assert.AreEqual(n('F', '#'), p("F# minor"));
            Assert.AreEqual(n('B', '\0'), p("B  min"));
            Assert.AreEqual(n('E', '\0'), p("E  min"));
            Assert.AreEqual(n('A', '\0'), p("A  min"));
            Assert.AreEqual(n('D', '\0'), p("D  min"));
            Assert.AreEqual(n('G', '\0'), p("G  min"));
            Assert.AreEqual(n('C', '\0'), p("C  min"));
            Assert.AreEqual(n('F', '\0'), p("Fm min"));
            Assert.AreEqual(n('B', 'b'), p("Bb min"));
            Assert.AreEqual(n('E', 'b'), p("Eb min"));
            Assert.AreEqual(n('A', 'b'), p("Ab min"));


            Assert.AreEqual(n('A', '#'), p("A# m"));
            Assert.AreEqual(n('D', '#'), p("D# m"));
            Assert.AreEqual(n('G', '#'), p("G# m"));
            Assert.AreEqual(n('C', '#'), p("C# m"));
            Assert.AreEqual(n('F', '#'), p("F# m"));
            Assert.AreEqual(n('B', '\0'), p("B  m"));
            Assert.AreEqual(n('E', '\0'), p("E  m"));
            Assert.AreEqual(n('A', '\0'), p("A  m"));
            Assert.AreEqual(n('D', '\0'), p("D  m"));
            Assert.AreEqual(n('G', '\0'), p("G  m"));
            Assert.AreEqual(n('C', '\0'), p("C  m"));
            Assert.AreEqual(n('F', '\0'), p("Fm m"));
            Assert.AreEqual(n('B', 'b'), p("Bb m"));
            Assert.AreEqual(n('E', 'b'), p("Eb m"));
            Assert.AreEqual(n('A', 'b'), p("Ab m"));
        }

        [TestMethod]
        public void Test_K_Mixolydian_Key() {
            static Note n(char c, char a) => new Note(c, a.AsAccidental());
            Note p(string k) => ParseKeyField("K:" + k).KeyValue;

            Assert.AreEqual(n('G', '#'), p("G# mixolydian"));
            Assert.AreEqual(n('C', '#'), p("C# mixolydian"));
            Assert.AreEqual(n('F', '#'), p("F# mixolydian"));
            Assert.AreEqual(n('B', '\0'), p("B  mixolydian"));
            Assert.AreEqual(n('E', '\0'), p("E  mixolydian"));
            Assert.AreEqual(n('A', '\0'), p("A  mix"));
            Assert.AreEqual(n('D', '\0'), p("D  mix"));
            Assert.AreEqual(n('G', '\0'), p("G  mix"));
            Assert.AreEqual(n('C', '\0'), p("C  mix"));
            Assert.AreEqual(n('F', '\0'), p("F  mix"));
            Assert.AreEqual(n('B', 'b'), p("Bb mix"));
            Assert.AreEqual(n('E', 'b'), p("Eb mix"));
            Assert.AreEqual(n('A', 'b'), p("Ab mix"));
            Assert.AreEqual(n('D', 'b'), p("Db mix"));
            Assert.AreEqual(n('G', 'b'), p("Gb mix"));
        }

        [TestMethod]
        public void Test_K_Dorian_Key() {
            static Note n(char c, char a) => new Note(c, a.AsAccidental());
            Note p(string k) => ParseKeyField("K:" + k).KeyValue;

            Assert.AreEqual(n('D', '#'), p("D# dorian"));
            Assert.AreEqual(n('G', '#'), p("G# dorian"));
            Assert.AreEqual(n('C', '#'), p("C# dorian"));
            Assert.AreEqual(n('F', '\0'), p("F  dorian"));
            Assert.AreEqual(n('B', '\0'), p("B  dorian"));
            Assert.AreEqual(n('E', '\0'), p("E  dor"));
            Assert.AreEqual(n('A', '\0'), p("A  dor"));
            Assert.AreEqual(n('D', '\0'), p("D  dor"));
            Assert.AreEqual(n('G', '\0'), p("G  dor"));
            Assert.AreEqual(n('C', '\0'), p("C  dor"));
            Assert.AreEqual(n('F', '\0'), p("F  dor"));
            Assert.AreEqual(n('B', 'b'), p("Bb dor"));
            Assert.AreEqual(n('E', 'b'), p("Eb dor"));
            Assert.AreEqual(n('A', 'b'), p("Ab dor"));
            Assert.AreEqual(n('D', 'b'), p("Db dor"));
        }

        [TestMethod]
        public void Test_K_Phrygian_Key() {
            static Note n(char c, char a) => new Note(c, a.AsAccidental());
            Note p(string k) => ParseKeyField("K:" + k).KeyValue;

            Assert.AreEqual(n('E', '#'), p("E# phrygian"));
            Assert.AreEqual(n('A', '#'), p("A# phrygian"));
            Assert.AreEqual(n('D', '#'), p("D# phrygian"));
            Assert.AreEqual(n('G', '#'), p("G# phrygian"));
            Assert.AreEqual(n('C', '#'), p("C# phrygian"));
            Assert.AreEqual(n('F', '#'), p("F# phr"));
            Assert.AreEqual(n('B', '\0'), p("B  phr"));
            Assert.AreEqual(n('E', '\0'), p("E  phr"));
            Assert.AreEqual(n('A', '\0'), p("A  phr"));
            Assert.AreEqual(n('D', '\0'), p("D  phr"));
            Assert.AreEqual(n('G', '\0'), p("G  phr"));
            Assert.AreEqual(n('C', '\0'), p("C  phr"));
            Assert.AreEqual(n('F', '\0'), p("F  phr"));
            Assert.AreEqual(n('B', 'b'), p("Bb phr"));
            Assert.AreEqual(n('E', 'b'), p("Eb phr"));
        }

        [TestMethod]
        public void Test_K_No_Key() {
            static Note n(char c, char a) => new Note(c, a.AsAccidental());
            Note p(string k) => ParseKeyField("K:" + k).KeyValue;

            Assert.AreEqual(n('x', ' '), p(""));
            Assert.AreEqual(n('x', ' '), p("  "));
            Assert.AreEqual(n('x', ' '), p("none"));
        }


        [TestMethod]
        public void Test_K_Lydian_Key() {
            static Note n(char c, char a) => new Note(c, a.AsAccidental());
            Note p(string k) => ParseKeyField("K:" + k).KeyValue;

            Assert.AreEqual(n('F', '#'), p("F# lydian"));
            Assert.AreEqual(n('B', '\0'), p("B  lydian"));
            Assert.AreEqual(n('E', '\0'), p("E  lydian"));
            Assert.AreEqual(n('A', '\0'), p("A  lydian"));
            Assert.AreEqual(n('D', '\0'), p("D  lydian"));
            Assert.AreEqual(n('G', '\0'), p("G  lyd"));
            Assert.AreEqual(n('C', '\0'), p("C  lyd"));
            Assert.AreEqual(n('F', '\0'), p("F  lyd"));
            Assert.AreEqual(n('B', 'b'), p("Bb lyd"));
            Assert.AreEqual(n('E', 'b'), p("Eb lyd"));
            Assert.AreEqual(n('A', 'b'), p("Ab lyd"));
            Assert.AreEqual(n('D', 'b'), p("Db lyd"));
            Assert.AreEqual(n('G', 'b'), p("Gb lyd"));
            Assert.AreEqual(n('C', 'b'), p("Cb lyd"));
            Assert.AreEqual(n('F', 'b'), p("Fb lyd"));
        }


        [TestMethod]
        public void Test_K_Locrian_Key() {
            static Note n(char c, char a) => new Note(c, a.AsAccidental());
            Note p(string k) => ParseKeyField("K:" + k).KeyValue;

            Assert.AreEqual(n('B', '#'), p("B# locrian"));
            Assert.AreEqual(n('E', '#'), p("E# locrian"));
            Assert.AreEqual(n('A', '#'), p("A# locrian"));
            Assert.AreEqual(n('D', '#'), p("D# locrian"));
            Assert.AreEqual(n('G', '#'), p("G# locrian"));
            Assert.AreEqual(n('C', '#'), p("C# loc"));
            Assert.AreEqual(n('F', '#'), p("F# loc"));
            Assert.AreEqual(n('B', '\0'), p("B  loc"));
            Assert.AreEqual(n('E', '\0'), p("E  loc"));
            Assert.AreEqual(n('A', '\0'), p("A  loc"));
            Assert.AreEqual(n('D', '\0'), p("D  loc"));
            Assert.AreEqual(n('G', '\0'), p("G  loc"));
            Assert.AreEqual(n('C', '\0'), p("C  loc"));
            Assert.AreEqual(n('F', '\0'), p("F  loc"));
            Assert.AreEqual(n('B', 'b'), p("Bb loc"));
        }


    }
}
