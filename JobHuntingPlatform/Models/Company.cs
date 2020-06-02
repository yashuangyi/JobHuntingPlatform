using SqlSugar;

namespace JobHuntingPlatform.Models
{
    /// <summary>
    /// 企业实体类.
    /// </summary>
    [SugarTable("company")]
    public partial class Company
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
        /// Gets or sets 性质.
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets 地址.
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Gets or sets 联系方式.
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// Gets or sets 审核是否通过.
        /// </summary>
        public int IsPass { get; set; }
    }
}