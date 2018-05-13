using System.Drawing;

namespace WindowsFormsApplication1
{
    public class Block : ItemOnScreen
    {

        public Block(int x, int y)
        {
            this.x = x;
            this.y = y;
            width = 20;
            height = 20;
        }

        public override void DrawItem(Graphics grfx)
        {
            SolidBrush brush =  new SolidBrush(Color.SkyBlue);
            Pen pen = new Pen(brush);
            Rectangle RectBlk = new Rectangle(x, y, this.width, this.height);
            grfx.FillRectangle(brush, RectBlk);
            grfx.DrawRectangle(pen, RectBlk);
        }
    }
}
