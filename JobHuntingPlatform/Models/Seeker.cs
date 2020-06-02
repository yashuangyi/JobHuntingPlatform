using SqlSugar;

namespace JobHuntingPlatform.Models
{
    /// <summary>
    /// 求职者实体类.
    /// </summary>
    [SugarTable("seeker")]
    public partial class Seeker
    {
        // 指定主键和自增列

        /// <summary>
        /// Gets or sets 编号.
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets 账号.
        /// </summary>
        public string Account { get; set; }

        /// <summary>
        /// Gets or sets 密码.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets 姓名.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets 性别.
        /// </summary>
        public string Sex { get; set; }

        /// <summary>
        /// Gets or sets 年龄.
        /// </summary>
        public int Age { get; set; }

        /// <summary>
        /// Gets or sets 联系方式.
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// Gets or sets 意向地址.
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Gets or sets 意向职位.
        /// </summary>
        public string Offer { get; set; }

        /// <summary>
        /// Gets or sets 简历路径.
        /// </summary>
        public string ResumePath { get; set; }

        /// <summary>
        /// Gets or sets 是否发布求职.
        /// </summary>
        public int IsRelease { get; set; }
    }
}