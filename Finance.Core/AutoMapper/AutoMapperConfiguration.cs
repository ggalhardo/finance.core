using AutoMapper;
using Finance.Domain._Core.Enum;
using Finance.Domain.Payments;
using Finance.Domain.Payments.Request;

namespace Finance.Core.AutoMapper
{
    public static class AutoMapperConfiguration
    {

        public static void Initialize()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapperProfile());
            });
        }
    }

    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Payment, PaymentRequest>();
            //Map request to entity with private set's
            CreateMap<PaymentRequest, Payment>().ConstructUsing(x => new Payment(x.Description, x.Amount, (int)((PaymentTypeEnum)System.Enum.Parse(typeof(PaymentTypeEnum), x.PaymentType, true))));
        }
    }
}
