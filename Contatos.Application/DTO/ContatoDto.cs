namespace Contatos.Application.Dto
{
    public class ContatoDto
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public DateTime DataNascimento { get; set; }
        public int Idade { get; set; }
        public bool Ativo { get; set; }

    }
}
