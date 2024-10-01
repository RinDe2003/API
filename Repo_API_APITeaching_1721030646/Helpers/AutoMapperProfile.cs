using AutoMapper;
using Repo_API_1721030646.Models;
using Repo_API_1721030646.DTO;

namespace Repo_API_1721030646.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // 
            CreateMap<Account, AccountDTO>().ReverseMap();

            //
            CreateMap<Product, ProductDTO>().ReverseMap();

            //
            CreateMap<Order, OrderDTO>().ReverseMap();
        }
    }
}
