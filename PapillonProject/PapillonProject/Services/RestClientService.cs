using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using static System.Net.WebRequestMethods;
using PapillonProject.Models;
using System.Net;

namespace PapillonProject.Services
{
    public class RestClientService : IRestClientService
    {
        private const string _url = "http://daviddurand.info/D228/papillons/";
        HttpClient client;
        JsonSerializerOptions serializerOptions;
        CookieContainer cookies;
        HttpClientHandler handler;

        public RestClientService()
        {
            cookies = new CookieContainer();
            handler = new HttpClientHandler();
            handler.CookieContainer = cookies;

            client = new HttpClient(handler);

            serializerOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                WriteIndented = true
            };
        }

        public async Task<List<T>> GetItems<T>(string endpoint)
        {
            var Items = new List<T>();

            Uri uri = new Uri(new Uri(_url), endpoint);
            try
            {
                HttpResponseMessage response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    Items = JsonSerializer.Deserialize<List<T>>(content, serializerOptions);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }

            return Items;
        }

        public async Task<object> GetItem(string endpoint)
        {
            var item = new object();

            Uri uri = new Uri(new Uri(_url), endpoint);
            try
            {
                HttpResponseMessage response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    item = JsonSerializer.Deserialize<object>(content, serializerOptions);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }

            return item;
        }

        public async Task<bool> PostItem<T>(T item, string endpoint)
        {
            Uri uri = new Uri(new Uri(_url), endpoint);

            try
            {
                string json = JsonSerializer.Serialize<T>(item, serializerOptions);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = null;

                response = await client.PostAsync(uri, content);

                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine(@"\tTodoItem successfully saved.");
                }

                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }

            return false;
        }

        public async Task<bool> Post(string endpoint)
        {
            // ServicePointManager.ServerCertificateValidationCallback = (sender, certificate, chain, sslPolicy) => { return true; };

            Uri uri = new Uri(new Uri(_url), endpoint);

            try
            {
                HttpResponseMessage response = null;

                response = await client.PostAsync(uri, new StringContent("", Encoding.UTF8, "application/json"));

                if (!response.IsSuccessStatusCode)
                {
                    Debug.Write(response.Content);
                }

                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
                return false;
            }
        }

        public async Task<string> GetRawResponse(string endpoint)
        {
            Uri uri = new Uri(new Uri(_url), endpoint);

            string content = null;

            try
            {
                HttpResponseMessage response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    content = await response.Content.ReadAsStringAsync();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }

            return content;
        }
    }
}