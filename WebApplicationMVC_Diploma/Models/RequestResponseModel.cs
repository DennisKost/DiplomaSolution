using System.Collections.Generic;

namespace WebApplicationMVC_Diploma.Models
{
    public class RequestResponseModel : IRequestResponseModel
    {
        private int position = 0;
        private readonly Dictionary<string[], string[]> dictionary = new Dictionary<string[], string[]>();
        private readonly Queue<string[]> requestList = new Queue<string[]>();
        private readonly Queue<KeyValuePair<string[], string[]>> responseList = new Queue<KeyValuePair<string[], string[]>>();

        public string SearchTerm { get; set ; }

        public List<string> ResponseList { get; set; }

        /// <summary>
        /// Получение запроса(если есть), метод предназначен для сервера
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        public bool GetRequest(out string[] result)
        {
            if (requestList.TryDequeue(out var res))
            {
                result = res;
                return true;
            }
            result = null;
            return false;
        }

        /// <summary>
        /// Добавление результата в ответ на запрос, метод предназначен для сервера
        /// </summary>
        /// <param name="message"></param>
        public void AddResult(KeyValuePair<string[], string[]> message){
            if (position >= 1000000)
            {
                position = 0;
                dictionary.Clear();
                //sqlModel.Clear();
            }
            dictionary.Add(message.Key, message.Value);
            //string key = string.Empty, value = string.Empty;
            //for (int i = 0; i < message.Key.Length; i++)
            //{
            //    key += message.Key[i];
            //    if (i == message.Key.Length - 1) key += "\r\n";
            //}
            //for (int i = 0; i < message.Value.Length; i++)
            //{
            //    key += message.Value[i];
            //    if (i == message.Value.Length - 1) value += "\r\n";
            //}
            //sqlModel.AddResult(new Dictionary { Key = key, Value = value });
            //sqlModel.Commit();
            responseList.Enqueue(new KeyValuePair<string[], string[]>(message.Key, message.Value));
            position++;
        }

        /// <summary>
        /// Добавление запроса в очередь для обработки на сервере, метод предназначен для клиента
        /// </summary>
        /// <param name="data"></param>
        public void AddRequest(string[] data){
            requestList.Enqueue(data);
        }

        /// <summary>
        /// Получение результата(если есть) от сервера, метод предназначен для клиента
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        public bool GetResult(out KeyValuePair<string[], string[]> result){
            if (responseList.TryDequeue(out var res))
            {
                result = res;
                return true;
            }
            result = res;
            return false;
        }

        public int Commit()
        {
            return 0;
        }
    }
}
