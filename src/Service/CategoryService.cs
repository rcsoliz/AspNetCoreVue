
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
    public interface ICategoryService
    {
        Task<CategoryDto> Create(CategoryCreateDto model);
        Task<CategoryDto> GetById(int id);
        Task<DataCollection<CategoryDto>> GetAllAsync(int page, int take);
        Task Update(int id, CategoryUpdateDto model);
        Task Remove(int id);
    }
    public class CategoryService : ICategoryService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public CategoryService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        public async Task<CategoryDto> Create(CategoryCreateDto model)
        {
            var result = new Category
            {
                Name = model.Name,
                Description = model.Description
            };
            await _context.AddAsync(result);
            await _context.SaveChangesAsync();

            return _mapper.Map<CategoryDto>(result);

        }

        public async Task<CategoryDto> GetById(int id)
        {
            return _mapper.Map<CategoryDto>(
                await _context.Categories.SingleAsync(x => x.CategoryId == id)
                );
        }

        public async Task<DataCollection<CategoryDto>> GetAllAsync(int page, int take)
        {
            return  _mapper.Map<DataCollection<CategoryDto>>(
                await _context.Categories.OrderByDescending(x =>x.CategoryId)
                                         .AsQueryable()
                                         .PagedAsync(page, take)
                );
        }

        public async Task Update(int id , CategoryUpdateDto model)
        {
            var entry = await _context.Categories.SingleAsync(x => x.CategoryId == id);
            entry.Name = model.Name;
            entry.Description = model.Description;

            await _context.SaveChangesAsync();
        }

        public async Task Remove(int id)
        {
            _context.Remove( new Category { 
               CategoryId = id 
            });

            await _context.SaveChangesAsync();
        }
    }

}
