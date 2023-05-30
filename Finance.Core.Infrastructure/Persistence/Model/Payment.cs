using System;

namespace Finance.Core.Infrastructure.Persistence.Model
{
    public class Payment
    {
        public Payment(string _description, decimal _value)
        {
            id = Guid.NewGuid();
            description = _description;
            value = _value;
        }

        public Payment(Guid _id, string _description, decimal _value)
        {
            id = _id;
            description = _description;
            value = _value;
        }

        public Guid id { get; private set; }
        public string description { get; private set; }
        public decimal value { get; private set; }
    }
}
