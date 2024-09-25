using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SMART_TDCM.API.Data;
using SMART_TDCM.CORE.Models;

namespace SMART_TDCM.API.Controllers
{
    public class ProductController : BaseController
    {
        private readonly ApplicationDbContext _context;

        public ProductController(ApplicationDbContext context)
        {
            _context = context;
        }


        [HttpGet]
        [Route("GetAllProducts")]
        public async Task<ActionResult<List<Product>>> GetAllProducts()
        {
            var products = await _context.Products.ToListAsync();
            return (products);
        }

        [HttpGet]
        [Route("GetAllProductsById/{Id}")]
        public async Task<ActionResult<Product>> GetAllProductsById(int Id)
        {
            var product = await _context.Products.FindAsync(Id);

            if (product == null)
            {
                return NotFound(new { Message = $"Product with Id {Id} not found." });
            }

            return Ok(product); 
        }

    }
}