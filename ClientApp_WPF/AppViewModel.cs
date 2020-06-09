using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Windows;
using System.Windows.Threading;
using ClassLibrary_Model;
using System.Threading.Tasks;

namespace ClientApp_WPF
{
    public class AppViewModel : BindableBase
    {
        private readonly IClient client = Client.GetClient;
        private readonly HttpClient httpClient = new HttpClient();
        private string currentFileName = null;
        private string selectedItem = null;

        public DataType DataType { get; set; } = DataType.Human;
        public ObservableCollection<string> ResultList { get; } = new ObservableCollection<string>();
        public ObservableCollection<string> HumanResponseList { get; set; } = new ObservableCollection<string>();
        public ObservableCollection<MathResponseItem> MathResponseList { get; set; } = new ObservableCollection<MathResponseItem>();
        public string CurrentFileName
        {
            get => currentFileName;
            set => SetProperty(ref currentFileName, value);
        }
        public string SelectedItem
        {
            get => selectedItem;
            set => SetProperty(ref selectedItem, value);
        }

        public AppViewModel()
        {
            httpClient.BaseAddress = new Uri("http://catalystcee-001-site1.htempurl.com/");
            //httpClient.BaseAddress = new Uri("https://localhost:44319/");
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        /// <summary>
        /// Обновление списка деталей из ответа
        /// </summary>
        public void SetResponseDetails()
        {
            if (DataType == DataType.Human)
            {
                HumanResponseList.Clear();
                foreach (string str in client.GetHumanValue(SelectedItem))
                    HumanResponseList.Add(str);
            }
            else
            {
                MathResponseList.Clear();
                foreach (var response in client.GetValue(SelectedItem))
                    MathResponseList.Add(response);
            }
        }

        /// <summary>
        /// Рисует детали в компасе
        /// </summary>
        /// <param name="data"></param>
        public void Draw()
        {
            string[] value = client.GetHumanValue(DataType != DataType.Human ? client.UnWrap(SelectedItem) : SelectedItem);
            // Среда .NET Core не позволяет использовать все возможности API компаса, 
            // поэтому запускается программа для рисования деталей в компасе, написанная на .NET Framework
            Process process = new Process();
            process.StartInfo.FileName = "MyDetails_ConsoleApp.exe";
            process.StartInfo.CreateNoWindow = true;
            foreach (string str in value) process.StartInfo.ArgumentList.Add(str);
            process.Start();
        }

        /// <summary>
        /// Запускает прослушивание веб-сервера
        /// </summary>
        public void Start()
        {
            GetData();
        }

        /// <summary>
        /// Перестраивает список ответов в соответствии с типом представления данных
        /// </summary>
        public void RebuildResponseList()
        {
            ResultList.Clear();
            if (DataType == DataType.Human)
                foreach (var str in client.Dictionary.Keys) ResultList.Insert(0, client.UnWrap(str));
            else foreach (var str in client.Dictionary.Keys) ResultList.Insert(0, str);
        }

        /// <summary>
        /// Асинхронная отправка запроса на веб-сервер
        /// </summary>
        /// <param name="request"></param>
        public async void SendRequest(string request)
        {
            try
            {
                if (DataType == DataType.Human) request = client.Wrap(request);
                await httpClient.PostAsJsonAsync("api/data", request.Split("\r\n") );
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Асинхронное получение ответов на запросы с веб-сервера
        /// </summary>
        private async void GetData()
        {
            Thread.CurrentThread.IsBackground = false;
            while (true)
            {
                try
                {
                    HttpResponseMessage response = await httpClient.GetAsync("api/data");
                    if (response.IsSuccessStatusCode)
                    {
                        var result = await response.Content.ReadAsAsync<KeyValuePair<string[], string[]>>();
                        if (client.Dictionary.TryAdd(result.Key[0], result.Value))
                        {
                            await Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Background, (SendOrPostCallback)delegate
                            {
                                if (DataType == DataType.Human) ResultList.Insert(0, client.UnWrap(result.Key[0]));
                                else ResultList.Insert(0, result.Key[0]);
                            }, null);
                        }
                    }
                    Thread.Sleep(10);
                }
                catch
                {
                    Thread.Sleep(3000);
                }
            }
        }
    }
}
