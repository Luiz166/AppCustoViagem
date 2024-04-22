using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AppCustoViagem
{
    public partial class MainPage : ContentPage
    {
        private App PropriedadesApp;

        public MainPage()
        {
            InitializeComponent();

            PropriedadesApp = (App)Application.Current;
        }

        private void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            try {
                Navigation.PushAsync(new ListaPedagios());
            } 
            catch (Exception ex) {
                DisplayAlert("Erro:", ex.Message ,"OK");
            }

        }

        private void Button_Clicked_Add(object sender, EventArgs e)
        {
            try {
                int NumeroPedagio = PropriedadesApp.ListaPedagios.Count;

                PropriedadesApp.ListaPedagios.Add(new Models.Pedagio()
                {
                    NumPedagio = NumeroPedagio + 1,
                    Valor = Convert.ToDecimal(txtValorPedagio.Text)
                });

                DisplayAlert("Concluido","Pedágio Adicionado", "OK");
                
                txtValorPedagio.Text = "";
            } catch (Exception ex)
            {
                DisplayAlert("Erro:", ex.Message, "OK");
            }
        }

        private void Button_Clicked_Calcular(object sender, EventArgs e)
        {
            try
            {
                decimal distancia = Convert.ToDecimal(txtDistancia.Text);
                decimal consumo = Convert.ToDecimal(txtConsumo.Text);
                decimal precoCombustivel = Convert.ToDecimal(txtValorCombustivel.Text);

                decimal gastoVeiculo = (distancia / consumo) * precoCombustivel;

                decimal custoPedagios = PropriedadesApp.ListaPedagios.Sum(item => item.Valor);

                decimal custoViagem = gastoVeiculo + custoPedagios;

                string resultado = string.Format(
                    "Sua viagem de {0} até {1} custará {2}",
                    txtOrigem.Text, txtDestino.Text, custoViagem.ToString("C"));

                DisplayAlert("Custo da Viagem:", resultado , "OK");
            }catch (Exception ex)
            {
                DisplayAlert("Erro:", ex.Message, "OK");
            }
        }

        private async void Button_Clicked_Limpar(object sender, EventArgs e) 
        {
            try
            {
                bool confirm = await DisplayAlert("Tem Certeza?", "Limpar todos os dados", "OK", "Cancelar");

                if (confirm)
                {
                    PropriedadesApp.ListaPedagios.Clear();

                    txtOrigem.Text = "";
                    txtDestino.Text = "";
                    txtDistancia.Text = "";
                    txtConsumo.Text = "";
                    txtValorCombustivel.Text = "";
                    txtValorPedagio.Text = "";
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Erro:", ex.Message, "OK");
            }
        }
    }
}
