using AutoMapper;
using Data.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AvtoPartMy.Models
{
    public class UserViewModel
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public string Photo { get; set; }
    }
    public class UserView : Profile
    {
        public UserView()
        {
            CreateMap<AppUser, UserViewModel>()
                .ForMember(x => x.Photo, opt => opt.MapFrom(x => "images/"
                    + (string.IsNullOrEmpty(x.FotoUser) ? "noimage.jpg" : x.FotoUser)));
        }
    }
}
