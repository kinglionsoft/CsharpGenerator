using System;
using System.IO;
using System.Text;
using CommonUtils;
using RepositoryUtils.Common;

namespace RepositoryUtils
{
    public class EntityGenerator
    {
        /// <summary>
        /// 命令行选项
        /// </summary>
        private readonly GenerateOption _option;

        private readonly ITableSelector _tableSelector;

        public EntityGenerator(GenerateOption option)
        {
            this._option = option;
            Logger.Level = option.LoggerLevel;

            // 硬编码；也可以将数据库实现分离到单独的程序集，然后反射装载
            switch (_option.DbType.ToLower())
            {
                case "mysql":
                    _tableSelector = new MySQL.MySqlTableSelector(_option.ConnectionString, _option.DbName);
                    break;
                case "mssql":
                    _tableSelector = new MsSQL.MsSqlTableSelector(_option.ConnectionString, _option.DbName);
                    break;
                default:
                    throw new Exception("暂不支持数据库：" + _option.DbType);
            }

            if (!Directory.Exists(option.OutDirectory))
            {
                try
                {
                    Directory.CreateDirectory(option.OutDirectory);
                }
                catch (Exception ex)
                {
                    throw new Exception("无法创建输出目录", ex);
                }
            }
        }

        /// <summary>
        /// 将Table转换为Class，并保存到文件
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="table"></param>
        private void Table2Class(TextWriter writer, Table table)
        {
            Logger.Log($"生成实体类：{table.Name}", ELoggerLevel.Debug);
            var tabsCount = 0;
            string intent;
            writer.WriteLine("using System;");
            writer.WriteLine("using System.Text;" + Environment.NewLine);
            writer.WriteLine($"namespace {this._option.NameSpace}");// 命名空间开始
            intent = WriteBracket(writer, EBracket.Left, ref tabsCount);

            var classLine = $"{intent}public class {table.Name}";
            if (!string.IsNullOrEmpty(this._option.Parent))
            {
                classLine += ": " + this._option.Parent;
            }
            writer.WriteLine(classLine);// 类开始
            intent = WriteBracket(writer, EBracket.Left, ref tabsCount);

            foreach (var col in table.Columns)
            {
                string type;
                switch (col.DataType.ToLower())
                {
                    case "char":
                    case "varchar":
                    case "nvarchar":
                    case "longtext":
                    case "text":
                    case "sysname":
                    case "nchar":
                    case "ntext":
                        type = "string";
                        break;
                    case "date":
                    case "datetime":
                        type = col.CanBeNull ? "DateTime?" : "DateTime";
                        break;
                    case "bit":
                        type = col.CanBeNull ? "bool?" : "bool";
                        break;
                    case "int":
                    case "tinyint":
                    case "smallint":
                        type = col.CanBeNull ? "int?" : "int";
                        break;
                    case "bigint":
                        type = col.CanBeNull ? "int64?" : "int64";
                        break;
                    case "double":
                        type = col.CanBeNull ? "double?" : "double";
                        break;
                    case "float":
                    case "real":
                        type = col.CanBeNull ? "float?" : "float";
                        break;
                    case "deciaml":
                    case "numberic":
                        type = col.CanBeNull ? "deciaml?" : "deciaml";
                        break;
                    case "varbinary":
                    case "binary":
                        type = "byte[]";
                        break;
                    default:
                        Logger.Log($"{col.DataType} 无法转换为C#对应的类型，将直接作为C#类型。", ELoggerLevel.Warn);
                        type = col.DataType;
                        break;
                }
                writer.WriteLine($"{intent}public {type} {col.Name} {{ get; set;}}");
            }
            WriteBracket(writer, EBracket.Right, ref tabsCount);// 类结束
            WriteBracket(writer, EBracket.Right, ref tabsCount); // 命名空间结束
        }

        /// <summary>
        /// 输出花括号和下一行的缩进
        /// </summary>
        private string WriteBracket(TextWriter writer, EBracket bracket, ref int tabsCount)
        {
            const string tab = "    "; // 使用4个空格表示Tab缩进
            var intent = "";
            if (bracket == EBracket.Left)
            {
                for (int i = 0; i < tabsCount; i++)
                {
                    intent += tab;
                }
                writer.WriteLine(intent + "{");
                tabsCount++;
                intent += tab;
            }
            else
            {
                tabsCount--;
                for (int i = 0; i < tabsCount; i++)
                {
                    intent += tab;
                }
                writer.WriteLine(intent + "}");
            }
            return intent;
        }

        /// <summary>
        /// 获取文件写入流
        /// </summary>
        private TextWriter GetClassWriter(string className)
        {
            Logger.Log($"创建文件：{className}.cs", ELoggerLevel.Debug);
            var classFile = Path.Combine(this._option.OutDirectory, className + ".cs");
            if (File.Exists(classFile))
            {
                try
                {
                    File.Delete(classFile);
                    Logger.Log($"删除已经存在的文件：{classFile}", ELoggerLevel.Debug);
                }
                catch (Exception ex)
                {
                    throw new Exception("删除文件失败", ex);
                }
            }
            return new StreamWriter(File.OpenWrite(classFile), Encoding.UTF8);
        }

        public void Generate()
        {
            foreach (var item in _tableSelector.GetTables())
            {
                using (var writer = GetClassWriter(item.Key))
                {
                    Table2Class(writer, item.Value);
                }
            }
            Logger.Log("生成成功", ELoggerLevel.Debug);
        }


        #region 辅助类

        /// <summary>
        /// 花括号
        /// </summary>
        private enum EBracket
        {
            Left,
            Right
        }

        #endregion
    }
}
