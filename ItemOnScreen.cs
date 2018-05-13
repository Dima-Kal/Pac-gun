using System.Drawing;

namespace WindowsFormsApplication1
{
    public abstract class ItemOnScreen
    {
        protected int x;
        protected int y;
        protected int height;
        protected int width;
        protected Image image;

        public int GetX()
        {
            return this.x;
        }

        public int GetY()
        {
            return this.y;
        }

        public void SetX(int x)
        {
            this.x = x;
        }

        public void SetY(int y)
        {
            this.y = y;
        }

        public virtual Rectangle GetFrame()
        {
            Rectangle frame = new Rectangle(this.x, this.y, this.width, this.height);
            return frame;
        }

        public virtual void DrawItem(Graphics grfx)
        {
            grfx.DrawImage(this.image, this.x, this.y, height, width);
        }


        public void SetImage(Image image)
        {
            this.image = image;
        }

        public Image GetImage()
        {
            return this.image;
        }

    }
}
