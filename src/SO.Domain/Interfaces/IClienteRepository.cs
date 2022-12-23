using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SO.Domain.Interfaces
{
    public interface IClienteRepository
    {
        //Task<Cliente> ObterPorEmail(string email);
        Task<Cliente> Adicionar(Cliente cliente);
        Task<Cliente> Atualizar(string email);
    }
}
