using System.Text.Json.Serialization;

namespace Finance.Domain.Payments
{
    public class PaymentRequest
    {
        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("amount")]
        public double Amount { get; set; }

        [JsonPropertyName("payment_type")]
        public string PaymentType { get; set; }
    }
}
