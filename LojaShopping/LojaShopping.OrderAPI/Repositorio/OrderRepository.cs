using Microsoft.EntityFrameworkCore;
using LojaShopping.OrderAPI.Model.Context;
using LojaShopping.OrderAPI.Model;

namespace LojaShopping.OrderAPI.Repositorio;

public class OrderRepository : IOrderRepository
{

    private readonly DbContextOptions<MySQLContext> _context;

    public OrderRepository(DbContextOptions<MySQLContext> context)
    {
        _context = context;
    }

    public async Task<bool> AddOrder(OrderHeader header)
    {
        if (header == null)
            return false;

        await using var _db = new MySQLContext(_context);
        _db.Headers.Add(header);
        await _db.SaveChangesAsync();
        return true;

    }

    public async Task UpdateOrderPagamentoStatus(long orderHeaderId, bool status)
    {

        await using var _db = new MySQLContext(_context);
        var header = await _db.Headers.FirstOrDefaultAsync(o => o.Id == orderHeaderId);

        if (header != null)
        {
            header.StatusPagamento = status;
            await _db.SaveChangesAsync();
        }
    }
}
