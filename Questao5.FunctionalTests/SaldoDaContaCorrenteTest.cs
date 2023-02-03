using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Newtonsoft.Json;
using NSubstitute;
using Questao5.Application.Commands.Requests;
using Questao5.Application.Queries.Requests;
using Questao5.Application.Queries.Responses;
using Questao5.Domain.Entities;
using Questao5.Domain.Enumerators;
using Questao5.Domain.Helper;
using System.Drawing;
using System.Net;
using System.Text;

namespace Questao5.FunctionalTests
{
    public class SaldoDaContaCorrenteTest
    {
        /// <summary>
        /// Método Obtem o Saldo da Conta Corrente do cliente
        /// </summary>
        [Fact]
        public void GetSaldoContaCorrenteByIdHandler_ContaCorrenteValida_ObterSaldoComSucesso()
        {
            // arrange
            var application = new WebApplicationFactory<Program>();
            var client = application.CreateClient();

            var contaCorrenteQuery = Substitute.For<GetSaldoContaCorrenteByIdQuery>();
            contaCorrenteQuery.IdContaCorrente = "382D323D-7067-ED11-8866-7D5DFA4A16C9";

            string json = JsonConvert.SerializeObject(contaCorrenteQuery);

            // act
            var response = client.PostAsync("/SaldoContaCorrente/obtersaldo", new StringContent(json, UTF8Encoding.UTF8, "application/json")).Result;

            var responseContent = string.Empty;
            using (var content = response.Content) { responseContent += content.ReadAsStringAsync().Result; }
            var responseOK = JsonConvert.DeserializeObject<GetSaldoContaCorrenteByIdResponse>(responseContent);

            // assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(789, responseOK.Numero);
        }

        /// <summary>
        /// Método valida se a Conta Corrente possui cadastrado no sistema
        /// </summary>
        [Fact]
        public void GetSaldoContaCorrenteByIdHandler_ContaCorrenteInValida_ContaCorrenteNaoCadastrada()
        {
            var application = new WebApplicationFactory<Program>();
            var client = application.CreateClient();

            var contaCorrenteQuery = Substitute.For<GetSaldoContaCorrenteByIdQuery>();
            contaCorrenteQuery.IdContaCorrente = "E24AFC0F-6A37-4168-9F2C-CEEBEE28DA9A";

            string json = JsonConvert.SerializeObject(contaCorrenteQuery);

            Assert.ThrowsAsync<BusinessException>(() => client.PostAsync("/SaldoContaCorrente/obtersaldo", new StringContent(json, UTF8Encoding.UTF8, "application/json")));
        }

        /// <summary>
        /// Método valida se a Conta Corrente está Ativa no sistema
        /// </summary>
        [Fact]
        public void GetSaldoContaCorrenteByIdHandler_ContaCorrenteInvalida_ContaCorrenteInativa()
        {
            var application = new WebApplicationFactory<Program>();
            var client = application.CreateClient();

            var contaCorrenteQuery = Substitute.For<GetSaldoContaCorrenteByIdQuery>();
            contaCorrenteQuery.IdContaCorrente = "D2E02051-7067-ED11-94C0-835DFA4A16C9";

            string json = JsonConvert.SerializeObject(contaCorrenteQuery);

            Assert.ThrowsAsync<BusinessException>(() => client.PostAsync("/SaldoContaCorrente/obtersaldo", new StringContent(json, UTF8Encoding.UTF8, "application/json")));
        }
    }
}
