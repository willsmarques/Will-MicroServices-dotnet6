using LojaShopping.OrderAPI.Model;

namespace LojaShopping.OrderAPI.Repositorio
{
    public interface IOrderRepository
    {
        Task<bool> AddOrder(OrderHeader header);
        Task UpdateOrderPagamentoStatus(long orderHeaderId, bool pago);

    }
}
