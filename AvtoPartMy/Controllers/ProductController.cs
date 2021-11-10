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
using System.Threading;
using System.Threading.Tasks;

namespace AvtoPartMy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly AppEFContext _context;
        private readonly IMapper _mapper;

        public ProductController(AppEFContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;

        }

        [HttpGet]

        public async Task<IActionResult> Get()
        {
            var products = await _context.Products.ToListAsync();
            return Ok(products);
        }

        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> Add([FromForm] ProductAddViewModel model)
        {
            try
            {
                string fileName = String.Empty;
                var product = _mapper.Map<Product>(model);

                if (model.Photo != null)
                {
                    string randomFilename = Path.GetRandomFileName() +
                        Path.GetExtension(model.Photo.FileName);

                    string dirPath = Path.Combine(Directory.GetCurrentDirectory(), "images");
                    fileName = Path.Combine(dirPath, randomFilename);
                    using (var file = System.IO.File.Create(fileName))
                    {
                        model.Photo.CopyTo(file);
                    }
                    product.Photo = randomFilename;
                }
                _context.Products.Add(product);
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    invalid = ex.Message
                });
            }
        }

        //[HttpGet]
        ////[Route("list")]
        //public async Task<IActionResult> Get()
        //{
        //    try
        //    {
        //        Thread.Sleep(2000);
        //        var model = await _context.Products
        //            .Select(x => _mapper.Map<ProductItemViewModel>(x)).ToListAsync();
        //        return Ok(model);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(new
        //        {
        //            invalid = ex.Message
        //        });
        //    }
        //}

    }
}
