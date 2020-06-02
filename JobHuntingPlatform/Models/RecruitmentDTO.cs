
namespace JobHuntingPlatform.Models
{
    public class RecruitmentDTO : Recruitment
    {
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
    }
}