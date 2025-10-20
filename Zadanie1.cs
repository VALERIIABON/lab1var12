using System;
using System.IO;

class Program
{
    static void Main()
    {
        try
        {
            // Чтение данных из файла
            string[] lines = File.ReadAllLines("input.txt");

            // Чтение размерности пространства
            int N = int.Parse(lines[0]);
            Console.WriteLine($"Размерность пространства: {N}");

            // Выделение памяти для матрицы G
            double[,] G = new double[N, N];

            // Чтение матрицы G
            Console.WriteLine("\nМатрица G:");
            for (int i = 0; i < N; i++)
            {
                string[] numbers = lines[i + 1].Split(' ');

                for (int j = 0; j < N; j++)
                {
                    G[i, j] = double.Parse(numbers[j]);
                }

                // Вывод строки матрицы
                for (int j = 0; j < N; j++)
                {
                    Console.Write(G[i, j] + " ");
                }
                Console.WriteLine();
            }

            bool symmetric = true;
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    if (G[i, j] != G[j, i])
                    {
                        symmetric = false;
                        break;
                    }
                }
                if (!symmetric) break;
            }

            if (!symmetric)
            {
                Console.WriteLine("Ошибка: матрица G не является симметричной");
                return;
            }
            else
            {
                Console.WriteLine("Матрица G симметрична");
            }

            // Чтение вектора x
            string[] vectorNumbers = lines[N + 1].Split(' ');

            double[] x = new double[N];
            Console.WriteLine("\nВектор x:");
            for (int i = 0; i < N; i++)
            {
                x[i] = double.Parse(vectorNumbers[i]);
                Console.Write(x[i] + " ");
            }
            Console.WriteLine();
            double length = VectorLength(G, x, N);

            Console.WriteLine($"\nДлина вектора: {length:F6}");
        }
        catch (FileNotFoundException)
        {
            Console.WriteLine("Ошибка: файл input.txt не найден");
        }
    }

    static double VectorLength(double[,] G, double[] x, int N)
    {
        double[] Gx = new double[N];

        for (int i = 0; i < N; i++)
        {
            Gx[i] = 0;
            for (int j = 0; j < N; j++)
            {
                Gx[i] += G[i, j] * x[j];
            }
        }

        double dotProduct = 0;
        for (int i = 0; i < N; i++)
        {
            dotProduct += x[i] * Gx[i];
        }

        double result = Math.Sqrt(dotProduct);

        return result;
    }
}