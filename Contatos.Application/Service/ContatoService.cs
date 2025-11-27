using AutoMapper;
using Contatos.Application.Dto;
using Contatos.Application.Service.Interface;
using Contatos.Domain.Entity;
using Contatos.Domain.Interface;

namespace Contatos.Application.Service
{
    public class ContatoService : IContatoService
    {
        private readonly IContatoRepository _repo;
        private readonly IMapper _mapper;

        public ContatoService(IContatoRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<ContatoDto> CriarAsync(ContatoCreateDto dto)
        {
            var contato = Contato.Criar(dto.Nome, dto.DataNascimento, dto.Sexo);

            await _repo.AdicionarAsync(contato);
            await _repo.SalvarAsync();

            return _mapper.Map<ContatoDto>(contato);
        }

        public async Task<IEnumerable<ContatoDto>> ObterTodosAsync()
        {
            var contatos = await _repo.ObterAtivosAsync();
            return _mapper.Map<IEnumerable<ContatoDto>>(contatos);
        }

        public async Task<ContatoDto> ObterPorIdAsync(Guid id)
        {
            var contato = await _repo.ObterAtivoPorIdAsync(id);

            if (contato == null || !contato.Ativo)
                throw new KeyNotFoundException("Contato não encontrado ou está inativo.");

            return _mapper.Map<ContatoDto>(contato);
        }

        public async Task AtualizarAsync(Guid id, ContatoUpdateDto dto)
        {
            var contato = await _repo.ObterAtivoPorIdAsync(id);

            if (contato == null)
                throw new KeyNotFoundException("Contato não encontrado.");

            contato.Atualizar(dto.Nome, dto.DataNascimento, dto.Sexo);

            await _repo.AtualizarAsync(contato);
            await _repo.SalvarAsync();
        }

        public async Task AlterarStatusAsync(Guid id)
        {
            var contato = await _repo.ObterAtivoPorIdAsync(id);

            if (contato == null)
                throw new KeyNotFoundException("Contato não encontrado.");

            contato.AlterarStatus();

            await _repo.AtualizarAsync(contato);
        }

        public async Task RemoverAsync(Guid id)
        {
            var contato = await _repo.ObterAtivoPorIdAsync(id);

            if (contato == null)
                throw new KeyNotFoundException("Contato não encontrado.");

            await _repo.RemoverAsync(id);
            await _repo.SalvarAsync();
        }


    }
}
