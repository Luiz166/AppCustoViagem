using AppCustoViagem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppCustoViagem
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListaPedagios : ContentPage
    {
        private App PropriedadesApp;

        public ListaPedagios()
        {
            InitializeComponent();

            PropriedadesApp = (App)Application.Current;

            lst_lista_pedagios.ItemsSource = PropriedadesApp.ListaPedagios;

            verifica_lista_vazia();
        }

        private void verifica_lista_vazia()
        {
            if (PropriedadesApp.ListaPedagios.Count == 0)
            {
                apresentacao_itens.IsVisible = false;
                msm_lista_vazia.IsVisible = true;
            }
        }

        private async void MenuItem_Clicked(object sender, EventArgs e)
        {
            try
            {
                MenuItem disparador = sender as MenuItem;

                var pedagio_selecionado = (Pedagio)disparador.BindingContext;

                bool confirm = await DisplayAlert("Tem certeza?", "Remover Pedágio?", "Sim", "Não");

                if (confirm)
                {
                    PropriedadesApp.ListaPedagios.RemoveAll(i => i.NumPedagio == pedagio_selecionado.NumPedagio);

                    lst_lista_pedagios.ItemsSource = new List<Pedagio>();

                    lst_lista_pedagios.ItemsSource = PropriedadesApp.ListaPedagios;

                    verifica_lista_vazia();
                }
            }
            catch (Exception ex)
            {
                DisplayAlert("Erro:", ex.Message, "OK");
            }
        }

        private void Somar_Clicked(object sender, EventArgs e)
        {
            try
            {
                decimal custo_pedagios = PropriedadesApp.ListaPedagios.Sum(item => item.Valor);

                string mensagem = string.Format(
                    "Você gastará {0} de pedágios",
                    custo_pedagios.ToString("C")
                );

                DisplayAlert("Custo dos Pedágios", mensagem, "OK");
            }
            catch (Exception ex)
            {
                DisplayAlert("Erro:", ex.Message, "OK");
            }
        }
    }
}