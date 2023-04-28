using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm
{

    public class Hanoi
    {
        private StringBuilder sb = new StringBuilder();
        private Stack<int>[] stack = new Stack<int>[3];
        private int input;
        private int cnt = 0;
        private enum Position { Left, Middle, Right }

        public void Start()
        {
            input = int.Parse(Console.ReadLine());

            for (int i = 0; i < stack.Length; i++)
                stack[i] = new Stack<int>();

            for (int i = input; i > 0; i--)
            {
                stack[0].Push(i);
            }

            Solve(input, Position.Left, Position.Right);
            Console.WriteLine(cnt);
            Console.WriteLine(sb.ToString());
        }

        private void Solve(int count, Position start, Position end)
        {
            if (count == 1)
            {
                int tmp = stack[(int)start].Pop();
                stack[(int)end].Push(tmp);
                sb.Append($"{(int)start + 1} {(int)end + 1}\n");
                cnt++;
                return;
            }

            // 나머지 하나를 구하는 코드
            Position other = (Position)(3 - (int)start - (int)end);

            Solve(count - 1, start, other);
            Solve(1, start, end);
            Solve(count - 1, other, end);
        }
    }
}
