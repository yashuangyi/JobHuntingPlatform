using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JobHuntingPlatform.Models
{
    public class ArticleDTO : Article
    {
        /// <summary>
        /// Gets or sets 编者.
        /// </summary>
        public string Name { get; set; }
    }
}