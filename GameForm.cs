using System;
using System.Drawing;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class GameForm : Form
    {
        private enum Directions { Up = 1, Down = 2, Left = 3, Right = 4 }

        private Pacman Pman;
        private RedGhost RGhost;
        private BlueGhost BGhost;
        private YellowGhost YGhost;
        private PinkGhost PGhost;
        private Block block;
        private Bullet bullet;

        private Maze maze;
        private int i;
        private int PacmanPicState = 1;
        private int PinkGhostAppearTimer;
        private bool ShootOnce;
        private bool Dead;

        private SolidBrush brush1;
        private Font text = new Font("Times New Roman", 13, FontStyle.Bold);

        private Bitmap BackBuffer;
        private Graphics DBgrfx;
        private Graphics grfx;

        public GameForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            BackBuffer = new Bitmap(ClientSize.Width, ClientSize.Height);
            DBgrfx = Graphics.FromImage(BackBuffer);
            brush1 = new SolidBrush(Color.Black);

            grfx = CreateGraphics();

            Pman = new Pacman(280,480,Properties.Resources.Pacman1);
            RGhost = new RedGhost(280, 280, Properties.Resources.RedGhost);
            BGhost = new BlueGhost(300, 300, Properties.Resources.BlueGhost);
            YGhost = new YellowGhost(280, 300, Properties.Resources.YellowGhost);
            PGhost = new PinkGhost(260, 300, Properties.Resources.PinkGhost);
            PinkGhostAppearTimer = 20;

            block = new Block(0, 0);
            bullet = new Bullet(0, 0, Properties.Resources.Bullet);

            maze = new Maze();

            DrawingTimer.Start();
        }

        public void StartGame()
        {
            maze.RestartMaze();
            ResetLocations();
            Pman.SetScore(0);
            Pman.ResetTries();
            Pman.ResetAmmo();
        }

        private void ResetRedLoc()
        {
            RGhost = new RedGhost(280, 280, Properties.Resources.RedGhost);
        }

        private void ResetBlueLoc()
        {
            BGhost = new BlueGhost(300, 300, Properties.Resources.BlueGhost);
        }

        private void ResetYellowLoc()
        {
            YGhost = new YellowGhost(280, 300, Properties.Resources.YellowGhost);
        }

        private void ResetPinkLoc()
        {
            PGhost = new PinkGhost(260, 300, Properties.Resources.PinkGhost);  
        }

        private void ResetLocations()
        {
            Pman.SetX(280);
            Pman.SetY(480);

            ResetRedLoc();
            ResetBlueLoc();
            ResetYellowLoc();
            ResetPinkLoc();                              
        }

        public void ShowScene()
        {
            brush1.Color = Color.Black;
            DBgrfx.FillRectangle(brush1,new Rectangle(0,0,this.Width,this.Height));

            maze.DrawMaze(DBgrfx);

            RGhost.DrawItem(DBgrfx);
            YGhost.DrawItem(DBgrfx);
            BGhost.DrawItem(DBgrfx);
            PGhost.DrawItem(DBgrfx);
            Pman.DrawItem(DBgrfx);

            DBgrfx.DrawString("Score:" + Pman.GetScore(), text, new SolidBrush(Color.White), 0, 650);
            DBgrfx.DrawString("Ammo:" + Pman.GetAmmo(), text, new SolidBrush(Color.White), 500, 650);
            DBgrfx.DrawString("Tries:" + Pman.GetTries(), text, new SolidBrush(Color.White), 265, 650);

            if (ShootOnce)
                bullet.DrawItem(DBgrfx);
            try
            {
                grfx.DrawImage(BackBuffer, 0, 0);
            }
            catch
            {
                DrawingTimer.Stop();
            }
        }

        private void GameForm_KeyDown(object sender, KeyEventArgs e)
        {
            int i = Pman.GetY() / 20;
            int j = Pman.GetX() / 20;

            if (Dead) return;
            if ((e.KeyCode == Keys.Up) || (e.KeyCode == Keys.W))
            {
                if (maze.GetMaze()[i - 1, j] != 1)
                    Pman.SetDirection((int)Directions.Up);
                PacmanTimer.Start();
                GhostTimer.Start();
            }

            if ((e.KeyCode == Keys.Down) || (e.KeyCode == Keys.S))
            {
                if (maze.GetMaze()[i + 1, j] != 1)
                    Pman.SetDirection((int)Directions.Down);
                PacmanTimer.Start();
                GhostTimer.Start();
            }

            if ((e.KeyCode == Keys.Left) || (e.KeyCode == Keys.A))
            {
                if (maze.GetMaze()[i, j - 1] != 1)
                    Pman.SetDirection((int)Directions.Left);
                PacmanTimer.Start();
                GhostTimer.Start();
            }

            if ((e.KeyCode == Keys.Right) || (e.KeyCode == Keys.D))
            {
                if (maze.GetMaze()[i, j + 1] != 1)
                    Pman.SetDirection((int)Directions.Right);
                PacmanTimer.Start();
                GhostTimer.Start();
            }

            if (e.KeyCode != Keys.Space || Pman.GetAmmo() <= 0 || ShootOnce) return;
            bullet.SetImage(Properties.Resources.Bullet);
            Pman.ReduceAmmo();
            bullet.SetX(Pman.GetX() + 5);
            bullet.SetY(Pman.GetY() + 5);
            ShootOnce = true;
            bullet.SetDirection(Pman.GetDirection());
            bullet.RotateBullet();
            BulletTimer.Start();
        }

        private void PacmanTimer_Tick(object sender, EventArgs e)
        {
            HighScore hs;
            DialogResult dr;
            if (!Dead)
            {
                if (PacmanPicState == 1)
                {
                    Pman.SetImage(Properties.Resources.Pacman2);
                    PacmanPicState = 2;
                }
                else
                {
                    Pman.SetImage(Properties.Resources.Pacman1);
                    PacmanPicState = 1;
                }

                Pman.RotatePacman();
                Pman.MovePacman(maze.GetMaze());
                if (maze.GameWin())
                {
                    Pman.SetScore(Pman.GetTries() * 200);
                    PacmanTimer.Stop();
                    GhostTimer.Stop();
                    hs = new HighScore(Pman);
                    hs.ShowDialog();
                    hs.Close();

                    dr = MessageBox.Show("Do you want to play again?",
                                         "Game Over",
                                         MessageBoxButtons.YesNo,
                                         MessageBoxIcon.Question);
                    if (dr == DialogResult.Yes)
                    {
                        StartGame();
                    }
                    else
                    {
                        DrawingTimer.Stop();
                        Close();
                    }
                }
            }
            else
            {
                if (i < 10)
                {
                    Pman.ResetDeathAnimationFrames();
                    Pman.SetImage(Pman.GetAnimation()[i]);
                    Pman.RotatePacman();
                    i++;
                }
                else
                {
                    i = 0;
                    Pman.SetImage(Properties.Resources.Pacman1);
                    ResetLocations();
                    PacmanTimer.Stop();
                    Dead = false;
                }
            }

            if (Pman.GetTries() != 0 || Dead) return;
            hs = new HighScore(Pman);
            hs.ShowDialog();
            hs.Close();

            dr = MessageBox.Show("Do you want to play again?",
                                 "Game Over",
                                 MessageBoxButtons.YesNo,
                                 MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                StartGame();
            }
            else
            {
                DrawingTimer.Stop();
                Close();
            }
        }

        private void BulletTimer_Tick(object sender, EventArgs e)
        {
            
            if (bullet.GetDirection() == (int)Directions.Up)
                bullet.SetY(bullet.GetY() - 20);
            if (bullet.GetDirection() == (int)Directions.Down)
                bullet.SetY(bullet.GetY() + 20);
            if (bullet.GetDirection() == (int)Directions.Left)
                bullet.SetX(bullet.GetX() - 20);
            if (bullet.GetDirection() == (int)Directions.Right)
                bullet.SetX(bullet.GetX() + 20);

            int i = (bullet.GetY()-5) / 20;
            int j = (bullet.GetX()-5) / 20;

            if (maze.GetMaze()[i, j] == 1)
            {
                bullet.SetImage(null);
                BulletTimer.Stop();
                ShootOnce = false;
            }

            if (bullet.CheckHitGhost(RGhost))
            {
                Pman.SetScore(Pman.GetScore() + 20);
                ResetRedLoc();
                BulletTimer.Stop();
                ShootOnce = false;
            }
            if (bullet.CheckHitGhost(YGhost))
            {
                Pman.SetScore(Pman.GetScore() + 20);
                ResetYellowLoc();
                BulletTimer.Stop();
                ShootOnce = false;
            }
            if (bullet.CheckHitGhost(BGhost))
            {
                Pman.SetScore(Pman.GetScore() + 20);
                ResetBlueLoc();
                BulletTimer.Stop();
                ShootOnce = false;
            }
            if (bullet.CheckHitGhost(PGhost))
            {
                Pman.SetScore(Pman.GetScore() + 20);
                ResetPinkLoc();
                BulletTimer.Stop();
                ShootOnce = false;
            }
        }

        private void DrawingTimer_Tick(object sender, EventArgs e)
        {
            ShowScene();
            if (Dead ||
                ((((!RGhost.CaughtPacman(Pman)) && (!YGhost.CaughtPacman(Pman))) && (!BGhost.CaughtPacman(Pman))) &&
                 (!PGhost.CaughtPacman(Pman)))) return;
            Dead = true;
            Pman.ReduceTries();
            GhostTimer.Stop();
            PinkGhostAppearTimer = 25;
        }

        private void GhostTimer_Tick(object sender, EventArgs e)
        {
            YGhost.MoveToRandomDirection(maze.GetMaze());
            RGhost.ChasePacman(Pman, maze.GetMaze());

            if (BGhost.GetChaseTime() != 0)
            {
                BGhost.ChasePacman(Pman, maze.GetMaze());
                BGhost.ReduceChaseTime();
            }
            else if (BGhost.GetRandomMoveTime() != 0)
            {
                if (maze.GetMaze()[BGhost.GetY()/20, BGhost.GetX()/20] != 3)
                    BGhost.MoveToRandomDirection(maze.GetMaze());
                BGhost.ReduceRandomMoveTime();
            }
            else
            {
                BGhost.ResetRandomMoveTime();
                BGhost.SetRandomChaseTime();
            }

            PinkGhostAppearTimer--;
            if (PinkGhostAppearTimer >= 0)
                PGhost.SetImageOpacity((float)0.95);
            else
            {
                PGhost.SetNewLocation(maze.GetMaze(), Pman);
                PinkGhostAppearTimer = 25;
            }
        }
    }
}