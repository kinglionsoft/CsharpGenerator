using System.Data.Common;

namespace RepositoryUtils.MySQL
{
    internal class MySqlTableSelector : TableSelectorBase
    {
        public MySqlTableSelector(string connectionString, string dbName) : base(connectionString, dbName)
        {
        }

        protected override DbConnection CreateConnection() =>
            new Pomelo.Data.MySql.MySqlConnection(this.ConnectionString);

        /// <summary>
        /// 生成查询数据表及其字段的select语句
        /// 输出：TableName, ColumnName, DataType, CanBeNull(1 or 0), Position, CharLength
        /// 参数：@dbName-数据库名称
        /// </summary>
        /// <returns></returns>
        protected override string GeneratorSelectText()
        {
            return
$@"SELECT 
	TABLE_NAME 		TableName,
	COLUMN_NAME 	ColumnName,
	DATA_TYPE			DataType,
	CASE WHEN CHARACTER_MAXIMUM_LENGTH is null then 0 else ORDINAL_POSITION end CharLength,
	CASE IS_NULLABLE WHEN 'YES' then 1 else 0 end CanBeNull,
	CASE WHEN ORDINAL_POSITION is null then 0 else ORDINAL_POSITION end Position
FROM  INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_SCHEMA='{this.DbName}'";
        }
    }
}
