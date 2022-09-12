using AutoMapper;
using NewsApi.DTO;
using NewsApi.Models;

namespace NewsApi.Profiles
{
    public class NewsDtoProfile : Profile
    {
        public NewsDtoProfile()
        {
            CreateMap<News, NewsDto>().ReverseMap();
        }
    }
}
