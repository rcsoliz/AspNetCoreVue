using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Model;
using Model.DTOs;
using Persistence.Database;
using Service.Comomns;
using Service.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service
{
    public interface IProductService
    {
        Task<ProductDto> Create(ProductCreateDto model);
        Task<ProductDto> GetById(int id);
        Task<DataCollection<ProductDto>> GetAllAsync(int page, int take);
        Task Update(int id, ProductUpdateDto model);
        Task Remove(int id);
    }
    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public ProductService(ApplicationDbContext context, 
                              IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ProductDto> Create(ProductCreateDto model)
        {
            var result = new Product
            {
                Name = model.Name,
                Description = model.Description,
                Price = model.Price,
                CategoryId = model.CategoryId
            };
            await _context.AddAsync(result);
            await _context.SaveChangesAsync();

            return _mapper.Map<ProductDto>(result);
        }

        public async Task<ProductDto> GetById(int id)
        {
            return _mapper.Map<ProductDto>(
                           await _context.Products.SingleAsync(x => x.ProductId == id)
                  );
        }

        public async Task<DataCollection<ProductDto>> GetAllAsync(int page, int take)
        {
            //return _mapper.Map<DataCollection<ProductDto>>(
            //       await _context.Products.OrderByDescending(x => x.ProductId)
            //                              .AsQueryable()
            //                              .PagedAsync(page, take)
            //      );

            return _mapper.Map<DataCollection<ProductDto>>(
                  await _context.Products
                                .Include(x => x.Category)
                                .OrderByDescending(x => x.ProductId)
                                .AsQueryable()
                                .PagedAsync(page, take)
                 );

        }

        public async Task Update (int id, ProductUpdateDto model)
        {
            var entry = await _context.Products.SingleAsync(x => x.ProductId == id);
            entry.ProductId = id;
            entry.Name = model.Name;
            entry.Description = model.Description;
            entry.Price = model.Price;
            entry.CategoryId = model.CategoryId;

             _context.Update(entry);

            await _context.SaveChangesAsync();
        }

        public async Task Remove(int id)
        {
            _context.Remove(new Product
            {
                ProductId = id
            });

            await _context.SaveChangesAsync();
        }

    
    }
}
