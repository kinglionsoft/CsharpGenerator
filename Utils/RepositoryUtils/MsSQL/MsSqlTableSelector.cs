using System.Data.Common;

namespace RepositoryUtils.MsSQL
{
    internal class MsSqlTableSelector : TableSelectorBase
    {
        public MsSqlTableSelector(string connectionString, string dbName) : base(connectionString, dbName)
        {
        }

        protected override DbConnection CreateConnection() =>
            new System.Data.SqlClient.SqlConnection(this.ConnectionString);

        /// <summary>
        /// 生成查询数据表及其字段的select语句
        /// 输出：TableName, ColumnName, DataType, CanBeNull(1 or 0), Position, CharLength
        /// </summary>
        /// <returns></returns>
        protected override string GeneratorSelectText()
        {
            return
@"select 
	a.name			TableName,
	b.name			ColumnName,
	t.name			DataType,
	b.column_id		Position,
	b.is_nullable	CanBeNull,
	b.max_length	CharLength
from sys.tables a inner join sys.columns b on a.object_id=b.object_id
inner join sys.types t on b.system_type_id=t.system_type_id
where a.type='U'";
        }
    }
}
