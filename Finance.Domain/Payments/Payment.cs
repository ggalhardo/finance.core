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

        //Constructors
        public Payment(string _description, decimal _amount)
        {
            id = Guid.NewGuid();
            description = _description;
            amount = _amount;
        }

        public Payment(Guid _id, string _description, decimal _amount)
        {
            id = _id;
            description = _description;
            amount = _amount;
        }
    }
}
