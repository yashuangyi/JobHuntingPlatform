using SqlSugar;

namespace JobHuntingPlatform.Models
{
    /// <summary>
    /// 招聘信息实体类.
    /// </summary>
    [SugarTable("recruitment")]
    public partial class Recruitment
    {
        // 指定主键和自增列

        /// <summary>
        /// Gets or sets 编号.
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets 企业编号.
        /// </summary>
        public int CompanyId { get; set; }

        /// <summary>
        /// Gets or sets 招聘职位.
        /// </summary>
        public string Offer { get; set; }

        /// <summary>
        /// Gets or sets 招聘人数.
        /// </summary>
        public int Number { get; set; }

        /// <summary>
        /// Gets or sets 发布时间.
        /// </summary>
        public string Time { get; set; }

        /// <summary>
        /// Gets or sets 要求.
        /// </summary>
        public string Require { get; set; }
    }
}