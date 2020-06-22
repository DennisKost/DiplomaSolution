using System.Collections.Generic;

namespace ClassLibrary_Model
{
    /// <summary>
    /// Интерфейс бизнес-логики клиента
    /// </summary>
    public interface IClient
    {
        public string[] GetHumanValue(string key);
        public MathResponseItem[] GetValue(string key);
        public string Wrap(string data);
        public string UnWrap(string data);
        public Dictionary<string, string[]> Dictionary { get; }
    }
}
