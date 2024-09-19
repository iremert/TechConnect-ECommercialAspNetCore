using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechConnect.EL.Concrete;

namespace TechConnect.Dto.BasketTotalDtos
{
    public class CreateBasketTotalDto
    {
        public string UserId { get; set; }
        public string DiscountId { get; set; }
        public Discount discount { get; set; }
        public List<BasketItem> BasketItems { get; set; }
        public decimal TotalPrice { get; set; }
        public bool IsFinished { get; set; }
    }
}
