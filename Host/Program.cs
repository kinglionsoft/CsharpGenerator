using System;
using System.Linq;
using CommandLine;
using CommonUtils;

namespace Host
{
    class Program
    {
        /// <summary>
        /// g -t mysql -o out -n test_namespace -d test -l 1 -c "server=172.16.20.xx;database=test;uid=root;pwd=1234;charset='utf8'"
        ///  g -t mssql -o out -n test_namespace -d DataChannel -l 1 -c "Data Source=.;Initial Catalog=DataChannel;Integrated Security=True"
        /// </summary>
        /// <param name="args"></param>
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