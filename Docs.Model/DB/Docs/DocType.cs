using System;
using System.ComponentModel.DataAnnotations;

namespace Docs.Model.DB.Docs
{
    /// <summary>
    /// Вид документа
    /// </summary>
    public class DocType
    {
        public int DocTypeId { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
