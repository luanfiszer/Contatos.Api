using Contatos.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contatos.Test
{
    public class ContatoTests
    {
        [Fact]
        public void CriarContato_DeveGerarIdadeCorreta()
        {
            var nascimento = DateTime.Today.AddYears(-20);

            var contato = Contato.Criar("João", nascimento, 'M');

            Assert.Equal(20, contato.Idade);
        }

        [Fact]
        public void CriarContato_ComMenorDeIdade_DeveLancarErro()
        {
            var nascimento = DateTime.Today.AddYears(-17);

            var exception = Assert.Throws<ArgumentException>(() =>
                Contato.Criar("Maria", nascimento, 'F')
            );
            Assert.Equal("O contato deve ser maior de idade (>= 18).", exception.Message);
        }

        [Fact]
        public void CriarContato_ComIdadeIgualZero_DeveLancarErro()
        {
            var nascimento = DateTime.Today;

            var exception = Assert.Throws<ArgumentException>(() =>
                Contato.Criar("Carlos", nascimento, 'M')
            );
            Assert.Equal("A idade não pode ser igual a 0.", exception.Message);
        }

        [Fact]
        public void CriarContato_ComDataFutura_DeveLancarErro()
        {
            // Arrange
            var nascimento = DateTime.Today.AddDays(1);

            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() =>
                Contato.Criar("Ana", nascimento, 'F')
            );

            Assert.Equal("A data de nascimento não pode ser maior que a data atual.", exception.Message);
        }


        [Fact]
        public void CriarContato_ComNomeVazio_DeveLancarErro()
        {
            var nascimento = DateTime.Today.AddYears(-30);

            var exception = Assert.Throws<ArgumentException>(() =>
                Contato.Criar("", nascimento, 'M')
            );
            Assert.Equal("O nome do contato não pode ser vazio.", exception.Message);
        }

        [Fact]
        public void CriarContato_ComSexoInvalido_DeveLancarErro()
        {
            var nascimento = DateTime.Today.AddYears(-20);

            var exception = Assert.Throws<ArgumentException>(() =>
                Contato.Criar("Juliana", nascimento, 'X')
            );
            Assert.Equal("Sexo deve ser 'M', 'F' ou nulo.", exception.Message);

        }

        [Fact]
        public void AtualizarContato_ComDadosValidos_DeveAtualizar()
        {
            var nascimento = DateTime.Today.AddYears(-25);
            var contato = Contato.Criar("João", nascimento, 'M');

            contato.Atualizar("João Silva", nascimento.AddYears(-1), 'M');

            Assert.Equal("João Silva", contato.Nome);
            Assert.Equal(26, contato.Idade);
        }

        [Fact]
        public void AlterarStatus_DeveAlternarAtivo()
        {
            var contato = Contato.Criar("Lucas", DateTime.Today.AddYears(-30), 'M');

            contato.AlterarStatus();
            var primeiro = contato.Ativo;

            contato.AlterarStatus();
            var segundo = contato.Ativo;

            Assert.False(primeiro);
            Assert.True(segundo);
        }
    }
}
