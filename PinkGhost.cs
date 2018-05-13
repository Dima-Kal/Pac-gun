using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace WindowsFormsApplication1
{
    public class PinkGhost : GhostCharacter
    {
        public PinkGhost(int x, int y, Image image)
        {
            this.height = 20;
            this.width = 20;
            this.x = x;
            this.y = y;
            this.image = image;

        }

        public void SetImageOpacity(float op)
        {
            Bitmap bmpPic = new Bitmap(this.image.Width, this.image.Height);
            Graphics gfxPic = Graphics.FromImage(bmpPic);
            ColorMatrix cmxPic = new ColorMatrix {Matrix33 = op};

            ImageAttributes iaPic = new ImageAttributes();
            iaPic.SetColorMatrix(cmxPic, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
            gfxPic.DrawImage(this.image, new Rectangle(0, 0, bmpPic.Width, bmpPic.Height), 0, 0, this.image.Width, this.image.Height, GraphicsUnit.Pixel, iaPic);
            gfxPic.Dispose();
            this.image = bmpPic;

        }

        public void SetNewLocation(int[,] maze, Pacman pman)
        {
            this.image = Properties.Resources.PinkGhost;
            int i, j;
            Random rnd = new Random();
        again:
            i = rnd.Next((pman.GetY() - 140) / 20, (pman.GetY() + 140) / 20);
            j = rnd.Next((pman.GetX() - 140) / 20, (pman.GetX() + 140) / 20);
            if ((i > 0) && (i < maze.GetLength(0)) && (j > 0) && (j < maze.GetLength(1)))
            {
                if (maze[i, j] == 1)
                    goto again;

            }
            else
                goto again;
            this.x = j * 20;
            this.y = i * 20;
        }

        public override Rectangle GetFrame()
        {
            Rectangle frame = new Rectangle(this.x, this.y, this.width, this.height);
            return frame;
        }

    }
}
