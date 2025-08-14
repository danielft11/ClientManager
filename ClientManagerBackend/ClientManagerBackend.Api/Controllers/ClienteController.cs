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
        public async Task<IEnumerable<ClienteDTO>> ObterClientes() 
        {
            return await _clienteServico.ObterClientesAsync();
        }

        [HttpGet]
        [Route("ObterClientePeloCPF/{cpf}")]
        [ProducesResponseType(typeof(ClienteDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ClienteDTO> ObterClientePeloCPF(string cpf) 
        {
            return await _clienteServico.ObterClientePeloCPF(cpf);
        }

        [HttpPost]
        [Route("CadastrarCliente")]
        [ProducesResponseType(typeof(ClienteDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(StatusResponseDTO), StatusCodes.Status400BadRequest)]
        public async Task<StatusResponseDTO> CadastrarCliente([FromBody] ClienteDTO clienteDTO)
        {
            return await _clienteServico.CadastrarClienteAsync(clienteDTO);
        }

        [HttpPut]
        [Route("AtualizarCliente")]
        [ProducesResponseType(typeof(ClienteDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(StatusResponseDTO), StatusCodes.Status404NotFound)]
        public async Task<StatusResponseDTO> AtualizarCliente([FromBody] ClienteDTO clienteDTO) 
        {
            return await _clienteServico.AtualizarClienteAsync(clienteDTO);
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
