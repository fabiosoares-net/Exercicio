using System;
using System.Drawing;
using System.Globalization;

namespace Questao1
{
    public class ContaBancaria 
    {
        public ContaBancaria(int numeroDaConta, string titular, double valor)
        {
            this.numeroDaconta = numeroDaConta;
            this.Titular = titular;

            Validar(valor);
            this.saldo = valor;
        }

        public ContaBancaria(int numeroDaConta, string titular)
        {
            this.numeroDaconta = numeroDaConta;
            this.Titular = titular;
        }

        private int numeroDaconta;
        public int NumeroDaconta 
        { 
            get { return numeroDaconta; }
        }
        public string Titular { get; set; }
        private double saldo;
        public double Saldo { get { return saldo; } }
        private double taxa = 3.50;


        public void Deposito(double valor)
        {
            Validar(valor);

            this.saldo += valor;
        }

        public void Saque(double valor)
        {
            Validar(valor); 

            this.saldo -= valor;
            this.saldo = (this.saldo - taxa);
        }

        private void Validar(double valor)
        {
            if (valor < 0)
            {
                throw new ArgumentException("O valor não pode ser menor que zero");
            }
        }
    }
}
