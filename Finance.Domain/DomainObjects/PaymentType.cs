namespace Finance.Domain.DomainObjects
{
    public class PaymentType
    {
        public int Id { get; private set; }

        public PaymentType(int id)
        {
            this.Id = id;
        }
    }
}
