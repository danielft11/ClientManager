using ClientManagerBackend.Dominio.Entidades;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClientManagerBackend.Aplicacao.Interfaces
{
    public interface IClienteServico
    {
        Task<IList<Cliente>> ObterClientesAsync();

        Task<Cliente> ObterClientePeloCPF(string cpf);

        Task<Cliente> CadastrarClienteAsync(Cliente cliente);

        Task<Cliente> AtualizarClienteAsync(Cliente cliente);

        Task<Cliente> DeletarClienteAsync(Cliente cliente);
    }
}
