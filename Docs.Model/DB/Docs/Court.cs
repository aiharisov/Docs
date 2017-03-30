using System;
using System.ComponentModel.DataAnnotations;

namespace Docs.Model.DB.Docs
{
    /// <summary>
    /// Суд
    /// </summary>
    public class Court
    {
        public int CourtId { get; set; }
        [Required, Index]
        public string Name { get; set; }
        [Required]
        public string BaseURL { get; set; }
        [Required]
        public string URLToDoc { get; set; }
        [Required]
        public string TypeSite { get; set; }
        [Required, Index]
        public int TypeSiteId { get; set; }
        [Required]
        public string Encoding { get; set; }
    }
}
