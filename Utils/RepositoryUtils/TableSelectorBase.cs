using System.Collections.Generic;
using System.Data.Common;
using System.Reflection.Metadata.Ecma335;
using RepositoryUtils.Common;

namespace RepositoryUtils
{
    internal abstract class TableSelectorBase : ITableSelector
    {
        protected readonly GenerateOption Option;

        protected TableSelectorBase(GenerateOption option)
        {
            Option = option;
        }

        /// <summary>
        /// 生成查询数据表及其字段的select语句
        /// 输出：TableName, ColumnName, DataType, CanBeNull(1 or 0), Position, CharLength
        /// </summary>
        /// <returns></returns>
        protected abstract string GeneratorSelectText();

        /// <summary>
        /// 创建数据库连接
        /// </summary>
        /// <returns></returns>
        protected abstract DbConnection CreateConnection();

        #region ITableSelector

        public IDictionary<string, Table> GetTables()
        {
            var result = new Dictionary<string, Table>();
            using (var connection = CreateConnection())
            using (var command = connection.CreateCommand())
            {
                command.CommandText = GeneratorSelectText();
                connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var tableName = reader["TableName"].ToString();
                    var columnName = reader["ColumnName"].ToString();
                    if (this.Option.Ignores == null || this.Option.Ignores.Contains(columnName))
                    {
                        continue;
                    }
                    var column = new Column
                    {
                        Name = columnName,
                        DataType = reader["DataType"].ToString(),
                        Position = int.Parse(reader["Position"].ToString()),
                        CanBeNull = reader["CanBeNull"].ToString()=="1",
                        CharLength =int.Parse(reader["CharLength"].ToString()),
                        Comment = reader["Comment"].ToString()
                    };
                    if (result.ContainsKey(tableName))
                    {
                        result[tableName].AddColumn(column);
                    }
                    else
                    {
                        var table = new Table(tableName);
                        table.AddColumn(column);
                        result[tableName] = table;
                    }
                }
            }
            return result;
        }

        #endregion
    }
}
