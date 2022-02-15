using AutoMapper;

namespace User.API.Helpers
{
    /// <summary>
    /// Application mapper class, inherits from Profile class.
    /// </summary>
    public class ApplicationMapper : Profile
    {
        public ApplicationMapper()
        {
            //CreateMap<UserModel, SignUpDTO>().ReverseMap();
        }
    }
}
