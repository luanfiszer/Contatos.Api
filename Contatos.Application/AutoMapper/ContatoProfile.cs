using AutoMapper;
using Contatos.Application.Dto;
using Contatos.Domain.Entity;

namespace Contatos.Application.AutoMapper
{
    public class ContatoProfile : Profile
    {
        public ContatoProfile()
        {
            CreateMap<Contato, ContatoDto>();
        }
    }
}
