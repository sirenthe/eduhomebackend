using AutoMapper;
using EduHome.Identity;
using EduHome.ViewModels;

namespace EduHome.Mappers
{
    public class Account :Profile
    {
        public Account() { 
            CreateMap<RegisterViewModel ,AppUser>().ReverseMap();
        }
    }
}
