using System;
using System.Collections.Generic;
using System.Text;

namespace Algorithm
{
    internal class StackStudy
    {
        // 선입 후출 (FILO)
        Stack<int> stack = new Stack<int>();

        public void Study()
        {
            for (int i = 0; i < 10; i++)
            {
                stack.Push(i);
            }
        }
    }
}
