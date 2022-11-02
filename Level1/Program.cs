using System;
using System.Collections;
using System.Net;

namespace Level1
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Number31();
        }

        static void Number1() // Найти сумму элементов матрицы А размера 5 × 7.
        {
            var matrix = GenerateMatrix(5, 7, (-10, 10));
            PrintMatrix(matrix);
            var sum = 0;
            foreach (var i in matrix)
            {
                sum += i;
            }
            Console.WriteLine($"Сумма элементов матрицы: {sum}");
        }
        static void Number6() // Сформировать одномерный массив из индексов минимальных элементов строк матрицы А размера 4 × 7.
        {
            var matrix = GenerateMatrix(7, 4, (-10, 10));
            PrintMatrix(matrix);
            Console.WriteLine("Минимальные элементы строк матрицы:");
            foreach (var i in MinElementsOfMatrix(matrix, Direction.Columns))
            {
                Console.Write($"{i} ");
            }
            Console.WriteLine();
            
            Console.WriteLine("Индексы минимальных элементов строк матрицы:");
            foreach (var i in IndexesOfMatrix(matrix, Function.Min, Direction.Columns))
            {
                Console.Write($"{i} ");
            }
            Console.WriteLine();
        }
        static void Number11() // Удалить строку матрицы А размера 5 × 7, содержащую минимальный элемент в 1-м столбце.
        {
            int[,] matrix = GenerateMatrix(5, 7, (-10, 10));
            PrintMatrix(matrix);
            
            int index = IndexesOfMatrix(matrix, Function.Min, Direction.Columns)[0];
            Console.WriteLine($"Индекс строки с минимальным элементом в 1-м столбце: {index}");
            matrix = DeletePartOfMatrix(matrix, Direction.Rows, index);
            PrintMatrix(matrix);
        }

        static void Number16() // В каждой строке матрицы А размера n × m максимальный элемент поместить в конец строки, сохранив порядок остальных элементов.
        {
            Console.Write("Введите N: ");
            int n = int.Parse(Console.ReadLine());
            Console.Write("Введите M: ");
            int m = int.Parse(Console.ReadLine());
            int[,] matrix = GenerateMatrix(5, 7, (-10, 10));
            PrintMatrix(matrix);
            
            int[] indexes = IndexesOfMatrix(matrix, Function.Max, Direction.Rows);
            for (var i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = indexes[i] + 1; j < matrix.GetLength(1); j++)
                    (matrix[i, j], matrix[i, j - 1]) = (matrix[i, j - 1], matrix[i, j]);
            }
            PrintMatrix(matrix);
        }
        static void Number21() // В матрице Н размера 5 × 7 заполнены первые 6 столбцов. Поместить в качестве предпоследнего столбца столбец, состоящий из максимальных элементов строк.
        {
            int[,] matrix = GenerateMatrix(5, 7, (-10, 10));
            matrix = ApplyFuncToMatrix(matrix, t => 0, Direction.Columns, 6);
            PrintMatrix(matrix);
            
            int[] maxElements = MaxElementsOfMatrix(matrix, Direction.Rows);
            for (var i = 0; i < matrix.GetLength(0); i++)
            {
                matrix[i, matrix.GetLength(1) - 2] = maxElements[i];
            }
            PrintMatrix(matrix);
        }
        static void Number26() // В матрице А размера 5 × 7 строку, содержащую максимальный элемент в 6-м столбце, заменить заданным вектором В размера 7.
        {
            int[,] matrix = GenerateMatrix(5, 7, (-10, 10));
            matrix[new Random().Next(0,5), 5] = 100;
            PrintMatrix(matrix);
            
            int[] vector = GenerateArray(7, (-10, 10));
            PrintArray(vector);
            
            int index = IndexesOfMatrix(matrix, Function.Max, Direction.Columns)[5];
            Console.WriteLine($"Индекс строки с максимальным элементом в 6-м столбце: {index}\n");
            for (var i = 0; i < matrix.GetLength(1); i++)
            {
                matrix[index, i] = vector[i];
            }
            PrintMatrix(matrix);
        }

        

        static void Number31() // В матрице А размера 5 × 8 заполнены первые 7 столбцов. Поместить вектор В размера 5 после столбца, содержащего минимальный элемент в 5-й строке.
        {
            int[,] matrix = GenerateMatrix(5, 8, (-10, 10));
            matrix = ApplyFuncToMatrix(matrix, t => 0, Direction.Columns, 7);
            matrix[4, new Random().Next(0,7)] = -100;
            PrintMatrix(matrix);
            
            int[] vector = GenerateArray(5, (-10, 10));
            PrintArray(vector);
            
            int index = IndexesOfMatrix(matrix, Function.Min, Direction.Rows)[4];
            Console.WriteLine($"Индекс столбца с минимальным элементом в 5-й строке: {index}\n");
            for (var i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = matrix.GetLength(1) - 1; j > index; j--)
                {
                    matrix[i, j] = matrix[i, j - 1];
                }
                matrix[i, index + 1] = vector[i];
            }
            PrintMatrix(matrix);
        }

        #region FunctionalMethods
        static int[,] ApplyFuncToMatrix(int[,] matrix, Func<int, int> func, Direction direction = Direction.Rows, int index = -1)
        {
            if (index != -1)
            {
                if (direction == Direction.Rows)
                {
                    for (var i = 0; i < matrix.GetLength(1); i++)
                    {
                        matrix[index, i] = func(matrix[index, i]);
                    }
                }
                else
                {
                    for (var i = 0; i < matrix.GetLength(0); i++)
                    {
                        matrix[i, index] = func(matrix[i, index]);
                    }
                }
            }
            else
            {
                for (int i = 0; i < matrix.GetLength(0); i++)
                {
                    for (int j = 0; j < matrix.GetLength(1); j++)
                    {
                        matrix[i, j] = func(matrix[i, j]);
                    }
                }
            }

            return matrix;
        }
        static int[] MinElementsOfMatrix(int[,] matrix, Direction direction)
        {
            if (direction == Direction.Rows)
            {
                int[] min = new int[matrix.GetLength(0)];
                for (int i = 0; i < matrix.GetLength(0); i++)
                {
                    min[i] = matrix[i, 0];
                    for (int j = 0; j < matrix.GetLength(1); j++)
                    {
                        if (matrix[i, j] < min[i])
                        {
                            min[i] = matrix[i, j];
                        }
                    }
                }

                return min;
            }
            else
            {
                int[] min = new int[matrix.GetLength(1)];
                for (int i = 0; i < matrix.GetLength(1); i++)
                {
                    min[i] = matrix[0, i];
                    for (int j = 0; j < matrix.GetLength(0); j++)
                    {
                        if (matrix[j, i] < min[i])
                        {
                            min[i] = matrix[j, i];
                        }
                    }
                }

                return min;
            }
        }
        static int[] MaxElementsOfMatrix(int[,] matrix, Direction direction)
        {
            if (direction == Direction.Rows)
            {
                int[] max = new int[matrix.GetLength(0)];
                for (int i = 0; i < matrix.GetLength(0); i++)
                {
                    max[i] = matrix[i, 0];
                    for (int j = 0; j < matrix.GetLength(1); j++)
                    {
                        if (matrix[i, j] > max[i])
                        {
                            max[i] = matrix[i, j];
                        }
                    }
                }

                return max;
            }
            else
            {
                int[] max = new int[matrix.GetLength(1)];
                for (int i = 0; i < matrix.GetLength(1); i++)
                {
                    max[i] = matrix[0, i];
                    for (int j = 0; j < matrix.GetLength(0); j++)
                    {
                        if (matrix[j, i] > max[i])
                        {
                            max[i] = matrix[j, i];
                        }
                    }
                }

                return max;
            }
        }
        static int[] IndexesOfMatrix(int[,] matrix, Function function, Direction direction)
        {
            int[] min = function == Function.Min ? MinElementsOfMatrix(matrix, direction) : MaxElementsOfMatrix(matrix, direction);
            int[] indexes;
            if (direction == Direction.Rows)
            {
                indexes = new int[matrix.GetLength(0)];
                for (int i = 0; i < matrix.GetLength(0); i++)
                {
                    for (int j = 0; j < matrix.GetLength(1); j++)
                    {
                        if (matrix[i, j] == min[i])
                        {
                            indexes[i] = j;
                            break;
                        }
                    }
                }
            }
            else
            {
                indexes = new int[matrix.GetLength(1)];
                for (int i = 0; i < matrix.GetLength(1); i++)
                {
                    for (int j = 0; j < matrix.GetLength(0); j++)
                    {
                        if (matrix[j, i] == min[i])
                        {
                            indexes[i] = j;
                            break;
                        }
                    }
                }
            }
            return indexes;
        }
        static int[,] GenerateMatrix(int n, int m, (int, int) range)
        {
            int[,] matrix = new int[n, m];
            Random random = new Random();
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    matrix[i, j] = random.Next(range.Item1, range.Item2 + 1);
                }
            }
            return matrix;
        }
        static int[] GenerateArray(int i, (int, int) range)
        {
            int[] vector = new int[i];
            Random random = new Random();
            for (int j = 0; j < vector.Length; j++)
            {
                vector[j] = random.Next(range.Item1, range.Item2 + 1);
            }
            return vector;
        }
        static int[,] DeletePartOfMatrix(int[,] matrix, Direction direction, int index)
        {
            int[,] newMatrix;
            if (direction == Direction.Rows)
            {
                newMatrix = new int[matrix.GetLength(0) - 1, matrix.GetLength(1)];
                for (int i = 0; i < index; i++)
                {
                    for (int j = 0; j < matrix.GetLength(1); j++)
                    {
                        newMatrix[i, j] = matrix[i, j];
                    }
                }
                for (int i = index; i < newMatrix.GetLength(0); i++)
                {
                    for (int j = 0; j < matrix.GetLength(1); j++)
                    {
                        newMatrix[i, j] = matrix[i + 1, j];
                    }
                }
            }
            else
            {
                newMatrix = new int[matrix.GetLength(0), matrix.GetLength(1) - 1];
                for (int i = 0; i < matrix.GetLength(0); i++)
                {
                    for (int j = 0; j < index; j++)
                    {
                        newMatrix[i, j] = matrix[i, j];
                    }
                }
                for (int i = 0; i < matrix.GetLength(0); i++)
                {
                    for (int j = index; j < newMatrix.GetLength(1); j++)
                    {
                        newMatrix[i, j] = matrix[i, j + 1];
                    }
                }
            }
            return newMatrix;
        }
        static void PrintMatrix(int[,] matrix)
        {
            int[] power = new int[matrix.GetLength(1)];
            for (int i = 0; i < matrix.GetLength(1); i++)
            {
                int currentPower = 0;

                for (int j = 0; j < matrix.GetLength(0); j++)
                {
                    int length = matrix[j, i].ToString().Length;
                    if (length > currentPower)
                        currentPower = length;
                }

                power[i] = currentPower;
            }
            
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                Console.Write("|");
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    int currentNumber = matrix[i, j];
                    int currentPower = matrix[i, j].ToString().Length - 1;

                    for (int k = 0; k < power[j] - currentPower; k++)
                    {
                        Console.Write(" ");
                    }

                    Console.Write(matrix[i, j]);
                }
                Console.WriteLine(" |");
            }

            Console.WriteLine();
        }
        static void PrintArray(Array arr)
        {
            IEnumerator enumerator = arr.GetEnumerator();
            while (enumerator.MoveNext())
                Console.Write($"{enumerator.Current} ");
            Console.WriteLine("\n");
        }
        #endregion
    }

    enum Direction
    {
        Rows,
        Columns
    }
    enum Function
    {
        Min,
        Max
    }
}