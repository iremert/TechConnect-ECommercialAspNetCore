﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechConnect.EL.Concrete;

namespace TechConnect.Dto.OrderingDtos
{
    public class ResultOrderingDto
    {
        public string ID { get; set; }
        public string UserId { get; set; }
        public string AddressId { get; set; }
        public Address Address { get; set; }
        public string BasketTotalId { get; set; }
        public BasketTotal BasketTotal { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime OrderDeliveryDate { get; set; }
        public OrderState OrderState { get; set; }
    }
}
