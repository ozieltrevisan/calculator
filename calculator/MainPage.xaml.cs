using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace calculator
{
    public partial class MainPage : ContentPage
    {
        private int numeroAtual = 1;
        private double valor1 = 1;
        private bool podeOperar = true;
        private bool negativo = true;
        private List<double> proximoValor = new List<double>();
        private List<char> operacoes = new List<char>();

        public MainPage()
        {
            InitializeComponent();
        }
        private void NumeroClicado(object sender, EventArgs e)
        {
            Button numero = (Button)sender;

            if (Resultado.Text == "0") Resultado.Text = "";
            if (numeroAtual < 0) numeroAtual *= -1;

            Resultado.Text += numero.Text;
            Resultado.TextColor = Color.Black;
            if (numeroAtual == 1)
            {
                if (double.TryParse(Resultado.Text, out double valor))
                {
                    Resultado.Text = valor.ToString();
                    valor1 = valor;

                }
            }
            else
            {
                int valores = valor1.ToString().Length + 1;
                if (proximoValor.Count > 1)
                {
                    for (int i = 0; i < proximoValor.Count - 1; i++)
                    {
                        valores += proximoValor[i].ToString().Length + 1;
                    }
                }
                string proximovalor = Resultado.Text.Remove(0, valores);
                if (double.TryParse(proximovalor, out double valor))
                {
                    if (operacoes.Count == proximoValor.Count)
                    {
                        proximoValor[proximoValor.Count - 1] = valor;
                        podeOperar = true;
                    }
                }

            }


        }
        private void OperadrClicado(object sender, EventArgs e)
        {
            Button operador = (Button)sender;
            if (valor1 != 0 && podeOperar || operador.Text.Equals("-") && negativo)
            {
                if (valor1 == 0 || Resultado.Text == "")
                {
                    Resultado.Text = "=";
                    Resultado.TextColor = Color.Black;
                    return;
                }
                if (proximoValor.Count > 0 && negativo && operador.Text.Equals("-") && proximoValor[proximoValor.Count - 1] == 0)


                {
                    numeroAtual = -2;
                    Resultado.Text += operador.Text;
                    podeOperar = false;
                    negativo = false;
                    return;
                }
                else
                {
                    negativo = true;
                    proximoValor.Add(0);
                    numeroAtual = -2;
                    operacoes.Add(operador.Text[index: 0]);
                    Resultado.Text += operador.Text;
                    podeOperar = false;
                }

            }

        }
        private void clear(object sender, EventArgs e)
        {
            Resultado.Text = "";
            numeroAtual = 1;
            valor1 = 0;
            negativo = true;
            podeOperar = true;
            proximoValor.Clear();
            operacoes.Clear();
        }

        private void Calcular(object sender, EventArgs e)
        {
            if (numeroAtual == 2)
            {
                double resultado = 0;
                int i = 0;
                foreach (char operador in operacoes)
                {
                    if (operador == 'x')
                    {
                        resultado = valor1 * proximoValor[i];
                    }
                    else if (operador == '-')
                    {
                        resultado = valor1 - proximoValor[i];
                    }
                    else if (operador == '+')
                    {
                        resultado = valor1 + proximoValor[i];
                    }
                    else if (operador == '/')
                    {
                        resultado = valor1 / proximoValor[i];
                    }
                    else if (operador == '%')
                    {
                        resultado = valor1 * proximoValor[i] /100;
                    }

                    valor1 = resultado;

                    if (proximoValor.Count > 0 && i < proximoValor.Count - 1) i++;

                }
                if (resultado == 0) numeroAtual = 1;
                operacoes.Clear();
                proximoValor.Clear();
                Resultado.Text = resultado.ToString();




            }
        }

    }
}
