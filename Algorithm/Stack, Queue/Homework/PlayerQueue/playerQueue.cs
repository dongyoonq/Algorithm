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

        /* 
        *  선택 정렬 알고리즘.
        * 큐에서 가장 작은 속도를 가진 플레이어를 찾아서 다른 플레이어와 교환하는 과정을 큐의 길이만큼 반복
        * 이 때, 가장 작은 속도를 가진 플레이어를 찾기 위해, 첫 번째 플레이어를 초기 최소값으로 설정하고, 
        * 큐의 모든 플레이어를 순회하면서 최소값을 갱신해 나간다.
        * 
        * 구체적으로 알고리즘은 다음과 같이 동작한다. 
        * 큐에서 가장 작은 속도를 가진 플레이어를 찾습니다. 이를 위해, 첫 번째 플레이어를 초기 최소값으로 설정한다.
        * 큐에서 첫 번째 플레이어를 뺀 나머지 플레이어를 모두 탐색하며, 가장 작은 속도를 가진 플레이어를 찾는다.
        * 가장 작은 속도를 가진 플레이어를 찾았으면, 해당 플레이어를 큐에서 빼서 초기 최소값인 첫 번째 플레이어와 교환한다.
        * 큐에서 꺼낸 플레이어를 큐의 맨 뒤에 추가한다.
        * 
        * 1~4번을 큐의 길이만큼 반복
        */
        public void Sort()
        {
            // 큐 정렬
            for(int i = 0; i < queue.Count; i++)
            {
                // 첫 번째 플레이어를 초기 최소값으로 설정하고, 
                Player minPlayer = queue.Dequeue();
                int minSpeed = minPlayer.Speed;

                // 큐에서 플레이어를 모두 탐색하며, 가장 작은 속도를 가진 플레이어를 찾는다.
                for (int j = 0; j < queue.Count; j++)
                {
                    Player player = queue.Dequeue();
                    // 작은 속도를 가진 플레이어를 찾았으면, 해당 플레이어를 큐에서 빼서 초기 최소값인 첫 번째 플레이어와 교환
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
