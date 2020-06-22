using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;

namespace LibraryServer
{
    public delegate string[] CallBack(string[] data);  

    public class Server
    {        
        public async void Start(CallBack call)
        {
            HttpClient client = new HttpClient
            {
                BaseAddress = new Uri("http://catalystcee-001-site1.htempurl.com/")
                //BaseAddress = new Uri("https://localhost:44319/")
            };
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            while (true)
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync("api");
                    if (!response.IsSuccessStatusCode)
                    {
                        Thread.Sleep(10);
                        continue;
                    }
                    string[] data = await response.Content.ReadAsAsync<string[]>();
                    string[] result = call.Invoke(data);
                    var pair = new KeyValuePair<string[], string[]>(data, result);
                    await client.PostAsJsonAsync("api", pair);
                }
                catch{
                    Thread.Sleep(3000);
                }                
            }
        }
    }
}