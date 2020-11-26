
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
    public interface IClientService
    {
        Task<ClientDto> Create(ClientCreateDto model);
        Task<ClientDto> GetById(int id);
        Task<DataCollection<ClientDto>> GetAllAsync(int page, int take);
        Task Update(int id, ClientUpdateDto model);
        Task Remove(int id);
    }
    public class ClientService : IClientService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public ClientService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ClientDto> Create(ClientCreateDto model)
        {
            var result = new Client
            {
                Name = model.Name,
                SurNames = model.SurNames,
                Telephone = model.Telephone,
                Address = model.Address,
                Notes = model.Notes,
                CountryId = model.CountryId
            };
            await _context.AddAsync(result);
            await _context.SaveChangesAsync();

            return _mapper.Map<ClientDto>(result);

        }

        public async Task<ClientDto> GetById(int id)
        {
            return _mapper.Map<ClientDto>(
                await _context.Clients.SingleAsync(x => x.ClientId == id)
                );
        }

        public async Task<DataCollection<ClientDto>> GetAllAsync(int page, int take)
        {

            //return _mapper.Map<DataCollection<ClientDto>>(
            //    await _context.Clients.OrderByDescending(x => x.ClientId)
            //                          .AsQueryable()
            //                          .PagedAsync(page, take)
            //    );


            return _mapper.Map<DataCollection<ClientDto>>(
                await _context.Clients
                      .Include(x => x.Country)
                .OrderByDescending(x => x.ClientId)
                                      .AsQueryable()
                                      .PagedAsync(page, take)
                );
        }

        public async Task Update(int id , ClientUpdateDto model)
        {
            var entry = await _context.Clients.SingleAsync(x => x.ClientId == id);
            entry.Name = model.Name;
            entry.SurNames = model.SurNames;
            entry.Telephone = model.Telephone;
            entry.Address = model.Address;
            entry.Notes = model.Notes;
            entry.CountryId = model.CountryId;

            await _context.SaveChangesAsync();
        }

        public async Task Remove(int id)
        {
            _context.Remove( new Client { 
               ClientId = id 
            });

            await _context.SaveChangesAsync();
        }
    }
}
