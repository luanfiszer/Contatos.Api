using System.ComponentModel.DataAnnotations.Schema;
namespace Contatos.Domain.Entity
{
    public class Contato : Entity
    {
        public string Nome { get; private set; }
        public DateTime DataNascimento { get; private set; }
        public char? Sexo { get; private set; }
        public bool Ativo { get; private set; }
        public int Idade => CalcularIdade(DataNascimento);

        protected Contato() { }

        private Contato(
            string nome,
            DateTime dataNascimento,
            char? sexo)
        {
            Nome = nome;
            DataNascimento = dataNascimento;
            Sexo = sexo;
            Ativo = true;
        }

        public static Contato Criar(string nome, DateTime dataNascimento, char? sexo)
        {
            Validar(nome, dataNascimento, sexo);
            var contato = new Contato(nome, dataNascimento, sexo);
            return contato;
        }

        public void Atualizar(string nome, DateTime dataNascimento, char? sexo)
        {
            Validar(nome, dataNascimento, sexo);

            Nome = nome;
            DataNascimento = dataNascimento;
            Sexo = sexo;
        }

        private static void Validar(string nome, DateTime dataNascimento, char? sexo)
        {
            if (string.IsNullOrWhiteSpace(nome))
                throw new ArgumentException("O nome do contato não pode ser vazio.");

            if (dataNascimento > DateTime.Today)
                throw new ArgumentException("A data de nascimento não pode ser maior que a data atual.");

            var idade = CalcularIdade(dataNascimento);

            if (idade == 0)
                throw new ArgumentException("A idade não pode ser igual a 0.");

            if (idade < 18)
                throw new ArgumentException("O contato deve ser maior de idade (>= 18).");

            if (sexo.HasValue && sexo != 'M' && sexo != 'F')
                throw new ArgumentException("Sexo deve ser 'M', 'F' ou nulo.");
        }

        private static int CalcularIdade(DateTime dataNascimento)
        {
            var hoje = DateTime.Now;
            var idade = hoje.Year - dataNascimento.Year;
            if (dataNascimento.Date > hoje.AddYears(-idade))
            {
                idade -= 1;
            }

            return idade;
        }

        public void AlterarStatus()
        {
            Ativo = !Ativo;
        }
    }
}
