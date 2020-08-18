namespace AbcTuneTool.Common {

    /// <summary>
    ///     helper functions for loggers
    /// </summary>
    public static class LoggerHelper {

        /// <summary>
        ///     log an error
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="messageNumber"></param>
        /// <param name="messageParameters"></param>
        public static void Error(this Logger logger, int messageNumber, params object[] messageParameters)
            => logger.Log(new LogEntry(LogSeverity.Error, messageNumber, messageParameters));

        /// <summary>
        ///     log an information
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="messageNumber"></param>
        /// <param name="messageParameters"></param>
        public static void Info(this Logger logger, int messageNumber, params object[] messageParameters)
            => logger.Log(new LogEntry(LogSeverity.Information, messageNumber, messageParameters));


    }
}
