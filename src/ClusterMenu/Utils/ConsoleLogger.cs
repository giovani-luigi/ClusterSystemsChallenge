using System;

namespace ClusterMenu.Utils {

    /// <summary>
    /// Provides an implementation of a <see cref="ILogger"/> that writes log messages to the console
    /// </summary>
    public class ConsoleLogger : ILogger {

        /// <inheritdoc />
        public void LogInfo(string message) {
            Console.WriteLine($@"{DateTime.Now.ToLongTimeString()} (i) {message}");
        }

        /// <inheritdoc />
        public void LogWarning(string message, Exception ex = null) {
            if (ex is null) {
                Console.WriteLine($@"{DateTime.Now.ToLongTimeString()} (!) {message}");
            }
            Console.WriteLine($@"{DateTime.Now.ToLongTimeString()} (!) {message}, Exception: {ex}");
        }

        /// <inheritdoc />
        public void LogError(string message, Exception ex) {
            Console.WriteLine($@"{DateTime.Now.ToLongTimeString()} (X) {message}, Exception: {ex}");
        }
    }
}
