using System;
using System.Drawing;

/*
   This ghost's behaviour is part chasing and part random walking.
*/

namespace WindowsFormsApplication1
{
    public class BlueGhost : GhostAI
    {
        int ChaseTime;
        int RandomMoveTime;
        Random rnd = new Random();
        public BlueGhost(int x, int y, Image image)
        {
            height = 20;
            width = 20;
            this.x = x;
            this.y = y;
            this.image = image;
            direction = (int)Directions.Left;
            RandomMoveTime = 0;
            ChaseTime = rnd.Next(10, 21);
        }

        public void SetRandomChaseTime()
        {
            this.ChaseTime = rnd.Next(10, 21);
        }

        public int GetChaseTime()
        {
            return this.ChaseTime;
        }

        public void ReduceChaseTime()
        {
            this.ChaseTime--;
        }

        public void ResetRandomMoveTime()
        {
            this.RandomMoveTime = 8;
        }

        public void ReduceRandomMoveTime()
        {
            this.RandomMoveTime--;
        }

        public int GetRandomMoveTime()
        {
            return this.RandomMoveTime;
        }
    }
}
