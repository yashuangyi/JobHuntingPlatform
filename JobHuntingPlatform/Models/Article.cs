using SqlSugar;

namespace JobHuntingPlatform.Models
{
    /// <summary>
    /// 文章实体类.
    /// </summary>
    [SugarTable("article")]
    public partial class Article
    {
        // 指定主键和自增列

        /// <summary>
        /// Gets or sets 编号.
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets 发布者编号.
        /// </summary>
        public int AdminId { get; set; }

        /// <summary>
        /// Gets or sets 标题.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets 发布时间.
        /// </summary>
        public string Time { get; set; }

        /// <summary>
        /// Gets or sets 文章路径.
        /// </summary>
        public string ArticlePath { get; set; }

        /// <summary>
        /// Gets or sets 是否置顶.
        /// </summary>
        public int IsTop { get; set; }
    }
}