namespace RepositoryUtils.Common
{
    /// <summary>
    /// 表字段
    /// </summary>
    internal class Column
    {
        /// <summary>
        /// 字段名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 字段类型
        /// </summary>
        public string DataType { get; set; }

        /// <summary>
        /// 是否允许为空
        /// </summary>
        public bool CanBeNull { get; set; }

        /// <summary>
        /// 字段顺序
        /// </summary>
        public int Position { get; set; }

        /// <summary>
        /// 如果是字段类型，显示字段长度
        /// </summary>
        public int CharLength { get; set; }

        /// <summary>
        /// 字段备注
        /// </summary>
        public string Comment { get; set; }
    }
}
