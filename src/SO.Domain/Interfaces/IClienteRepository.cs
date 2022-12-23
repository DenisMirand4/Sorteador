using System;
using System.Threading.Tasks;

namespace SO.Domain.Interfaces
{
    public interface IClienteRepository
    {
        Task<Cliente> Adicionar(Cliente cliente);
        Task<Cliente> Atualizar(string email);
    }
}
