using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Docs.Model.DB.Log
{
    public class ScrapperLog
    {
        public int ScrapperLogId { get; set; }
        [Index]
        public DateTime DateOperation { get; set; }
        [Index]
        public int SudId { get; set; }
    }
}
