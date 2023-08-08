using AutoMapper;
using BasicIC.Models.Main.M03;
using Ninject;
using Repository.EF;
using System;

namespace BasicIC.Config
{
    public class AutoMapperConfig
    {
        static readonly IKernel kernel = null;
        public static MapperConfiguration MapperConfiguration()
        {
            return new MapperConfiguration(config =>
            {
                config.ConstructServicesUsing(t => kernel.Get(t));
                config.AddProfile(new MappingProfile());
            });
        }
    }
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<DateTime?, TimeSpan?>().ConvertUsing(new DateTimeToTimeSpanConverter());
            CreateMap<TimeSpan?, DateTime?>().ConvertUsing(new TimeSpanToDateTimeConverter());

            // Add as many of these lines as you need to map your objects
            CreateMap<CustomerModel, M03_Customer>().ReverseMap();
            CreateMap<ProductModel, M03_Product>().ReverseMap();
            CreateMap<ImageModel, M03_Image>().ReverseMap();
            CreateMap<AttributeModel, M03_Attribute>().ReverseMap();
            CreateMap<ProductAttributeModel, M03_ProductAttribute>().ReverseMap();
            CreateMap<SupplierModel, M03_Supplier>().ReverseMap();
            CreateMap<WishListModel, M03_WishList>().ReverseMap();
            CreateMap<CartModel, M03_Cart>().ReverseMap();
            CreateMap<CartDetailModel, M03_CartDetail>().ReverseMap();
            CreateMap<OrderModel, M03_Order>().ReverseMap();
            CreateMap<OrderDetailModel, M03_OrderDetail>().ReverseMap();
            CreateMap<EmployeeModel, M03_Employee>().ReverseMap();
            CreateMap<AddressModel, M03_Address>().ReverseMap();
        }

        public class DateTimeToTimeSpanConverter : ITypeConverter<DateTime?, TimeSpan?>
        {
            public TimeSpan? Convert(DateTime? source, TimeSpan? destination, ResolutionContext context)
            {
                if (source != null)
                    return source.Value.TimeOfDay;
                else
                    return null;
            }
        }

        public class TimeSpanToDateTimeConverter : ITypeConverter<TimeSpan?, DateTime?>
        {
            public DateTime? Convert(TimeSpan? source, DateTime? destination, ResolutionContext context)
            {
                if (source != null)
                    return new DateTime() + source.Value;
                else
                    return null;
            }
        }
    }
}