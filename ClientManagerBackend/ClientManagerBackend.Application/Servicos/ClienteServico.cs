using ClientManagerBackend.Aplicacao.DTOs;
using ClientManagerBackend.Aplicacao.Interfaces;
using ClientManagerBackend.Dominio.Entities;
using ClientManagerBackend.Dominio.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClientManagerBackend.Aplicacao.Servicos
{
    public class ClienteServico : IClienteServico
    {
        private readonly IClienteRepositorio _clienteRepositorio;

        public ClienteServico(IClienteRepositorio clienteRepositorio)
        {
            _clienteRepositorio = clienteRepositorio;
        }

        public async Task<IList<ClienteDTO>> GetCustomersAsync()
        {
            var clientes = await _clienteRepositorio.GetCustomersAsync();
  
            var customersDTO = clientes.Select(client => CustomerToDTO(client)).ToList();

            return customersDTO;
        }

        public async Task<ClienteDTO> GetCustomerByCpfAsync(string cpf)
        {
            var cliente = await _clienteRepositorio.GetCustomerByCpfAsync(cpf);
            
            return CustomerToDTO(cliente);
        }

        public async Task<StatusResponseDTO> AddCustomerAsync(ClienteDTO clienteDTO)
        {
            var clienteJaExiste = await _clienteRepositorio.GetCustomerByCpfAsync(clienteDTO.Cpf);

            try
            {
                if (clienteJaExiste is not null)
                    return new StatusResponseDTO()
                    {
                        Status = "Erro",
                        Mensagem = "Já existe um cliente no banco de dados cadastrado com este Cpf."
                    };

                var cliente = DtoToCustomer(clienteDTO);

                await _clienteRepositorio.AddCustomerAsync(cliente);
            }
            catch (Exception ex)
            {
                return new StatusResponseDTO()
                {
                    Status = "Erro",
                    Mensagem = $"Falha ao criar o cliente: {ex.Message}"
                };
            }
            
            return new StatusResponseDTO()
            {
                Status = "Sucesso",
                Mensagem = "Cliente cadastrado com sucesso!"
            };
        }

        public async Task<StatusResponseDTO> UpdateCustomerAsync(ClienteDTO clienteDTO)
        {
            try
            {
                var cliente = await _clienteRepositorio.GetCustomerByCpfAsync(clienteDTO.Cpf);
                if (cliente is null)
                    return new StatusResponseDTO()
                    {
                        Status = "Error",
                        Mensagem = "Nenhum cliente com este Cpf foi encontrado na nossa base de dados."
                    };

                cliente.UpdateModel(
                nome: clienteDTO.Nome,
                cpf: clienteDTO.Cpf,
                telefone: clienteDTO.Telefone,
                email: new Dominio.ValueObjects.EmailVO(clienteDTO.Email),
                nascimento: clienteDTO.Nascimento);

                await _clienteRepositorio.UpdateCustomerAsync(cliente);
            }
            catch (Exception ex)
            {
                return new StatusResponseDTO()
                {
                    Status = "Erro",
                    Mensagem = $"Falha ao atualizar o cliente: {ex.Message}"
                };
            }
            
            return new StatusResponseDTO()
            {
                Status = "Sucesso!",
                Mensagem = "Cliente alterado com sucesso."
            };
        }

        public async Task<StatusResponseDTO> DeletarClienteAsync(string cpf)
        {
            var cliente = await _clienteRepositorio.GetCustomerByCpfAsync(cpf);

            if (cliente is null)
                return new StatusResponseDTO()
                {
                    Status = "Erro",
                    Mensagem = "Nenhum cliente com este CPF foi encontrado."
                };

            await _clienteRepositorio.DeleteCustomerAsync(cliente);

            return new StatusResponseDTO()
            {
                Status = "Sucesso!",
                Mensagem = "Cliente removido com sucesso."
            };
        }

        private Cliente DtoToCustomer(ClienteDTO clienteDTO) 
        {
            return new Cliente(
                nome: clienteDTO.Nome,
                cpf: clienteDTO.Cpf,
                telefone: clienteDTO.Telefone,
                email: new Dominio.ValueObjects.EmailVO(clienteDTO.Email),
                nascimento: clienteDTO.Nascimento
            );
        }

        public ClienteDTO CustomerToDTO(Cliente cliente)
        {
            if (cliente == null) return null;

            return new ClienteDTO
            {
                Nome = cliente.Nome,
                Cpf = cliente.Cpf,
                Telefone = cliente.Telefone,
                Email = cliente.Email.Address, 
                Nascimento = cliente.Nascimento
            };
        }

    }

}
