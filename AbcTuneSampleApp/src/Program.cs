using System;
using AbcTuneTool.Model;

namespace AbcTuneSampleApp {
    class Program {

        static void Main() {

            var s = new StandardToneSystem(KnownStrings.LocrianMode, 11);
            var k = s.DefineKey(7, true);

            foreach (var n in k.MainTones)
                System.Console.WriteLine(n.ToString());

            return;

            var app = new TokenizeFile();
            app.RunSample();
            Console.ReadLine();
        }
    }
}
