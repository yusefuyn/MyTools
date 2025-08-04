
using System;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.Net;
using System.Reflection;
using System.Reflection.Emit;
using System.Security.Cryptography;
using System.Text;
using System.Xml.Linq;

public class PaymentRequest
{
    public string Locale { get; set; }
    public string ConversationId { get; set; }
    public string Price { get; set; }
    public string PaidPrice { get; set; }
    public string Currency { get; set; }
    public int Installment { get; set; }
    public string BasketId { get; set; }
    public string PaymentChannel { get; set; }
    public string PaymentGroup { get; set; }
    public PaymentCard PaymentCard { get; set; }
    public Buyer Buyer { get; set; }
    public Address ShippingAddress { get; set; }
    public Address BillingAddress { get; set; }
    public List<BasketItem> BasketItems { get; set; }
}
public class PaymentCard
{
    public string CardHolderName { get; set; }
    public string CardNumber { get; set; }
    public string ExpireMonth { get; set; }
    public string ExpireYear { get; set; }
    public string Cvc { get; set; }
    public int RegisterCard { get; set; }
}
public class Buyer
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string GsmNumber { get; set; }
    public string Email { get; set; }
    public string IdentityNumber { get; set; }
    public string LastLoginDate { get; set; }
    public string RegistrationDate { get; set; }
    public string RegistrationAddress { get; set; }
    public string Ip { get; set; }
    public string City { get; set; }
    public string Country { get; set; }
    public string ZipCode { get; set; }
}
public class Address
{
    public string ContactName { get; set; }
    public string City { get; set; }
    public string Country { get; set; }
    public string AddressLine { get; set; } // Not: Iyzipay SDK'da bazen Address_ kullanılır
    public string ZipCode { get; set; }
}
public class CreatePaymentRequest
{
    public string Locale { get; set; }
    public string ConversationId { get; set; }
    public decimal Price { get; set; }
    public decimal PaidPrice { get; set; }
    public string Currency { get; set; }
    public int Installment { get; set; }
    public string BasketId { get; set; }
    public string PaymentChannel { get; set; }
    public string PaymentGroup { get; set; }
    public PaymentCard PaymentCard { get; set; }
    public Buyer Buyer { get; set; }
    public Address ShippingAddress { get; set; }
    public Address BillingAddress { get; set; }
    public List<BasketItem> BasketItems { get; set; }
}
public class BasketItem
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Category1 { get; set; }
    public string Category2 { get; set; }
    public string ItemType { get; set; }
    public decimal Price { get; set; }
}


public class HelloWorld
{

    public static CreatePaymentRequest request = new CreatePaymentRequest
    {
        Locale = "TR",
        ConversationId = "123456789",
        Price =6M,
        PaidPrice = 6.0M,
        Currency = "TRY",
        Installment = 1,
        BasketId = "B67832",
        PaymentChannel = "WEB",
        PaymentGroup = "PRODUCT",
        PaymentCard = new PaymentCard
        {
            CardHolderName = "John Doe",
            CardNumber = "5528790000000008",
            ExpireMonth = "12",
            ExpireYear = "2030",
            Cvc = "123",
            RegisterCard = 0
        },
        Buyer = new Buyer
        {
            Id = "BY789",
            Name = "John",
            Surname = "Doe",
            GsmNumber = "+905350000000",
            Email = "sandboxtest@email.com",
            IdentityNumber = "74300864791",
            LastLoginDate = "2015-10-05 12:43:35",
            RegistrationDate = "2013-04-21 15:12:09",
            RegistrationAddress = "Altunizade Mah. İnci Çıkmazı Sokak No: 3 İç Kapı No: 10 Üsküdar İstanbul",
            Ip = "85.34.78.112",
            City = "Istanbul",
            Country = "Turkey",
            ZipCode = "34732"
        },
        ShippingAddress = new Address
        {
            ContactName = "Jane Doe",
            City = "Istanbul",
            Country = "Turkey",
            AddressLine = "Altunizade Mah. İnci Çıkmazı Sokak No: 3 İç Kapı No: 10 Üsküdar İstanbul",
            ZipCode = "34742"
        },
        BillingAddress = new Address
        {
            ContactName = "Jane Doe",
            City = "Istanbul",
            Country = "Turkey",
            AddressLine = "Altunizade Mah. İnci Çıkmazı Sokak No: 3 İç Kapı No: 10 Üsküdar İstanbul",
            ZipCode = "34742"
        },
        BasketItems = new List<BasketItem>
    {
        new BasketItem
        {
            Id = "BI101",
            Name = "Binocular",
            Category1 = "Collectibles",
            Category2 = "Accessories",
            ItemType = "PHYSICAL",
            Price = 1.0M
        },
        new BasketItem
        {
            Id = "BI102",
            Name = "Game code",
            Category1 = "Game",
            Category2 = "Online Game Items",
            ItemType = "VIRTUAL",
            Price = 2.0M
        },
        new BasketItem
        {
            Id = "BI103",
            Name = "Usb",
            Category1 = "Electronics",
            Category2 = "Usb / Cable",
            ItemType = "PHYSICAL",
            Price = 3.0M
        }
    }
    };
    public static void Main(string[] args)
    {
        Console.WriteLine(GenerateAuthorizationHeaderV2("https://sandbox-api.iyzipay.com", Newtonsoft.Json.JsonConvert.SerializeObject(request)));
    }



    private static string GenerateAuthorizationHeaderV2(string uri, string body)
    {
        const string iyziWsHeaderName = "IYZWSv2";
        const string separator = ":";

        string apiKey = "sandbox-VqtcTMGd8oVpjzTxIpOuPuszUg9bq3Vo";
        string secretKey = "sandbox-vHyZeutjE3uFJOg3spLk6fnJP7X8bBUl";

        string randomString = GenerateRandomHexString(16);
        string hash = GenerateHashV2(apiKey, separator, uri, randomString, secretKey, body);

        return $"{iyziWsHeaderName} {hash}";
    }

    private static string GenerateHashV2(string apiKey, string separator, string uri, string randomString, string secretKey, string body)
    {
        string dataToSign = randomString + uri + body;

        using (var hmac = new System.Security.Cryptography.HMACSHA256(Encoding.UTF8.GetBytes(secretKey)))
        {
            byte[] hashBytes = hmac.ComputeHash(Encoding.UTF8.GetBytes(dataToSign));
            string signature = BitConverter.ToString(hashBytes).Replace("-", "").ToLowerInvariant();

            var authorizationParams = new[]
            {
            "apiKey" + separator + apiKey,
            "randomKey" + separator + randomString,
            "signature" + separator + signature
        };

            string joinedParams = string.Join("&", authorizationParams);
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(joinedParams));
        }
    }

    private static string GenerateRandomHexString(int byteLength)
    {
        using (var rng = RandomNumberGenerator.Create())
        {
            var bytes = new byte[byteLength];
            rng.GetBytes(bytes);
            return BitConverter.ToString(bytes).Replace("-", "").ToLowerInvariant();
        }
    }
}
