using System;
using System.Collections.Generic;
//using System.Net.Http;
//using System.Net.Http.Headers;
using LibraryServer;
using System.IO;

namespace ChatApp
{
    /// <summary>
    /// Имитация обработки запроса сервером
    /// </summary>
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            //Запускаем функцию в библиотеке, передавая делегат
            var adaprer = new Server();
            adaprer.Start(Report);
            Console.ReadKey();
        }

        /// <summary>
        /// Функция обратного вызова,
        /// которая реализует делегат, определенный в библиотеке
        /// Метод должен возвращать результат обработки строк data, 
        /// обращаясь к некоторой логике
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private static string[] Report(string[] data)
        {
            return SomeLogic(data);
        }

        /// <summary>
        /// Функция некоторой логики обработки данных
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private static string[] SomeLogic(string[] data)
        {
            var result = new List<string>();
            using (var reader = new StreamReader("answer.txt")){
                while (!reader.EndOfStream) result.Add(reader.ReadLine());
            }
            return result.ToArray(); 
            return new string[] {"объектов_выборки= 2",
                                "3 2 3",
                                "<cnl>",
                                "(",
                                "         <cnl>",
                                "                   <obj>. all",
                                "            +",
                                "                   <pred>. all",
                                "            +",
                                "                   <sub>. отсутствие_отверстия",
                                "1 2 1",
                                "lalalalalalalalalalalalalalalalalalalalalalalalalalalalalalalalalala",
                                " hyehyehyehyehyehyehyehyehyehyehyehyehyehyehye          hye",
                                "huy2huy2huy2huy2huy2huy2huy2huy2huy2huy2huy2huy2huy2huy2huy2huy2huy2huy2huy2   huy2",
                                " huyhuyhuyhuyhuyhuyhuyhuyhuyhuyhuyhuyhuyhuyhuyhuyhuyhuyhuyhuyhuyhuy                  huy"};
        }
    }
}
