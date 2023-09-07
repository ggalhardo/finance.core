namespace Finance.Domain.DomainObjects.Extensions
{
    public static class PaymentTypeExtension
    {
        public static PaymentType Map(this int pId)
        {
            return new PaymentType(pId);
        }

    }
}
