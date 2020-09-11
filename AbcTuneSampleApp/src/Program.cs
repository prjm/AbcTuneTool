using System;
using AbcTuneTool.Common;
using AbcTuneTool.Model;

namespace AbcTuneSampleApp {
    class Program {

        static void Main() {

            var s = new MinorKeyTable();
            s.DefineKey('A', 'b'.AsAccidental());

            foreach (var n in s.Tones.MainTones)
                System.Console.WriteLine(n.ToString());

            Console.WriteLine();
            Console.Write("Accidentals: ");
            foreach (var n in s.Tones.Accidentals)
                System.Console.Write(n.ToString());
            Console.WriteLine();

            return;

            var app = new TokenizeFile();
            app.RunSample();
            Console.ReadLine();
        }
    }
}
