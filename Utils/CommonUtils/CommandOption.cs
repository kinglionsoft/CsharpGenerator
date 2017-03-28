using CommandLine;

namespace CommonUtils
{
    public class CommandOption
    {
        [Option('l',"loggerlevel", Required = false, Default=2, HelpText = "设置日志输出级别, 0-6")]
        public ELoggerLevel LoggerLevel { get; set; }
    }
}
