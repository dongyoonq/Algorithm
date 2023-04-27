using System;
using System.Collections.Generic;
using System.Text;

namespace Algorithm
{
    internal class QueueStudy
    {
        // 선입 선출 (FIFO)

        Queue<int> queue = new Queue<int>();

        public void Study()
        {
            for(int i = 0; i < 10; i++)
            {
                queue.Enqueue(i);
            }
        }
    }
}
