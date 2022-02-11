using AutoMapper;
using ClientManagerBackend.Aplicacao.DTOs;
using ClientManagerBackend.Dominio.Entidades;

namespace ClientManagerBackend.Aplicacao.Automapper
{
    public class DominioParaDTOMapping : Profile
    {
        public DominioParaDTOMapping()
        {
            CreateMap<Cliente, ClienteDTO>().ReverseMap();
        }

    }
}
