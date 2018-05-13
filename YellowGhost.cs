using System.Drawing;

namespace WindowsFormsApplication1
{
 /*
  This Ghost's behaviour is random,it moves until it reaches an intersection in the maze.
  once it has reached an intersection a path will be chosen randomly until the next intersection.
*/
    public class YellowGhost : GhostCharacter
    {
        public YellowGhost(int x, int y, Image image)
        {
            height = 20;
            width = 20;
            this.x = x;
            this.y = y;
            this.image = image;
            FinishMove = false;
            InTrapZone = true;
        }
       
    }
}



