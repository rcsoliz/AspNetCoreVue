
using AutoMapper;
using Common;
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
    public interface IOrderService
    {
        Task<OrderDto> Create(OrderCreateDto model);
        Task<OrderDto> GetById(int id);
        Task<DataCollection<OrderDto>> GetAll(int page, int take);
    }
    public class OrderService : IOrderService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public OrderService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<OrderDto> Create(OrderCreateDto model)
        {
            var Entry = _mapper.Map<Order>(model);

            //preparar order Detaill
            PrepareDetail(Entry.Items);

            //preparar order Header
            PrepareHeader(Entry);

            await _context.AddAsync(Entry);
            await _context.SaveChangesAsync();

            return _mapper.Map<OrderDto>(
                await GetById(Entry.OrderId)
            );
        }

        public async Task<OrderDto> GetById(int id)
        {
            return _mapper.Map<OrderDto>(
                 await _context.Orders.OrderByDescending(x => x.OrderId)
                                      .Include(x => x.Client)
                                      .Include(x => x.Items)
                                      .ThenInclude(x => x.Product)
                                      .SingleAsync(x => x.OrderId == id)
                 );
        }

        public async Task<DataCollection<OrderDto>> GetAll(int page, int take)
        {
            return _mapper.Map<DataCollection<OrderDto>>(
                        await _context.Orders.OrderByDescending(x => x.OrderId)
                        .Include(x => x.Client)
                        .Include(x => x.Items)
                        .ThenInclude(x => x.Product)
                        .AsQueryable()
                        .PagedAsync(page, take)
);
        }

        private void PrepareDetail(IEnumerable<OrderDetail> items)
        {
            foreach(var item in items)
            {
                item.Total = item.Quantity * item.UnitPrice;
                item.Iva = item.Total * Parameter.pIva;
                item.SubTotal = item.Total - item.Iva;
            }
        }

        private void PrepareHeader(Order item)
        {
            item.Total = item.Items.Sum(x => x.Total);
            item.SubTotal = item.Items.Sum(x => x.SubTotal);
            item.Iva = item.Items.Sum(x => x.Iva );
        }

    }
}
