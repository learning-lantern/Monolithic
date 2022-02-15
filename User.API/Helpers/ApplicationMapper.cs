using AutoMapper;
using User.API.Data.DTOs;
using User.API.Data.Models;

namespace User.API.Helpers
{
    /// <summary>
    /// Application mapper class, inherits from Profile class.
    /// </summary>
    public class ApplicationMapper : Profile
    {
        public ApplicationMapper()
        {
            CreateMap<UserModel, SignUpDTO>().ReverseMap();
        }
    }
}
