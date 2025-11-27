namespace Contatos.Application.Dto
{
    public class ContatoCreateDto
    {
        public string Nome { get; set; }
        public DateTime DataNascimento { get; set; }
        public char? Sexo { get; set; }
    }
}
