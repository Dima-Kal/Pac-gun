using System;
using System.Drawing;

namespace WindowsFormsApplication1
{
    public abstract class GhostCharacter : ItemOnScreen
    {
        protected int direction;
        protected bool InTrapZone, FinishMove;
        protected int OppositeDir;

        public override Rectangle GetFrame()
        {
            Rectangle frame = new Rectangle(this.x, this.y, 30, 30);
            return frame;
        }

        public bool CaughtPacman(Pacman pman)
        {
            return this.GetFrame().IntersectsWith(pman.GetFrame());
        }

        public void MoveToRandomDirection(int[,] maze)
        {
            Random rnd = new Random();
            int i = this.y/20;
            int j = this.x/20;
            bool rechedIntersection = false;

            if (InTrapZone)
                //Get out of Trap Zone
            {
                direction = (int) Directions.Up;
                if (maze[i - 1, j] == 1)
                    direction = rnd.Next(3, 5);

                if (direction == (int) Directions.Right && maze[i, j] != 3) InTrapZone = false;
                if (direction == (int) Directions.Left && maze[i, j] != 3) InTrapZone = false;
            }

            finish:

            if (!InTrapZone && FinishMove)
            {
                if (this.direction == (int) Directions.Up)
                    this.OppositeDir = (int) Directions.Down;
                if (this.direction == (int) Directions.Down)
                    this.OppositeDir = (int) Directions.Up;
                if (this.direction == (int) Directions.Left)
                    this.OppositeDir = (int) Directions.Right;
                if (this.direction == (int) Directions.Right)
                    this.OppositeDir = (int) Directions.Left;
                again:
                this.direction = rnd.Next(1, 5);
                if (direction == OppositeDir)
                    goto again;
                if (this.direction == (int) Directions.Up)
                    if ((maze[i - 1, j] == 1) || (maze[i - 1, j] == 3))
                        goto again;

                if (this.direction == (int) Directions.Down)
                    if ((maze[i + 1, j] == 1) || (maze[i + 1, j] == 3))
                        goto again;

                if (this.direction == (int) Directions.Left)
                    if ((maze[i, j - 1] == 1) || (maze[i, j - 1] == 3))
                        goto again;

                if (this.direction == (int) Directions.Right)
                    if ((maze[i, j + 1] == 1) || (maze[i, j + 1] == 3))
                        goto again;

                FinishMove = false;
            }


            // check for available intersections
            if (!InTrapZone && (!rechedIntersection && (((maze[i - 1, j] != 1) && (maze[i, j + 1] != 1))
                                                        || ((maze[i, j + 1] != 1) && (maze[i + 1, j] != 1))
                                                        || ((maze[i + 1, j] != 1) && (maze[i, j - 1] != 1))
                                                        || ((maze[i, j - 1] != 1) && (maze[i - 1, j] != 1)))))
            {
                FinishMove = true;
                rechedIntersection = true;
                goto finish;
            }


            if (this.direction == (int) Directions.Up && maze[i - 1, j] != 1)
                if (!InTrapZone)
                {
                    if (maze[i - 1, j] != 3)
                        this.y -= 20;
                }
                else
                    this.y -= 20;


            if (this.direction == (int) Directions.Down && maze[i + 1, j] != 1)
                if (!InTrapZone)
                {
                    if (maze[i + 1, j] != 3)
                        this.y += 20;
                }
                else
                    this.y += 20;

            if (this.direction == (int) Directions.Left && maze[i, j - 1] != 1)
                if (!InTrapZone)
                {
                    if (maze[i, j - 1] != 3)
                        this.x -= 20;
                }
                else
                    this.x -= 20;

            if (this.direction == (int) Directions.Right && maze[i, j + 1] != 1)
                if (!InTrapZone)
                {
                    if (maze[i, j + 1] != 3)
                        this.x += 20;
                }
                else
                    this.x += 20;
        }
    }
}