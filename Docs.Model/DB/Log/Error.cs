using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Docs.Model.DB.Log
{
    public class Error
    {
        public int ErrorId { get; set; }
        [Index]
        public DateTime DateOperation { get; set; }
        public string Module { get; set; }
        public string Comment { get; set; }
        public string Message { get; set; }
        public string Source { get; set; }
        public string Target { get; set; }
        public string Trace { get; set; }
    }
}
