using System.Diagnostics;

namespace AbcTuneTool.Common {

    /// <summary>
    ///     system status information
    /// </summary>
    public class SystemStatus {

        /// <summary>
        ///     create a new system status
        /// </summary>
        /// <param name="process"></param>
        public SystemStatus(Process process)
            => WorkingSet64 = process.WorkingSet64;

        /// <summary>
        ///     create a diff of two status points
        /// </summary>
        /// <param name="infoBefore"></param>
        /// <param name="infoAfterRun"></param>
        public SystemStatus(SystemStatus infoBefore, SystemStatus infoAfterRun)
            => WorkingSet64 = infoAfterRun.WorkingSet64 - infoBefore.WorkingSet64;

        /// <summary>
        ///     print the system status to a logger
        /// </summary>
        /// <param name="logger"></param>
        public void ToLogger(Logger logger)
            => logger.Info(LogMessage.CurrentWorkingSetSize, WorkingSet64);

        /// <summary>
        ///     working set size
        /// </summary>
        public long WorkingSet64 { get; }
    }
}
