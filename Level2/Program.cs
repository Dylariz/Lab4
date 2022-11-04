using System;
using System.Collections;

namespace Level2
{
    internal class Program
    {
        public static void Main(string[] args)
        {
           Number6(); 
        }
        
        /// <summary>
        /// Дана матрица A размера 5 * 7. Для каждой строки сравнить элементы,
        /// расположенные непосредственно перед и после максимального элемента этой
        /// строки, и меньший из них увеличить в 2 раза. Если максимальный элемент является
        /// первым или последним в строке, то увеличить в 2 раза только один соседний с
        /// максимальным элемент.
        /// </summary>
        static void Number1()
        {
            int[,] matrix = GenerateMatrix(5, 7, (-10, 20));
            PrintMatrix(matrix);
            int[] maxIndexes = IndexesOf(matrix, Function.Max, Direction.Rows);
            
            Console.WriteLine("Индексы минимальных элементов строк матрицы:");
            foreach (var i in maxIndexes)
            {
                Console.Write($"{i} ");
            }
            Console.WriteLine("\n");
            
            for (int i = 0; i < maxIndexes.Length; i++)
            {
                if (maxIndexes[i] == 0)
                {
                    matrix[i, maxIndexes[i] + 1] *= 2;
                }
                else if (maxIndexes[i] == matrix.GetLength(1) - 1)
                {
                    matrix[i, maxIndexes[i] - 1] *= 2;
                }
                else
                {
                    if (matrix[i, maxIndexes[i] - 1] < matrix[i, maxIndexes[i] + 1])
                    {
                        matrix[i, maxIndexes[i] - 1] *= 2;
                    }
                    else
                    {
                        matrix[i, maxIndexes[i] + 1] *= 2;
                    }
                }
            }
            PrintMatrix(matrix);
        }
        
        /// <summary>
        /// Сформировать матрицу размера n * 3n, составленную из трех единичных квадратных
        /// матриц размера n * n.
        /// </summary>
        static void Number6()
        {
            Console.Write("Введите размерность матрицы N: ");
            int n = int.Parse(Console.ReadLine());
            
            int[,] unitMatrix = GenerateUnitMatrix(n);

            PrintMatrix(unitMatrix);
            
            int[,] resultMatrix = new int[n, 3 * n];
            
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    resultMatrix[i, j] = unitMatrix[i, j];
                    resultMatrix[i, j + n] = unitMatrix[i, j];
                    resultMatrix[i, j + 2 * n] = unitMatrix[i, j];
                }
            }
            
            PrintMatrix(resultMatrix);
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
        static int[] MinElementsOf(int[,] matrix, Direction direction)
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
        static int[] MaxElementsOf(int[,] matrix, Direction direction)
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
        static int[] IndexesOf(int[,] matrix, Function function, Direction direction)
        {
            int[] min = function == Function.Min ? MinElementsOf(matrix, direction) : MaxElementsOf(matrix, direction);
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
        static int[] CountOf(int[,] matrix, Sign sign, Direction direction)
        {
            int[] count = new int[direction == Direction.Rows ? matrix.GetLength(0) : matrix.GetLength(1)];
            for (int i = 0; i < count.Length; i++)
            {
                for (int j = 0; j < (direction == Direction.Rows ? matrix.GetLength(1) : matrix.GetLength(0)); j++)
                {
                    if (sign == Sign.Positive && matrix[direction == Direction.Rows ? i : j, direction == Direction.Rows ? j : i] > 0)
                    {
                        count[i]++;
                    }
                    else if (sign == Sign.Negative && matrix[direction == Direction.Rows ? i : j, direction == Direction.Rows ? j : i] < 0)
                    {
                        count[i]++;
                    }
                }
            }

            return count;
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
        static int[,] GenerateUnitMatrix(int n)
        {
            int[,] matrix = new int[n, n];
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    matrix[i, j] = i == j ? 1 : 0;
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
        static int[,] SwapPartOfMatrix(int[,] matrix, Direction direction, int index1, int index2)
        {
            if (direction == Direction.Rows)
                for (int i = 0; i < matrix.GetLength(1); i++)
                    (matrix[index1, i], matrix[index2, i]) = (matrix[index2, i], matrix[index1, i]);
            else
                for (int i = 0; i < matrix.GetLength(0); i++)
                    (matrix[i, index1], matrix[i, index2]) = (matrix[i, index2], matrix[i, index1]);
            
            return matrix;
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
    enum Sign
    {
        Negative,
        Positive
    }
}