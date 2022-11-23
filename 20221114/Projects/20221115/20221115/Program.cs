using System;
using System.Text;

namespace Application
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            //MakeTree();
            //MakeSin();
            //Answer();
            TestSine();
            TestAnswer();
        }

        private static void MakeTree()
        {
            for(int i=1;i<10;i++)
            {
                for(int j=1;j<10;j++)
                {
                    if (i >= j)
                        Console.Write(j);
                    else
                        Console.Write(" ");

                }
                Console.WriteLine();

                if (i == 9) break;

                for (int j = 1; j <= 9; j++)
                {
                    if (j >= 10 - i)
                        Console.Write(j);
                    else
                        Console.Write(" ");
                }
                Console.WriteLine();
            }

            Console.WriteLine();
        }

        static string black = "■";
        static string white = "□";

        private static void MakeSin()
        {
            bool[,] dots = new bool[20, 11];
            const double PI = Math.PI;
            int x = 0;

            for (int i = 0; i < 360; i += 18)
            {
                dots[x, (int)Math.Round(Math.Sin(i * PI / 180) * -5) + 5] = true;
                x++;
            }

            for (int i = 0; i < 11; i++) 
            {
                for (int j = 0; j < 20; j++) 
                {
                    if (dots[j, i])
                        Console.Write(white);
                    else
                        Console.Write(black);
                }

                Console.WriteLine();
            }

            Console.WriteLine();
        }

        private static void Answer()
        {
            for (int y = 0; y < 11; ++y)
            {
                var sb = new StringBuilder();

                for (int x = 0; x < 20; ++x)
                {
                    var d = (double)x / 19;
                    d *= Math.PI;
                    d *= 2.0;

                    var sin = Math.Sin(d);
                    var yy = (sin * 5.0 + 5.0);
                    var yyy = 10.0 - Math.Round(yy, 0);

                    if (y == yyy)
                        sb.Append("□");
                    else
                        sb.Append("■");
                }

                Console.WriteLine(sb.ToString());
            }
        }

        private static void TestSine()
        {
            bool[,] dots = new bool[30, 15];
            const double PI = Math.PI;
            int x = 0;

            for (int i = 0; i < 360; i += 12)
            {
                dots[x, (int)Math.Round(Math.Sin(i * PI / 180) * -7) + 7] = true;
                x++;
            }

            for (int i = 0; i < 15; i++)
            {
                for (int j = 0; j < 30; j++)
                {
                    if (dots[j, i])
                        Console.Write(white);
                    else
                        Console.Write(black);
                }

                Console.WriteLine();
            }

            Console.WriteLine();
        }

        private static void TestAnswer()
        {
            for (int y = 0; y < 15; ++y)
            {
                var sb = new StringBuilder();

                for (int x = 0; x < 30; ++x)
                {
                    var d = (double)x / 29;
                    d *= Math.PI;
                    d *= 2.0;

                    var sin = Math.Sin(d);
                    var yy = (sin * 7.0 + 7.0);
                    var yyy = 14.0 - Math.Round(yy, 0);

                    if (y == yyy)
                        sb.Append("□");
                    else
                        sb.Append("■");
                }

                Console.WriteLine(sb.ToString());
            }
        }
    }
}
