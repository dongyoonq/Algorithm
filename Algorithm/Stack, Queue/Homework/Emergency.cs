using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm
{
    internal class Emergency
    {
        private string patient;
        private int goldenTime;
        private PriorityQueue<string, int> priorityQueue = new PriorityQueue<string, int>();

        public void Start()
        {
            for(int i = 0; i < 5; i++)
            {
                Input();
            }

            Cure();
        }

        private void Input()
        {
            Console.Write("환자 이름을 입력하세요 : ");
            patient = Console.ReadLine();
            Console.Write("골든 타임을 입력하세요 : ");
            goldenTime = int.Parse(Console.ReadLine());

            priorityQueue.Enqueue(patient, goldenTime);
        }

        private void Cure()
        {
            while(priorityQueue.Count > 0)
            {
                Console.WriteLine($"{priorityQueue.Dequeue()}환자가 치료되었다.");
            }
        }
    }
}
