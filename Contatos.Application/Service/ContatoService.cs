using AutoMapper;
using Contatos.Application.Dto;
using Contatos.Application.Service.Interface;
using Contatos.Domain.Entity;
using Contatos.Domain.Interface;

namespace Contatos.Application.Service
{
    public class ContatoService : IContatoService
    {
        private readonly IContatoRepository _contatoRepository;
        private readonly IMapper _mapper;

        public ContatoService(IContatoRepository contatoRepository, IMapper mapper)
        {
            _contatoRepository = contatoRepository;
            _mapper = mapper;
        }

        public async Task<ContatoDto> CriarAsync(ContatoCreateDto dto)
        {
            var contato = Contato.Criar(dto.Nome, dto.DataNascimento, dto.Sexo);

            await _contatoRepository.Adicionar(contato);
            await _contatoRepository.SalvarAsync();

            return _mapper.Map<ContatoDto>(contato);
        }

        public async Task<IEnumerable<ContatoDto>> ListarAsync()
        {
            var contatos = await _contatoRepository.ObterTodosAsync();

            if (contatos == null)
                throw new KeyNotFoundException("Contatos não encontrados.");

            return _mapper.Map<IEnumerable<ContatoDto>>(contatos);
        }

        public async Task<ContatoDto> ObterPorIdAsync(Guid id)
        {
            var contato = await _contatoRepository.ObterPorIdAsync(id);

            if (contato == null || !contato.Ativo)
                throw new KeyNotFoundException("Contato não encontrado ou está inativo.");

            return _mapper.Map<ContatoDto>(contato);
        }

        public async Task AtualizarAsync(Guid id, ContatoUpdateDto dto)
        {
            var contato = await _contatoRepository.ObterPorIdAsync(id);

            if (contato == null)
                throw new KeyNotFoundException("Contato não encontrado.");

            contato.Atualizar(dto.Nome, dto.DataNascimento, dto.Sexo);

            await _contatoRepository.SalvarAsync();
        }

        public async Task AlterarStatusAsync(Guid id)
        {
            var contato = await _contatoRepository.ObterPorIdAsync(id);

            if (contato == null)
                throw new KeyNotFoundException("Contato não encontrado.");

            contato.AlterarStatus();

            await _contatoRepository.SalvarAsync();
        }

        public async Task RemoverAsync(Guid id)
        {
            var contato = await _contatoRepository.ObterPorIdAsync(id);

            if (contato == null)
                throw new KeyNotFoundException("Contato não encontrado.");

            await _contatoRepository.RemoverAsync(contato);
            await _contatoRepository.SalvarAsync();
        }


    }
}
