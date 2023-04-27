using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm
{
    // 분할정복 문제
    internal class MyHanoi
    {
        private Stack<int>[] stack = new Stack<int>[3];
        public enum Place {  Left, Middle , Right }

        public void Start()
        {
            Init();
            Algorithm(4, Place.Left, Place.Right);
        }

        private void Algorithm(int count, Place start, Place end)
        {
            if(count == 1)
            {
                // 그냥 이동
                int node = stack[(int)start].Pop();
                stack[(int)end].Push(node);
                Console.WriteLine($"{start}에서 {end}로 {node}이동");
                return;
            }

            Place other = (Place)(3 - (int)start - (int)end);

            Algorithm(count - 1, start, other);
            Algorithm(1, start, end);
            Algorithm(count - 1, other, end);
        }

        private void Init()
        {
            for (int i = 0; i < stack.Length; i++)
                stack[i] = new Stack<int>();

            for(int i = 4; i > 0; i--)
            {
                stack[0].Push(i);
            }
        }
    }
}
