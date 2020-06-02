namespace JobHuntingPlatform.Models
{
    public class ResumeDTO : Record
    {
        /// <summary>
        /// Gets or sets 姓名.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets 简历路径.
        /// </summary>
        public string ResumePath { get; set; }
    }
}