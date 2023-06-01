namespace Finance.Domain.DomainObjects.Extensions
{
    public static class PaymentTypeExtension
    {

        public static PaymentType Map(this int id)
        {
            var type = new PaymentType();

            type.id = id;

            return type;
        }

    }
}
