using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SO.Data.Interfaces;
using SO.Domain;

namespace Sorteador.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly IClienteRepository _clienteRepository;

        public ClienteController(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }
        
        [HttpGet]
        public async Task<IActionResult> CadastrarCliente(Cliente cliente)
        {
            return Ok(await _clienteRepository.Adicionar(cliente));
            
        }
    }
    
    
    
    
}
