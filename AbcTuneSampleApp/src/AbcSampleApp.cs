using System;
using System.Diagnostics;
using System.Resources;
using AbcTuneTool.Common;

namespace AbcTuneSampleApp {

    public abstract class AbcSampleApp {

        public AbcSampleApp() {
            Logger = new Logger();
            Cache = new StringCache();
            StringBuilderPool = new StringBuilderPool();
            ResMgr = new ResourceManager("AbcTuneSampleApp.resources.MessageTexts", typeof(AbcSampleApp).Assembly);
            ListPools = new ListPools();
        }

        public Logger Logger { get; }
        public StringCache Cache { get; }
        public StringBuilderPool StringBuilderPool { get; }
        public ResourceManager ResMgr { get; }
        public ListPools ListPools { get; }

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