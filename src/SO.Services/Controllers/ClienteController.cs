using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SO.Domain;
using SO.Domain.Interfaces;
using System;
using System.Threading.Tasks;

namespace SO.Services.Controllers
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
        
        [HttpPost]
        [Route("CadastrarCliente")]
        public async Task<IActionResult> CadastrarCliente(string nome, string telefone, string CPF, string email)
        {
            var clienteCadastrado = await _clienteRepository.Adicionar(new Cliente
            {
                Nome = nome,
                Telefone = telefone,
                Cpf = CPF,
                Email = email,
                Id = Guid.NewGuid(),
                Numero = new Random().Next(0, 99999)
            });
            return Ok(clienteCadastrado);
        }

        [HttpPut]
        [Route("NovoNumero")]
        public async Task<IActionResult> NovoNumero(string email)
        {
            var clienteAtualizado = await _clienteRepository.Atualizar(email);
            return Ok(clienteAtualizado);
        }
    }
}
