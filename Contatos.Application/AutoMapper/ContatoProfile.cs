using AutoMapper;
using Contatos.Application.Dto;
using Contatos.Domain.Entity;

namespace Contatos.Application.AutoMapper
{
    public class ContatoProfile : Profile
    {
        public ContatoProfile()
        {
            CreateMap<Contato, ContatoDto>()
                .ForMember(dest => dest.Idade, opt => opt.MapFrom(src => src.Idade));

            CreateMap<ContatoCreateDto, Contato>()
                .ConstructUsing(dto => Contato.Criar(dto.Nome, dto.DataNascimento, dto.Sexo));

            CreateMap<ContatoUpdateDto, Contato>()
                .AfterMap((dto, entity) =>
                {
                    entity.Atualizar(dto.Nome, dto.DataNascimento, dto.Sexo);
                });
        }
    }
}
