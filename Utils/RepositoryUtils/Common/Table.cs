using System.Collections.Generic;

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
        public string Name { get; set; }

        /// <summary>
        /// 架构，各数据库定义不同。如：MySQL一般指数据库名称，MsSQL是指一个独立于数据库用户的非重复命名空间
        /// </summary>
        public string Schema { get; set; }

        /// <summary>
        /// 表字段集合
        /// </summary>
        public IList<Column> Columns { get; set; }

        public Table()
        {
            this.Columns = new List<Column>();
        }

        public Table(string name):this()
        {
            this.Name = name;
        }

        public void AddColumn(Column column) => this.Columns.Add(column);
    }
}
