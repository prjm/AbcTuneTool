using System.Linq;
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
            static Tone t(char c, char a) => new Tone(c, a.AsAccidental());
            Tone q(string k) => ParseKeyField("K:" + k).KeyValue.Tones.MainTones.First();

            Assert.AreEqual(field.Kind, InformationFieldKind.Key);
            Assert.AreEqual(false, field.Kind.InFileHeader());
            Assert.AreEqual(true, field.Kind.InTuneHeader());
            Assert.AreEqual(true, field.Kind.InTuneBody());
            Assert.AreEqual(true, field.Kind.InInline());
            Assert.AreEqual(InformationFieldContent.Key, field.Kind.GetContentType());

            Assert.AreEqual(t('C', '#'), q("C#"));
            Assert.AreEqual(t('F', '#'), q("F#"));
            Assert.AreEqual(t('B', ' '), q("B"));
            Assert.AreEqual(t('E', ' '), q("E"));
            Assert.AreEqual(t('A', ' '), q("A"));
            Assert.AreEqual(t('D', ' '), q("D"));
            Assert.AreEqual(t('G', ' '), q("G"));
            Assert.AreEqual(t('C', ' '), q("C"));
            Assert.AreEqual(t('F', ' '), q("F"));
            Assert.AreEqual(t('B', 'b'), q("Bb"));
            Assert.AreEqual(t('E', 'b'), q("Eb"));
            Assert.AreEqual(t('A', 'b'), q("Ab"));
            Assert.AreEqual(t('D', 'b'), q("Db"));
            Assert.AreEqual(t('G', 'b'), q("Gb"));
            Assert.AreEqual(t('C', 'b'), q("Cb"));

            Assert.AreEqual(t('C', '#'), q("C# major"));
            Assert.AreEqual(t('F', '#'), q("F# major"));
            Assert.AreEqual(t('B', ' '), q("B maj"));
            Assert.AreEqual(t('E', ' '), q("E maj"));
            Assert.AreEqual(t('A', ' '), q("A maj"));
            Assert.AreEqual(t('D', ' '), q("D maj"));
            Assert.AreEqual(t('G', ' '), q("G maj"));
            Assert.AreEqual(t('C', ' '), q("C maj"));
            Assert.AreEqual(t('F', ' '), q("F maj"));
            Assert.AreEqual(t('B', 'b'), q("Bb Maj"));
            Assert.AreEqual(t('E', 'b'), q("Eb Maj"));
            Assert.AreEqual(t('A', 'b'), q("Ab Maj"));
            Assert.AreEqual(t('D', 'b'), q("Db Maj"));
            Assert.AreEqual(t('G', 'b'), q("Gb Maj"));
            Assert.AreEqual(t('C', 'b'), q("Cb Maj"));
        }

        [TestMethod]
        public void Test_K_Minor_Key() {
            static Tone t(char c, char a) => new Tone(c, a.AsAccidental());
            Tone q(string k) => ParseKeyField("K:" + k).KeyValue.Tones.Tones[0];

            Assert.AreEqual(t('A', '#'), q("A# minor"));
            Assert.AreEqual(t('D', '#'), q("D# minor"));
            Assert.AreEqual(t('G', '#'), q("G# minor"));
            Assert.AreEqual(t('C', '#'), q("C# minor"));
            Assert.AreEqual(t('F', '#'), q("F# minor"));
            Assert.AreEqual(t('B', ' '), q("B  min"));
            Assert.AreEqual(t('E', ' '), q("E  min"));
            Assert.AreEqual(t('A', ' '), q("A  min"));
            Assert.AreEqual(t('D', ' '), q("D  min"));
            Assert.AreEqual(t('G', ' '), q("G  min"));
            Assert.AreEqual(t('C', ' '), q("C  min"));
            Assert.AreEqual(t('F', ' '), q("F min"));
            Assert.AreEqual(t('B', 'b'), q("Bb min"));
            Assert.AreEqual(t('E', 'b'), q("Eb min"));
            Assert.AreEqual(t('A', 'b'), q("Ab min"));

            Assert.AreEqual(t('A', '#'), q("A# m"));
            Assert.AreEqual(t('D', '#'), q("D# m"));
            Assert.AreEqual(t('G', '#'), q("G# m"));
            Assert.AreEqual(t('C', '#'), q("C# m"));
            Assert.AreEqual(t('F', '#'), q("F# m"));
            Assert.AreEqual(t('B', ' '), q("B  m"));
            Assert.AreEqual(t('E', ' '), q("E  m"));
            Assert.AreEqual(t('A', ' '), q("A  m"));
            Assert.AreEqual(t('D', ' '), q("D  m"));
            Assert.AreEqual(t('G', ' '), q("G  m"));
            Assert.AreEqual(t('C', ' '), q("C  m"));
            Assert.AreEqual(t('F', ' '), q("F  m"));
            Assert.AreEqual(t('B', 'b'), q("Bb m"));
            Assert.AreEqual(t('E', 'b'), q("Eb m"));
            Assert.AreEqual(t('A', 'b'), q("Ab m"));
        }

        [TestMethod]
        public void Test_K_Mixolydian_Key() {
            static Tone t(char c, char a) => new Tone(c, a.AsAccidental());
            Tone q(string k) => ParseKeyField("K:" + k).KeyValue.Tones.Tones[0];

            Assert.AreEqual(t('G', '#'), q("G# mixolydian"));
            Assert.AreEqual(t('C', '#'), q("C# mixolydian"));
            Assert.AreEqual(t('F', '#'), q("F# mixolydian"));
            Assert.AreEqual(t('B', ' '), q("B  mixolydian"));
            Assert.AreEqual(t('E', ' '), q("E  mixolydian"));
            Assert.AreEqual(t('A', ' '), q("A  mix"));
            Assert.AreEqual(t('D', ' '), q("D  mix"));
            Assert.AreEqual(t('G', ' '), q("G  mix"));
            Assert.AreEqual(t('C', ' '), q("C  mix"));
            Assert.AreEqual(t('F', ' '), q("F  mix"));
            Assert.AreEqual(t('B', 'b'), q("Bb mix"));
            Assert.AreEqual(t('E', 'b'), q("Eb mix"));
            Assert.AreEqual(t('A', 'b'), q("Ab mix"));
            Assert.AreEqual(t('D', 'b'), q("Db mix"));
            Assert.AreEqual(t('G', 'b'), q("Gb mix"));
        }

        [TestMethod]
        public void Test_K_Dorian_Key() {
            static Tone t(char c, char a) => new Tone(c, a.AsAccidental());
            Tone q(string k) => ParseKeyField("K:" + k).KeyValue.Tones.Tones[0];

            Assert.AreEqual(t('D', '#'), q("D# dorian"));
            Assert.AreEqual(t('G', '#'), q("G# dorian"));
            Assert.AreEqual(t('C', '#'), q("C# dorian"));
            Assert.AreEqual(t('F', ' '), q("F  dorian"));
            Assert.AreEqual(t('B', ' '), q("B  dorian"));
            Assert.AreEqual(t('E', ' '), q("E  dor"));
            Assert.AreEqual(t('A', ' '), q("A  dor"));
            Assert.AreEqual(t('D', ' '), q("D  dor"));
            Assert.AreEqual(t('G', ' '), q("G  dor"));
            Assert.AreEqual(t('C', ' '), q("C  dor"));
            Assert.AreEqual(t('F', ' '), q("F  dor"));
            Assert.AreEqual(t('B', 'b'), q("Bb dor"));
            Assert.AreEqual(t('E', 'b'), q("Eb dor"));
            Assert.AreEqual(t('A', 'b'), q("Ab dor"));
            Assert.AreEqual(t('D', 'b'), q("Db dor"));
        }

        [TestMethod]
        public void Test_K_Phrygian_Key() {
            static Tone t(char c, char a) => new Tone(c, a.AsAccidental());
            Tone q(string k) => ParseKeyField("K:" + k).KeyValue.Tones.Tones[0];

            Assert.AreEqual(t('E', '#'), q("E# phrygian"));
            Assert.AreEqual(t('A', '#'), q("A# phrygian"));
            Assert.AreEqual(t('D', '#'), q("D# phrygian"));
            Assert.AreEqual(t('G', '#'), q("G# phrygian"));
            Assert.AreEqual(t('C', '#'), q("C# phrygian"));
            Assert.AreEqual(t('F', '#'), q("F# phr"));
            Assert.AreEqual(t('B', ' '), q("B  phr"));
            Assert.AreEqual(t('E', ' '), q("E  phr"));
            Assert.AreEqual(t('A', ' '), q("A  phr"));
            Assert.AreEqual(t('D', ' '), q("D  phr"));
            Assert.AreEqual(t('G', ' '), q("G  phr"));
            Assert.AreEqual(t('C', ' '), q("C  phr"));
            Assert.AreEqual(t('F', ' '), q("F  phr"));
            Assert.AreEqual(t('B', 'b'), q("Bb phr"));
            Assert.AreEqual(t('E', 'b'), q("Eb phr"));
        }

        [TestMethod]
        public void Test_K_No_Key() {
            static Tone t(char c, char a) => new Tone(c, a.AsAccidental());
            Tone q(string k) => ParseKeyField("K:" + k).KeyValue.Tones.Tones[0];

            Assert.AreEqual(t('C', ' '), q(""));
            Assert.AreEqual(t('C', ' '), q("  "));
            Assert.AreEqual(t('C', ' '), q("none"));
        }


        [TestMethod]
        public void Test_K_Explicit_Key() {
            //static Tone t(char c, char a) => new Tone(c, a.AsAccidental());
            Tone[] a(string k) => ParseKeyField("K:" + k).KeyValue.Tones.Accidentals.ToArray();

            Assert.AreEqualSeq(StringToTones("Bb", "Eb"), a("D Phr"));
            Assert.AreEqualSeq(StringToTones("Bb", "Eb", "f#"), a("D Phr ^f"));
            Assert.AreEqualSeq(StringToTones("F#"), a("D maj =C"));
        }

        [TestMethod]
        public void Test_K_Lydian_Key() {
            static Tone t(char c, char a) => new Tone(c, a.AsAccidental());
            Tone q(string k) => ParseKeyField("K:" + k).KeyValue.Tones.Tones[0];

            Assert.AreEqual(t('F', '#'), q("F# lydian"));
            Assert.AreEqual(t('B', ' '), q("B  lydian"));
            Assert.AreEqual(t('E', ' '), q("E  lydian"));
            Assert.AreEqual(t('A', ' '), q("A  lydian"));
            Assert.AreEqual(t('D', ' '), q("D  lydian"));
            Assert.AreEqual(t('G', ' '), q("G  lyd"));
            Assert.AreEqual(t('C', ' '), q("C  lyd"));
            Assert.AreEqual(t('F', ' '), q("F  lyd"));
            Assert.AreEqual(t('B', 'b'), q("Bb lyd"));
            Assert.AreEqual(t('E', 'b'), q("Eb lyd"));
            Assert.AreEqual(t('A', 'b'), q("Ab lyd"));
            Assert.AreEqual(t('D', 'b'), q("Db lyd"));
            Assert.AreEqual(t('G', 'b'), q("Gb lyd"));
            Assert.AreEqual(t('C', 'b'), q("Cb lyd"));
            Assert.AreEqual(t('F', 'b'), q("Fb lyd"));
        }


        [TestMethod]
        public void Test_K_Locrian_Key() {
            static Tone t(char c, char a) => new Tone(c, a.AsAccidental());
            Tone q(string k) => ParseKeyField("K:" + k).KeyValue.Tones.Tones[0];

            Assert.AreEqual(t('B', '#'), q("B# locrian"));
            Assert.AreEqual(t('E', '#'), q("E# locrian"));
            Assert.AreEqual(t('A', '#'), q("A# locrian"));
            Assert.AreEqual(t('D', '#'), q("D# locrian"));
            Assert.AreEqual(t('G', '#'), q("G# locrian"));
            Assert.AreEqual(t('C', '#'), q("C# loc"));
            Assert.AreEqual(t('F', '#'), q("F# loc"));
            Assert.AreEqual(t('B', ' '), q("B  loc"));
            Assert.AreEqual(t('E', ' '), q("E  loc"));
            Assert.AreEqual(t('A', ' '), q("A  loc"));
            Assert.AreEqual(t('D', ' '), q("D  loc"));
            Assert.AreEqual(t('G', ' '), q("G  loc"));
            Assert.AreEqual(t('C', ' '), q("C  loc"));
            Assert.AreEqual(t('F', ' '), q("F  loc"));
            Assert.AreEqual(t('B', 'b'), q("Bb loc"));
        }


    }
}
