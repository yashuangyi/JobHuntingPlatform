using SqlSugar;

namespace JobHuntingPlatform.Models
{
    /// <summary>
    /// 管理员实体类.
    /// </summary>
    [SugarTable("admin")]
    public partial class Admin
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
    }
}