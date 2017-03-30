using System;
using System.ComponentModel.DataAnnotations;

namespace Docs.Model.DB.Docs
{
    /// <summary>
    /// Инстанция
    /// </summary>
    public class Instance
    {
        public int InstanceId { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
