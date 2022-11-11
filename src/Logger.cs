using System;
using System.Collections.Generic;
using System.IO;

namespace KouCoCoa {
    internal static class Logger {
        public static string Timestamp {
            get {
                return $"{DateTime.Now:yyyy-MM-dd HH:mm:ss}";
            }
        }

        private static string _logPath = "";

        public static void CreateLogFile() {
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

        public static void WriteLine(string logMessage, LogLevel logLevel = LogLevel.Info) {
            if (Globals.RunConfig.SilenceLogger) {
                return;
            }
            string line;
            line = $"[{Timestamp}] [{logLevel}] {logMessage}";
            CommitLog(line, logLevel);
        }

        private static void CommitLog(string line, LogLevel logLevel) {
            // Output log only if we are at an appropriate loglevel
            if (Globals.RunConfig.LoggingLevel >= logLevel) {
                Console.WriteLine(line);
                try {
                    using (StreamWriter sw = File.AppendText(_logPath)) {
                        sw.WriteLine(line);
                    }
                } catch (Exception) {
                    throw;
                }
            }
        }
    }
}
