using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;

namespace WindowsFormsApplication1
{
    public partial class Menu : Form
    {
        Bitmap _backBuffer;
        Graphics _dBgrfx;
        Graphics grfx;

        Pacman PM1 = new Pacman(340, 200, Properties.Resources.Pacman1);
        Pacman PM2 = new Pacman(170, 303, Properties.Resources.Pacman1);
        RedGhost RG = new RedGhost(270, 200, Properties.Resources.RedGhost);
        BlueGhost BG = new BlueGhost(240, 200, Properties.Resources.BlueGhost);
        YellowGhost YG = new YellowGhost(210, 200, Properties.Resources.YellowGhost);
        PinkGhost PG = new PinkGhost(180, 200, Properties.Resources.PinkGhost);
        int PacmanPicState = 1;
        int MenuState = 1;

        public Menu()
        {
            InitializeComponent();
        }

        private void Animation_Tick(object sender, EventArgs e)
        {
            _dBgrfx.DrawImage(Properties.Resources.Menu, -10, 0, Width , Height - 50);

            PM1.SetX(PM1.GetX() + 15);
            RG.SetX(RG.GetX() + 15);
            BG.SetX(BG.GetX() + 15);
            YG.SetX(YG.GetX() + 15);
            PG.SetX(PG.GetX() + 15);

            if (PacmanPicState == 1)
            {
                PM1.SetImage(Properties.Resources.Pacman2);
                PacmanPicState = 2;
            }
            else
            {
                PM1.SetImage(Properties.Resources.Pacman1);
                PacmanPicState = 1;
            }

            if (PM1.GetX() > Width)
                PM1.SetX(0);
            if (RG.GetX() > Width)
                RG.SetX(0);
            if (BG.GetX() > Width)
                BG.SetX(0);
            if (YG.GetX() > Width)
                YG.SetX(0);
            if (PG.GetX() > Width)
                PG.SetX(0);

            PM1.DrawItem(_dBgrfx);
            PM2.DrawItem(_dBgrfx);
            RG.DrawItem(_dBgrfx);
            BG.DrawItem(_dBgrfx);
            YG.DrawItem(_dBgrfx);
            PG.DrawItem(_dBgrfx);

            grfx.DrawImage(_backBuffer, 0, 0);
        }

        private void Menu_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
            {
                if (PM2.GetY() == 303)
                    PM2.SetY(503);
                else
                    PM2.SetY(PM2.GetY() - 50);
            }

            if (e.KeyCode == Keys.Down)
            {
                if (PM2.GetY() == 503)
                    PM2.SetY(303);
                else
                    PM2.SetY(PM2.GetY() + 50);
            }

            if (e.KeyCode == Keys.Enter)
            {
                if (PM2.GetY() == 303)
                {
                    Animation.Stop();
                    Visible = false;
                    GameForm gf = new GameForm();
                    gf.ShowDialog();
                    gf.Close();
                    Animation.Start();
                    Visible = true;
                    TopMost = true;
                    Focus();
                }

                if (PM2.GetY() == 353)
                {
                    MenuState = 2;
                    Animation.Stop();
                    grfx.DrawImage(Properties.Resources.MenuHelp, -10, 0, Width, Height - 50);
                }

                if (PM2.GetY() == 403)
                {
                    MenuState = 2;
                    Animation.Stop();
                    HighScore hs = new HighScore();
                    hs.ShowDialog();
                    hs.Close();
                    Animation.Start();
                }

                if (PM2.GetY() == 453)
                {
                    MenuState = 2;
                    Animation.Stop();
                    grfx.DrawImage(Properties.Resources.MenuAbout, -10, 0, Width, Height - 50);
                }

                if (PM2.GetY() == 503)
                    Application.Exit();
            }
            if (e.KeyCode == Keys.Escape)
                if (MenuState == 1)
                    Application.Exit();
                else
                {
                    Animation.Start();
                    MenuState = 1;
                    grfx.DrawImage(Properties.Resources.Menu, -10, 0, Width, Height - 50);
                }
        }
    


        private void Menu_Load(object sender, EventArgs e)
        {
            grfx = CreateGraphics();
            _backBuffer = new Bitmap(ClientSize.Width, ClientSize.Height);
            _dBgrfx = Graphics.FromImage(_backBuffer);

            if (File.Exists("data.txt")) return;
            MessageBox.Show("File: data.txt not found");
            Application.Exit();
        }
    }
}