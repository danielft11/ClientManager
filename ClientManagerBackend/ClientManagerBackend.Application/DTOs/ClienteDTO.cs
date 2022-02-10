using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ClientManagerBackend.Aplicacao.DTOs
{
    public class ClienteDTO
    {
        [Required]
        [JsonPropertyName("nome")]
        public string Nome { get; set; }

        [Required]
        [JsonPropertyName("cpf")]
        public string Cpf { get; set; }

        [JsonPropertyName("telefone")]
        public string Telefone { get; set; }

        [JsonPropertyName("email")]
        public string Email { get; set; }

        [Required]
        [JsonPropertyName("nascimento")]
        public DateTime Nascimento { get; set; }

    }

}
