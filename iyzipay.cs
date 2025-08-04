
using Iyzipay;
using Iyzipay.Model;
using Iyzipay.Request;
using System;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.Net;
using System.Reflection;
using System.Reflection.Emit;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

public class HelloWorld
{
    public static async Task Main(string[] args)
    {
        CreatePaymentRequest request = new CreatePaymentRequest()
        {
            Locale = Locale.TR.ToString(),
            ConversationId = "123456789",
            Price = "1",
            PaidPrice = "1.20",
            Installment = 1,
            BasketId = "BID1",
            PaymentCard = new PaymentCard()
            {
                CardHolderName = "John Doe",
                CardNumber = "5528790000000008",
                ExpireMonth = "12",
                ExpireYear = "2030",
                Cvc = "123",
                CardAlias = "My Card",
                CardToken = "card_123456789",
                CardUserKey = "card_user_123456789",
                ConsumerToken = "consumer_123456789",
                RegisterConsumerCard = "true",
                UcsToken = "ucs_123456789",
                RegisterCard = 0,
            },
            Buyer = new Buyer()
            {
                Id = "BY789",
                Name = "John",
                Surname = "Doe",
                GsmNumber = "+905350000000",
                Email = "email@email.com",
                IdentityNumber = "74300864791",
                LastLoginDate = "2015-10-05 12:43:35",
                RegistrationDate = "2013-04-21 15:12:09",
                RegistrationAddress = "Nidakule Göztepe, Merdivenköy Mah. Bora Sok. No:1",
                Ip = "85.34.78.112",
                City = "Istanbul",
                Country = "Turkey",
                ZipCode = "34732",
            },
            ShippingAddress = new Address()
            {
                ContactName = "Jane Doe",
                City = "Istanbul",
                Country = "Turkey",
                Description = "Nidakule Göztepe, Merdivenköy Mah. Bora Sok. No:1",
                ZipCode = "34742",
            },
            BillingAddress = new Address()
            {
                ContactName = "Jane Doe",
                City = "Istanbul",
                Country = "Turkey",
                Description = "Nidakule Göztepe, Merdivenköy Mah. Bora Sok. No:1",
                ZipCode = "34742",
            },
            BasketItems = new List<BasketItem>() {
                new BasketItem()
                {
                    Id = "BI101",
                    Name = "Binocular",
                    ChargedFromMerchant = true,
                    SubMerchantKey = "submerchant_123456789",
                    SubMerchantPrice = "0.05",
                    WithholdingTax = "0.05",
                    Category1 = "Collectibles",
                    Category2 = "Accessories",
                    ItemType = BasketItemType.PHYSICAL.ToString(),
                    Price = "0.03",
                }
            },


            GsmNumber = "+905300000000",
            ConnectorName = "IyziConnect",
            PaymentSource = "MOBILE",
            PosOrderId = "POS123456789",
            Reward = new LoyaltyReward()
            {
                RewardAmount = "0.10",
                RewardUsage = 1,
            },
            Currency = Currency.TRY.ToString(),
            PaymentChannel = PaymentChannel.WEB.ToString(),
            PaymentGroup = PaymentGroup.PRODUCT.ToString(),
           

           
        };



        Options options = new Options()
        {
            ApiKey = "sandbox-VqtcTMGd8oVpjzTxIpOuPuszUg9bq3Vo",
            SecretKey = "sandbox-vHyZeutjE3uFJOg3spLk6fnJP7X8bBU",
            BaseUrl = "https://sandbox-api.iyzipay.com",
        };

        try
        {
            ThreedsInitialize threedsInitialize = await ThreedsInitialize.Create(request, options);
            string view = threedsInitialize.HtmlContent;

            string tokToken = threedsInitialize.ConversationId; // Form'a ait token'ı kayıt ederek sonradan ödeme kontrolü yaparken kullancağız.
        }
        catch (Exception ex)
        {

            throw;
        }
    }
}
