using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace Lab_4
{
    internal class Program
    {

        static double[,] matrixMultip(double[,] matrixA, double[,] matrixB)
        {
            double[,] matrixResult = new double[4, 4];
            //PrintMatrix(matrixA);
            //PrintSepT();
            //PrintMatrix(matrixB);
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0;j < 4; j++)
                {
                    double sum = 0;
                    for (int k = 0; k < 4; k++)
                    {
                        sum += matrixA[i, k] * matrixB[k, j];
                    }
                    matrixResult[i, j] = sum;
                }
            }

            return matrixResult;
        }
        static void PrintMatrix(double[,] Matrix)
        {
            for (int i = 0; i < Matrix.GetLength(0); i++)
            {
                for (int j = 0; j < Matrix.GetLength(1); j++)
                {
                    Console.Write(Math.Round(Matrix[i, j], 10) + "\t");
                    //Console.Write(Matrix[i, j] + "\t");
                }
                Console.WriteLine();
            }
        }
        static void PrintSep()
        {
            Console.WriteLine(string.Concat(Enumerable.Repeat("=", 50)));
        }

        static void PrintSepT()
        {
            Console.WriteLine(string.Concat(Enumerable.Repeat("-", 50)));
        }
        static double[,] generatE()
        {
            double[,] matrixE = new double[4, 4];

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (i == j) matrixE[i, j] = 1;
                    else matrixE[i, j] = 0;
                }
            }

            return matrixE;
        }



        static void vorfrobenius(double[,] matrix_A)
        {
            int n, m;

            n = matrix_A.GetLength(0);
            m = matrix_A.GetLength(1);

            double[,] matrixS = generatE();

            double tmp_num;

            for (int i = n - 1; i > 0; i--) 
            {
                Console.WriteLine($"\nStep #{4 - i} {matrix_A[i, i - 1]} (element of the matrix A, different from zero)");
                double[,] matrix_E = generatE();
                double[,] matrix_Et = generatE();

                for (int j = 0; j < n; j++)
                {
                    tmp_num = 0;
                    if (i != j + 1)
                    {
                        tmp_num = matrix_A[i, j] / (-matrix_A[i, i - 1]);
                    }

                    else if (i == j + 1)
                    {
                        tmp_num = 1 / matrix_A[i, i - 1];
                    }
                    matrix_E[i - 1, j] = tmp_num;
                    matrix_Et[i - 1, j] = matrix_A[i, j];

                }
                PrintSep();
                matrixS = matrixMultip(matrixS, matrix_E);

                Console.WriteLine($"Промiжна матриця M{i}");
                PrintMatrix(matrix_E);
                PrintSepT();
                Console.WriteLine($"Промiжна обернена матриця M{i}");
                PrintMatrix(matrix_Et);
                PrintSepT();
                matrix_A = matrixMultip(matrix_Et, matrixMultip(matrix_A, matrix_E));
                Console.WriteLine($"Промiжна матриця А{4 - i}");
                PrintMatrix(matrix_A);
                PrintSep();
            }
            Console.WriteLine("\n\nResult Matrix P (що має нормальну форму Фробенiуса):");
            PrintMatrix(matrix_A);
            Console.WriteLine($"\n\nAlpha характеристичного рiвняння:");
            for (int i = 0; i < 4; i++) {
                Console.Write($"i^{4-i}+({-matrix_A[0, i]})");
            }

            Console.WriteLine("=0\n\nМатрицю подiбностi S=M3*M2*M1");
            PrintMatrix(matrixS);

            Console.ReadLine();
        }


        static void Main(string[] args)
        {
            PrintSep();
            Console.WriteLine("\tЛабораторна робота #4");
            Console.WriteLine("Виконав студент групи IC-31 Коваль Богдан");
            PrintSep();

            int t, k;
            double a, b, g, d;

            t = 1;
            k = -8;

            a = 0.11 * t;
            b = 0.02 * k;
            g = 0.02 * k;
            d = 0.015 * t;
            
            double[,] Matrix_A =
            {
                {6.26 + a, 1.1 - b, 0.97 + g, 1.24 - d},
                {1.10 - b, 4.16 - a, 1.3, 0.16},
                {0.97 + g, 1.3, 5.44 + a, 2.1},
                {1.24 - d, 0.16, 2.10, 6.10 - a},
            };
            
            double[,] matrix_T =
            {
                { 2.2, 1, 0.5, 2 },
                { 1, 1.3, 2, 1 },
                { 0.5, 2, 0.5, 1.6},
                { 2, 1, 1.6, 2 }
            };
            /*
            double[,] matrix_T2 =
{
                { 2.3, 1, 1.5, 2.1 },
                { 1.8, 1.5, 2.1, 1.2 },
                { 1.5, 2, 1.5, 1.9},
                { 2.3, 1.2, 1.9, 2.1 }
            };*/

            Console.WriteLine("Input Matrix: ");
            PrintMatrix(Matrix_A);

            vorfrobenius(Matrix_A);


            Console.ReadLine();

        }
    }
}
