using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IG.ENMS.Models.Starlink.NMS
{
    public class UserCustomerTerminalMap
    {
        public int userId { get; set; }
        public string userName { get; set; }
        public Boolean Internal { get; set; }
        public int customerId { get; set; }

        public string accountName { get; set; }
        public string accountNumber { get; set; }
        public string serviceLineNumber { get; set; }

        public int nodeId { get; set; }

        public string terminalID { get; set; }
    }
}
