using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm
{
    public class DividePaper
    {
        public void Start()
        {
            int breadth = int.Parse(Console.ReadLine());
            int[,] paper = new int[breadth, breadth];
            int blueCnt = 0;
            int whiteCnt = 0;

            for (int i = 0; i < breadth; i++)
            {
                int[] line = Array.ConvertAll(Console.ReadLine().Split(' '), int.Parse);
                for (int j = 0; j < line.Length; j++)
                    paper[i, j] = line[j];
            }

            Divide(breadth, 0, 0);

            Console.WriteLine(whiteCnt);
            Console.WriteLine(blueCnt);

            void Divide(int breadth, int startX, int startY)
            {
                int check = paper[startY, startX];
                for (int y = startY; y < startY + breadth; y++)
                {
                    for (int x = startX; x < startX + breadth; x++)
                    {
                        if (paper[y, x] != check)
                        {
                            Divide(breadth / 2, startX, startY);                                // y 0-3, x 0-3
                            Divide(breadth / 2, startX + breadth / 2, startY);                 // y 0-3, x 4-7
                            Divide(breadth / 2, startX, startY + breadth / 2);                  // y 4-7, x 0-3
                            Divide(breadth / 2, startX + breadth / 2, startY + breadth / 2);    // y 4-7, x 4-7
                            return;
                        }
                    }
                }

                if (check == 1)
                    blueCnt++;
                else
                    whiteCnt++;
            }
        }
    }
}
