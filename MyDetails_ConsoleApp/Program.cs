using System;

namespace MyDetails
{
    public class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            var kompas = new KompasModel();
            foreach (string str in args)
            {
                switch (str)
                {
                    case "треугольник без отверстий":
                        kompas.DrawTriangleWithoutHoles();
                        break;
                    case "треугольник с отверстиями круглыми радиально":
                        kompas.DrawTriangleWithRoundHole();
                        break;
                    case "треугольник с отверстием некруглым в центре":
                        kompas.DrawTriangleWithNonCircularHole();
                        break;
                    case "треугольник с отверстиями круглыми радиально и отверстием некруглым в центре":
                        kompas.DrawTriangleWithRoundAndNonCircularHoles();
                        break;
                    case "прямоугольник без отверстий":
                        kompas.DrawRectangleWithoutHoles();
                        break;
                    case "прямоугольник с отверстиями круглыми радиально":
                        kompas.DrawRectangleWithRoundHole();
                        break;
                    case "прямоугольник с отверстием некруглым в центре":
                        kompas.DrawRectangleWithNonCircularHole();
                        break;
                    case "прямоугольник с отверстиями круглыми радиально и отверстием некруглым в центре":
                        kompas.DrawRectangleWithRoundAndNonCircularHoles();
                        break;
                    case "четырехугольник с непрямыми углами без отверстий":
                        kompas.DrawQuadrangleWithoutHoles();
                        break;
                    case "четырехугольник с непрямыми углами с отверстием круглым в центре":
                        kompas.DrawQuadrangleWithRoundHole();
                        break;
                    case "четырехугольник с непрямыми углами с отверстием некруглым в центре":
                        kompas.DrawQuadrangleWithNonCircularHole();
                        break;
                    case "четырехугольник с непрямыми углами с отверстиями круглыми радиально и отверстием некруглым в центре":
                        kompas.DrawQuadrangleWithRoundAndNonCircularHoles();
                        break;
                }
            }
        }
    }
}
