using System.Collections.Generic;
using CommonUtils.Extensions;

namespace RepositoryUtils.Common
{
    /// <summary>
    /// 数据表
    /// </summary>
    internal class Table
    {
        /// <summary>
        /// 表名称
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// 架构，各数据库定义不同。如：MySQL一般指数据库名称，MsSQL是指一个独立于数据库用户的非重复命名空间
        /// </summary>
        public string Schema { get; }

        /// <summary>
        /// 表字段集合
        /// </summary>
        public IList<Column> Columns { get; set; }
        
        public Table(string name, string schema)
        {
            this.Name = TableNameNormalize(name);
            Schema = schema;
            this.Columns = new List<Column>();
        }

        public void AddColumn(Column column) => this.Columns.Add(column);

        /// <summary>
        /// 将DB中的表名映射为C#类名
        /// </summary>
        /// <param name="dbTableName"></param>
        /// <returns></returns>
        protected virtual string TableNameNormalize(string dbTableName)
        {
            return dbTableName.ToCamelCase();
        }
    }
}
