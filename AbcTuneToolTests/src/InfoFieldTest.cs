﻿using System.Linq;
using AbcTuneTool.Common;
using AbcTuneTool.Model;
using AbcTuneTool.Model.Fields;
using AbcTuneTool.Model.Symbolic;
using AbcTuneTool.Model.TuneElements;

namespace AbcTuneToolTests {

    public class InfoFieldTest : CommonTest {

        private static Tone KeyOf(string k)
            => ParseKeyField("K:" + k).KeyValue.Tones.Tones[0];

        private static ClefSettings ClefOfK(string k, KeyStatus s = KeyStatus.NoKey)
            => ParseKeyField("K:" + k, s).Clef;

        private static Fraction LengthOf(string l)
            => ParseLengthField("L:" + l).Length;

        private static Meter MeterOf(string l)
            => ParseMeterField("M:" + l).MeterValue;

        private static Tone ToneOf(char c, char a)
            => new Tone(c, a.AsAccidental());

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
            static Tone q(string k) => ParseKeyField("K:" + k).KeyValue.Tones.MainTones.First();

            Assert.AreEqual(field.Kind, InformationFieldKind.Key);
            Assert.AreEqual(false, field.Kind.InFileHeader());
            Assert.AreEqual(true, field.Kind.InTuneHeader());
            Assert.AreEqual(true, field.Kind.InTuneBody());
            Assert.AreEqual(true, field.Kind.InInline());
            Assert.AreEqual(InformationFieldContent.Key, field.Kind.GetContentType());

            Assert.AreEqual(t('c', '#'), q("C#"));
            Assert.AreEqual(t('f', '#'), q("F#"));
            Assert.AreEqual(t('b', ' '), q("B"));
            Assert.AreEqual(t('e', ' '), q("E"));
            Assert.AreEqual(t('a', ' '), q("A"));
            Assert.AreEqual(t('d', ' '), q("D"));
            Assert.AreEqual(t('g', ' '), q("G"));
            Assert.AreEqual(t('c', ' '), q("C"));
            Assert.AreEqual(t('f', ' '), q("F"));
            Assert.AreEqual(t('b', 'b'), q("Bb"));
            Assert.AreEqual(t('e', 'b'), q("Eb"));
            Assert.AreEqual(t('a', 'b'), q("Ab"));
            Assert.AreEqual(t('d', 'b'), q("Db"));
            Assert.AreEqual(t('g', 'b'), q("Gb"));
            Assert.AreEqual(t('c', 'b'), q("Cb"));

            Assert.AreEqual(t('c', '#'), q("C# major"));
            Assert.AreEqual(t('f', '#'), q("F# major"));
            Assert.AreEqual(t('b', ' '), q("B maj"));
            Assert.AreEqual(t('e', ' '), q("E maj"));
            Assert.AreEqual(t('a', ' '), q("A maj"));
            Assert.AreEqual(t('d', ' '), q("D maj"));
            Assert.AreEqual(t('g', ' '), q("G maj"));
            Assert.AreEqual(t('c', ' '), q("C maj"));
            Assert.AreEqual(t('f', ' '), q("F maj"));
            Assert.AreEqual(t('b', 'b'), q("Bb Maj"));
            Assert.AreEqual(t('e', 'b'), q("Eb Maj"));
            Assert.AreEqual(t('a', 'b'), q("Ab Maj"));
            Assert.AreEqual(t('d', 'b'), q("Db Maj"));
            Assert.AreEqual(t('g', 'b'), q("Gb Maj"));
            Assert.AreEqual(t('c', 'b'), q("Cb Maj"));
        }

        [TestMethod]
        public void Test_K_Minor_Key() {
            static Tone t(char c, char a) => new Tone(c, a.AsAccidental());
            static Tone q(string k) => ParseKeyField("K:" + k).KeyValue.Tones.Tones[0];

            Assert.AreEqual(t('a', '#'), q("A# minor"));
            Assert.AreEqual(t('d', '#'), q("D# minor"));
            Assert.AreEqual(t('g', '#'), q("G# minor"));
            Assert.AreEqual(t('c', '#'), q("C# minor"));
            Assert.AreEqual(t('f', '#'), q("F# minor"));
            Assert.AreEqual(t('b', ' '), q("B  min"));
            Assert.AreEqual(t('e', ' '), q("E  min"));
            Assert.AreEqual(t('a', ' '), q("A  min"));
            Assert.AreEqual(t('d', ' '), q("D  min"));
            Assert.AreEqual(t('g', ' '), q("G  min"));
            Assert.AreEqual(t('c', ' '), q("C  min"));
            Assert.AreEqual(t('f', ' '), q("F min"));
            Assert.AreEqual(t('b', 'b'), q("Bb min"));
            Assert.AreEqual(t('e', 'b'), q("Eb min"));
            Assert.AreEqual(t('a', 'b'), q("Ab min"));

            Assert.AreEqual(t('a', '#'), q("A# m"));
            Assert.AreEqual(t('d', '#'), q("D# m"));
            Assert.AreEqual(t('g', '#'), q("G# m"));
            Assert.AreEqual(t('c', '#'), q("C# m"));
            Assert.AreEqual(t('f', '#'), q("F# m"));
            Assert.AreEqual(t('b', ' '), q("B  m"));
            Assert.AreEqual(t('e', ' '), q("E  m"));
            Assert.AreEqual(t('a', ' '), q("A  m"));
            Assert.AreEqual(t('d', ' '), q("D  m"));
            Assert.AreEqual(t('g', ' '), q("G  m"));
            Assert.AreEqual(t('c', ' '), q("C  m"));
            Assert.AreEqual(t('f', ' '), q("F  m"));
            Assert.AreEqual(t('b', 'b'), q("Bb m"));
            Assert.AreEqual(t('e', 'b'), q("Eb m"));
            Assert.AreEqual(t('a', 'b'), q("Ab m"));
        }

        [TestMethod]
        public void Test_K_Mixolydian_Key() {
            static Tone t(char c, char a) => new Tone(c, a.AsAccidental());
            static Tone q(string k) => ParseKeyField("K:" + k).KeyValue.Tones.Tones[0];

            Assert.AreEqual(t('g', '#'), q("G# mixolydian"));
            Assert.AreEqual(t('c', '#'), q("C# mixolydian"));
            Assert.AreEqual(t('f', '#'), q("F# mixolydian"));
            Assert.AreEqual(t('b', ' '), q("B  mixolydian"));
            Assert.AreEqual(t('e', ' '), q("E  mixolydian"));
            Assert.AreEqual(t('a', ' '), q("A  mix"));
            Assert.AreEqual(t('d', ' '), q("D  mix"));
            Assert.AreEqual(t('g', ' '), q("G  mix"));
            Assert.AreEqual(t('c', ' '), q("C  mix"));
            Assert.AreEqual(t('f', ' '), q("F  mix"));
            Assert.AreEqual(t('b', 'b'), q("Bb mix"));
            Assert.AreEqual(t('e', 'b'), q("Eb mix"));
            Assert.AreEqual(t('a', 'b'), q("Ab mix"));
            Assert.AreEqual(t('d', 'b'), q("Db mix"));
            Assert.AreEqual(t('g', 'b'), q("Gb mix"));
        }

        [TestMethod]
        public void Test_K_Dorian_Key() {
            static Tone t(char c, char a) => new Tone(c, a.AsAccidental());
            static Tone q(string k) => ParseKeyField("K:" + k).KeyValue.Tones.Tones[0];

            Assert.AreEqual(t('d', '#'), q("D# dorian"));
            Assert.AreEqual(t('g', '#'), q("G# dorian"));
            Assert.AreEqual(t('c', '#'), q("C# dorian"));
            Assert.AreEqual(t('f', ' '), q("F  dorian"));
            Assert.AreEqual(t('b', ' '), q("B  dorian"));
            Assert.AreEqual(t('e', ' '), q("E  dor"));
            Assert.AreEqual(t('a', ' '), q("A  dor"));
            Assert.AreEqual(t('d', ' '), q("D  dor"));
            Assert.AreEqual(t('g', ' '), q("G  dor"));
            Assert.AreEqual(t('c', ' '), q("C  dor"));
            Assert.AreEqual(t('f', ' '), q("F  dor"));
            Assert.AreEqual(t('b', 'b'), q("Bb dor"));
            Assert.AreEqual(t('e', 'b'), q("Eb dor"));
            Assert.AreEqual(t('a', 'b'), q("Ab dor"));
            Assert.AreEqual(t('d', 'b'), q("Db dor"));
        }

        [TestMethod]
        public void Test_K_Phrygian_Key() {
            static Tone t(char c, char a) => new Tone(c, a.AsAccidental());
            static Tone q(string k) => ParseKeyField("K:" + k).KeyValue.Tones.Tones[0];

            Assert.AreEqual(t('e', '#'), q("E# phrygian"));
            Assert.AreEqual(t('a', '#'), q("A# phrygian"));
            Assert.AreEqual(t('d', '#'), q("D# phrygian"));
            Assert.AreEqual(t('g', '#'), q("G# phrygian"));
            Assert.AreEqual(t('c', '#'), q("C# phrygian"));
            Assert.AreEqual(t('f', '#'), q("F# phr"));
            Assert.AreEqual(t('b', ' '), q("B  phr"));
            Assert.AreEqual(t('e', ' '), q("E  phr"));
            Assert.AreEqual(t('a', ' '), q("A  phr"));
            Assert.AreEqual(t('d', ' '), q("D  phr"));
            Assert.AreEqual(t('g', ' '), q("G  phr"));
            Assert.AreEqual(t('c', ' '), q("C  phr"));
            Assert.AreEqual(t('f', ' '), q("F  phr"));
            Assert.AreEqual(t('b', 'b'), q("Bb phr"));
            Assert.AreEqual(t('e', 'b'), q("Eb phr"));
        }

        [TestMethod]
        public void Test_K_No_Key() {
            static Tone t(char c, char a) => new Tone(c, a.AsAccidental());
            static Tone q(string k) => ParseKeyField("K:" + k).KeyValue.Tones.Tones[0];

            Assert.AreEqual(t('c', ' '), q(""));
            Assert.AreEqual(t('c', ' '), q("  "));
            Assert.AreEqual(t('c', ' '), q("none"));
        }

        [TestMethod]
        public void Test_K_Explicit_Key() {
            static Tone[] a(string k) => ParseKeyField("K:" + k).KeyValue.Tones.Accidentals.ToArray();

            Assert.AreEqualSeq(StringToTones("bb", "eb"), a("D Phr"));
            Assert.AreEqualSeq(StringToTones("bb", "eb", "f#"), a("D Phr ^f"));
            Assert.AreEqualSeq(StringToTones("f#"), a("D maj =c"));

            Assert.AreEqualSeq(StringToTones("bb", "eb", "f#"), a("D exp _b _e  ^f"));

            Assert.AreEqualSeq(StringToTones(""), a("HP"));
            Assert.AreEqualSeq(StringToTones("f#", "c#", "g="), a("Hp"));

        }

        [TestMethod]
        public void Test_K_Lydian_Key() {
            static Tone t(char c, char a) => new Tone(c, a.AsAccidental());
            static Tone q(string k) => ParseKeyField("K:" + k).KeyValue.Tones.Tones[0];

            Assert.AreEqual(t('f', '#'), q("F# lydian"));
            Assert.AreEqual(t('b', ' '), q("B  lydian"));
            Assert.AreEqual(t('e', ' '), q("E  lydian"));
            Assert.AreEqual(t('a', ' '), q("A  lydian"));
            Assert.AreEqual(t('d', ' '), q("D  lydian"));
            Assert.AreEqual(t('g', ' '), q("G  lyd"));
            Assert.AreEqual(t('c', ' '), q("C  lyd"));
            Assert.AreEqual(t('f', ' '), q("F  lyd"));
            Assert.AreEqual(t('b', 'b'), q("Bb lyd"));
            Assert.AreEqual(t('e', 'b'), q("Eb lyd"));
            Assert.AreEqual(t('a', 'b'), q("Ab lyd"));
            Assert.AreEqual(t('d', 'b'), q("Db lyd"));
            Assert.AreEqual(t('g', 'b'), q("Gb lyd"));
            Assert.AreEqual(t('c', 'b'), q("Cb lyd"));
            Assert.AreEqual(t('f', 'b'), q("Fb lyd"));
        }


        [TestMethod]
        public void Test_K_Locrian_Key() {
            Assert.AreEqual(ToneOf('b', '#'), KeyOf("B# locrian"));
            Assert.AreEqual(ToneOf('e', '#'), KeyOf("E# locrian"));
            Assert.AreEqual(ToneOf('a', '#'), KeyOf("A# locrian"));
            Assert.AreEqual(ToneOf('d', '#'), KeyOf("D# locrian"));
            Assert.AreEqual(ToneOf('g', '#'), KeyOf("G# locrian"));
            Assert.AreEqual(ToneOf('c', '#'), KeyOf("C# loc"));
            Assert.AreEqual(ToneOf('f', '#'), KeyOf("F# loc"));
            Assert.AreEqual(ToneOf('b', ' '), KeyOf("B  loc"));
            Assert.AreEqual(ToneOf('e', ' '), KeyOf("E  loc"));
            Assert.AreEqual(ToneOf('a', ' '), KeyOf("A  loc"));
            Assert.AreEqual(ToneOf('d', ' '), KeyOf("D  loc"));
            Assert.AreEqual(ToneOf('g', ' '), KeyOf("G  loc"));
            Assert.AreEqual(ToneOf('c', ' '), KeyOf("C  loc"));
            Assert.AreEqual(ToneOf('f', ' '), KeyOf("F  loc"));
            Assert.AreEqual(ToneOf('b', 'b'), KeyOf("Bb loc"));
        }

        [TestMethod]
        public void Test_K_Clefs() {
            Assert.AreEqual(ClefMode.Treble, ClefOfK("treble").Clef);
            Assert.AreEqual(ClefMode.Treble, ClefOfK("clef=treble").Clef);
            Assert.AreEqual(ClefMode.Alto, ClefOfK("alto").Clef);
            Assert.AreEqual(ClefMode.Alto, ClefOfK("clef=alto").Clef);
            Assert.AreEqual(ClefMode.Tenor, ClefOfK("tenor").Clef);
            Assert.AreEqual(ClefMode.Tenor, ClefOfK("clef=tenor").Clef);
            Assert.AreEqual(ClefMode.Bass, ClefOfK("bass").Clef);
            Assert.AreEqual(ClefMode.Bass, ClefOfK("clef=bass").Clef);

            Assert.AreEqual(ClefMode.Treble, ClefOfK("treble").Clef);
            Assert.AreEqual(ClefTranspose.Undefined, ClefOfK("treble").ClefTranspose);
            Assert.AreEqual(ClefTranspose.AddEight, ClefOfK("treble+8").ClefTranspose);
            Assert.AreEqual(ClefTranspose.SubtractEight, ClefOfK("treble-8").ClefTranspose);

            Assert.AreEqual(2, ClefOfK("treble").ClefLine);
            Assert.AreEqual(1, ClefOfK("treble1").ClefLine);
            Assert.AreEqual(1, ClefOfK("treble1+8").ClefLine);

            Assert.AreEqual('a', ClefOfK("treble middle=a").Middle);
            Assert.AreEqual('A', ClefOfK("treble middle=A").Middle);

            Assert.AreEqual(33, ClefOfK("trebble transpose=33").Transpose);
            Assert.AreEqual(-64, ClefOfK("trebble transpose=-64").Transpose);
            Assert.AreEqual(33, ClefOfK("trebble middle=a transpose=33").Transpose);
            Assert.AreEqual(-64, ClefOfK("trebble middle=a transpose=-64").Transpose);

            Assert.AreEqual(0, ClefOfK("trebble").Octaves);
            Assert.AreEqual(+1, ClefOfK("trebble octave=1").Octaves);
            Assert.AreEqual(-1, ClefOfK("trebble octave=-1").Octaves);

            Assert.AreEqual(5, ClefOfK("trebble").Stafflines);
            Assert.AreEqual(1, ClefOfK("trebble stafflines=1").Stafflines);
            Assert.AreEqual(5, ClefOfK("trebble stafflines=5").Stafflines);

            Assert.AreEqual(3, ClefOfK("G maj trebble stafflines=3", KeyStatus.ValidKey).Stafflines);
            Assert.AreEqual(3, ClefOfK("G maj clef=trebble stafflines=3", KeyStatus.ValidKey).Stafflines);

        }

        [TestMethod]
        public void Test_L_NoteLength() {
            var field = ParseLengthField("L:1/1");
            Assert.AreEqual(field.Kind, InformationFieldKind.UnitNoteLength);
            Assert.AreEqual(field.Kind.InFileHeader(), true);
            Assert.AreEqual(field.Kind.InTuneHeader(), true);
            Assert.AreEqual(field.Kind.InTuneBody(), true);
            Assert.AreEqual(field.Kind.InInline(), true);
            Assert.AreEqual(field.Kind.GetContentType(), InformationFieldContent.NoteLength);

            Assert.AreEqual(new Fraction(1, 1), LengthOf("1/1"));
            Assert.AreEqual(new Fraction(1, 2), LengthOf("1/2"));
            Assert.AreEqual(new Fraction(1, 4), LengthOf("1/4"));
            Assert.AreEqual(new Fraction(1, 8), LengthOf("1/8"));
            Assert.AreEqual(new Fraction(1, 16), LengthOf("1/16"));
            Assert.AreEqual(new Fraction(1, 32), LengthOf("1/32"));
            Assert.AreEqual(new Fraction(1, 64), LengthOf("1/64"));

        }

        [TestMethod]
        public void Test_M_Meter() {
            var field = ParseMeterField("M:3/4");
            Assert.AreEqual(field.Kind, InformationFieldKind.Meter);
            Assert.AreEqual(field.Kind.InFileHeader(), true);
            Assert.AreEqual(field.Kind.InTuneHeader(), true);
            Assert.AreEqual(field.Kind.InTuneBody(), true);
            Assert.AreEqual(field.Kind.InInline(), true);
            Assert.AreEqual(field.Kind.GetContentType(), InformationFieldContent.Meter);

            Assert.AreEqual(new Meter(new Fraction(4, 4)), MeterOf("C"));
            Assert.AreEqual(new Meter(new Fraction(2, 2)), MeterOf("C|"));
            Assert.AreEqual(new Meter(new Fraction(3, 4)), MeterOf("3/4"));
            Assert.AreEqual(new Meter(new Fraction(1, 4), new Fraction(2, 4)), MeterOf("(1+2)/4"));
            Assert.AreEqual(new Meter(new Fraction(1, 4), new Fraction(2, 4)), MeterOf("1+2/4"));

        }

        [TestMethod]
        public void Test_M_Macro() {
            var field = ParseMacroField("m: ~G2 = {A}G{F}G");
            Assert.AreEqual(field.Kind, InformationFieldKind.Macro);
            Assert.AreEqual(field.Kind.InFileHeader(), true);
            Assert.AreEqual(field.Kind.InTuneHeader(), true);
            Assert.AreEqual(field.Kind.InTuneBody(), true);
            Assert.AreEqual(field.Kind.InInline(), true);
            Assert.AreEqual(field.Kind.GetContentType(), InformationFieldContent.Macro);
        }

        [TestMethod]
        public void Test_N_Notes() {
            var field = ParseInfoField("N:note text");
            Assert.AreEqual(field.Kind, InformationFieldKind.Notes);
            Assert.AreEqual(field.Kind.InFileHeader(), true);
            Assert.AreEqual(field.Kind.InTuneHeader(), true);
            Assert.AreEqual(field.Kind.InTuneBody(), true);
            Assert.AreEqual(field.Kind.InInline(), true);
            Assert.AreEqual(field.Kind.GetContentType(), InformationFieldContent.StringContent);
        }

        [TestMethod]
        public void Test_O_Origin() {
            var field = ParseInfoField("O:Germany; Bavaria");
            Assert.AreEqual(field.Kind, InformationFieldKind.Origin);
            Assert.AreEqual(field.Kind.InFileHeader(), true);
            Assert.AreEqual(field.Kind.InTuneHeader(), true);
            Assert.AreEqual(field.Kind.InTuneBody(), false);
            Assert.AreEqual(field.Kind.InInline(), false);
            Assert.AreEqual(field.Kind.GetContentType(), InformationFieldContent.Origin);

            Assert.AreEqual("Germany", field.Value.GetValueAfterSemicolon(0));
            Assert.AreEqual("Bavaria", field.Value.GetValueAfterSemicolon(1));
        }

        [TestMethod]
        public void Test_P_Parts() {
            var field = ParsePartsField("P:A");
            Assert.AreEqual(field.Kind, InformationFieldKind.Parts);
            Assert.AreEqual(field.Kind.InFileHeader(), false);
            Assert.AreEqual(field.Kind.InTuneHeader(), true);
            Assert.AreEqual(field.Kind.InTuneBody(), true);
            Assert.AreEqual(field.Kind.InInline(), true);
            Assert.AreEqual(field.Kind.GetContentType(), InformationFieldContent.Parts);

            field = ParsePartsField("P:...A");
            Assert.AreEqual('A', (field.Items[3] as PartName)?.Name);

            field = ParsePartsField("P:...A10");
            Assert.AreEqual(10, (field.Items[4] as PartRepeat)?.Repeat);

            field = ParsePartsField("P:...A3..");
            Assert.AreEqual(3, (field.Items[4] as PartRepeat)?.Repeat);

            field = ParsePartsField("P:(abC)3");
            Assert.AreEqual('C', ((field.Items[0] as PartGroup)?.Items[2] as PartName)?.Name);
        }

        [TestMethod]
        public void Test_Q_Tempo() {
            var field = ParseTempoField("Q:1/2=120");
            Assert.AreEqual(field.Kind, InformationFieldKind.Tempo);
            Assert.AreEqual(field.Kind.InFileHeader(), false);
            Assert.AreEqual(field.Kind.InTuneHeader(), true);
            Assert.AreEqual(field.Kind.InTuneBody(), true);
            Assert.AreEqual(field.Kind.InInline(), true);
            Assert.AreEqual(field.Kind.GetContentType(), InformationFieldContent.Tempo);

            field = ParseTempoField("Q:1/2=120");
            Assert.AreEqual(field.Bpm, 120);
            Assert.AreEqualSeq(new[] { new Fraction(1, 2) }, field.Fractions.ToArray());

            field = ParseTempoField("Q:1/4 1/2=140");
            Assert.AreEqual(field.Bpm, 140);
            Assert.AreEqualSeq(new[] { new Fraction(1, 4), new Fraction(1, 2) }, field.Fractions.ToArray());

            field = ParseTempoField("Q:\"allegro\" 1/4 1/2=140");
            Assert.AreEqual("allegro", field.Tempo);

            field = ParseTempoField("Q:   \"allegro\" 1/4 1/2=140");
            Assert.AreEqual("allegro", field.Tempo);

            field = ParseTempoField("Q: 1/4 1/2=140 \"allegro\" ");
            Assert.AreEqual("allegro", field.Tempo);
        }

        [TestMethod]
        public void Test_R_Rythm() {
            var field = ParseInfoField("R:polka");
            Assert.AreEqual(field.Kind, InformationFieldKind.Rythm);
            Assert.AreEqual(field.Kind.InFileHeader(), true);
            Assert.AreEqual(field.Kind.InTuneHeader(), true);
            Assert.AreEqual(field.Kind.InTuneBody(), true);
            Assert.AreEqual(field.Kind.InInline(), true);
            Assert.AreEqual(field.Kind.GetContentType(), InformationFieldContent.StringContent);
        }

        [TestMethod]
        public void Test_R_Remark() {
            var field = ParseInfoField("r:this is a remark");
            Assert.AreEqual(field.Kind, InformationFieldKind.Remark);
            Assert.AreEqual(field.Kind.InFileHeader(), true);
            Assert.AreEqual(field.Kind.InTuneHeader(), true);
            Assert.AreEqual(field.Kind.InTuneBody(), true);
            Assert.AreEqual(field.Kind.InInline(), true);
            Assert.AreEqual(field.Kind.GetContentType(), InformationFieldContent.StringContent);
        }

        [TestMethod]
        public void Test_S_Source() {
            var field = ParseInfoField("S:tunes from germany");
            Assert.AreEqual(field.Kind, InformationFieldKind.Source);
            Assert.AreEqual(field.Kind.InFileHeader(), true);
            Assert.AreEqual(field.Kind.InTuneHeader(), true);
            Assert.AreEqual(field.Kind.InTuneBody(), false);
            Assert.AreEqual(field.Kind.InInline(), false);
            Assert.AreEqual(field.Kind.GetContentType(), InformationFieldContent.StringContent);
        }

        [TestMethod]
        public void Test_S_SymbolLine() {
            var field = ParseSymbolLineField("s:\"^sl ow\"");
            Assert.AreEqual(field.Kind, InformationFieldKind.SymbolLine);
            Assert.AreEqual(field.Kind.InFileHeader(), false);
            Assert.AreEqual(field.Kind.InTuneHeader(), false);
            Assert.AreEqual(field.Kind.InTuneBody(), true);
            Assert.AreEqual(field.Kind.InInline(), false);
            Assert.AreEqual(field.Kind.GetContentType(), InformationFieldContent.Symbols);

            Assert.AreEqual("\"^sl ow\"", field.Value.GetValueAfterWhitespace(0));
            Assert.AreEqual(AnnotationPosition.Above, (field.Elements[0] as Annotation)?.Position);
            Assert.AreEqual("sl ow", (field.Elements[0] as Annotation)?.Text);

            field = ParseSymbolLineField("s:!trill!");
            Assert.AreEqual(DecorationSymbol.Trill, (field.Elements[0] as TuneSymbol)?.Symbol);

            field = ParseSymbolLineField("s: H");
            Assert.AreEqual(DecorationSymbol.Fermata, (field.Elements[0] as TuneSymbol)?.Symbol);

            field = ParseSymbolLineField("s: C7");
            Assert.AreEqual('C', (field.Elements[0] as ChordSymbol)?.Note);
            Assert.AreEqual("7", (field.Elements[0] as ChordSymbol)?.Type);

            field = ParseSymbolLineField("s: C#7");
            Assert.AreEqual('C', (field.Elements[0] as ChordSymbol)?.Note);
            Assert.AreEqual('^'.AsAccidental(), (field.Elements[0] as ChordSymbol)?.Accidental);
            Assert.AreEqual("7", (field.Elements[0] as ChordSymbol)?.Type);

            field = ParseSymbolLineField("s: C7/Eb");
            Assert.AreEqual('C', (field.Elements[0] as ChordSymbol)?.Note);
            Assert.AreEqual("7", (field.Elements[0] as ChordSymbol)?.Type);
            Assert.AreEqual('E', (field.Elements[0] as ChordSymbol)?.BassNote);
            Assert.AreEqual('_'.AsAccidental(), (field.Elements[0] as ChordSymbol)?.BassAccidental);
        }

        [TestMethod]
        public void Test_T_Title() {
            var field = ParseInfoField("T:a small tune");
            Assert.AreEqual(field.Kind, InformationFieldKind.TuneTitle);
            Assert.AreEqual(field.Kind.InFileHeader(), false);
            Assert.AreEqual(field.Kind.InTuneHeader(), true);
            Assert.AreEqual(field.Kind.InTuneBody(), true);
            Assert.AreEqual(field.Kind.InInline(), false);
            Assert.AreEqual(field.Kind.GetContentType(), InformationFieldContent.StringContent);
        }

        [TestMethod]
        public void Test_U_UserDefined() {
            var field = ParseUserDefinedField("U:p = \"^+\"");
            Assert.AreEqual(field.Kind, InformationFieldKind.UserDefined);
            Assert.AreEqual(field.Kind.InFileHeader(), true);
            Assert.AreEqual(field.Kind.InTuneHeader(), true);
            Assert.AreEqual(field.Kind.InTuneBody(), true);
            Assert.AreEqual(field.Kind.InInline(), true);
            Assert.AreEqual(field.Kind.GetContentType(), InformationFieldContent.UserDefined);

            Assert.AreEqual(new Annotation(AnnotationPosition.Above, "+"), field.Symbol);

            field = ParseUserDefinedField("U:p = !none!");
            Assert.AreEqual("p", field.Alias);
            Assert.AreEqual(new UndefinedTuneSymbol(), field.Symbol);

            field = ParseUserDefinedField("U:p = !nil!");
            Assert.AreEqual("p", field.Alias);
            Assert.AreEqual(new UndefinedTuneSymbol(), field.Symbol);
        }

        [TestMethod]
        public void Test_V_Voice() {
            var field = ParseVoiceField("V:3");
            Assert.AreEqual(field.Kind, InformationFieldKind.Voice);
            Assert.AreEqual(field.Kind.InFileHeader(), false);
            Assert.AreEqual(field.Kind.InTuneHeader(), true);
            Assert.AreEqual(field.Kind.InTuneBody(), true);
            Assert.AreEqual(field.Kind.InInline(), true);
            Assert.AreEqual(field.Kind.GetContentType(), InformationFieldContent.Voice);

            Assert.AreEqual("3", field.Id);

            field = ParseVoiceField("V:3 name=\"q\" ");
            Assert.AreEqual("q", field.Name);

            field = ParseVoiceField("V:3 nm=\"q\" ");
            Assert.AreEqual("q", field.Name);

            field = ParseVoiceField("V:3 subname=\"q\" ");
            Assert.AreEqual("q", field.Subname);

            field = ParseVoiceField("V:3 snm=\"q\" ");
            Assert.AreEqual("q", field.Subname);

            field = ParseVoiceField("V:3 stem=up ");
            Assert.AreEqual(StemDirection.Up, field.StemDirection);

            field = ParseVoiceField("V:3 stem=DOWN ");
            Assert.AreEqual(StemDirection.Down, field.StemDirection);

            field = ParseVoiceField("V:3 clef=alto ");
            Assert.AreEqual(ClefMode.Alto, field.Clef.Clef);
        }

        [TestMethod]
        public void Test_W_Words() {
            var field = ParseInfoField("W:words");
            Assert.AreEqual(field.Kind, InformationFieldKind.WordsAfterTune);
            Assert.AreEqual(field.Kind.InFileHeader(), false);
            Assert.AreEqual(field.Kind.InTuneHeader(), true);
            Assert.AreEqual(field.Kind.InTuneBody(), true);
            Assert.AreEqual(field.Kind.InInline(), false);
            Assert.AreEqual(field.Kind.GetContentType(), InformationFieldContent.Words);
        }

        [TestMethod]
        public void Test_w_Words() {
            var field = ParseInfoField("w:words");
            Assert.AreEqual(field.Kind, InformationFieldKind.WordsInTune);
            Assert.AreEqual(field.Kind.InFileHeader(), false);
            Assert.AreEqual(field.Kind.InTuneHeader(), false);
            Assert.AreEqual(field.Kind.InTuneBody(), true);
            Assert.AreEqual(field.Kind.InInline(), false);
            Assert.AreEqual(field.Kind.GetContentType(), InformationFieldContent.Words);
        }

        [TestMethod]
        public void Test_X_ReferenceNumber() {
            var field = ParseInfoField("X:123");
            Assert.AreEqual(field.Kind, InformationFieldKind.RefNumber);
            Assert.AreEqual(field.Kind.InFileHeader(), false);
            Assert.AreEqual(field.Kind.InTuneHeader(), true);
            Assert.AreEqual(field.Kind.InTuneBody(), false);
            Assert.AreEqual(field.Kind.InInline(), false);
            Assert.AreEqual(field.Kind.GetContentType(), InformationFieldContent.RefNumber);

            var f = ParseRefNumberField("X:123");
            Assert.AreEqual(123ul, f?.Number);

            f = ParseRefNumberField("X:");
            Assert.AreEqual(null, f?.Number);
        }

        [TestMethod]
        public void Test_Z_Transcription() {
            var field = ParseInfoField("Z:demo");
            Assert.AreEqual(field.Kind, InformationFieldKind.Transcription);
            Assert.AreEqual(field.Kind.InFileHeader(), true);
            Assert.AreEqual(field.Kind.InTuneHeader(), true);
            Assert.AreEqual(field.Kind.InTuneBody(), false);
            Assert.AreEqual(field.Kind.InInline(), false);
            Assert.AreEqual(field.Kind.GetContentType(), InformationFieldContent.Transcription);

            var f = ParseTranscriptionField("Z:abc-transcription xx");
            Assert.AreEqual(TranscriptionKind.Transcription, f.TranscriptionKind);

            f = ParseTranscriptionField("Z:abc-edited-by xx");
            Assert.AreEqual(TranscriptionKind.Edited, f.TranscriptionKind);

            f = ParseTranscriptionField("Z:abc-copyright xx");
            Assert.AreEqual(TranscriptionKind.Copyright, f.TranscriptionKind);
        }
    }
}