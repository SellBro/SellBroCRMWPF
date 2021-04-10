using System;
using System.Net.Http;
using System.Net.Http.Headers;
using SellBroCRMWPF.Auth;

namespace SellBroCRMWPF.API
{
    public class Client
    {
        private static HttpClient _client;
        
        public static HttpClient GetInstance()
        {
            if (_client == null)
            {
                SetUpClient();
            }

            return _client;
        }

        private static void SetUpClient()
        {
            _client = new HttpClient();
            _client.BaseAddress = new Uri(Instance.BaseUrl);
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(Instance.MediaType));
        }

        public static void SetToken()
        {
            _client.DefaultRequestHeaders.Add("Authorization", AuthenticationUser.GetInstance().Token);
        }
        
    }
}