using System.Collections.Generic;
using CommandLine;

namespace RepositoryUtils
{
    [Verb("g", HelpText = "根据数据库生成实体类")]
    public class GenerateOption: CommonUtils.CommandOption
    {
        [Option('t',"dbtype",Required =true,HelpText ="指定数据库类型，如：mssql、mysql、oracle")]
        public string DbType { get; set; }

        [Option('c', "connectionstring", Required = true, HelpText = "设置连接字符串")]
        public string ConnectionString { get; set; }

        [Option('o',"out", Required = true, HelpText = "设置输入目录")]
        public string OutDirectory { get; set; }

        [Option('n', "namespace", Required = false, Default="namespace_xxx", HelpText = "设置命名空间")]
        public string NameSpace { get; set; }

        [Option('d',"dbname", Required = true, HelpText = "指定数据库名称")]
        public string DbName{ get; set; }

        [Option('p', "parent", Required = false, HelpText = "指定父类")]
        public string Parent { get; set; }

        [Option('i', "ignore", Required = false,HelpText = "忽略指定的列名，多个列使用:分隔")]
        public IList<string> Ignores { get; set; }

    }
}
