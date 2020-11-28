using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using MathNet.Numerics;
using MathNet.Numerics.LinearAlgebra.Double;
namespace Sudoku.Core
{
    public class SudokuBuilder
    {
        /// <summary>
        /// 制作完整的数独
        /// </summary>
        /// <returns></returns>
        public RSudoku MakeWholeSudoku()
        {
            Random rm = new Random();
            var list1 = RandomHelper.GetRandom(1, true, 9, true, 9, rm, false);
            var a = GetBaseSudoku(list1);
            var sudokuMatrix = Upset(a);
            var result = ListToString(sudokuMatrix);
            Debug.WriteLine("新生成的完整数独是：" + result);
            return new RSudoku(result);
        }
        public  string ListToString(List<List<int>> tempquestion)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var list in tempquestion)
            {
                foreach (var value in list)
                {
                    sb.Append(value);
                }
            }
            return sb.ToString();
        }
        /// <summary>
        /// 生成基本的数独
        /// </summary>
        /// <returns></returns>
        private List<List<int>> GetBaseSudoku(List<int> list1)
        {
            var list2 = moveToTail(list1, 3);
            var list3 = moveToTail(list1, 6);
            var list4 = moveToTail(list1, 1);
            var list5 = moveToTail(list1, 4);
            var list6 = moveToTail(list1, 7);
            var list7 = moveToTail(list1, 2);
            var list8 = moveToTail(list1, 5);
            var list9 = moveToTail(list1, 8);
            var a = new List<List<int>> { list1, list2, list3, list4, list5, list6, list7, list8, list9 };
            return a;
        }
        /// <summary>
        /// 交换行列顺序，增加随机性
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        public List<List<int>> Upset(List<List<int>> a)
        {
            double[,] doubles = new double[9, 9];
            for (int i = 0; i < a.Count; i++)
            {
                var inner = a[i];
                for (int j = 0; j < inner.Count; j++)
                {
                    doubles[i, j] = a[i][j];
                }
            }
            var matrix = DenseMatrix.OfArray(doubles);
            for (int i = 0; i < 10; i++)
            {
                var permutations = new Permutation(UnOrderlist());
                matrix.PermuteRows(permutations);
                var permutations1 = new Permutation(UnOrderlist());
                matrix.PermuteColumns(permutations1);
            }
            for (int i = 0; i < matrix.RowCount; i++)
            {
                for (int j = 0; j < matrix.ColumnCount; j++)
                {
                    a[i][j] = (int)matrix[i, j];
                }
            }
            return a;
        }
        /// <summary>
        /// 打乱顺序的数组
        /// </summary>
        /// <returns></returns>
        public int[] UnOrderlist()
        {
            Random rm = new Random();
            List<int> int1 = new List<int>();
            int1.AddRange(RandomHelper.GetRandom(0, true, 2, true, 3, rm, false));
            int1.AddRange(RandomHelper.GetRandom(3, true, 5, true, 3, rm, false));
            int1.AddRange(RandomHelper.GetRandom(6, true, 8, true, 3, rm, false));
            return int1.ToArray();
        }
        private List<int> moveToTail(List<int> list, int size)
        {
            List<int> a = new List<int>();
            List<int> b = new List<int>();
            for (int i = 0; i < list.Count; i++)
            {
                if (i < size)
                {
                    a.Add(list[i]);
                }
                else
                {
                    b.Add(list[i]);
                }
            }
            b.AddRange(a);
            return b;
        }
    }
}
