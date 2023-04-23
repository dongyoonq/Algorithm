using System;
using System.Collections.Generic;
using System.Text;

namespace Algorithm
{
    /// <summary>
    /// 속도가 빠른 플레이어 순으로 Queue를 정렬시켜주는 알고리즘
    /// </summary>
    public class playerQueue
    {
        /// <summary>
        /// Queue에 Player 클래스를 담는다.
        /// </summary>
        private Queue<Player> queue = new Queue<Player>();

        /// <summary>
        /// Queue에 Player의 정보들을 담는 초기화 메서드이다.
        /// </summary>
        public void Init()
        {
            Player player1 = new Player("플레이어 1", 12);
            Player player2 = new Player("플레이어 2", 52);
            Player player3 = new Player("플레이어 3", 7);
            Player player4 = new Player("플레이어 4", 18);
            Player player5 = new Player("플레이어 5", 60);
            Player player6 = new Player("플레이어 6", 21);
            Player player7 = new Player("플레이어 7", 15);
            Player player8 = new Player("플레이어 8", 60);
            Player player9 = new Player("플레이어 9", 70);

            
            queue.Enqueue(player1);
            queue.Enqueue(player2);
            queue.Enqueue(player3);
            queue.Enqueue(player4);
            queue.Enqueue(player5);
            queue.Enqueue(player6);
            queue.Enqueue(player7);
            queue.Enqueue(player8);
            queue.Enqueue(player9);

        }

        /**
         * 큐에서 가장 작은 속도를 가진 플레이어를 찾아서 다른 플레이어와 교환하는 알고리즘입니다.
         * 선택 정렬과 유사한 알고리즘입니다.
         * 
         * 구체적으로 알고리즘은 다음과 같이 동작합니다.
         * 
         * 1. 큐에서 첫 번째 플레이어를 초기 최소값으로 설정합니다.
         * 2. 큐에서 첫 번째 플레이어를 뺀 나머지 플레이어를 모두 탐색하며, 가장 작은 속도를 가진 플레이어를 찾습니다.
         *    (이때, 큐에서 플레이어를 뺀 후에, 나머지 플레이어를 탐색하는 과정에서 큐가 변경됩니다.)
         * 3. 가장 작은 속도를 가진 플레이어를 찾았으면, 해당 플레이어를 큐에서 빼서 초기 최소값인 첫 번째 플레이어와 교환합니다.
         * 4. 1-3번 과정을 큐의 길이보다 하나 작은 횟수만큼 반복합니다.
         * 5. 최종적으로 큐는 플레이어의 속도가 빠른 순서대로 정렬됩니다.
         */
        public void Sort()
        {
            // 큐 정렬
            // 큐의 크기만큼 반복하여 정렬
            for (int i = 0; i < queue.Count; i++)
            {
                // 첫 번째 플레이어를 초기 최소값으로 설정하고, 
                // 해당 플레이어의 속도를 초기 최소값으로 설정한다.
                Player minPlayer = queue.Dequeue();
                int minSpeed = minPlayer.Speed;

                // 큐에서 플레이어를 모두 탐색하며, 가장 작은 속도를 가진 플레이어를 찾는다.
                for (int j = 0; j < queue.Count; j++)
                {
                    Player player = queue.Dequeue();
                    // 작은 속도를 가진 플레이어를 찾았으면, 
                    // 해당 플레이어를 큐에서 빼서 초기 최소값인 첫 번째 플레이어와 교환한다.
                    if (player.Speed < minSpeed)
                    {
                        queue.Enqueue(minPlayer);
                        minPlayer = player;
                        minSpeed = player.Speed;
                    }
                    else
                        queue.Enqueue(player);
                }
                // 큐에서 꺼낸 플레이어를 큐의 맨 뒤에 추가한다.
                queue.Enqueue(minPlayer);
            }
        }

        /// <summary>
        /// 정렬된 queue를 출력해주는 메서드
        /// </summary>
        public void Render()
        {
            while(queue.Count != 0)
                queue.Dequeue().Action();
        }
    }
}
