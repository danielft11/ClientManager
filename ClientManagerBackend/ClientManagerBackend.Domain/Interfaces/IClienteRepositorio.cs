using ClientManagerBackend.Dominio.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClientManagerBackend.Dominio.Interfaces
{
    public interface IClienteRepositorio
    {
        Task<IList<Cliente>> GetCustomersAsync();

        Task<Cliente> GetCustomerByCpfAsync(string cpf);

        Task<Cliente> AddCustomerAsync(Cliente cliente);

        Task<Cliente> UpdateCustomerAsync(Cliente cliente);

        Task DeleteCustomerAsync(Cliente cliente);
    }

}
