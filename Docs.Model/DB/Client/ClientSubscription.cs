using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Docs.Model.DB.Client
{
    public class ClientSubscription
    {
        public int id { get; set; }
        [Required]
        public bool IsActive { get; set; }
        public List<SubscriptionSud> SudList { get; set; }
        public List<SubscriptionInstance> InstanceList { get; set; }
    }
}
