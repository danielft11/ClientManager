using ClientManagerBackend.Aplicacao.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClientManagerBackend.Aplicacao.Interfaces
{
    public interface IClienteServico
    {
        Task<IList<ClienteDTO>> ObterClientesAsync();

        Task<ClienteDTO> ObterClientePeloCPF(string cpf);

        Task<StatusResponseDTO> CadastrarClienteAsync(ClienteDTO cliente);

        Task<StatusResponseDTO> AtualizarClienteAsync(ClienteDTO cliente);

        Task<StatusResponseDTO> DeletarClienteAsync(string cpf);
    }
}
