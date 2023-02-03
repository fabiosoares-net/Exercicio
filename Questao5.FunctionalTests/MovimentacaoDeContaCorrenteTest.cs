using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Newtonsoft.Json;
using NSubstitute;
using Questao5.Application.Commands.Requests;
using Questao5.Application.Queries.Requests;
using Questao5.Domain.Entities;
using Questao5.Domain.Enumerators;
using Questao5.Domain.Helper;
using System.Drawing;
using System.Net;
using System.Text;

namespace Questao5.FunctionalTests
{
    public class MovimentacaoDeContaCorrenteTest
    {
        /// <summary>
        /// Método cria uma Movimentação na conta corrente
        /// </summary>
        [Fact]
        public void CreateMovimentoHandler_MovimentoValido_MovimentoCriadoComSucesso()
        {
            var application = new WebApplicationFactory<Program>();
            var client = application.CreateClient();

            var movimentoCommand = Substitute.For<CreateMovimentoCommand>();
            movimentoCommand.IdRequisicao = Guid.NewGuid().ToString();
            movimentoCommand.IdContaCorrente = "382D323D-7067-ED11-8866-7D5DFA4A16C9";
            movimentoCommand.Valor = 550;
            movimentoCommand.TipoMovimento = 'C';

            string json = JsonConvert.SerializeObject(movimentoCommand);

            var response = client.PostAsync("/Movimento/criar", new StringContent(json, UTF8Encoding.UTF8, "application/json")).Result;

            var responseContent = string.Empty;
            using (var content = response.Content) { responseContent += content.ReadAsStringAsync().Result; }

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        /// <summary>
        /// Método valida se a conta corrente está cadastrada no sistema
        /// </summary>
        [Fact]
        public void CreateMovimentoHandler_MovimentoInvalido_ContaCorrenteNaoCadastrada()
        {
            var application = new WebApplicationFactory<Program>();
            var client = application.CreateClient();

            var movimentoCommand = Substitute.For<CreateMovimentoCommand>();
            movimentoCommand.IdRequisicao = Guid.NewGuid().ToString();
            movimentoCommand.IdContaCorrente = "39F1808F-441F-4CCD-B2EC-343A8671E673";
            movimentoCommand.Valor = 550;
            movimentoCommand.TipoMovimento = 'C';

            string json = JsonConvert.SerializeObject(movimentoCommand);

            Assert.ThrowsAsync<BusinessException>(() => client.PostAsync("/Movimento/criar", new StringContent(json, UTF8Encoding.UTF8, "application/json")));
        }

        /// <summary>
        /// Método valida se a Conta Corrente se encontra Ativa no sistema
        /// </summary>
        [Fact]
        public void CreateMovimentoHandler_MovimentoInvalido_ContaCorrenteInativa()
        {
            var application = new WebApplicationFactory<Program>();
            var client = application.CreateClient();

            var movimentoCommand = Substitute.For<CreateMovimentoCommand>();
            movimentoCommand.IdRequisicao = Guid.NewGuid().ToString();
            movimentoCommand.IdContaCorrente = "D2E02051-7067-ED11-94C0-835DFA4A16C9";
            movimentoCommand.Valor = 550;
            movimentoCommand.TipoMovimento = 'C';

            string json = JsonConvert.SerializeObject(movimentoCommand);

            Assert.ThrowsAsync<BusinessException>(() => client.PostAsync("/Movimento/criar", new StringContent(json, UTF8Encoding.UTF8, "application/json")));
        }

        /// <summary>
        /// Método valida se o valor colocado é Negativo
        /// </summary>
        [Fact]
        public void CreateMovimentoHandler_MovimentoInvalido_ValorNegativoOuInvalido()
        {
            var application = new WebApplicationFactory<Program>();
            var client = application.CreateClient();

            var movimentoCommand = Substitute.For<CreateMovimentoCommand>();
            movimentoCommand.IdRequisicao = Guid.NewGuid().ToString();
            movimentoCommand.IdContaCorrente = "382D323D-7067-ED11-8866-7D5DFA4A16C9";
            movimentoCommand.Valor = -550;
            movimentoCommand.TipoMovimento = 'C';

            string json = JsonConvert.SerializeObject(movimentoCommand);

            Assert.ThrowsAsync<BusinessException>(() => client.PostAsync("/Movimento/criar", new StringContent(json, UTF8Encoding.UTF8, "application/json")));
        }

        /// <summary>
        /// Método valida se o Tipo da Movimentação é invalido
        /// </summary>
        [Fact]
        public void CreateMovimentoHandler_MovimentoInvalido_TipoMovimentacaoInvalida()
        {
            var application = new WebApplicationFactory<Program>();
            var client = application.CreateClient();

            var movimentoCommand = Substitute.For<CreateMovimentoCommand>();
            movimentoCommand.IdRequisicao = Guid.NewGuid().ToString();
            movimentoCommand.IdContaCorrente = "382D323D-7067-ED11-8866-7D5DFA4A16C9";
            movimentoCommand.Valor = 550;
            movimentoCommand.TipoMovimento = 'E';

            string json = JsonConvert.SerializeObject(movimentoCommand);

            Assert.ThrowsAsync<BusinessException>(() => client.PostAsync("/Movimento/criar", new StringContent(json, UTF8Encoding.UTF8, "application/json")));
        }
    }
}
