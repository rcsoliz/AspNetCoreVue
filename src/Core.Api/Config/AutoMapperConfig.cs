using AutoMapper;
using Model;
using Model.DTOs;
using Model.Identity;
using Service.Comomns;
using System.Linq;


namespace Core.Api.Config
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<Country, CountryDto>();
            CreateMap<DataCollection<Country>, DataCollection<CountryDto>>();

            CreateMap<Category, CategoryDto>();
            CreateMap<DataCollection<Category>, DataCollection<CategoryDto>>();

            CreateMap<Client, ClientDto>();
            CreateMap<DataCollection<Client>, DataCollection<ClientDto>>();

            CreateMap<Product, ProductDto>();
            CreateMap<DataCollection<Product>, DataCollection<ProductDto>>();

            CreateMap<Order, OrderDto>();
            CreateMap<OrderDetail, OrderDetailDto>();
            CreateMap<DataCollection<Order>, DataCollection<OrderDto>>();

            CreateMap<ApplicationUser, ApplicationUserDto>()
                    .ForMember(
                        dest => dest.FullName,
                        opts => opts.MapFrom(src => src.LastName + ", " + src.FirstName)
                    ).ForMember(
                        dest => dest.Roles,
                        opts => opts.MapFrom(src => src.UserRoles.Select(y => y.Role.Name).ToList())
                    );
            CreateMap<DataCollection<ApplicationUser>, DataCollection<ApplicationUserDto>>();

            //Create Order
            CreateMap<OrderCreateDto, Order>();
            CreateMap<OrderDetailCreateDto, OrderDetail>();

        } 
    }
}
