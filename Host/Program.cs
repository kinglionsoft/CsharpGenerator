using System;
using System.Linq;
using CommandLine;
using CommonUtils;

namespace Host
{
    class Program
    {
        static void Main(string[] args)
        {
            Parser.Default.ParseArguments<RepositoryUtils.GenerateOption>(args)
                .WithParsed<RepositoryUtils.GenerateOption>(option =>
               {
                   SafeRun(option, (o) =>
                   {
                       new RepositoryUtils.EntityGenerator(o).Generate();
                   });
               })
                .WithNotParsed(error =>
                {
                    Logger.Log(string.Join("; ", error.Select(a=>a.ToString())), ELoggerLevel.Error);
                });            
        }

        static void SafeRun<TOption>(TOption option, Action<TOption> action)
        {
            try
            {
                action(option);
            }
            catch (Exception ex)
            {
                Logger.Log(ex, ELoggerLevel.Error);
                Environment.Exit(4);
            }
        }
    }
}