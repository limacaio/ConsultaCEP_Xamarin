using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using ConsultarCEP.Servico;
using ConsultarCEP.Servico.Models;

namespace ConsultarCEP
{
    public partial class MainPage : ContentPage
    {
        string cep;
        public MainPage()
        {
            InitializeComponent();

            btnConsultar.Clicked += BuscarCEP;
        }


        private void BuscarCEP(object sender, EventArgs args)
        {

            //logica do app
            if (cep != null)
            {
                cep = etyCep.Text.Trim();
            }
            else
            {
                DisplayAlert("ERROR CRITICO", "INFORME O CEP", "OK");
            }


            if (ValidaCEP(cep))
            {
                try
                {
                    Endereco end = ViaCEPServico.BuscarEnderecoViaCEP(cep);

                    if (end != null)
                    {
                        lbResultado.Text = string.Format("Enderero: {0}, {1}-{2} \nBairro: {3} \nCod.IBGE: {4}", end.logradouro, end.localidade, end.uf, end.bairro, end.ibge);
                    }
                    else
                    {
                        DisplayAlert("ERRO", "Endereço não Encontrado para o CEP informado: " + cep, "OK");
                    }

                }
                catch (Exception e)
                {
                    DisplayAlert("ERROR CRITICO", e.Message, "OK");
                }
            }

        }

        //fazendo validações
        private bool ValidaCEP(string cep)
        {
            bool valido = true;
            int novoCEP = 0;

            if (cep.Length != 8 )
            {
                DisplayAlert("ERRO", "CEP Invalido! o CEP deve possuir 8 caracteres.", "OK");
                valido = false;
            }

            if (!int.TryParse(cep, out novoCEP))
            {
                DisplayAlert("ERRO", "CEP Invalido! Informe apenas numeros", "OK");
                valido = false;
            }
            return valido;
        }
    }
}
