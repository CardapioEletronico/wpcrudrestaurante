using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using CRUDRestaurante.Resources;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;


namespace CRUDRestaurante
{
    public partial class MainPage : PhoneApplicationPage
    {
       
        public MainPage()
        {
            
            InitializeComponent();

        }
        private string ip = "http://10.21.0.137";

        public object JsonConvert { get; private set; }

        private async void buttonSelect_Click(object sender, RoutedEventArgs e)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(ip);
            var response = await httpClient.GetAsync("/20131011110061/api/restaurante");

             var str = response.Content.ReadAsStringAsync().Result;
            
             List<Restaurante> obj = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Restaurante>>(str);

             foreach (var cu in obj)
            {
                TextBlock txt = new TextBlock();
                txt.Text = "";
                txt.Text = cu.Id.ToString() + " - " + cu.Descricao.ToString();
                Stack.Children.Add(txt);      
            }
        }

        private async void  buttonInsert_Click(object sender, RoutedEventArgs e)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(ip);
            Restaurante f = new Restaurante
            {
                Id = int.Parse(textBoxId.Text),
                Descricao = textBoxDesc.Text
            };
            List<Restaurante> fl = new List<Restaurante>();
            fl.Add(f);
            string s = "=" + Newtonsoft.Json.JsonConvert.SerializeObject(fl);

            var content = new StringContent(s, Encoding.UTF8,"application/x-www-form-urlencoded");

            await httpClient.PostAsync("/20131011110061/api/restaurante", content);

        }

        private async void  buttonUpdate_Click(object sender, RoutedEventArgs e)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(ip);

            Restaurante f = new Restaurante
            {
                Id = int.Parse(textBoxId.Text),
                Descricao = textBoxDesc.Text
            };

            string s = "=" + Newtonsoft.Json.JsonConvert.SerializeObject(f);

            var Stack = new StringContent(s, Encoding.UTF8, "application/x-www-form-urlencoded");

            await httpClient.PutAsync("/20131011110061/api/restaurante/" + f.Id, Stack);
        }

        private async void  buttonDelete_Click(object sender, RoutedEventArgs e)
        {

            HttpClient httpClient = new HttpClient();

            httpClient.BaseAddress = new Uri(ip);

            await httpClient.DeleteAsync("/20131011110061/api/restaurante/" + textBoxId.Text);

        }
    }
}