using AutoMapper;
using ClientManagerBackend.Aplicacao.DTOs;
using ClientManagerBackend.Aplicacao.Interfaces;
using ClientManagerBackend.Dominio.Entidades;
using ClientManagerBackend.Dominio.Interfaces;
using System.Collections.Generic;
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

        public async Task<IList<ClienteDTO>> ObterClientesAsync()
        {
            var clientes = await _clienteRepositorio.ObterClientesAsync();
            return _mapper.Map<List<ClienteDTO>>(clientes);
        }

        public async Task<ClienteDTO> ObterClientePeloCPF(string cpf)
        {
            var cliente = await _clienteRepositorio.ObterClientePeloCPF(cpf);
            return _mapper.Map<ClienteDTO>(cliente);
        }

        public async Task<StatusResponseDTO> CadastrarClienteAsync(ClienteDTO clienteDTO)
        {
            var clienteJaExiste = await _clienteRepositorio.ObterClientePeloCPF(clienteDTO.Cpf);

            if (clienteJaExiste is not null)
                return new StatusResponseDTO()
                {
                    Status = "Erro",
                    Mensagem = "Já existe um cliente no banco de dados cadastrado com este CPF."
                };

            var cliente = _mapper.Map<Cliente>(clienteDTO);

            await _clienteRepositorio.CadastrarClienteAsync(cliente);

            return new StatusResponseDTO()
            {
                Status = "Sucesso",
                Mensagem = "Cliente cadastrado com sucesso!"
            };
        }

        public async Task<StatusResponseDTO> AtualizarClienteAsync(ClienteDTO clienteDTO)
        {
            var cliente = await _clienteRepositorio.ObterClientePeloCPF(clienteDTO.Cpf);

            if (cliente is null)
                return new StatusResponseDTO()
                {
                    Status = "Erro",
                    Mensagem = "Nenhum cliente com este CPF foi encontrado na nossa base de dados."
                };

            var clienteAlterar = _mapper.Map<Cliente>(clienteDTO);

            await _clienteRepositorio.AtualizarClienteAsync(clienteAlterar);

            return new StatusResponseDTO()
            {
                Status = "Sucesso!",
                Mensagem = "Cliente alterado com sucesso."
            };
        }

        public async Task<StatusResponseDTO> DeletarClienteAsync(string cpf)
        {
            var cliente = await _clienteRepositorio.ObterClientePeloCPF(cpf);

            if (cliente is null)
                return new StatusResponseDTO()
                {
                    Status = "Erro",
                    Mensagem = "Nenhum cliente com este CPF foi encontrado na nossa base de dados."
                };

            await _clienteRepositorio.DeletarClienteAsync(cliente);

            return new StatusResponseDTO()
            {
                Status = "Sucesso!",
                Mensagem = "Cliente removido com sucesso."
            };
        }

    }

}
