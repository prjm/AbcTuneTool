using System;

namespace AbcTuneSampleApp {
    class Program {

        static void Main() {
            /*
            var s = new StandardToneSystem(KnownStrings.LocrianMode, 11);
            var k = s;//.DefineKey(-7);

            foreach (var n in k.MainTones)
                System.Console.WriteLine(n.Name + " " + n.Accidental.AsChar());

            return;
            */
            var app = new TokenizeFile();
            app.RunSample();
            Console.ReadLine();
        }
    }
}
