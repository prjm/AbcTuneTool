using System;
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
        public SystemStatus(Process process) {
            WorkingSet64 = process.WorkingSet64;
            StatusTime = DateTime.Now;
        }

        /// <summary>
        ///     create a difference of two status points
        /// </summary>
        /// <param name="infoBefore"></param>
        /// <param name="infoAfterRun"></param>
        public SystemStatus(SystemStatus infoBefore, SystemStatus infoAfterRun) : this(Process.GetCurrentProcess()) {
            RequiredWorkingSet = infoAfterRun.WorkingSet64 - infoBefore.WorkingSet64;
            Duration = infoAfterRun.StatusTime - infoAfterRun.StatusTime;
        }

        /// <summary>
        ///     print the system status to a logger
        /// </summary>
        /// <param name="logger"></param>
        public void ToLogger(Logger logger) {
            logger.Info(LogMessage.RequiredWorkingSetMemory, WorkingSet64);
            logger.Info(LogMessage.RequiredDuration, Duration.Ticks);
        }

        /// <summary>
        ///     working set size
        /// </summary>
        public long WorkingSet64 { get; }

        /// <summary>
        ///     status time
        /// </summary>
        public DateTime StatusTime { get; }

        /// <summary>
        ///     required working set
        /// </summary>
        public long RequiredWorkingSet { get; }

        /// <summary>
        ///     required durtion
        /// </summary>
        public TimeSpan Duration { get; }
    }
}
