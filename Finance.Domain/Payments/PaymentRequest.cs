using System.Text.Json.Serialization;

namespace Finance.Domain.Payments
{
    public class PaymentRequest
    {
        //Properties
        public string description { get; set; }
        public decimal amount { get; set; }

        [JsonPropertyName("payment_type")]
        public string paymentType { get; set; }
    }
}
