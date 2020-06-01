﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace WebApp_NetCore.Models
{
    public class RequestResponseModel : IRequestResponseModel
    {
        private int position = 0;
        private readonly Dictionary<string[], string[]> dictionary = new Dictionary<string[], string[]>();
        private readonly Queue<string[]> requestList = new Queue<string[]>();
        private readonly Queue<KeyValuePair<string[], string[]>> responseList = new Queue<KeyValuePair<string[], string[]>>();

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
            }
            dictionary.Add(message.Key, message.Value);
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
    }
}
