using System.Collections.Generic;

namespace ClientApp_WPF
{
    /// <summary>
    /// Интерфейс бизнес-логики клиента
    /// </summary>
    interface IClient
    {
        public string[] GetHumanValue(string key);
        public MathResponseItem[] GetValue(string key);
        public string Wrap(string data);
        public string UnWrap(string data);
        public Dictionary<string,string[]> Dictionary { get; }
    }
}
