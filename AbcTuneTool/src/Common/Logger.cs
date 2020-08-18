using System.Collections.Generic;

namespace AbcTuneTool.Common {

    /// <summary>
    ///     simple logger
    /// </summary>
    public class Logger {

        /// <summary>
        ///     create a new logger
        /// </summary>
        public Logger()
            => Entries = new List<LogEntry>();

        /// <summary>
        ///     list of log entries
        /// </summary>
        public List<LogEntry> Entries { get; }

        /// <summary>
        ///     number of entries
        /// </summary>
        public int EntryCount
            => Entries.Count;

        /// <summary>
        ///     make a new log entry
        /// </summary>
        /// <param name="entry"></param>
        public void Log(LogEntry entry)
           => Entries.Add(entry);

    }
}
