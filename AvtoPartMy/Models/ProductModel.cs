using AutoMapper;
using Data.Entities.Identity;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AvtoPartMy.Models
{
    //public class ProductModel
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //    public int Price { get; set; }
    //    public IFormFile Photo { get; set; }
    //}

    public class ProductAddViewModel
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public IFormFile Photo { get; set; }
    }
    public class ProductItemViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public decimal Price { get; set; }
        public string Category { get; set; }
        public int Quantity { get; set; }
        public string InventoryStatus { get; set; }
        public int Rating { get; set; }

    }
    public class AppMapProfileProduct : Profile
    {
        public AppMapProfileProduct()
        {
            CreateMap<ProductAddViewModel, Product>()
               .ForMember(x => x.Photo, opt => opt.Ignore());

            CreateMap<Product, ProductItemViewModel>()
               .ForMember(x => x.Description, opt => opt.MapFrom(x => "Product Description"))
               .ForMember(x => x.Image, opt => opt.MapFrom(x => @"\images\" + x.Photo))
               .ForMember(x => x.Category, opt => opt.MapFrom(x => "Accessories"))
               .ForMember(x => x.Quantity, opt => opt.MapFrom(x => 25))
               .ForMember(x => x.InventoryStatus, opt => opt.MapFrom(x => "INSTOCK"))
               .ForMember(x => x.Rating, opt => opt.MapFrom(x => new Random().Next(1, 5)));
        }
    }
    
}
