using System.ComponentModel.DataAnnotations.Schema;
namespace Contatos.Domain.Entity
{
    public class Contato : Entity
    {
        public string Nome { get; private set; }
        public DateTime DataNascimento { get; private set; }
        public char? Sexo { get; private set; }
        public bool Ativo { get; private set; }
        [NotMapped]
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

            return;
        }

        private static void Validar(string nome, DateTime dataNascimento, char? sexo)
        {
            if (string.IsNullOrEmpty(nome))
                throw new ArgumentNullException("O nome do contato não pode ser vazio");

            if (dataNascimento > DateTime.Now)
                throw new ArgumentException("A data não pode ser maior que a data atual");

            if (dataNascimento < DateTime.MinValue)
                throw new ArgumentException("Data de nascimento inválida");

            if (CalcularIdade(dataNascimento) < 18)
                throw new ArgumentException("O contato deve ser maior de idade");
        }


        private static int CalcularIdade(DateTime dataNascimento)
        {
            var hoje = DateTime.Now;
            var idade = hoje.Year - dataNascimento.Year;
            idade -= dataNascimento.Date > hoje.AddYears(-idade) ? 1 : 0;

            return idade;
        }

        public void AlterarStatus()
        {
            Ativo = !Ativo;
        }
    }
}
