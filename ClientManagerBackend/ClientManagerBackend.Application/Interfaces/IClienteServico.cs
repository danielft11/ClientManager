using ClientManagerBackend.Aplicacao.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClientManagerBackend.Aplicacao.Interfaces
{
    public interface IClienteServico
    {
        Task<IList<ClienteDTO>> GetCustomersAsync();

        Task<ClienteDTO> GetCustomerByCpfAsync(string cpf);

        Task<StatusResponseDTO> AddCustomerAsync(ClienteDTO cliente);

        Task<StatusResponseDTO> UpdateCustomerAsync(ClienteDTO cliente);

        Task<StatusResponseDTO> DeletarClienteAsync(string cpf);
    }
}
