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
	a.TABLE_NAME 		TableName,
	a.COLUMN_NAME 	ColumnName,
	a.DATA_TYPE			DataType,
	CASE WHEN a.CHARACTER_MAXIMUM_LENGTH is null then 0 else ORDINAL_POSITION end CharLength,
	CASE a.IS_NULLABLE WHEN 'YES' then 1 else 0 end CanBeNull,
	CASE WHEN a.ORDINAL_POSITION is null then 0 else ORDINAL_POSITION end Position
FROM  INFORMATION_SCHEMA.COLUMNS a inner JOIN INFORMATION_SCHEMA.TABLES b on a.TABLE_NAME=b.TABLE_NAME and a.TABLE_SCHEMA=b.TABLE_SCHEMA
WHERE  b.TABLE_TYPE='BASE TABLE'
AND a.TABLE_SCHEMA='{this.DbName}'";
        }
    }
}
