using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LojaShopping.ProcessoPagamento
{
    public class ProcessoPagamento : IProcessoPagamento
    {
        bool IProcessoPagamento.ProcessoPagamento()
        {
            return true;
        }
    }
}
