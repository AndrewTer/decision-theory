using System;

namespace ConsoleApplication1
{
    class Program
    {
        static int num_k = 6;
        static int num_alt = 6;

        static void Domin(int[,,] bo, int[,] result)
        {
            for (int i = 0; i < num_k; i++)
                for (int j = 0; j < num_alt; j++)
                {
                    result[i, j] = 1;
                    for (int k = 0; k < num_alt && (result[i, j] != 0); k++)
                        if (j != k && bo[i, j, k] == 0) result[i, j] = 0;
                }
        }
        static void Block(int[,,] bo, int[,] result)
        {
            for (int i = 0; i < num_k; i++)
                for (int j = 0; j < num_alt; j++)
                {
                    result[i, j] = 1;
                    for (int k = 0; (k < num_alt) && (result[i, j] != 0); k++)
                        if ((k != j) && bo[i, k, j] != 0) result[i, j] = 0;
                }
        }
        static void Tournament(int[,,] bo, double[,] res)
        {
            Console.WriteLine("Таблица результатов турнира:");
            for (int i = 0; i < num_k; i++)
            {
                int num = i + 1;
                Console.Write("Критерий " + num + ": ");
                for (int j = 0; j < num_alt; j++)
                {
                    res[i, j] = 0;
                    for (int k = 0; k < num_alt; k++)
                        if ((k != j) && (bo[i, j, k] != 0))
                            if (bo[i, k, j] == 0) res[i, j] += 1;
                    Console.Write(res[i, j] + " ");
                }
                Console.WriteLine();
            }
        }
        
        static void Weight(double[,] result, double[] znach)
        {
                // Для вывода
                for (int i = 0; i < num_k; i++)
                {
                    int num = i + 1;
                    Console.Write("Принтер " + num + ": ");

                    for (int j = 0; j < num_alt; j++)
                    {
                        result[j, i] = result[j, i] * znach[j];
                        
                        Console.Write(result[j, i] + "\t");
                    }
                    Console.WriteLine();
                }

            // Для подсчёта
            Console.Write("\nКоличество очков:\n");
            for (int i = 0; i < num_k; i++)
            {
                double x = 0;
                for (int j = 0; j < num_alt; j++)
                {
                    x += result[j, i];
                }
                result[num_alt - 1, i] = x;
                int num = i + 1;
                Console.WriteLine("Принтер " + num + ": " + x);
            }

        }
        static void FunCh(int[,] result)
        {
            for (int i = 0; i < num_alt; i++)
            {
                int num = 0;
                for (int j = 0; j < num_k; j++)
                    num += result[j, i] != 0 ? 1 : 0;
                if (num >= num_k / 2)
                    result[num_k - 1, i] = 1;
                else
                    result[num_k - 1, i] = 0;
            }
        }

        static void PrintVector(int[] result)
        {
            int i;
            bool ch = false;

            for (i = 0; i < num_alt; i++)
                if (result[i] == 1)
                {
                    if (ch)
                        Console.Write(", ");
                    ch = true;
                    Console.Write(i + 1);
                }

            if (!ch)
                Console.WriteLine("нет предложений");
            else
                Console.WriteLine("");
        }

        static void Main()
        {
            double[] znach = { 0.25, 0.2, 0.1, 0.1, 0.15, 0.15 };

            int[,,] bo = { { { 1, 1, 1, 1, 1, 1 },
                    { 0, 0, 1, 1, 1, 1 },
                    { 0, 0, 0, 0, 0, 0 },
                    { 0, 0, 1, 0, 1, 0 },
                    { 0, 0, 1, 0, 1, 0 },
                    { 0, 0, 1, 1, 1, 0 } },

                    { { 1, 1, 0, 0, 1, 0 },
                    { 0, 1, 1, 0, 1, 0 },
                    { 0, 0, 0, 0, 0, 0 },
                    { 1, 1, 1, 1, 1, 1 },
                    { 0, 1, 1, 0, 1, 0 },
                    { 1, 1, 1, 1, 1, 1 } },

                    { { 1, 1, 1, 1, 1, 1 },
                    { 1, 1, 0, 1, 0, 1 },
                    { 0, 1, 1, 0, 1, 0 },
                    { 0, 1, 1, 1, 1, 0 },
                    { 0, 0, 0, 0, 0, 0 },
                    { 0, 1, 1, 1, 1, 1 } },

                    { { 0, 0, 0, 0, 0, 0 },
                    { 1, 1, 1, 0, 0, 0 },
                    { 1, 1, 1, 0, 0, 0 },
                    { 1, 1, 1, 1, 1, 0 },
                    { 0, 0, 0, 1, 1, 1 },
                    { 1, 1, 1, 1, 1, 1 } },

                    { { 0, 0, 0, 0, 0, 0 },
                    { 1, 1, 1, 0, 0, 1 },
                    { 1, 1, 1, 0, 0, 1 },
                    { 1, 1, 1, 1, 1, 1 },
                    { 1, 1, 1, 0, 1, 1 },
                    { 1, 1, 1, 0, 0, 1 } },

                    { { 1, 1, 1, 0, 1, 0 },
                    { 0, 1, 1, 0, 1, 0 },
                    { 0, 1, 1, 0, 1, 0 },
                    { 0, 0, 0, 1, 1, 0 },
                    { 0, 0, 0, 0, 0, 0 },
                    { 1, 1, 1, 1, 1, 1 } }
            };

            int[,] resDom = new int[num_k, num_alt];
            int[,] resBloc = new int[num_k, num_alt];
            double[,] resTour = new double[num_k, num_alt];
            double[,] resWeight = new double[num_k, num_alt];
            int[,] resCh = new int[num_k, num_alt];
            double[] tempd = new double[num_alt];
            int[] tempi = new int[num_alt];

            Console.WriteLine("Механизм блокировки\n");
            Block(bo, resBloc);
            for (int i = 0; i < num_k; i++)
            {
                Console.Write("Критерий " + (i + 1));
                Console.Write("\tПредложения: ");
                for (int j = 0; j < num_alt; j++)
                    if (resBloc[i, j] == 1)
                        Console.Write((j + 1) + "  ");
                Console.WriteLine();
            }

            FunCh(resBloc);
            Console.Write("Итог: ");
            for (int i = 0; i < num_alt; i++)
                tempi[i] = resBloc[num_k - 1, i];
            PrintVector(tempi);

            Console.WriteLine("\n----------------------------------------------------------\nМеханизм доминирования\n");
            Domin(bo, resDom);
            for (int i = 0; i < num_k; i++)
            {
                Console.Write("Критерий " + (i + 1));
                Console.Write("\tПредложения: ");
                for (int j = 0; j < num_alt; j++)
                    if (resDom[i, j] == 1)
                        Console.Write((j + 1) + "  ");
                Console.WriteLine();
            }

            FunCh(resDom);
            Console.Write("Итог: ");
            for (int i = 0; i < num_alt; i++)
                tempi[i] = resDom[num_k - 1, i];
            PrintVector(tempi);

            Console.WriteLine("\n----------------------------------------------------------\nТурнирный механизм\n");
            Tournament(bo, resTour);

            
            Console.WriteLine("\nТаблица результатов турнира * коэффициент значимости:");

            Weight(resTour, znach);

            Console.WriteLine();
            Console.ReadLine();
        }
    }
}
