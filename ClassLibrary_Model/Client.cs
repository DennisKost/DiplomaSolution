using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace ClassLibrary_Model
{
    /// <summary>
    /// Бизнес-логика клиента, реализующая интерфейс IClient
    /// </summary>
    public class Client : IClient
    {
        private static IClient Сlient { get; } = new Client();
        public Dictionary<string, string[]> Dictionary { get; } = new Dictionary<string, string[]>();

        public Client() { }

        /// <summary>
        /// Возвращает единственный экземпляр класса Client (интерфейс IClient) в соответствии с паттерном Singleton
        /// </summary>
        /// <param name="window"></param>
        /// <returns></returns>
        public static IClient GetClient => Сlient;

        /// <summary>
        /// Разбор ответа и получение названий деталей на человеческом языке
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string[] GetHumanValue(string key)
        {
            // Задача разбора ответа и получение названий конкретных деталей пока не решена
            var result = new List<string>();
            //string[] value = Dictionary[Wrap(key)];
            result.Add("четырехугольник с непрямыми углами без отверстий");
            result.Add("четырехугольник с непрямыми углами с отверстием круглым в центре");
            result.Add("четырехугольник с непрямыми углами с отверстием некруглым в центре");
            result.Add("четырехугольник с непрямыми углами с отверстиями круглыми радиально и отверстием некруглым в центре");
            //result.Add("деталь с круглым и некруглым отверстиями");
            return result.ToArray();
        }

        /// <summary>
        /// Делит ответ на три состовляющих - общее, отличие запроса от детали и отличие детали от запроса
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public MathResponseItem[] GetValue(string key)
        {
            string[] value = Dictionary[key];
            string[] strArray = value[0].Split(' ');
            int count = Convert.ToInt32(strArray[1]);
            var mathResponses = new MathResponseItem[count];
            string[] resultArray = new string[count * 3].Select((item) => item = string.Empty).ToArray();
            int position = 1;
            for (int i = 0; i < count; i++)
            {
                string[] posArray = value[position++].Split(' ');
                int[] positions = { Convert.ToInt32(posArray[0]), Convert.ToInt32(posArray[1]), Convert.ToInt32(posArray[2]) };
                for (int k = position; k < position + positions[0]; k++)
                {
                    resultArray[i * 3] += value[k] + "\n";
                }
                position += positions[0];
                for (int k = position; k < position + positions[1]; k++)
                {
                    resultArray[i * 3 + 1] += value[k] + "\n";
                }
                position += positions[1];
                for (int k = position; k < position + positions[2]; k++)
                {
                    resultArray[i * 3 + 2] += value[k] + "\n";
                }
                position += positions[2];
                mathResponses[i] = new MathResponseItem()
                {
                    Common = resultArray[i * 3],
                    Diff1 = resultArray[i * 3 + 1],
                    Diff2 = resultArray[i * 3 + 2]
                };
                //int max = Math.Max(Math.Max(positions[0], positions[1]), positions[2]);
                //for (int j = 0; j < 3; j++)
                //{
                //    if (positions[j] < max)
                //    {
                //        for (int k = 0; k < max - positions[j]; k++) resultArray[i * 3 + j] += "\n";
                //    }
                //}
            }
            //return resultArray;
            return mathResponses;
        }

        /// <summary>
        /// Упаковка сообщения в математическую формулу
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public string Wrap(string data)
        {
            // Задача правильной упаковки запроса в математическую формулу пока не решена
            // Возвращается тривиальная упаковка
            return "(cnl(cnl all all) " + data + " контур)";
        }

        /// <summary>
        /// Распаковка сообщения из математической формулы
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public string UnWrap(string data)
        {
            // Задача правильной распаковки пока не решена
            // Возвращается результат с учетом тривиальной упаковки
            try
            {
                return Regex.Split(data, @"(\(cnl\(cnl all all\) )|( контур)")[2];
            }
            catch { return null; }
        }
    }
}
