namespace Questao5.Application.Queries.Responses
{
    public class GetSaldoContaCorrenteByIdResponse
    {
        public int Numero { get; set; }
        public string Nome { get; set; }
        public string DataHoraResposta { get; set; }
        public decimal Saldo { get; set; }
    }
}
