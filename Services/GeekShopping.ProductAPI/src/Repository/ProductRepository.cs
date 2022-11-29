using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using GeekShopping.ProductAPI.src.Data.ValueObjects;
using GeekShopping.ProductAPI.src.Models;
using GeekShopping.ProductAPI.src.Models.Context;
using Microsoft.EntityFrameworkCore;

namespace GeekShopping.ProductAPI.src.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly MySqlContext _context;
        private IMapper _mapper;

        public ProductRepository(MySqlContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ProductVO> Create(ProductVO vo)
        {
            var product = _mapper.Map<Product>(vo);
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
            return vo;
        }

        public async Task<bool> Delete(long id)
        {
            try
            {
                var product = await _context.Products.FindAsync(id);
                if (product == null) return false; // maybe that's a unnecessary validation tho

                _context.Remove(product);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public async Task<IEnumerable<ProductVO>> GetAll()
        {
            List<Product> products = await _context.Products.ToListAsync();
            return _mapper.Map<List<ProductVO>>(products);
        }

        public async Task<ProductVO> GetById(long id)
        {
            var product = await _context.Products.FindAsync(id);
            return _mapper.Map<ProductVO>(product);
        }

        public async Task<ProductVO> Update(ProductVO vo)
        {
            var product = _mapper.Map<Product>(vo);
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
            return vo;
        }
    }
}