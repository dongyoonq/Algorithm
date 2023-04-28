using System;
using System.Collections.Generic;
using System.Text;

namespace Algorithm
{
    public class DynamicTriangle
    {
        private StringBuilder sb = new StringBuilder();

        public void Start()
        {
            int n = int.Parse(Console.ReadLine());
            int[,] triAngle = new int[n + 1, n + 1];
            int maxRoute = 0;

            for (int i = 0; i < n; i++)
            {
                int[] tmp = Array.ConvertAll(Console.ReadLine().Split(' '), int.Parse);
                for (int j = 0; j < tmp.Length; j++)
                {
                    triAngle[i, j] = tmp[j];
                }
            }

            for (int i = 1; i <= n; i++)
            {
                for (int j = 0; j <= i; j++)
                {
                    if (j == 0)
                        triAngle[i, 0] = triAngle[i - 1, 0] + triAngle[i, 0];
                    else if (j == i)
                        triAngle[i, j] = triAngle[i - 1, j - 1] + triAngle[i, j];
                    else
                        triAngle[i, j] = Bigger(triAngle[i - 1, j - 1], triAngle[i - 1, j]) + triAngle[i, j];

                    maxRoute = Bigger(maxRoute, triAngle[i, j]);
                }
            }

            Console.WriteLine(maxRoute);

            int Bigger(int left, int right)
            {
                return left > right ? left : right;
            }

        }
    }
}
