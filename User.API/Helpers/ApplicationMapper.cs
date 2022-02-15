using AutoMapper;
using User.API.Data.DTOs;
using User.API.Data.Models;

namespace User.API.Helpers
{
    public class ApplicationMapper : Profile
    {
        public ApplicationMapper()
        {
            CreateMap<UserModel, SignUpDTO>().ReverseMap();
        }
    }
}
