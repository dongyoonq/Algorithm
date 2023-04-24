using System;
using System.Collections.Generic;
using System.Text;

namespace Algorithm
{
    /// <summary>
    /// 환자이름과 골든타임을 입력받아
    /// 급한 환자부터 치료하는 응급실을 구현한 클래스이다.
    /// </summary>
    internal class Emergency
    {
        private string patient;
        private int patientCount;
        private int goldenTime;
        private MyPriorityQueue<string, int> priorityQueue = new MyPriorityQueue<string, int>();

        /// <summary>
        /// 프로그램을 시작하는 메서드이다. 치료 환자수를 입력받고
        /// 그 수만큼 입력받는 Input메서드를 호출하고, 최종적으로 Cure에서
        /// 우선순위가 급한 환자부터 치료한다.
        /// </summary>
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

        /// <summary>
        /// 새로운 환자 정보를 입력받아 우선순위 큐에 추가하는 메서드
        /// 콘솔에서 환자 이름과 골든 타임을 입력받는다. 
        /// </summary>
        private void Input()
        {
            Console.Write("환자 이름을 입력하세요 : ");
            patient = Console.ReadLine();
            Console.Write("골든 타임을 입력하세요 : ");
            goldenTime = int.Parse(Console.ReadLine());

            priorityQueue.Enqueue(patient, goldenTime);
        }

        /// <summary>
        /// 우선순위 큐에서 환자를 치료하는 메서드
        /// </summary>
        private void Cure()
        {
            Console.WriteLine();
            // 우선순위 큐에 남아있는 환자가 없을 때 까지 반복한다.
            while (priorityQueue.Count > 0)
            {
                // 우선순위가 가장 높은 환자를 치료하고, 해당 환자의 이름을 출력한다.
                Console.WriteLine($"{priorityQueue.Dequeue()}환자가 치료되었다.");
            }
        }
    }
}
