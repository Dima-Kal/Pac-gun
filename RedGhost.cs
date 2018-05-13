using System.Drawing;

/*
  This ghost's behaviour is always chasing the pacman
*/

namespace WindowsFormsApplication1
{
    public class RedGhost : GhostAI
    {
        public RedGhost(int x, int y, Image image)
        {
            height = 20;
            width = 20;
            this.x = x;
            this.y = y;
            this.image = image;
            FinishMove = false;
            direction = (int)Directions.Up;
        }
    }
}
