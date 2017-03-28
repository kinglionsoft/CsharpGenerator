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
            return "";
        }
    }
}
