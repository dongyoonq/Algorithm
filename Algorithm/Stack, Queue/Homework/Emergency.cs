using System;
using System.Collections.Generic;
using System.Text;

namespace Algorithm
{
    internal class Emergency
    {
        private string patient;
        private int patientCount;
        private int goldenTime;
        private MyPriorityQueue<string, int> priorityQueue = new MyPriorityQueue<string, int>();

        public void Start()
        {
            Console.Write("치료할 환자 수를 입력하세요 : ");
            patientCount = int.Parse(Console.ReadLine());

            for (int i = 0; i < patientCount; i++)
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
            Console.WriteLine();
            while (priorityQueue.Count > 0)
            {
                Console.WriteLine($"{priorityQueue.Dequeue()}환자가 치료되었다.");
            }
        }
    }
}
