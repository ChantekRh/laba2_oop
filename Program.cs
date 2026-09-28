using System;
using System.IO;

namespace Lab2
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            const int rows = 4;
            const int cols = 6;
            double[,] matrixA = new double[rows, cols];
            double[] arrayB = new double[rows];

            Console.WriteLine("Оберіть спосіб введення даних:");
            Console.WriteLine("1 — Зчитати з файлу input.txt");
            Console.WriteLine("2 — Згенерувати випадковими числами");
            Console.Write("Ваш вибір (1/2): ");

            string choice = Console.ReadLine();

            if (choice == "1")
            {
                if (File.Exists("input.txt"))
                {
                    ReadFromFile("input.txt", matrixA, rows, cols);
                }
                else
                {
                    Console.WriteLine("Файл input.txt не знайдено! Генеруємо випадкові числа.");
                    GenerateRandomMatrix(matrixA, rows, cols);
                }
            }
            else
            {
                GenerateRandomMatrix(matrixA, rows, cols);
            }

            Console.WriteLine("\n--- Початковий двовимірний масив A(4, 6) ---");
            PrintMatrix(matrixA, rows, cols);
            CalculatePositiveAverages(matrixA, arrayB, rows, cols);

            // Виведення результату
            Console.WriteLine("\n--- Вектор середніх арифметичних додатних елементів B(4) ---");
            PrintArray(arrayB);

            // Збереження результатів у файл
            SaveResultsToFile("output.txt", matrixA, arrayB, rows, cols);
            Console.WriteLine("\nРезультати успішно збережено у файл output.txt");

            Console.WriteLine("\nНатисніть будь-яку клавішу для завершення...");
            Console.ReadKey();
        }

        static void CalculatePositiveAverages(double[,] sourceMatrix, double[] targetArray, int rows, int cols)
        {
            for (int i = 0; i < rows; i++)
            {
                double sum = 0;
                int count = 0;

                for (int j = 0; j < cols; j++)
                {
                    if (sourceMatrix[i, j] > 0)
                    {
                        sum += sourceMatrix[i, j];
                        count++;
                    }
                }

                targetArray[i] = sum / count;
            }
        }

        static void GenerateRandomMatrix(double[,] matrix, int rows, int cols)
        {
            Random rnd = new Random();
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    matrix[i, j] = rnd.Next(-10, 20);
                }
                matrix[i, rnd.Next(0, cols)] = rnd.Next(1, 15);
            }
        }

        static void ReadFromFile(string filename, double[,] matrix, int rows, int cols)
        {
            string[] lines = File.ReadAllLines(filename);
            int k = 0;
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    if (k < lines.Length && double.TryParse(lines[k].Trim(), out double val))
                    {
                        matrix[i, j] = val;
                        k++;
                    }
                    else
                    {
                        matrix[i, j] = 1;
                    }
                }
            }
        }

        static void PrintMatrix(double[,] matrix, int rows, int cols)
        {
            for (int i = 0; i < rows; i++)
            {
                Console.Write($"Рядок {i}: ");
                for (int j = 0; j < cols; j++)
                {
                    Console.Write($"{matrix[i, j],7:F1}");
                }
                Console.WriteLine();
            }
        }

        static void PrintArray(double[] array)
        {
            for (int i = 0; i < array.Length; i++)
            {
                Console.WriteLine($"Рядок {i}: середнє позитивних = {array[i]:F2}");
            }
        }

        static void SaveResultsToFile(string filename, double[,] matrix, double[] array, int rows, int cols)
        {
            using (StreamWriter sw = new StreamWriter(filename))
            {
                sw.WriteLine("--- Двовимірний масив A(4, 6) ---");
                for (int i = 0; i < rows; i++)
                {
                    for (int j = 0; j < cols; j++)
                    {
                        sw.Write($"{matrix[i, j],7:F1}");
                    }
                    sw.WriteLine();
                }

                sw.WriteLine("\n--- Вектор середніх арифметичних позитивних елементів B(4) ---");
                for (int i = 0; i < array.Length; i++)
                {
                    sw.WriteLine($"Рядок {i}: {array[i]:F2}");
                }
            }
        }
    }
}
