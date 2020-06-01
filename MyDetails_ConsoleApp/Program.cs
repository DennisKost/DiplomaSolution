using System;

namespace MyDetails_ConsoleApp
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            var kompas = new KompasModel();
            foreach (string str in args)
            {
                switch (str)
                {
                    case "деталь без отверстий":
                        kompas.DrawWithoutHoles();
                        break;
                    case "деталь с круглым отверстием":
                        kompas.DrawWithRoundHole();
                        break;
                    case "деталь с некруглым отверстием":
                        kompas.DrawWithNonCircularHole();
                        break;
                    case "деталь с круглым и некруглым отверстиями":
                        kompas.DrawWithRoundAndNonCircularHoles();
                        break;
                }
            }
        }
    }
}
