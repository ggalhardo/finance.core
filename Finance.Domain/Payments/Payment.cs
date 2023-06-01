using Finance.Domain.DomainObjects;
using Finance.Domain.DomainObjects.Extensions;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Finance.Domain.Payments
{
    public class Payment
    {
        //Properties
        [BsonGuidRepresentation(GuidRepresentation.Standard)]
        public Guid id { get; private set; }
        public string description { get; private set; }
        public decimal amount { get; private set; }
        public PaymentType type { get; private set; }

        //Constructors
        public Payment(string _description, decimal _amount, int _type)
        {
            id = Guid.NewGuid();
            description = _description;
            amount = _amount;
            type = _type.Map();
        }

        public Payment(Guid _id, string _description, decimal _amount, int _type)
        {
            id = _id;
            description = _description;
            amount = _amount;
            type = _type.Map();
        }
    }
}
