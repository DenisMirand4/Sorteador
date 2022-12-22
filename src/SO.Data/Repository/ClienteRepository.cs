using Microsoft.EntityFrameworkCore;
using SO.Data.Interfaces;
using SO.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SO.Data.Repository
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly ContextoPrincipal _contexto;

        public ClienteRepository(ContextoPrincipal contexto)
        {
            _contexto = contexto;
        }

        public async Task<Cliente> Adicionar(Cliente cliente)
        {
            _contexto.Clientes.Add(cliente);
            await _contexto.SaveChangesAsync();
            return cliente;
        }

        public async Task<Cliente> Atualizar(Cliente cliente)
        {
            _contexto.Clientes.Update(cliente);
            await _contexto.SaveChangesAsync();
            return cliente;
        }

        public async Task<Cliente> ObterPorId(Guid id)
        {
            var r = await _contexto.Clientes.FindAsync(id);
            if (r == null)
            {
                throw new Exception("Cliente não encontrado");
            }
            return r;
        }

        public async Task<IEnumerable<Cliente>> ObterTodos()
        {
            return await _contexto.Clientes.ToListAsync();
        }

        public async Task<Cliente> Remover(Guid id)
        {
            var cliente = await ObterPorId(id);
            _contexto.Clientes.Remove(cliente);
            await _contexto.SaveChangesAsync();
            return cliente;
        }
    }
}
