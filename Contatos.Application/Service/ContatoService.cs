using AutoMapper;
using Contatos.Api.Results;
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

        public async Task<Result<IEnumerable<ContatoDto>>> ListarAsync()
        {
            var contatos = await _contatoRepository.ObterTodosAsync(c => c.Ativo);

            if (contatos.Count() == 0)
                return Result<IEnumerable<ContatoDto>>.NotFound("Contatos não encontrados.");

            return Result<IEnumerable<ContatoDto>>.Ok(_mapper.Map<IEnumerable<ContatoDto>>(contatos));
        }

        public async Task<Result<ContatoDto>> ObterPorIdAsync(Guid id)
        {
            var contato = await _contatoRepository.ObterAsync(c => c.Id == id && c.Ativo);

            if (contato == null)
                return Result<ContatoDto>.NotFound("Contato não encontrado.");

            return Result<ContatoDto>.Ok(_mapper.Map<ContatoDto>(contato));
        }

        public async Task<Result<ContatoDto>> CriarAsync(ContatoRequestDto dto)
        {
            try
            {
                var contato = Contato.Criar(dto.Nome, dto.DataNascimento, dto.Sexo);

                await _contatoRepository.Adicionar(contato);
                await _contatoRepository.SalvarAsync();

                return Result<ContatoDto>.Ok(_mapper.Map<ContatoDto>(contato));
            }
            catch (Exception ex)
            {
                return Result<ContatoDto>.BusinessError(ex.Message);
            }
        }

        public async Task<Result> AtualizarAsync(Guid id, ContatoRequestDto dto)
        {
            try
            {
                var contato = await _contatoRepository.ObterAsync(c => c.Id == id && c.Ativo);

                if (contato == null)
                    return Result<ContatoDto>.NotFound("Contato não encontrado.");

                contato.Atualizar(dto.Nome, dto.DataNascimento, dto.Sexo);

                await _contatoRepository.SalvarAsync();

                return Result.Ok("Contato atualizado com sucesso.");
            }
            catch(Exception ex)
            {
                return Result.BusinessError(ex.Message);
            }
           
        }

        public async Task<Result> AlterarStatusAsync(Guid id)
        {
            var contato = await _contatoRepository.ObterAsync(c => c.Id == id);

            if (contato == null)
                return Result<ContatoDto>.NotFound("Contato não encontrado.");

            contato.AlterarStatus();

            await _contatoRepository.SalvarAsync();

            return Result.Ok($"Status atualizado para {contato.Ativo}.");
        }

        public async Task<Result> RemoverAsync(Guid id)
        {
            var contato = await _contatoRepository.ObterAsync(c => c.Id == id && c.Ativo);

            if (contato == null)
                return Result<ContatoDto>.NotFound("Contato não encontrado.");

            await _contatoRepository.RemoverAsync(contato);
            await _contatoRepository.SalvarAsync();

            return Result.Ok("Contato excluido com sucesso.");
        }


    }
}
