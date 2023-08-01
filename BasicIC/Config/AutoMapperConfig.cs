using AutoMapper;
using BasicIC.Models.Main.M03;
using Ninject;
using Repository.CustomModel;
using Repository.EF;
using BasicIC.Models.Main.M03;
using System;
using System.Linq;

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
            CreateMap<DateTime?, Nullable<TimeSpan>>().ConvertUsing(new DateTimeToTimeSpanConverter());
            CreateMap<Nullable<TimeSpan>, Nullable<DateTime>>().ConvertUsing(new TimeSpanToDateTimeConverter());

            // Add as many of these lines as you need to map your objects
            CreateMap<CustomerModel, M03_Customer>().ReverseMap();
            CreateMap<ProductModel, M03_Product>().ReverseMap();
            CreateMap<ImageModel, M03_Image>().ReverseMap();
            CreateMap<AttributeModel, M03_Attribute>().ReverseMap();
            CreateMap<ProductAttributeModel, M03_ProductAttribute>().ReverseMap();
            CreateMap<SupplierModel, M03_Supplier>().ReverseMap();
            CreateMap<WishListModel, M03_WishList>().ReverseMap();
        }

        public class DateTimeToTimeSpanConverter : ITypeConverter<DateTime?, Nullable<System.TimeSpan>>
        {
            public Nullable<System.TimeSpan> Convert(DateTime? source, Nullable<System.TimeSpan> destination, ResolutionContext context)
            {
                if (source != null)
                    return source.Value.TimeOfDay;
                else
                    return null;
            }
        }

        public class TimeSpanToDateTimeConverter : ITypeConverter<Nullable<System.TimeSpan>, Nullable<DateTime>>
        {
            public DateTime? Convert(Nullable<System.TimeSpan> source, DateTime? destination, ResolutionContext context)
            {
                if (source != null)
                    return new DateTime() + source.Value;
                else
                    return null;
            }
        }
    }
}