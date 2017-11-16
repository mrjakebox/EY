using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkWithFiles
{

    public class Logger
    {
        private string DatetimeFormat = "yyyy-MM-dd HH:mm:ss.fff";
        private bool append;
        public Logger(bool append = false) {
            this.append = append;
        }
        public void Debug(string text)
        {
            WriteFormattedLog(LogLevel.DEBUG, text);
        }
        public void Error(string text)
        {
            WriteFormattedLog(LogLevel.ERROR, text);
        }
        public void Fatal(string text)
        {
            WriteFormattedLog(LogLevel.FATAL, text);
        }
        public void Info(string text)
        {
            WriteFormattedLog(LogLevel.INFO, text);
        }
        public void Trace(string text)
        {
            WriteFormattedLog(LogLevel.TRACE, text);
        }
        public void Warning(string text)
        {
            WriteFormattedLog(LogLevel.WARNING, text);
        }
        private void WriteFormattedLog(LogLevel level, string text)
        {
            if (append)
            {
                string datetext = DateTime.Now.ToString(DatetimeFormat);
                string pretext;

                Console.Write(datetext);


                switch (level)
                {
                    case LogLevel.TRACE: pretext = " [TRACE]   "; Console.ForegroundColor = ConsoleColor.Gray; break;
                    case LogLevel.INFO: pretext = " [INFO]    "; Console.ForegroundColor = ConsoleColor.Green; break;
                    case LogLevel.DEBUG: pretext = " [DEBUG]   "; Console.ForegroundColor = ConsoleColor.DarkGreen; break;
                    case LogLevel.WARNING: pretext = " [WARNING] "; Console.ForegroundColor = ConsoleColor.Yellow; break;
                    case LogLevel.ERROR: pretext = " [ERROR]   "; Console.ForegroundColor = ConsoleColor.Red; break;
                    case LogLevel.FATAL: pretext = " [FATAL]   "; Console.ForegroundColor = ConsoleColor.DarkRed; break;
                    default: pretext = ""; break;
                }

                Console.Write(pretext);
                Console.ResetColor();
                Console.WriteLine(text);
            }
        }
        [Flags]
        private enum LogLevel
        {
            TRACE,
            INFO,
            DEBUG,
            WARNING,
            ERROR,
            FATAL
        }
    }
}
