using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Docs.Model.DB.Client
{
    public class Client
    {
        public int ClientId { get; set; }
        [Index, Required]
        public string EMail { get; set; }
        [Required]
        public string Pswd { get; set; }
        public string MobilePhone { get; set; }
        [Required]
        public bool IsActive { get; set; }
        [Required]
        public ClientInfo Info { get; set; }
        [Required]
        public ClientSubscription Subscription { get; set; }
    }
}
