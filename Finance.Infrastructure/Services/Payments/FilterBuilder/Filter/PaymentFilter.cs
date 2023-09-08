namespace Finance.Infrastructure.Services.Payments.FilterBuilder.Filter
{
    public class PaymentFilter
    {
        public string Id { get; private set; }

        public PaymentFilter WithId(string id)
        {
            this.Id = id.Trim();
            return this;
        }
    }
}
