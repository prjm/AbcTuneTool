using System;
using System.Diagnostics;
using System.Resources;
using AbcTuneTool.Common;
using AbcTuneTool.FileIo;

namespace AbcTuneSampleApp {

    public abstract class AbcSampleApp {

        public AbcSampleApp() {
            Logger = new Logger();
            Cache = new StringCache();
            CharCache = new AbcCharacterCache();
            StringBuilderPool = new StringBuilderPool();
            ResMgr = new ResourceManager("AbcTuneSampleApp.MessageTexts", typeof(AbcSampleApp).Assembly);
        }

        public Logger Logger { get; }
        public StringCache Cache { get; }
        public AbcCharacterCache CharCache { get; }
        public StringBuilderPool StringBuilderPool { get; }
        public ResourceManager ResMgr { get; }

        protected abstract void Run();

        public void RunSample() {
            GC.Collect();
            var infoBefore = new SystemStatus(Process.GetCurrentProcess());
            Run();
            var infoAfterRun = new SystemStatus(Process.GetCurrentProcess());
            var diff = new SystemStatus(infoBefore, infoAfterRun);

            diff.ToLogger(Logger);

            foreach (var message in Logger.Entries) {
                Console.Write(message.Severity.ToShortString());
                Console.Write(message.MessageNumber.ToString());
                Console.Write("\t\t");

                var text = ResMgr.GetString($"M_{ message.MessageNumber }");

                Console.Write(string.Format(text ?? string.Empty, message.MessageParameters));
                Console.WriteLine();
            }
        }

    }
}