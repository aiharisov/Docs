using System;
using System.ComponentModel.DataAnnotations;

namespace Docs.Model.DB.Client
{
    public class ClientInfo
    {
        
        public int ClientId { get; set; }
        public string FIO { get; set; }
        public DateTime DateBirth { get; set; }

    }
}
