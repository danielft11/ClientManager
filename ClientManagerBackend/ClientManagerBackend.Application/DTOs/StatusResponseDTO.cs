using System.Text.Json.Serialization;

namespace ClientManagerBackend.Aplicacao.DTOs
{
    public class StatusResponseDTO
    {
        [JsonPropertyName("status")]
        public string Status { get; set; }

        [JsonPropertyName("mensagem")]
        public string Mensagem { get; set; }
    }
}
