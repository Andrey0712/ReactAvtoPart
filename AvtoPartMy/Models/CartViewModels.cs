using AutoMapper;
using Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AvtoPartMy.Models
{
    public class CartAddViewModel
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }

    public class CartItemViewModel
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public string ProductImage { get; set; }
        public decimal ProductPrice { get; set; }
        public int Quantity { get; set; }
    }
    public class AppMapProfileCart : Profile
    {
        public AppMapProfileCart()
        {
            CreateMap<CartAddViewModel, CartEntity>();

            CreateMap<CartEntity, CartItemViewModel>()
               .ForMember(x => x.ProductName, opt => opt.MapFrom(x => x.Product.Name))
               .ForMember(x => x.ProductImage, opt => opt.MapFrom(x => @"\images\" + x.Product.Photo))
               .ForMember(x => x.ProductPrice, opt => opt.MapFrom(x => x.Product.Price));
        }
    }
}
