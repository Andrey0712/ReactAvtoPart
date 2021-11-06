using AutoMapper;
using AvtoPartMy.Models;
using Data;
using Data.Entities.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AvtoPartMy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly AppEFContext _context;
        

        public ProductController(AppEFContext context
            )
        {
            _context = context;
           
            
        }

        [HttpGet]
        

        public async Task<IActionResult> Get()
        {
            var products = await _context.Products.ToListAsync();
            return Ok(products);
        }

        //[HttpPost]
        //[Route("addProduct")]
        //public async Task<IActionResult> RegisterAsync([FromForm] ProductModel model)
        //{
        //           
        //    string fileNameUser = string.Empty;

        //    
        //    if (model.Photo != null)
        //    {
        //        var ext = Path.GetExtension(model.Photo.FileName);
        //        fileNameUser = Path.GetRandomFileName() + ext;
        //        var dir = Path.Combine(Directory.GetCurrentDirectory(), "images");
        //        var filePath = Path.Combine(dir, fileNameUser);

        //        using (var stream = System.IO.File.Create(filePath))
        //        {
        //            await model.Photo.CopyToAsync(stream);
        //        }
        //    }

        //    try
        //    {

        //        var product = new Product
        //        {
        //            Id= model.Id,
        //            Name = model.Name,
        //            Photo = fileNameUser,
        //            Price= model.Price

        //        };


        //        _context.Products.Add(product);
        //        _context.SaveChanges();

        //        return Ok();
        //    }
        //    catch
        //    {
        //        return BadRequest(new { message = "Щось пішло не так - помилка з БД" });

        //    }
        //}

    }
}
