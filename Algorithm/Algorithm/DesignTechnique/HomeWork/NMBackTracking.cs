using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm
{
    public class NMBackTracking
    {
        private StringBuilder sb = new StringBuilder();

        public void Start()
        {
            int[] nm = Array.ConvertAll(Console.ReadLine().Split(' '), int.Parse);
            int n = nm[0];
            int m = nm[1];
            bool[] visited = new bool[(int)Math.Pow(n, m) + 1];
            int[] arr = new int[(int)Math.Pow(n, m)];
            BackTracking(arr, visited, n, m, 0);
            Console.WriteLine(sb.ToString());
        }

        private void BackTracking(int[] arr, bool[] visited, int n, int m, int cnt)
        {
            if (cnt == m)
            {
                for (int i = 0; i < m; i++)
                    sb.Append($"{arr[i]} ");
                sb.Append("\n");
            }
            else
            {
                for (int i = 1; i <= n; i++)
                {
                    //if (!visited[i])
                    //{
                    //visited[i] = true;
                    arr[cnt] = i;
                    BackTracking(arr, visited, n, m, cnt + 1);
                    //visited[i] = false;
                    //}
                }
            }
        }
    }
}