using AutoMapper;
using ClientManagerBackend.Aplicacao.Interfaces;
using ClientManagerBackend.Dominio.Entidades;
using ClientManagerBackend.Dominio.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientManagerBackend.Aplicacao.Servicos
{
    public class ClienteServico : IClienteServico
    {
        private readonly IClienteRepositorio _clienteRepositorio;
        private readonly IMapper _mapper;

        public ClienteServico(IClienteRepositorio clienteRepositorio, IMapper mapper)
        {
            _clienteRepositorio = clienteRepositorio;
            _mapper = mapper;
        }

        public async Task<IList<Cliente>> ObterClientesAsync()
        {
            throw new NotImplementedException();
        }
        
        public async Task<Cliente> ObterClientePeloCPF(string cpf)
        {
            throw new NotImplementedException();
        }
        
        public async Task<Cliente> CadastrarClienteAsync(Cliente cliente)
        {
            throw new NotImplementedException();
        }
        
        public async Task<Cliente> AtualizarClienteAsync(Cliente cliente)
        {
            throw new NotImplementedException();
        }
        
        public async Task<Cliente> DeletarClienteAsync(Cliente cliente)
        {
            throw new NotImplementedException();
        }
    }
}
