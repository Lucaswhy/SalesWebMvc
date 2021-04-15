using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesWebMvc.Models.Enums {
    public enum PaymentStatus : int {
        Authorized = 0,
        Paid = 1,
        Pending = 2,
        Refunded = 3,
        Unpaid = 4,
        Voided = 5
    }
}
