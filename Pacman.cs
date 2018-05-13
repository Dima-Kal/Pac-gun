using System.Drawing;

namespace WindowsFormsApplication1
{
    public class Pacman : ItemOnScreen
    {
        private int ammo;
        private int score;
        private int tries;
        private int direction;
        private Image[] deathAnimation = new Image[10];

        public Pacman(int x, int y, Image image)
        {
            ammo = 0;
            score = 0;
            tries = 3;
            height = 20;
            width = 20;
            this.x = x;
            this.y = y;
            this.image = image;
        }

        public void ResetDeathAnimationFrames()
        {
            this.deathAnimation[0] = Properties.Resources.frame1;
            this.deathAnimation[1] = Properties.Resources.frame2;
            this.deathAnimation[2] = Properties.Resources.frame3;
            this.deathAnimation[3] = Properties.Resources.frame4;
            this.deathAnimation[4] = Properties.Resources.frame5;
            this.deathAnimation[5] = Properties.Resources.frame6;
            this.deathAnimation[6] = Properties.Resources.frame7;
            this.deathAnimation[7] = Properties.Resources.frame8;
            this.deathAnimation[8] = Properties.Resources.frame9;
            this.deathAnimation[9] = Properties.Resources.frame10;
        }

        public void RotatePacman()
        {
            if (this.direction == (int)Directions.Up)
                this.image.RotateFlip(System.Drawing.RotateFlipType.Rotate90FlipY);
            if (this.direction == (int)Directions.Down)
                this.image.RotateFlip(System.Drawing.RotateFlipType.Rotate90FlipX);
            if (this.direction == (int)Directions.Left)
                this.image.RotateFlip(System.Drawing.RotateFlipType.Rotate180FlipY);
        }

        public void MovePacman(int[,] maze)
        {   
            int i = this.y / 20;
            int j = this.x / 20;

            if (this.direction == (int)Directions.Left)
                j--;
            if (this.direction == (int)Directions.Right)
                j++;

            if (j > maze.GetLength(1) - 1)
                j = 0;
            if (j < 0)
                j = maze.GetLength(1) - 1;

            if (maze[i, j] != 1)
                this.x = (j * 20);
            else
            {
                if (this.direction == (int)Directions.Left)
                    j++;
                if (this.direction == (int)Directions.Right)
                    j--;

                this.x = (j * 20);
            }

            if (this.direction == (int)Directions.Up)
                i--;
            if (this.direction == (int)Directions.Down)
                i++;

            if (maze[i, j] == 0)
            {
                this.score++;
                maze[i, j] = 9;
            }

            if (maze[i, j] == 5)
            {
                this.ammo += 5;
                maze[i, j] = 9;
            }

            if (maze[i, j] != 1)
                this.y = (i * 20);
        }

        public void ReduceAmmo()
        {
            this.ammo--;
        }

        public void ResetAmmo()
        {
            this.ammo = 0;
        }

        public int GetScore()
        {
            return this.score;
        }

        public void SetScore(int num)
        {
            this.score = num;
        }

        public int GetAmmo()
        {
            return this.ammo;
        }

        public int GetTries()
        {
            return this.tries;
        }

        public void ResetTries()
        {
             this.tries = 3;
        }

        public void ReduceTries()
        {
            this.tries--;
        }

        public void SetDirection(int direction)
        {
            this.direction = direction;
        }

        public int GetDirection()
        {
            return this.direction;
        }

        public Image[] GetAnimation()
        {
            return this.deathAnimation;
        }
    
    }
}