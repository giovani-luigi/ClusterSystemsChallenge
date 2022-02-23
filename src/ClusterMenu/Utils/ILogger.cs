using System;

namespace ClusterMenu.Utils {

    /// <summary>
    /// Interface for loggers
    /// </summary>
    public interface ILogger {

        /// <summary>
        /// Write an information to the log
        /// </summary>
        void LogInfo(string message);

        /// <summary>
        /// Write a warning message <paramref name="message"/> to the log,
        /// with optionally <paramref name="exception"/> as related exception.
        /// </summary>
        void LogWarning(string message, Exception exception = null);

        /// <summary>
        /// Write a warning message <paramref name="message"/> to the log,
        /// with <paramref name="exception"/> as related exception.
        /// </summary>
        void LogError(string message, Exception exception);

    }
}
