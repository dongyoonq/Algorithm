using System;
using System.Collections.Generic;
using System.Text;

namespace Algorithm
{
    public class Player
    {
        private string name;
        private int speed;

        public int Speed { get { return speed; } set { speed = value; } }

        public Player(string name, int speed)
        {
            this.name = name;
            this.speed = speed;
        }

        public void Action()
        {
            Console.WriteLine($"{this.name}이 행동합니다");
        }
    }
}
