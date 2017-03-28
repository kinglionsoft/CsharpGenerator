using System;

namespace CommonUtils
{
    public static class Logger
    {
        private static ELoggerLevel _level = ELoggerLevel.Info;

        public static ELoggerLevel Level
        {
            get => _level;
            set { if (value != _level) _level = value; }
        }

        public static void Log(string msg, ELoggerLevel type = ELoggerLevel.Info)
        {
            if (type < _level) return;

            Console.WriteLine($"{type.ToString().ToUpper()}: {msg}");
        }
        
        public static void Log(Exception ex, ELoggerLevel level)
        {
            Log(GetError(ex), level);
        }

        private static string GetError(Exception ex)
        {
            var divider = "-->";
            var exceptionMessage = ex.Message;
            var deep = 3;
            var innerException = ex.InnerException;
            while (innerException != null && deep > 0)
            {
                exceptionMessage += divider + innerException.Message;
                innerException = innerException.InnerException;
                deep--;
            }

            return ex.Message + divider + exceptionMessage;
        }
    }

    public enum ELoggerLevel
    {
        Off = 0,
        Debug = 1,
        Info = 2,
        Warn = 3,
        Error = 4,
        FATAL = 5,
        All = 6,
    }
}
