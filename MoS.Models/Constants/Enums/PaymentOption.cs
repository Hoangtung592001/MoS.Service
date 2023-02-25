using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoS.Models.Constants.Enums
{
    public static class PaymentOption
    {
        public static Guid PayByCash = new Guid("E3037927-7A79-43A9-91B2-F788406436A6");

        public enum PaymentOptionTypeDescriptionIDs
        {
            Cash = 1,
            Visa = 2
        }
    }
}
