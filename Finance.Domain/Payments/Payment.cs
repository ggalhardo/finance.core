using Finance.Domain.DomainObjects;
using Finance.Domain.DomainObjects.Extensions;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Finance.Domain.Payments
{
    public class Payment
    {
        [BsonRepresentation(MongoDB.Bson.BsonType.String)]
        public Guid Id { get; private set; }
        public string Description { get; private set; }
        public double Amount { get; private set; }
        public PaymentType Type { get; private set; }

        public Payment(string pDescription, double pAmount, int pType)
        {
            this.Description = pDescription;
            this.Amount = pAmount;
            this.Type = pType.Map();
        }

        public Payment(Guid pId, string pDescription, double pAmount, int pType)
        {
            this.Id = pId;
            this.Description = pDescription;
            this.Amount = pAmount;
            this.Type = pType.Map();
        }

        public Payment(Guid pId, string pDescription, double pAmount, PaymentType pType)
        {
            this.Id = pId;
            this.Description = pDescription;
            this.Amount = pAmount;
            this.Type = pType;
        }
    }
}
