using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Net.Http;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SellBroCRMWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string token = "";
        
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            LoginPostRequest();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            RegisterPostRequest();
        }

        private async void LoginPostRequest()
        {
            LoginUser loginUser = new LoginUser{Email = "Pepe228@gmail.com", Password = "228"};
            
            string json = JsonSerializer.Serialize(loginUser);
            var data = new StringContent(json, Encoding.UTF8, API.MediaType);
            
            using var client = new HttpClient();
            
            var response = await client.PostAsync(API.Login, data);
            
            var res = response.Content.ReadAsStringAsync();
            resp.Text = res.Result;
        }

        private async void RegisterPostRequest()
        {
            RegisterUser registerUser = new RegisterUser{Email = "Pepe228@gmail.com", Password = "228"};
            
            string json = JsonSerializer.Serialize(registerUser);
            var data = new StringContent(json, Encoding.UTF8, API.MediaType);
            
            using var client = new HttpClient();
            
            var response = await client.PostAsync(API.Register, data);
            
            var res = response.Content.ReadAsStringAsync();
            resp.Text = res.Result; 
        }

        private void ParceToken()
        {
            
        }
    }
}
