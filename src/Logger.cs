using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace KouCoCoa {
    internal static class Logger {
        internal static string Timestamp {
            get {
                return $"{DateTime.Now:yyyy-MM-dd HH:mm:ss}";
            }
        }

        private static string _logPath = "";

        internal static void CreateLogFile() {
            var logDirectory = "Logs";
            System.IO.Directory.CreateDirectory(logDirectory);
            var logFileName = $"[{DateTime.Now:yyyy-MM-dd}]KouCoCoa-{DateTime.Now:HHmmss}.log";
            _logPath = Path.Combine(logDirectory, logFileName);
            try {
                using (StreamWriter sw = File.CreateText(_logPath)) {
                    sw.WriteLine($"[{Timestamp}] [KouKou] おはよう、 お兄ちゃん! こうこう、頑張ります！!");
                }
            } catch (Exception) {
                throw;
            }
            Console.WriteLine($"Log file initiated at {_logPath}");
        }

        internal static async Task WriteLine(string logMessage, LogLevel? logLevel = null) {
#if DEBUG
            if (Globals.SilenceLogger) {
                return;
            }
#endif
            logLevel ??= LogLevel.Info;
            string line;
            line = $"[{Timestamp}] [{logLevel}] {logMessage}";
            await CommitLog(line, logLevel);
        }

        private static async Task CommitLog(string line, LogLevel? logLevel) {
            // console output, always happens
            Console.WriteLine(line);
            // write to file system only on appropriate config/loglevel
            if (Globals.RunConfig.LoggingLevel >= logLevel) {
                try {
                    using (StreamWriter sw = File.AppendText(_logPath)) {
                        await sw.WriteLineAsync(line);
                    }
                } catch (Exception) {
                    throw;
                }
            }
        }
    }
}
