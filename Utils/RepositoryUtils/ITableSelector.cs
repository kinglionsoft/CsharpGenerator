using System.Collections.Generic;
using RepositoryUtils.Common;

namespace RepositoryUtils
{
    internal interface ITableSelector
    {
        /// <summary>
        /// 获取所有数据表
        /// </summary>
        /// <returns>key: 表名称，value: 表数据</returns>
        IDictionary<string, Table> GetTables();
    }
}
