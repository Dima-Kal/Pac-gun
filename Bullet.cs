using System.Drawing;

namespace WindowsFormsApplication1
{
    public class Bullet : ItemOnScreen
    {
        private int direction;
        public Bullet(int x, int y, Image image)
        {
            height = 20;
            width = 10;
            this.x = x;
            this.y = y;
            this.image = image;
        }

        public bool CheckHitGhost(GhostCharacter g)
        {
            return this.GetFrame().IntersectsWith(g.GetFrame());
        }

        public void RotateBullet()
        {
            this.height = 20;
            this.width = 10;

            if (this.direction == (int)Directions.Up)
            {
                this.image.RotateFlip(System.Drawing.RotateFlipType.Rotate90FlipY);
                this.height = 8;
                this.width = 20;
            }
            if (this.direction == (int)Directions.Down)
            {
                this.image.RotateFlip(System.Drawing.RotateFlipType.Rotate90FlipX);
                this.height = 8;
                this.width = 20;
            }
            if (this.direction == (int)Directions.Left)
            {
                this.image.RotateFlip(System.Drawing.RotateFlipType.Rotate180FlipY);
                this.height = 20;
                this.width = 10;
            }
        }

        public void SetDirection(int direction)
        {
            this.direction = direction;
        }

        public int GetDirection()
        {
            return this.direction;
        }
    }
}
