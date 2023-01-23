using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoS.Models.Constants.Enums
{
    public class OrderStatus
    {
        public enum OrderStatusIDs
        {
            PREPARING = 1,
            PREPARED = 2,
            DELIVERING = 3,
            DELIVERED = 4
        }
    }
}
