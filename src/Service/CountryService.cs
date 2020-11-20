
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
    public interface ICountryService
    {
        Task<CountryDto> Create(CountryCreateDto model);
        Task<CountryDto> GetById(int id);
        Task<DataCollection<CountryDto>> GetAllAsync(int page, int take);
        Task Update(int id, CountryUpdateDto model);
        Task Remove(int id);
    }

    public class CountryService : ICountryService
{
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public CountryService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        public async Task<CountryDto> Create(CountryCreateDto model)
        {
            var result = new Country
            {
                Name = model.Name
            };
            await _context.AddAsync(result);
            await _context.SaveChangesAsync();

            return _mapper.Map<CountryDto>(result);

        }

        public async Task<CountryDto> GetById(int id)
        {
            return _mapper.Map<CountryDto>(
                await _context.Countries.SingleAsync(x => x.CountryId == id)
                );
        }

        public async Task<DataCollection<CountryDto>> GetAllAsync(int page, int take)
        {
            return _mapper.Map<DataCollection<CountryDto>>(
                await _context.Countries.OrderByDescending(x => x.CountryId)
                                         .AsQueryable()
                                         .PagedAsync(page, take)
               );
        }

        public async Task Update(int id, CountryUpdateDto model)
        {
            var entry = await _context.Countries.SingleAsync(x => x.CountryId == id);
            entry.Name = model.Name;

            await _context.SaveChangesAsync();
        }

        public async Task Remove(int id)
        {
            _context.Remove(new Country
            {
                CountryId = id
            });

            await _context.SaveChangesAsync();
        }

    }
}
