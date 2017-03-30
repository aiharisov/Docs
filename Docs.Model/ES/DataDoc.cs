using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Docs.Model.ES
{
    public class DataDoc
    {
        public int Id { get; set; }
        public DateTime DateDoc { get; set; }
        public int CourtId { get; set; }
        public int InstanceId { get; set; }
        public int CaseId { get; set; }
        public int TypeDocId { get; set; }
        public string HTML { get; set; }
        public string SearchText { get; set; }
        public DateTime DateOp { get; set; }
    }
}
