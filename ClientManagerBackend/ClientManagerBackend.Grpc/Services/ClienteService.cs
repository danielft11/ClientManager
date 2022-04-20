using ClientManagerBackend.Dominio.Interfaces;
using cli = ClientManagerBackend.Dominio.Entidades.Cliente;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System;

namespace ClientManagerBackend.Grpc
{
    public class ClienteService : Cliente.ClienteBase
    {
        private readonly ILogger<ClienteService> _logger;
        private readonly IClienteRepositorio _clienteRepositorio;
        public ClienteService(ILogger<ClienteService> logger, IClienteRepositorio clienteRepositorio)
        {
            _logger = logger;
            _clienteRepositorio = clienteRepositorio;
        }

        public override Task<AddClienteResponse> AddCliente(AddClienteRequest request, ServerCallContext context)
        {
            var cliente = new cli
            {
                Nome = request.Nome,
                Cpf = request.Cpf,
                Email = request.Email,
                Nascimento = DateTime.Now
            };

            _clienteRepositorio.CadastrarClienteAsync(cliente);

            return Task.FromResult(new AddClienteResponse
            {
                Message = "Cliente cadastrado com sucesso: " + cliente.Nome
            });
        }
    }
}
