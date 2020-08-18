namespace AbcTuneTool.Common {

    /// <summary>
    ///     log message severity
    /// </summary>
    public enum LogSeverity {

        /// <summary>
        ///     undefined severity
        /// </summary>
        Undefined = 0,

        /// <summary>
        ///     information message
        /// </summary>
        Information = 1,

        /// <summary>
        ///     warning message
        /// </summary>
        Warning = 2,

        /// <summary>
        ///     error message
        /// </summary>
        Error = 3

    }

    /// <summary>
    ///     message severity helper
    /// </summary>
    public static class LogSeverityHelper {

        /// <summary>
        ///     convert a message severity to a short string
        /// </summary>
        /// <param name="severity"></param>
        /// <returns></returns>
        public static string ToShortString(this LogSeverity severity)
            => severity switch
            {
                LogSeverity.Warning => "W",
                LogSeverity.Error => "E",
                LogSeverity.Information => "I",
                _ => "?"
            };

    }

}
