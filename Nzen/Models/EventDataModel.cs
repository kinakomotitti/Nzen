using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nzen.Models
{
    public class EventDataModel
    {
        public int serial_no { get; set; }

        public string group_id { get; set; }

        public string group_entry_date { get; set; }

        public string info_type { get; set; }

        public string data { get; set; }
    }
}
