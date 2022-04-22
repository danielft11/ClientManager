using ClientManagerBackend.Dominio.Interfaces;
using Cli = ClientManagerBackend.Dominio.Entidades.Cliente;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using ClientManagerBackend.Grpc.Extensions;
using System.Linq;

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

        public override async Task GetClientesByStream(GetClientesRequest request, IServerStreamWriter<ClienteLookupModel> responseStream, ServerCallContext context)
        {
            var clientes = await _clienteRepositorio.ObterClientesAsync();

            var clientesLookup = clientes.ToClientesLookupList();

            if (clientesLookup == null || !clientesLookup.Any())
                await responseStream.WriteAsync(null);

            foreach (var cliente in clientesLookup)
            {
                await responseStream.WriteAsync(cliente);
            }

        }

        public override Task<AddClienteResponse> AddCliente(AddClienteRequest request, ServerCallContext context)
        {
            var cliente = new Cli
            {
                Nome = request.Nome,
                Cpf = request.Cpf,
                Telefone = request.Telefone,
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
