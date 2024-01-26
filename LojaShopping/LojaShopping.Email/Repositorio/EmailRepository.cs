using LojaShopping.Email.Mensagem;
using LojaShopping.Email.Model;
using LojaShopping.Email.Model.Context;
using Microsoft.EntityFrameworkCore;

namespace LojaShopping.Email.Repositorio;

public class EmailRepository : IEmailRepository
{

    private readonly DbContextOptions<MySQLContext> _context;

    public EmailRepository(DbContextOptions<MySQLContext> context)
    {
        _context = context;
    }

    public async Task LogEmail(AtualizacaoPagamentoMensagem mensagem)
    {
        EmailLog email = new EmailLog()
        {
            Email = mensagem.Email,
            DataEnvio = DateTime.Now,
            Log = $"Order - {mensagem.OrderId} enviada com sucesso!"
        };


        await using var _db = new MySQLContext(_context);
        _db.Emails.Add(email);
        await _db.SaveChangesAsync();
    }
}
