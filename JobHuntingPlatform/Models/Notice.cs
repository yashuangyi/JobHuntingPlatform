using SqlSugar;

namespace JobHuntingPlatform.Models
{
    /// <summary>
    /// 通知单实体类.
    /// </summary>
    [SugarTable("notice")]
    public partial class Notice
    {
        // 指定主键和自增列

        /// <summary>
        /// Gets or sets 编号.
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets 通知者编号.
        /// </summary>
        public int SourceId { get; set; }

        /// <summary>
        /// Gets or sets 被通知者编号.
        /// </summary>
        public int TargetId { get; set; }

        /// <summary>
        /// Gets or sets 通知时间.
        /// </summary>
        public string Time { get; set; }

        /// <summary>
        /// Gets or sets 通知内容.
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// Gets or sets 通知类型（投递简历，面试邀请，企业回复，求职者回复）.
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets 是否已回复.
        /// </summary>
        public int IsReply { get; set; }
    }
}