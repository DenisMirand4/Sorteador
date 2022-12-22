using SO.Domain;

namespace SO.Data.Interfaces
{
    public interface IClienteRepository
    {
        Task<IEnumerable<Cliente>> ObterTodos();
        Task<Cliente> ObterPorId(Guid id);
        Task<Cliente> Adicionar(Cliente cliente);
        Task<Cliente> Atualizar(Cliente cliente);
        Task<Cliente> Remover(Guid id);
    }
}
