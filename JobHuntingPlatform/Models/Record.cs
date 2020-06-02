using SqlSugar;

namespace JobHuntingPlatform.Models
{
    /// <summary>
    /// 简历投递记录实体类.
    /// </summary>
    [SugarTable("record")]
    public partial class Record
    {
        // 指定主键和自增列

        /// <summary>
        /// Gets or sets 编号.
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets 求职者编号.
        /// </summary>
        public int SeekerId { get; set; }

        /// <summary>
        /// Gets or sets 招聘信息编号.
        /// </summary>
        public int RecruitmentId { get; set; }

        /// <summary>
        /// Gets or sets 投递时间.
        /// </summary>
        public string Time { get; set; }
    }
}