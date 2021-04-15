using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesWebMvc.Models.Enums {
    public enum DeliveryStatus : int {
        ReadyToDeliver = 0,
        DeliveryInProgress = 1,
        Delivered = 2,
        Received = 3,
        Returned = 4,
        NotDelivered = 5
    }
}
