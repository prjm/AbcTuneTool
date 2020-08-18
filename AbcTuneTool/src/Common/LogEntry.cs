namespace AbcTuneTool.Common {

    /// <summary>
    ///     log entry
    /// </summary>
    public class LogEntry {

        /// <summary>
        ///     create a new log entry
        /// </summary>
        /// <param name="severity">severity</param>
        /// <param name="messageNumber">message number</param>
        /// <param name="messageParameters">message parameters</param>
        public LogEntry(LogSeverity severity, int messageNumber, params object[] messageParameters) {
            Severity = severity;
            MessageNumber = messageNumber;
            MessageParameters = messageParameters;
        }

        /// <summary>
        ///     message severity
        /// </summary>
        public LogSeverity Severity { get; }

        /// <summary>
        ///     message number
        /// </summary>
        public int MessageNumber { get; }

        /// <summary>
        ///     message parameters
        /// </summary>
        public object[] MessageParameters { get; }

    }
}
