using System;

namespace WindowsFormsApplication1
{
    public abstract class GhostAI :GhostCharacter
    {

        public void ChasePacman(Pacman pman, int[,] maze)
        {
            int i = this.y / 20;
            int j = this.x / 20;
            int k = pman.GetX() / 20;
            int l = pman.GetY() / 20;

            if (((maze[i - 1, j] != 1) && (maze[i, j + 1] != 1))
             || ((maze[i, j + 1] != 1) && (maze[i + 1, j] != 1))
             || ((maze[i + 1, j] != 1) && (maze[i, j - 1] != 1))
             || ((maze[i, j - 1] != 1) && (maze[i - 1, j] != 1)))
            {
                if (this.direction == (int)Directions.Up)
                    this.OppositeDir = (int)Directions.Down;
                if (this.direction == (int)Directions.Down)
                    this.OppositeDir = (int)Directions.Up;
                if (this.direction == (int)Directions.Left)
                    this.OppositeDir = (int)Directions.Right;
                if (this.direction == (int)Directions.Right)
                    this.OppositeDir = (int)Directions.Left;

                int moveScoreUp = this.OppositeDir != (int) Directions.Up ? EvaluateMove(k, l, j, i - 1, maze) : -1;
                int moveScoreDown = this.OppositeDir != (int) Directions.Down ? EvaluateMove(k, l, j, i + 1, maze) : -1;
                int moveScoreLeft = this.OppositeDir != (int) Directions.Left ? EvaluateMove(k, l, j - 1, i, maze) : -1;
                int moveScoreRight = this.OppositeDir != (int) Directions.Right ? EvaluateMove(k, l, j + 1, i, maze) : -1;

                int score = Max(moveScoreUp, moveScoreDown, moveScoreLeft, moveScoreRight);

                if (score == moveScoreUp)
                    direction = (int)Directions.Up;
                if (score == moveScoreDown)
                    direction = (int)Directions.Down;
                if (score == moveScoreLeft)
                    direction = (int)Directions.Left;
                if (score == moveScoreRight)
                    direction = (int)Directions.Right;
            }


            if (this.direction == (int) Directions.Up)
                this.y -= 20;

            if (this.direction == (int) Directions.Down)
                this.y += 20;

            if (this.direction == (int) Directions.Left)
                this.x -= 20;

            if (this.direction == (int) Directions.Right)
                this.x += 20;

            j = this.x / 20;

            if (j > maze.GetLength(1) - 2)
                this.x = 20;
            if (j < 1)
                this.x = (maze.GetLength(1) - 2) * 20;
        }


        private int EvaluateMove(int k,int l,int j, int i, int[,] maze)
        {
            int xghost = j * 20;
            int yghost = i * 20;
            int xgoal = k * 20;
            int ygoal = l * 20;

            return (maze[i, j] == 1) || (maze[i, j] == 4)
                       ? -1
                       : 1000 - (Math.Abs(xghost - xgoal) + Math.Abs(yghost - ygoal));
        }

        private int Max(params int[] numbers)
        {
            int max = 0;
            foreach (int i in numbers)
                if (i > max)
                    max = i;
            return max;
        }
    }
}
