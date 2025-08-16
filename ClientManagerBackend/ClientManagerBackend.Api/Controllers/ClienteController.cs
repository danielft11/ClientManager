using ClientManagerBackend.Aplicacao.DTOs;
using ClientManagerBackend.Aplicacao.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClientManagerBackend.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClienteController : ControllerBase
    {
        private readonly IClienteServico _clienteServico;
        private readonly ILogger _logger;

        public ClienteController(IClienteServico clienteServico, ILogger<ClienteController> logger)
        {
            _clienteServico = clienteServico;
            _logger = logger;
        }

        [HttpGet]
        [Route("ObterClientes")]
        [ProducesResponseType(typeof(IEnumerable<ClienteDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ObterClientes() 
        {
            var customers = await _clienteServico.GetCustomersAsync();

            return Ok(customers);
        }

        [HttpGet]
        [Route("ObterClientePeloCPF/{cpf}")]
        [ProducesResponseType(typeof(ClienteDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ObterClientePeloCPF(string cpf) 
        {
            var customer = await _clienteServico.GetCustomerByCpfAsync(cpf);
            
            return Ok(customer);
        }

        [HttpPost]
        [Route("CadastrarCliente")]
        [ProducesResponseType(typeof(ClienteDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(StatusResponseDTO), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CadastrarCliente([FromBody] ClienteDTO clienteDTO)
        {
            var response = await _clienteServico.AddCustomerAsync(clienteDTO);
            if (response.Status == "Erro")
                return BadRequest(response);

            return Ok(response);
        }

        [HttpPut]
        [Route("AtualizarCliente")]
        [ProducesResponseType(typeof(ClienteDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(StatusResponseDTO), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> AtualizarCliente([FromBody] ClienteDTO clienteDTO) 
        {
           var response = await _clienteServico.UpdateCustomerAsync(clienteDTO);
           if (response.Status == "Erro")
                return BadRequest(response);

            return Ok(response);
        }

        [HttpDelete]
        [Route("RemoverCliente/{cpf}")]
        [ProducesResponseType(typeof(ClienteDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(StatusResponseDTO), StatusCodes.Status404NotFound)]
        public async Task<StatusResponseDTO> RemoverCliente(string cpf) 
        {
            return await _clienteServico.DeletarClienteAsync(cpf);
        }

    }
}
