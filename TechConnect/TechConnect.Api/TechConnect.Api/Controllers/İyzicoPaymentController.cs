using Iyzipay;
using Iyzipay.Model;
using Iyzipay.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using TechConnect.Api.Hubs;
using TechConnect.Api.Models;

namespace TechConnect.Api.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class İyzicoPaymentController : ControllerBase
    {
        private readonly IHubContext<PayHub> _hubContext;

        public İyzicoPaymentController(IHubContext<PayHub> hubContext)
        {
            _hubContext = hubContext;
        }




        [HttpPost]
        public async Task<IActionResult> Pay(CreditAndUser creditAndUser)
        {

            Options options = new()
            {
                ApiKey = "sandbox-ZfJeWrGtL7skwVIXOAbkqHUDe5rzHOYO",
                SecretKey = "sandbox-PXge6IxosiotdLeUDaXvWIZkP7DoIzuL",
                BaseUrl = "https://sandbox-api.iyzipay.com"
            };

            CreatePaymentRequest request = new CreatePaymentRequest();
            request.Locale = Locale.TR.ToString();
            request.ConversationId = Guid.NewGuid().ToString();
            //request.Price = creditAndUser.Price;
            //request.PaidPrice = creditAndUser.Price;
            request.Price = "1";
            request.PaidPrice = "1.2";
            request.Currency = Currency.TRY.ToString();
            request.Installment = 1;
            request.BasketId = "a";
            request.PaymentChannel = PaymentChannel.WEB.ToString();
            request.PaymentGroup = PaymentGroup.PRODUCT.ToString();
            request.CallbackUrl = "https://localhost:7237/api/İyzicoPayment/PayCallBack";

            PaymentCard paymentCard = new PaymentCard();
            paymentCard.CardHolderName = creditAndUser.NameSurname;
            paymentCard.CardNumber = creditAndUser.CartNumber;
            paymentCard.ExpireMonth = creditAndUser.ExpirationDateMonth;
            paymentCard.ExpireYear = creditAndUser.ExpirationDateYear;
            paymentCard.Cvc = creditAndUser.CVV;
            paymentCard.RegisterCard = 0;
            request.PaymentCard = paymentCard;

            Buyer buyer = new Buyer();
            buyer.Id = "a";
            buyer.Name = creditAndUser.Name;
            buyer.Surname = creditAndUser.Surname;
            buyer.GsmNumber = creditAndUser.PhoneNumber;
            buyer.Email =creditAndUser.Email;
            buyer.IdentityNumber = "74300864791";
            buyer.LastLoginDate = "2015-10-05 12:43:35";
            buyer.RegistrationDate = "2013-04-21 15:12:09";
            buyer.RegistrationAddress = "a";
            buyer.Ip = "a";
            buyer.City = "a";
            buyer.Country = "a";
            buyer.ZipCode = "a";
            request.Buyer = buyer;

            Address shippingAddress = new Address();
            shippingAddress.ContactName = "a";
            shippingAddress.City = "a";
            shippingAddress.Country = "a";
            shippingAddress.Description = "a";
            shippingAddress.ZipCode = "a";
            request.ShippingAddress = shippingAddress;

            Address billingAddress = new Address();
            billingAddress.ContactName = "a";
            billingAddress.City = "a";
            billingAddress.Country = "a";
            billingAddress.Description = "a";
            billingAddress.ZipCode = "a";
            request.BillingAddress = billingAddress;

            List<BasketItem> basketItems = new List<BasketItem>();
            BasketItem firstBasketItem = new BasketItem();
            firstBasketItem.Id = "a";
            firstBasketItem.Name = "a";
            firstBasketItem.Category1 = "a";
            firstBasketItem.Category2 = "a";
            firstBasketItem.ItemType = BasketItemType.PHYSICAL.ToString();
            //firstBasketItem.Price = creditAndUser.Price;
            firstBasketItem.Price = "1";
            
            basketItems.Add(firstBasketItem);
            request.BasketItems = basketItems;

            ThreedsInitialize threedsInitialize = ThreedsInitialize.Create(request, options);
            return Ok(new { Content = threedsInitialize.HtmlContent });
        }





        [HttpPost]
        public async Task<IActionResult> PayCallBack([FromForm] IFormCollection formcollections)
        {
            CallbackData data = new(
                Status: formcollections["status"],
                PaymentId: formcollections["paymentId"],
                ConversationData: formcollections["conversationData"],
                ConversationId: formcollections["conversationId"],
                MDStatus: formcollections["mdStatus"]);

            if (data.Status == "success")
            {
                await _hubContext.Clients.All.SendAsync("Receive", "Ödeme başarıyla tamamlandı!          Siparişlerim kısmından kontrol edebilirsiniz :) ");
                return Ok();
            }

            if (data.Status == "failure")
            {
                await _hubContext.Clients.All.SendAsync("Receive", "Ödeme sırasında bir hata oluştu. Lütfen bilgilerinizi kontrol ediniz !");
                return Ok();
            }

            return Ok();
        }


        public sealed record CallbackData(
    string Status,
    string PaymentId,
    string ConversationData,
    string ConversationId,
    string MDStatus);
    }
}
