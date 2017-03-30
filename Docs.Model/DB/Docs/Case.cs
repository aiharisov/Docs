using System;
using System.ComponentModel.DataAnnotations;

namespace Docs.Model.DB.Docs
{
    /// <summary>
    /// Справочник типов дела
    /// </summary>
    public class Case
    {
        public int Caseid { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
