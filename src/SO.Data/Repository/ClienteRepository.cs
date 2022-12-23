using Microsoft.EntityFrameworkCore;
using SO.Domain;
using SO.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
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
            if (EmailEhValido(cliente.Email) == false || await ObterPorEmail(cliente.Email) != null) { 
                throw new Exception("Email inválido");
            }
            if(CpfEhValido(cliente.Cpf) == false)
            {
                throw new Exception("CPF inválido");
            }
            while (await NumeroEhValido(cliente.Numero) == false)
            {
                cliente.Numero = new Random().Next(0, 99999);
            }
            _contexto.Clientes.Add(cliente);
            await _contexto.SaveChangesAsync();
            EscreverArquivo(cliente);
            return cliente;
        }

        public async Task<Cliente> Atualizar(string email)
        {
            var cliente = await ObterPorEmail(email);
            if (cliente == null)
            {
                throw new Exception("Cliente não encontrado");
            }
            cliente.Numero = new Random().Next(0, 99999);
            while (await NumeroEhValido(cliente.Numero) == false)
            {
                cliente.Numero = new Random().Next(0, 99999);
            }
            _contexto.Clientes.Update(cliente);
            await _contexto.SaveChangesAsync();
            EscreverArquivo(cliente);
            return cliente;
        }
        private void EscreverArquivo(Cliente cliente)
        {
            File.WriteAllTextAsync(new StringBuilder("../../").Append(cliente.Email).Append(".txt").ToString(), cliente.Numero.ToString());
        }

        private async Task<Cliente> ObterPorEmail(string email)
        {
            var r = await _contexto.Clientes.FirstOrDefaultAsync(x => x.Email == email);
            return r;
        }

        private async Task<bool> NumeroEhValido(int numero)
        {
            var r = await _contexto.Clientes.FirstOrDefaultAsync(x => x.Numero == numero);
            if (r == null)
            {
                return true;
            }
            return false;
        }

        private static bool EmailEhValido(string email)
        {
            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            return regex.IsMatch(email);
        }
        private static bool CpfEhValido(string cpf)
        {
            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            string tempCpf;
            string digito;
            int soma;
            int resto;
            cpf = cpf.Trim();
            cpf = cpf.Replace(".", "").Replace("-", "");
            if (cpf.Length != 11)
                return false;
            tempCpf = cpf.Substring(0, 9);
            soma = 0;

            for (int i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = resto.ToString();
            tempCpf = tempCpf + digito;
            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = digito + resto.ToString();
            return cpf.EndsWith(digito);
        }
    }
}
